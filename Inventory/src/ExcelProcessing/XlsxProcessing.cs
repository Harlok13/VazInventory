using Aspose.Cells;
using Inventory.Entities;
using static Inventory.Settings;

namespace Inventory.ExcelProcessing;

public class XlsxProcessing
{
    private readonly string _fullFilePath; 

    private readonly Workbook _wb;
    private readonly Worksheet _sheet;

    public XlsxProcessing(string filePath, string fileName)
    {
        _fullFilePath = Path.Combine(filePath, $"{fileName}");
        _wb = new Workbook(_fullFilePath);
        _sheet = _wb.Worksheets[WorkSheetNum];
    }

    public async Task SetServers(Context context,
        Dictionary<string, IEntities> cacheDict)
    {
        for (int contour = 0; contour < Contours.Length; contour++)
        {
            for (int row = 0; row < ServersInfoRows[contour].Length; row++)
            {
                var domainKey = GetValueOrNull(ServersInfoRows[contour][row], ServerDomainCol)!;

                var osName = GetValueOrNull(ServersInfoRows[contour][row], ServerOsNameCol);
                var osVersion = GetValueOrNull(ServersInfoRows[contour][row], ServerOsVersionCol);
                var applicationName = GetValueOrNull(ServersInfoRows[contour][row], ServerApplicationNameCol);
                var applicationVersion = GetValueOrNull(ServersInfoRows[contour][row], ServerApplicationVersionCol);
                var location = GetValueOrNull(ServersInfoRows[contour][row], ServerLocationCol);

                if (CheckDomainIsNotNull(ServersInfoRows[contour][row]))
                {
                    SetContour(Contours[contour], cacheDict);
                    SetServerKind(ServersKind[row], cacheDict);
                    SetServerLocation(location, cacheDict);
                    SetServerOs(osName, osVersion, cacheDict);
                    SetServerApplication(applicationName, applicationVersion, cacheDict);
                    var code = SetInformationSystem(cacheDict);

                    await context.AddAsync(new Server
                    {
                        Contour = cacheDict[Contours[contour]] as Contour,
                        Domain = domainKey,
                        ServerOs = cacheDict[$"{osName}{osVersion}"] as ServerOs,
                        ServerKind = cacheDict[ServersKind[row]] as ServerKind,
                        Location = location != null && cacheDict.TryGetValue(location, out var record)
                            ? record as Location
                            : null,
                        ServerApplication = cacheDict[$"{applicationName}{applicationVersion}"] as ServerApplication,
                        InformationSystem = cacheDict[code] as InformationSystem
                    });
                }
            }
        }
    }

    private bool CheckDomainIsNotNull(int serverRow)
    {
        var domainValue = _sheet.Cells.GetCell(serverRow, ServerDomainCol).StringValue;
        return !(string.IsNullOrEmpty(domainValue) || domainValue.ToLower() == DomainMissingMessage);
    }

    private string? GetValueOrNull(int row, int col)
    {
        var value = _sheet.Cells.GetCell(row, col).StringValue;
        return string.IsNullOrEmpty(value) ? null : value;
    }

    private string SetInformationSystem(Dictionary<string, IEntities> cacheDict)
    {
        var code = _sheet.Cells.GetCell(
            row: InformationSystemCodeRowCol.Row,
            column: InformationSystemCodeRowCol.Col
        ).StringValue;

        var name = _sheet.Cells.GetCell(
            row: InformationSystemNameRowCol.Row,
            column: InformationSystemNameRowCol.Col
        ).StringValue;

        var recordExists = cacheDict.ContainsKey(code);

        if (!recordExists)
        {
            var newRecord = new InformationSystem { Code = code, Name = name };
            cacheDict.Add(code, newRecord);
        }

        return code;
    }


    private void SetServerOs(string? name, string? version, Dictionary<string, IEntities> cacheDict)
    {
        string key = $"{name}{version}";
        var recordExists = cacheDict.ContainsKey(key);

        if (!recordExists)
        {
            var newRecord = new ServerOs { Name = name, Version = version };
            cacheDict.Add(key, newRecord);
        }
    }

    private void SetServerApplication(string? name, string? version, Dictionary<string, IEntities> cacheDict)
    {
        var key = $"{name}{version}";
        var recordExists = cacheDict.ContainsKey(key);

        if (!recordExists)
        {
            var newRecord = new ServerApplication { Name = name, Version = version };
            cacheDict.Add(key, newRecord);
        }
    }

    private void SetServerLocation(string? location, Dictionary<string, IEntities> cacheDict)
    {
        var recordExists = location != null && cacheDict.ContainsKey(location);

        if (!recordExists && location != null)
        {
            var newRecord = new Location { LocationIn = location };
            cacheDict.Add(location, newRecord);
        }
    }

    private void SetContour(string contour, Dictionary<string, IEntities> cacheDict)
    {
        var recordExists = cacheDict.ContainsKey(contour);

        if (!recordExists)
        {
            var newRecord = new Contour { Name = contour };
            cacheDict.Add(contour, newRecord);
        }
    }

    private void SetServerKind(string kindName, Dictionary<string, IEntities> cacheDict)
    {
        var recordExists = cacheDict.ContainsKey(kindName);
        // if (!RecordInCache(kindName))
        if (!recordExists)
        {
            var newRecord = new ServerKind { KindName = kindName };
            cacheDict.Add(kindName, newRecord);
        }
    }
}