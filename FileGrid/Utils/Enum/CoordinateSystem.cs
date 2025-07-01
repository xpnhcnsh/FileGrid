using System.ComponentModel.DataAnnotations;

namespace FileGrid.Utils.Enum;

public enum CoordinateSystem
{
    [Display(Name = "北京1954")]
    BeiJing1954,
    [Display(Name = "西安1980")]
    Xian1980,
    [Display(Name = "中国大地2000")]
    ChinaGeoDetic2000,
    [Display(Name = "GPS全球1984")]
    WGS84,
    [Display(Name = "UTM投影")]
    UTM
}
