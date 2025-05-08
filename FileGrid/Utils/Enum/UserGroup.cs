using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace FileGrid.Utils;

public enum UserGroup
{
    [Description("具有所有权限")]
    God = 0,
    [Description("CCTEG人员，可访问除IT部分外所有信息")]
    CCTEGL0,
    [Description("CCTEG人员，只可访问管理类页面，例如项目/组织架构/人员等信息")]
    CCTEGL1,
    [Description("CCTEG人员，可访问所有人员、项目信息")]
    CCTEGL2,
    [Description("CCTEG人员，只能访问某些项目部的人员和项目信息")]
    CCTEGL3,
    [Description("CCTEG人员，只能访问某些项目的人员和项目信息")]
    CCTEGL4,
    [Description("外协人员，可访问和其账户相关的具体项目和人员")]
    Outsource,
    [Description("甲方人员，可访问和其账户相关的具体项目和人员")]
    PartA
}
