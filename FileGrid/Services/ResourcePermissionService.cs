using Microsoft.EntityFrameworkCore;
using FileGrid.Entities;
using FileGrid.Utils.Enum;

namespace FileGrid.Services
{
    public class ResourcePermissionService(FileGridContext context)
    {
        private readonly FileGridContext _context = context;

        /// <summary>
        /// 验证用户对资源的操作权限
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="resourcePath">资源路径</param>
        /// <param name="requiredOp">所需操作权限</param>
        /// <returns>是否具有权限</returns>
        public async Task<bool> CheckPermissionAsync(User user, string resourcePath, ResourceOperation requiredOp)
        {
            var effectiveOps = await GetEffectivePermissionsAsync(user, resourcePath);
            return (effectiveOps & requiredOp) == requiredOp;
        }

        /// <summary>
        /// 获取用户对资源的有效权限（合并直接权限和继承权限）
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="resourcePath">资源路径</param>
        /// <returns>合并后的权限</returns>
        public async Task<ResourceOperation> GetEffectivePermissionsAsync(User user, string resourcePath)
        {
            // 1. 获取直接权限
            var directPermissions = await _context.ResourcePermissions
                .Where(p =>
                    (p.PrincipalType == PrincipalType.User && p.PrincipalTag == $"User:{user.Id}") ||
                    (p.PrincipalType == PrincipalType.UserGroup && p.PrincipalTag == $"UserGroup:{user.UserGroup}")
                )
                .Where(p => p.ResourcePath == resourcePath)
                .ToListAsync();

            // 2. 获取继承权限（向上遍历父目录）
            var inheritedPermissions = await GetInheritedPermissionsAsync(user, resourcePath);

            // 3. 合并权限
            var allPermissions = directPermissions.Concat(inheritedPermissions);
            return MergeOperations(allPermissions);
        }

        /// <summary>
        /// 合并权限位掩码
        /// </summary>
        /// <param name="permissions">权限列表</param>
        /// <returns>合并后的权限</returns>
        private ResourceOperation MergeOperations(IEnumerable<ResourcePermission> permissions)
        {
            var result = ResourceOperation.None;

            // 只处理未过期的权限
            foreach (var perm in permissions.Where(p => p.ExpireTime == null || p.ExpireTime > DateTime.UtcNow))
            {
                result |= perm.Operations;
            }

            return result;
        }

        /// <summary>
        /// 递归获取父目录权限
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="path">资源路径</param>
        /// <returns>继承的权限列表</returns>
        private async Task<List<ResourcePermission>> GetInheritedPermissionsAsync(User user, string path)
        {
            var permissions = new List<ResourcePermission>();

            // 处理路径分隔符，确保跨平台兼容性
            path = path.Replace('\\', '/');

            // 循环向上查找父目录
            string parentPath = GetParentPath(path);
            while (!string.IsNullOrEmpty(parentPath))
            {
                var parentPerms = await _context.ResourcePermissions
                    .Where(p =>
                        ((p.PrincipalType == PrincipalType.User && p.PrincipalTag == $"User:{user.Id}") ||
                         (p.PrincipalType == PrincipalType.UserGroup && p.PrincipalTag == $"UserGroup:{user.UserGroup}")) &&
                        p.ResourcePath == parentPath &&
                        p.ApplyToSubItems
                    )
                    .ToListAsync();

                permissions.AddRange(parentPerms);
                parentPath = GetParentPath(parentPath);
            }

            return permissions;
        }

        /// <summary>
        /// 获取父路径
        /// </summary>
        /// <param name="path">当前路径</param>
        /// <returns>父路径</returns>
        private string GetParentPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // 统一使用正斜杠处理路径
            path = path.Replace('\\', '/');

            // 移除尾部斜杠
            if (path.EndsWith("/"))
                path = path.Substring(0, path.Length - 1);

            int lastSlashIndex = path.LastIndexOf('/');
            if (lastSlashIndex <= 0)
                return string.Empty; // 根目录没有父目录或者路径格式不符合预期

            return path.Substring(0, lastSlashIndex);
        }

        /// <summary>
        /// 创建资源权限
        /// </summary>
        /// <param name="permission">权限对象</param>
        public async Task<ResourcePermission> CreatePermissionAsync(ResourcePermission permission)
        {
            _context.ResourcePermissions.Add(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        /// <summary>
        /// 移除资源权限
        /// </summary>
        /// <param name="id">权限ID</param>
        public async Task<bool> RemovePermissionAsync(int id)
        {
            var permission = await _context.ResourcePermissions.FindAsync(id);
            if (permission == null)
                return false;

            _context.ResourcePermissions.Remove(permission);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 获取用户所有权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        public async Task<List<ResourcePermission>> GetUserPermissionsAsync(Guid userId)
        {
            // 获取用户信息，包括用户组
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return new List<ResourcePermission>();

            // 获取用户的所有权限（包括用户组权限）
            return await _context.ResourcePermissions
                .Where(p =>
                    (p.PrincipalType == PrincipalType.User && p.PrincipalTag == $"User:{userId}") ||
                    (p.PrincipalType == PrincipalType.UserGroup && p.PrincipalTag == $"UserGroup:{user.UserGroup}")
                )
                .ToListAsync();
        }
    }
}