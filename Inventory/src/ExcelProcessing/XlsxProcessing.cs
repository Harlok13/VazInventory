using Aspose.Cells;
using Inventory.Data;
using Inventory.Data.Entities;
using Inventory.Entities;
using static Inventory.Settings;

namespace Inventory.ExcelProcessing;

public class XlsxProcessing
{
    private readonly Workbook _wb;
    private readonly Worksheet _sheet;

    public XlsxProcessing(string filePath, string fileName)
    {
        var fullFilePath = Path.Combine(filePath, $"{fileName}");
        _wb = new Workbook(fullFilePath);
        _sheet = _wb.Worksheets[WorkSheetNum];
    }

    public async Task SetServersAsync(InventoryContext context, Dictionary<string, IEntities> cacheDict)
    {
        for (int contour = 0; contour < Contours.Length; contour++)
        {
            for (int row = 0; row < ServersInfoRows[contour].Length; row++)
            {
                var domainKey = GetValueOrNull(ServersInfoRows[contour][row], ServerDomainCol)!;

                var osName = GetValueOrNull(ServersInfoRows[contour][row], ServerOsNameCol);
                var osVersion = GetValueOrNull(ServersInfoRows[contour][row], ServerOsVersionCol);
                var osKey = $"{osName}{osVersion}";

                var applicationName = GetValueOrNull(ServersInfoRows[contour][row], ServerApplicationNameCol);
                var applicationVersion = GetValueOrNull(ServersInfoRows[contour][row], ServerApplicationVersionCol);
                var applicationKey = $"{applicationName}{applicationVersion}";

                var location = GetValueOrNull(ServersInfoRows[contour][row], ServerLocationCol);

                if (CheckDomainIsNotNull(ServersInfoRows[contour][row]))
                {
                    TrySetContour(Contours[contour], cacheDict);
                    TrySetServerKind(ServersKind[row], cacheDict);
                    TrySetServerLocation(location, cacheDict);
                    TrySetServerOs(osName, osVersion, osKey, cacheDict);
                    TrySetServerApplication(applicationName, applicationVersion, applicationKey, cacheDict);
                    var code = SetInformationSystem(cacheDict);

                    await context.AddAsync(new Server
                    {
                        Contour = cacheDict[Contours[contour]] as Contour,
                        Domain = domainKey,
                        ServerOs = cacheDict[osKey] as ServerOs,
                        ServerKind = cacheDict[ServersKind[row]] as ServerKind,
                        Location = location != null && cacheDict.TryGetValue(location, out var record)
                            ? record as Location
                            : null,
                        ServerApplication = cacheDict[applicationKey] as ServerApplication,
                        InformationSystem = cacheDict[code] as InformationSystem
                    });
                }
            }
        }
    }

    private bool CheckDomainIsNotNull(int serverRow)
    {
        var domainValue = _sheet.Cells.GetCell(serverRow, ServerDomainCol).StringValue;
        return !(string.IsNullOrEmpty(domainValue) || !domainValue.Contains(DomainSearchWord));
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


    private bool TrySetServerOs(string? name, string? version, string osKey, Dictionary<string, IEntities> cacheDict) =>
        cacheDict.TryAdd(osKey, new ServerOs { Name = name, Version = version });


    private bool TrySetServerApplication(string? name, string? version, string applicationKey,
        Dictionary<string, IEntities> cacheDict) =>
        cacheDict.TryAdd(applicationKey, new ServerApplication { Name = name, Version = version });


    private void TrySetServerLocation(string? location, Dictionary<string, IEntities> cacheDict)
    {
        var recordExists = location != null && cacheDict.ContainsKey(location);

        if (!recordExists && location != null)
        {
            var newRecord = new Location { LocationIn = location };
            cacheDict.Add(location, newRecord);
        }
    }

    private bool TrySetContour(string contour, Dictionary<string, IEntities> cacheDict) =>
        cacheDict.TryAdd(contour, new Contour { Name = contour });


    private bool TrySetServerKind(string kindName, Dictionary<string, IEntities> cacheDict) =>
        cacheDict.TryAdd(kindName, new ServerKind { KindName = kindName });
}