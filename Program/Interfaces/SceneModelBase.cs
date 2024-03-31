using Godot;

namespace Program.Interfaces;

/// <summary>
/// 场景模型基类
/// </summary>
public abstract class SceneModelBase
{
    /// <summary>
    /// 获取或设置场景节点
    /// </summary>
    public Node? Scene { get; set; }

    /// <summary>
    /// 获取或设置预先打包的场景
    /// </summary>
    public PackedScene? PackedScene { get; set; }

    /// <summary>
    /// 用于初始化场景模型的方法，每个子类必须实现
    /// </summary>
    public abstract void Ready();

    /// <summary>
    /// 用于处理场景模型的方法，每个子类必须实现
    /// </summary>
    /// <param name="delta">从上一帧到当前帧经过的时间（秒）</param>
    public abstract void Process(double delta);

    /// <summary>
    /// 设置预先打包的场景
    /// </summary>
    /// <param name="sceneName">场景名，将会替换 "Scene" 得到实际文件名</param>
    public virtual void SetPackedScene(string sceneName)
    {
        var targetName = sceneName.Replace("Scene", "");
        var url = $"res://Scenes//{targetName}.tscn";
        GD.Print($"加载 PackedScene, {sceneName}:{url}");
        PackedScene = ResourceLoader.Load<PackedScene>(url);
    }
}