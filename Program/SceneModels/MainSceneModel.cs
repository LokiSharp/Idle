using Extension.Utils;
using Godot;
using Program.Interfaces;
using Program.Services;

namespace Program.SceneModels;

public class MainSceneModel : SceneModelBase
{
    private readonly TestService _testService = new();
    private readonly TestUtils _testUtils = new();

    public override void Ready()
    {
        GD.Print("运行 MainSceneModel Ready");
        _testService.Test();
        _testUtils.Test();
    }

    public override void Process(double delta)
    {
        GD.Print("运行 MainSceneModel Process");
    }
}