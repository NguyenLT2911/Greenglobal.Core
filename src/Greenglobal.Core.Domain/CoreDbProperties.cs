namespace Greenglobal.Core;

public static class CoreDbProperties
{
    public static string DbTablePrefix { get; set; } = "";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Core";

    public static string DbSchemaAuth { get; set; } = "auth";
}
