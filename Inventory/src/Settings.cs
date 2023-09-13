using System.Configuration;

namespace Inventory;

public static class Settings
{
    public static readonly string DbDSN = ConfigurationManager.AppSettings.Get("DbDSN")
                                          ?? throw new Exception("Db connection string is missing!");

    public static readonly bool RecreatingDb = true;
    
    public static readonly string PathToXlsxFiles = ConfigurationManager.AppSettings.Get("PathToXlsxFiles")
                                                    ?? throw new Exception("Path to xlsx files is missing!");

    public static readonly int WorkSheetNum = 0;

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

    public const string DomainMissingMessage = "отсутствует";

    public static readonly int ServerDomainCol = 2;

    public static readonly int ServerOsNameCol = 3;
    public static readonly int ServerOsVersionCol = 4;

    public static readonly int ServerApplicationNameCol = 5;
    public static readonly int ServerApplicationVersionCol = 6;

    public static readonly int ServerLocationCol = 7;
}