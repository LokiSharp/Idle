using Microsoft.Extensions.DependencyInjection;
using Program.SceneModels;

namespace Godot.SceneScripts;

public partial class MainScene : Node
{
    public MainScene()
    {
        Model = Program.Program.Services.GetService<MainSceneModel>();
        Model.Scene = this;
        Model.SetPackedScene(nameof(MainScene));
    }

    private MainSceneModel Model { get; }

    public override void _Ready()
    {
        Model.Ready();
        base._Ready();
    }

    public override void _Process(double delta)
    {
        Model.Process(delta);
        base._Process(delta);
    }
}