using Godot;
using NLog;
using NLog.Config;

namespace Extension.Utils;

public class NlogHelper
{
    private readonly Logger _logger;

    public NlogHelper()
    {
        var url = $"{AppDomain.CurrentDomain.BaseDirectory}Assets/Nlog.config";
        GD.Print($"Nlog 加载完毕，url 地址为[{url}]");
        LogManager.Configuration = new XmlLoggingConfiguration(url);

        _logger = LogManager.GetCurrentClassLogger();
    }

    public void Debug(string msg)
    {
        GD.Print($"[DEBUG]:{msg}");
        _logger.Debug(msg);
    }

    public void Info(string msg)
    {
        GD.Print($"[INFO]:{msg}");
        _logger.Info(msg);
    }

    public void Error(string msg)
    {
        GD.PrintErr($"[ERROR]:{msg}");
        GD.PushError(msg);
        _logger.Error(msg);
    }

    public void Warning(string msg)
    {
        GD.Print($"[WARN]:{msg}");
        GD.PushWarning(msg);
        _logger.Warn(msg);
    }
}