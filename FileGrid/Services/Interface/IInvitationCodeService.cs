using FileGrid.Entities;
using FileGrid.Utils;
using FileGrid.Utils.Class;

namespace FileGrid.Services.Interface;

public interface IInvitationCodeService
{
    Task<string> GenerateInviteCodeAsync(UserGroup userGroup, TimeSpan validDurationHours, Guid createdById);
    UserGroup DecodeInviteCode(string code);
    Task<bool> ConsumeInviteCodeAsync(string code, Guid usedById);
    Task<List<InvitationCode>> GetAllInvitationCodesAsync();
    Task<bool> DeleteInviteCodeAsync(int codeId);
}
