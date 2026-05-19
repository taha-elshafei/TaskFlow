using TaskFlow.Domain.Common;

namespace TaskFlow.Domain.Entities;

public class Permission : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<RolePermission> RolePermissions { get; set; } = [];
}
