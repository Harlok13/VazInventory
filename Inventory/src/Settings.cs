using System.Configuration;

namespace Inventory;

public static class Settings
{
    public static readonly string DbDSN = ConfigurationManager.AppSettings.Get("DbDSN")
                                          ?? throw new Exception("Db connection string is missing!");

    public const bool RecreatingDb = true;
    
    private static readonly string PathToInventoryXlsxFiles = ConfigurationManager.AppSettings.Get("PathToInventoryXlsxFiles")
                                                    ?? throw new Exception("Path to xlsx files is missing!");

    public const int WorkSheetNum = 0;

    public static readonly (int Row, int Col) InformationSystemCodeRowCol = (0, 4);
    public static readonly (int Row, int Col) InformationSystemNameRowCol = (1, 4);

    public static readonly int[][] ServersInfoRows =
    {
        new int[] { 8, 13, 18, 23 }, // PROD
        new int[] { 43, 48, 53, 58 }, // TEST
        new int[] { 78, 83, 88, 93 }, // DEV
        new int[] { 113, 118, 123, 128 } // MODE
    };

    public static readonly string[] Contours = { "PROD", "TEST", "DEV", "MODE" };

    public static readonly string[] ServersKind = { "DBMS", "Frontend", "Backend", "API" };

    public const string DomainSearchWord = "vaz";

    public const int ServerDomainCol = 2;

    public const int ServerOsNameCol = 3;
    public const int ServerOsVersionCol = 4;

    public const int ServerApplicationNameCol = 5;
    public const int ServerApplicationVersionCol = 6;

    public const int ServerLocationCol = 7;
    
    // ________________________________________________________________________________________________

    public static readonly string DbLanitDSN = ConfigurationManager.AppSettings.Get("DbLanitDSN")
                                               ?? throw new Exception("Db Lanit connection string is missing!");
    
    public const int LanitCodeCol = 0;
    public const int LanitSystemNameCol = 1;
    public const int LanitDomainCol = 3;
    public const int LanitServersKindCol = 4;
    public const int LanitIntegrationsCol = 5;

    private static readonly string PathToLanitXlsxFile = ConfigurationManager.AppSettings.Get("PathToLanitXlsxFile")
                                                        ?? throw new Exception("Path to Lanit xlsx file is missing!");

    public const string LanitFileName = "LanitCompare.xlsx";
    
    public const int StartRow = 1;

    public static readonly List<string> DirectoriesForParse = new()
        { PathToInventoryXlsxFiles, PathToLanitXlsxFile };

    public static string Language = new string[] { "Ru", "En" }[0];
}