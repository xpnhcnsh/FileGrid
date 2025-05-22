using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FileGrid.Utils.Enum;

namespace FileGrid.Entities;

public class Project
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? ProjectGroupId { get; set; }  // 所属项目组

    [ForeignKey(nameof(ProjectGroupId))]
    public virtual Department? ProjectGroup { get; set; }  // 导航属性
    public ProjectStatus Status { get; set; } = ProjectStatus.Active;  // 项目状态
    public DateTime? PlannedStartDate { get; set; }  // 项目预计开始时间
    public DateTime? ActualStartDate { get; set; }  // 项目实际开始时间
    public DateTime? PlannedEndDate { get; set; }  // 项目预计结束时间
    public DateTime? ActualEndDate { get; set; }  // 项目实际结束时间
    public Guid? ManagerId { get; set; }  // 项目经理
    public virtual User? Manager { get; set; }
    public Guid? DeputyManagerId { get; set; }   // 项目副经理
    public virtual User? DeputyManager { get; set; }
    public Guid? SafetyLeaderId { get; set; }   // 安全负责
    public virtual User? SafetyLeader { get; set; }
    public Guid? ProductionLeaderId { get; set; }  // 生产负责
    public virtual User? ProductionLeader { get; set; }
    public Guid? TechnicalLeaderId { get; set; }   // 技术负责
    public virtual User? TechnicalLeader { get; set; }
    public virtual ICollection<User> OtherParticipants { get; set; } = []; // 其他参与人员
    public int? PartAId { get; set; }  // 甲方公司ID
    public virtual Company? PartA { get; set; }  // 甲方

    public virtual ProjectResource? Resource { get; set; }  // 关联资源（MinIO存储路径）
    public virtual ICollection<UserProject> AuthorizedUsers { get; set; } = [];  // 授权用户
    public virtual ICollection<ProjectOutsource> Outsources { get; set; } = [];  // 外包单位
}
