namespace FileGrid.Utils.Enum;

public enum FileStatus
{
    normal = 0, //正常：可被下载、覆盖、迁出
    freezed = 1, //冻结：无法下载、覆盖、迁出
    deprivated = 2, //过期：可以下载，无法覆盖、迁出
    todelete = 3 //待删除：无法下载、覆盖、迁出，定时任务会定期删除这类文件
}
