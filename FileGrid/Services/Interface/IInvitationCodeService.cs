using FileGrid.Utils;

namespace FileGrid.Services.Interface;

public interface IInvitationCodeService
{
    string GenerateInviteCode(UserGroup userGroup, TimeSpan validDuration, Guid createdById);
    (UserGroup userGroup, DateTime expiryDate, Guid codeId)? DecodeInviteCode(string code);
    Task<bool> ConsumeInviteCodeAsync(Guid codeId, Guid usedById);
}
