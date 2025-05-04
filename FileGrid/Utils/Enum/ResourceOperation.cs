namespace FileGrid.Utils.Enum;

// 权限操作类型（位掩码）
[Flags]
public enum ResourceOperation
{
    None = 0,
    Read = 1 << 0,      // 查看/下载
    Write = 1 << 1,     // 上传/覆盖
    Delete = 1 << 2,    // 删除资源
    Manage = 1 << 3,    // 权限管理
    Share = 1 << 4,     // 分享资源
    All = Read | Write | Delete | Manage | Share
}
