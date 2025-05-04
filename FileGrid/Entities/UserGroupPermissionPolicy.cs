using System;
using FileGrid.Utils;
using FileGrid.Utils.Enum;

namespace FileGrid.Entities;

// 用户组权限策略模板
public class UserGroupPermissionPolicy
{
    public int Id { get; set; }
    public UserGroup UserGroup { get; set; }
    public string PathPattern { get; set; } = null!;   // 通配符路径，如 "projects/*/design/"
    public ResourceOperation Operations { get; set; }
}
