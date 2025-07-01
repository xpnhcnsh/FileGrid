using System.ComponentModel.DataAnnotations;

namespace FileGrid.Utils.Enum;

public enum CompletionConditionType
{
    [Display(Name = "文件审批")]
    FileApproval,
    [Display(Name = "参数满足")]
    ParameterMeet,
    [Display(Name = "手动")]
    Manual,
    [Display(Name = "前置满足")]
    PrerequisiteMet
}
