using Microsoft.EntityFrameworkCore;

namespace FileGrid.Utils;

public enum UserGroup
{
    God = 0, //所有权限
    CCTEGL1, //所有人员、项目信息
    CCTEGL2, //在L1的基础上，只能访问某些项目部的人员和项目信息
    CCTEGL3, //在L2的基础上，只能访问某些项目的人员和项目信息
    CCTEGL4, //只可访问管理类页面，例如项目/组织架构/人员等信息
    CCTEGL5, //除IT部分外所有信息
    Outsource, //可访问和其账户相关的具体项目和人员
    PartA //可访问和其账户相关的具体项目和人员
}

