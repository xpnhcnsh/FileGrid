using System.ComponentModel.DataAnnotations;

namespace FileGrid.Utils.Enum;

public enum DrillHoleStatus
{
    [Display(Name = "未开始")]
    NotStarted,
    [Display(Name = "准备中")]
    Preparing,
    [Display(Name = "进行中")]
    InProgress,
    [Display(Name = "已完成")]
    Completed,
    [Display(Name = "已封孔")]
    Sealed,
    [Display(Name = "已废弃")]
    Abandoned
}
