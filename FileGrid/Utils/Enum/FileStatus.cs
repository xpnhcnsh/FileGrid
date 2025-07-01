using System.ComponentModel.DataAnnotations;

namespace FileGrid.Utils.Enum;

public enum FileStatus
{
    [Display(Description = "正常:可下载、可覆盖、可迁出", ShortName = "正常")]
    normal = 0,
    [Display(Description = "冻结:无法下载、无法覆盖、无法迁出", ShortName = "冻结")]
    freezed = 1,
    [Display(Description = "过期:可以下载，无法覆盖、无法迁出", ShortName = "过期")]
    deprivated = 2,
    [Display(Description = "待删除:无法下载、无法覆盖、无法迁出，会被定时任务删除", ShortName = "待删除")]
    todelete = 3
}
