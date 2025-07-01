using System.ComponentModel.DataAnnotations;

namespace FileGrid.Utils.Enum;

public enum PhaseType
{
    [Display(Name = "准备阶段")]
    Preparation,
    [Display(Name = "垂直钻孔")]
    VerticalDrilling,
    [Display(Name = "定向钻孔")]
    InclinedDrilling,
    [Display(Name = "下套管")]
    Casing,
    [Display(Name = "注浆固管")]
    GroutingCasing,
    [Display(Name = "扩孔")]
    Reaming,
    [Display(Name = "填砾")]
    GravelFilling,
    [Display(Name = "填砂")]
    SandFilling,
    [Display(Name = "注浆")]
    Grouting,
    [Display(Name = "注浆候凝")]
    GroutingSetting,
    [Display(Name = "常规抽水试验")]
    PumpingTest,
    [Display(Name = "分层抽水试验")]
    StratifiedPumpingTest,
    [Display(Name = "压水试验")]
    PackerTest,
    [Display(Name = "常规测井")]
    ConventionalLogging,
    [Display(Name = "自然伽马测井")]
    NaturalGammaLogging,
    [Display(Name = "电阻率测井")]
    ResistivityLogging,
    [Display(Name = "声波测井")]
    AcousticLogging,
    [Display(Name = "密度测井")]
    DensityLogging,
    [Display(Name = "中子测井")]
    NeutronLogging,
    [Display(Name = "井径测量")]
    CaliperLogging,
    [Display(Name = "气测录井")]
    GasLogging,
    [Display(Name = "泥浆参数测井")]
    MudLogging,
    [Display(Name = "阵列综合测井")]
    ArrayComprehensiveLogging,
    [Display(Name = "阵列电阻率测井")]
    ArrayResistivityLogging,
    [Display(Name = "阵列感应测井")]
    ArrayInductionLogging,
    [Display(Name = "阵列声波测井")]
    ArrayAcousticLogging,
    [Display(Name = "阵列侧向测井")]
    ArrayLateralLogging,
    [Display(Name = "阵列微电阻率测井")]
    ArrayMicroResistivityLogging,
    [Display(Name = "实验室检测")]
    LabTesting,
    [Display(Name = "安装长观设备")]
    MonitoringInstrumentInstallation,
    [Display(Name = "封孔")]
    Sealing,
    [Display(Name = "其他")]
    Undefined
}
