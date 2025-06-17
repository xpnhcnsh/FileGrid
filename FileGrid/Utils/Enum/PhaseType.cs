namespace FileGrid.Utils.Enum;

public enum PhaseType
{
    Preparation,
    VerticalDrilling,
    InclinedDrilling,
    Casing,
    GroutingCasing, //注浆固管
    Reaming,
    GravelFilling, //填砾
    SandFilling, //填砂
    Grouting, //注浆
    GroutingSetting, //注浆候凝
    PumpingTest, //常规抽水试验
    StratifiedPumpingTest, //分层抽水试验
    PackerTest, //压水试验
    ConventionalLogging, //常规测井
    NaturalGammaLogging, //自然伽马测井
    ResistivityLogging, //电阻率测井
    AcousticLogging, //声波测井
    DensityLogging, //密度测井
    NeutronLogging, //中子测井
    CaliperLogging, //井径测量
    GasLogging, //气测录井
    MudLogging, //泥浆参数测井
    ArrayComprehensiveLogging, //阵列综合测井
    ArrayResistivityLogging, //阵列电阻率测井
    ArrayInductionLogging, //阵列感应测井
    ArrayAcousticLogging, //阵列声波测井
    ArrayLateralLogging, //阵列侧向测井
    ArrayMicroResistivityLogging, //阵列微电阻率测井
    LabTesting, //实验室检测
    MonitoringInstrumentInstallation, //安装水文长观设备
    Sealing, //封孔
    Undefined //其他
}
