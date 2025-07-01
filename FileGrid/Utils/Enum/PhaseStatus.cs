using System.ComponentModel.DataAnnotations;

namespace FileGrid.Utils.Enum;

public enum PhaseStatus
{
    [Display(Name = "未开始")]
    NotStarted,
    [Display(Name = "进行中")]
    InProgress,
    [Display(Name = "已完成")]
    Completed
}
