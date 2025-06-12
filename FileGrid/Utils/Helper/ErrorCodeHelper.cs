using System;
using FileGrid.Utils.Enum;

namespace FileGrid.Utils.Class;

public class ErrorCodeHelper
{
    private static readonly Dictionary<ErrorCode, string> _errorMessages = new()
    {
        { ErrorCode.DuplicateDirectors, "项目的几个负责人不能重复" },
        { ErrorCode.EmptyOutsources, "外协单位不能为空" },
        { ErrorCode.EmptyPartA, "业主单位不能为空" },
    };

    public static string GetErrorMessage(ErrorCode errorCode)
    {
        return _errorMessages.TryGetValue(errorCode, out var message)
            ? message
            : "未知错误";
    }
}
