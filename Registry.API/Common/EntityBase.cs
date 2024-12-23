using System.ComponentModel.DataAnnotations;

namespace Registry.API.Common;

public class EntityBase
{
    [Key] public virtual long Id { get; set; }

    /// <summary>
    ///     when add entity set create date
    /// </summary>
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    ///     when update entity set modified date
    /// </summary>
    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    ///     work on the creator
    /// </summary>
    public long CreateBy { get; set; }

    /// <summary>
    ///     work on the updater
    /// </summary>
    public long ModifyBy { get; set; }

    /// <summary>
    ///     entity deleted
    /// </summary>
    public bool IsDelete { get; set; }
}