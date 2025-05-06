using System;
using FileGrid.Entities;
using FileGrid.Services.Interface;
using FileGrid.Utils;
using FileGrid.Utils.Class;

namespace FileGrid.Services;

public class InvitationCodeService : IInvitationCodeService
{
    private readonly FileGridContext _context;
    private readonly byte[] _key;
    private readonly byte[] _iv;
    private readonly TimeSpan _defaultValidDuration;

    public InvitationCodeService(IConfiguration config, FileGridContext context)
    {
        _key = Convert.FromBase64String(config["InviteCode:Key"]);
        _iv = Convert.FromBase64String(config["InviteCode:IV"]);
        _defaultValidDuration = TimeSpan.FromHours(config.GetValue<int>("InviteCode:DefaultValidDurationHours"));
        _context = context;
    }

    public Task<bool> ConsumeInviteCodeAsync(Guid codeId, Guid usedById)
    {
        throw new NotImplementedException();
    }

    public (UserGroup userGroup, DateTime expiryDate, Guid codeId)? DecodeInviteCode(string code)
    {
        throw new NotImplementedException();
    }

    public string GenerateInviteCode(UserGroup userGroup, TimeSpan validDuration, Guid createdById)
    {
        throw new NotImplementedException();
    }
}
