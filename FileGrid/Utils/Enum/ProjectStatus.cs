using System.ComponentModel.DataAnnotations;

namespace FileGrid.Utils.Enum;

public enum ProjectStatus
{
    [Display(Description = "项目正在进行中", ShortName = "正常")]
    Active = 0,
    [Display(Description = "项目已完成", ShortName = "完成")]
    Completed = 1,
    [Display(Description = "项目已存档", ShortName = "存档")]
    Archived = 2,
    [Display(Description = "项目已取消", ShortName = "取消")]
    Cancelled = 3

}
