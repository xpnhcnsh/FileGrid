using System.ComponentModel.DataAnnotations;

namespace FileGrid.Utils.Enum;

public enum CompanyType
{
    [Display(Name = "中煤科工")]
    CCTEG = 0,
    [Display(Name = "外协施工单位")]
    Outsource,
    [Display(Name = "业主")]
    PartA
}
