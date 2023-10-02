using Aspose.Cells;
using Inventory.BL.ModelValidation;
using Inventory.DAL.Cache;
using Inventory.DAL.Context;
using Inventory.DAL.Entities;
using Inventory.DAL.Entities.Lanit;
using Inventory.DAL.ExcelToDatabase;
using Microsoft.EntityFrameworkCore;

namespace Inventory.BL.ExcelProcessing;

class XlsxProcessing : IXlsxProcessing
{
    private readonly Worksheet _sheet;
    private readonly string _fileName;
    private readonly IValidation _validation = new EntityValidation();

    public XlsxProcessing(string filePath, string fileName)
    {
        _fileName = fileName;
        var fullFilePath = Path.Combine(filePath, $"{_fileName}");
        var wb = new Workbook(fullFilePath);

        _sheet = wb.Worksheets[WorkSheetNum];
    }

    public async Task StartAsync(DbContext context, IExcelToDatabase excelToDatabase, ICacheData cacheData)
    {
        // ref all
        if (!_fileName.Equals(LanitFileName))
        {
            await SetServerAsync(context, excelToDatabase, cacheData);
        }
        else
        {
            await SetLanitAsync(context, excelToDatabase);
        }
    }
    
    private async Task SetServerAsync(DbContext context, IExcelToDatabase excelToDatabase, ICacheData cacheData)
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
                var locationKey = $"{location}";

                if (CheckDomainIsNotNull(ServersInfoRows[contour][row]))
                {
                    TrySetContour(Contours[contour], cacheData);
                    TrySetServerKind(ServersKind[row], cacheData);
                    TrySetServerLocation(location, locationKey, cacheData);
                    TrySetServerOs(osName, osVersion, osKey, cacheData);
                    TrySetServerApplication(applicationName, applicationVersion, applicationKey, cacheData);
                    var code = SetInformationSystem(cacheData);

                    await excelToDatabase.AddDataToContextAsync(context, new Server
                    {
                        Contour = (cacheData.Get<Contour>(Contours[contour]) as Contour)!,
                        Domain = domainKey,
                        ServerOs = cacheData.Get<ServerOs>(osKey) as ServerOs,
                        ServerKind = (cacheData.Get<ServerKind>(ServersKind[row]) as ServerKind)!,
                        Location = cacheData.Get<Location>(locationKey) as Location,
                        ServerApplication = cacheData.Get<ServerApplication>(applicationKey) as ServerApplication,
                        InformationSystem = cacheData.Get<InformationSystem>(code) as InformationSystem
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


    private string SetInformationSystem(ICacheData cacheData)
    {
        var code = _sheet.Cells.GetCell(
            row: InformationSystemCodeRowCol.Row,
            column: InformationSystemCodeRowCol.Col
        ).StringValue;

        var name = _sheet.Cells.GetCell(
            row: InformationSystemNameRowCol.Row,
            column: InformationSystemNameRowCol.Col
        ).StringValue;
        
        var infSystem = new InformationSystem { Code = code, Name = name };
        _validation.Validate(infSystem);

        cacheData.TryAdd(code, infSystem);

        return code;
    }

    private bool TrySetServerOs(string? name, string? version, string osKey, ICacheData cacheData) =>
        cacheData.TryAdd(osKey, new ServerOs { Name = name, Version = version });

    private bool TrySetServerApplication(string? name, string? version, string applicationKey, ICacheData cacheDict) =>
        cacheDict.TryAdd(applicationKey, new ServerApplication { Name = name, Version = version });

    private bool TrySetServerLocation(string? location, string locationKey, ICacheData cacheData) =>
        cacheData.TryAdd(locationKey, new Location { LocationIn = location });

    private bool TrySetContour(string contour, ICacheData cacheData) =>
        cacheData.TryAdd(contour, new Contour { Name = contour });

    private bool TrySetServerKind(string kindName, ICacheData cacheData) =>
        cacheData.TryAdd(kindName, new ServerKind { KindName = kindName });
    
    private async Task SetLanitAsync(DbContext context, IExcelToDatabase excelToDatabase)
    {
        // TODO: fix, del try/catch
        var maxRows = _sheet.Cells.MaxRow;

        for (var row = StartRow; row < maxRows; row++)
        {
            try
            {
                await excelToDatabase.AddDataToContextAsync(context, new Lanit
                {
                    Code = GetValueOrNull(_sheet.Cells.GetCell(row, LanitCodeCol).StringValue),
                    Domain = GetValueOrNull(_sheet.Cells.GetCell(row, LanitDomainCol).StringValue),
                    Name = GetValueOrNull(_sheet.Cells.GetCell(row, LanitSystemNameCol).StringValue),
                    Integrations = GetValueOrNull(_sheet.Cells.GetCell(row, LanitIntegrationsCol).StringValue),
                    ServersKind = GetValueOrNull(_sheet.Cells.GetCell(row, LanitServersKindCol).StringValue)
                });

            }
            catch
            {
            }
        }
    }

    private string? GetValueOrNull(string cell)
    {
        // TODO ref
        return string.IsNullOrEmpty(cell) ? null : cell;
    }

}