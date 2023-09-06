using System.Configuration;

namespace Inventory;

public static class Settings
{
    public static readonly string DbDSN = ConfigurationManager.AppSettings.Get("DbDSN")
                                          ?? throw new Exception("Db connection string is missing!");

    public static readonly bool RecreatingDb = true;
}