using Extension.Utils;
using Program.Interfaces;
using Program.Services;

namespace Program.SceneModels;

public class MainSceneModel(TestService testService, NlogHelper nlogHelper) : SceneModelBase
{
    public override void Ready()
    {
        nlogHelper.Info("运行 MainSceneModel Ready");
        testService.Test();
    }

    public override void Process(double delta)
    {
        nlogHelper.Info("运行 MainSceneModel Process");
    }
}