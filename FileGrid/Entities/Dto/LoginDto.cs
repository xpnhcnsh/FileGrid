using System;
using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities.Dto;

public class LoginDto
{
    [Required(ErrorMessage = "用户名不能为空")]
    [StringLength(20, ErrorMessage = "用户名长度不能超过20个字符")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "密码不能为空")]
    [DataType(DataType.Password)]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "密码长度1-20个字符")]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}
