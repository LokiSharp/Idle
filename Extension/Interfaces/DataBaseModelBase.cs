using FreeSql.DataAnnotations;

namespace Extension.Interfaces;

/// <summary>
///     数据库基类
/// </summary>
public class DataBaseModelBase
{
    /// <summary>
    ///     主键自增
    /// </summary>
    [Column(IsPrimary = true, IsIdentity = true)]
    public long Id { get; set; }

    /// <summary>
    ///     创建时间
    /// </summary>
    public DateTime CreateTime { get; set; } = DateTime.Now;

    /// <summary>
    ///     更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; } = DateTime.Now;

    /// <summary>
    ///     假删除
    /// </summary>
    public bool IsDelete { get; set; } = false;

    public DateTime DeleteTime { get; set; }
}