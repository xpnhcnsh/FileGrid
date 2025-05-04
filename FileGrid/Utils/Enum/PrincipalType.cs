namespace FileGrid.Utils.Enum;

/// <summary>
/// 权限主体类型
/// </summary>
public enum PrincipalType
{
    User, // 用户
    UserGroup  // 用户组，根据accountType分为：CCTEGL1-CCTEGL5、Outsource、PartA
}
