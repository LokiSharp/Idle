using FreeSql;
using Godot;

namespace Extension.Utils;

public class FreeSqlHelper
{
    public FreeSqlHelper()
    {
        var baseUrl = AppDomain.CurrentDomain.BaseDirectory;
        var connectStr = $"Data Source={baseUrl}Assets\\sqliteDb.db";
        GD.Print($"sqliteDb 数据库连接位置为:{connectStr}");

        SqliteDb = new FreeSqlBuilder()
            .UseConnectionString(DataType.Sqlite, $"Data Source={baseUrl}\\Assets\\sqliteDb.db")
            .UseAutoSyncStructure(true) //DbFirst模式
            .Build();
    }

    public IFreeSql SqliteDb { get; set; }
}