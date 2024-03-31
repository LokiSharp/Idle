using Extension.Interfaces;
using Extension.Utils;
using Program.DB;
using Program.Services;

namespace Program.SceneModels;

public class MainSceneModel(
    TestService testService,
    NlogHelper nlogHelper,
    FreeSqlHelper freeSqlHelper)
    : SceneModelBase
{
    public override void Ready()
    {
        nlogHelper.Info("运行 MainSceneModel Ready");
        var isConnect = freeSqlHelper.SqliteDb.Ado.ExecuteConnectTest(10);
        nlogHelper.Info($"数据库连接状态:[{isConnect}]");
        var insertLists = new Character().FakeMany(10);
        var insertNum = freeSqlHelper.SqliteDb.Insert(insertLists).ExecuteAffrows();
        nlogHelper.Info($"数据库插入[{insertNum}]行数据");
        var selectLists = freeSqlHelper.SqliteDb.Queryable<Character>().OrderByDescending(t => t.Id).Take(10).ToList();
        nlogHelper.Info($"数据库读取[{selectLists.Count}]行数据");
        testService.Test();
    }

    public override void Process(double delta)
    {
        nlogHelper.Info("运行 MainSceneModel Process");
    }
}