# Idle
基于 Godot 引擎使用 C# 开发的放置游戏

## 项目结构
* `Godot` - Godot 引擎生成的解决方案
  * `Scenes` - 用于存放 Godot 场景
  * `SceneScripts` - 用于存放 Godot 场景脚本，用于建立逻辑连接关系
* `Program` - Godot 运行逻辑
  * `Program.cs` - IOC 容器
  * `Services` - 服务类
  * `ScenModels` - 场景脚本实际运行类，IOC 装配生成
* `Extension` 扩展类
  * `Assets` - 资源文件夹
  * `Interfaces` - 接口类
  * `Utils` - 工具类