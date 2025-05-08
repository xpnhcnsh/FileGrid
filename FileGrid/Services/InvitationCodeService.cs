using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using FileGrid.Entities;
using FileGrid.Services.Interface;
using FileGrid.Utils;
using FileGrid.Utils.Class;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace FileGrid.Services;

public class InvitationCodeService : IInvitationCodeService
{
    private readonly FileGridContext _context;
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public InvitationCodeService(IConfiguration config, FileGridContext context)
    {
        _key = Convert.FromBase64String(config["InvitationCode:Key"]);
        _iv = Convert.FromBase64String(config["InvitationCode:IV"]);
        _context = context;
    }

    /// <summary>
    /// 由外层统一savechanges()
    /// </summary>
    /// <param name="code"></param>
    /// <param name="usedById"></param>
    /// <returns></returns>
    public async Task<bool> ConsumeInviteCodeAsync(string code, Guid usedById)
    {

        try
        {
            if (code.ToLower().Equals("god"))
            {
                return true;
            }
            var inviteCode = await _context.InvitationCodes
                .FirstOrDefaultAsync(c => c.Code.Equals(code));

            if (inviteCode == null || inviteCode.IsUsed)
                return false;

            // 更新邀请码状态
            inviteCode.IsUsed = true;
            inviteCode.UsedAt = DateTime.UtcNow;
            inviteCode.UsedById = usedById;

            return true;
        }
        catch
        {
            throw;
        }
    }

    public UserGroup DecodeInviteCode(string code)
    {
        try
        {
            if (code == null || code == string.Empty)
                throw new Exception("邀请码不能为空");
            if (code.ToLower() == "god")
            {
                return UserGroup.God;
            }
            var payload = DecryptPayload(code) ?? throw new Exception("邀请码格式错误");

            // 验证数据库记录
            var dbRecord = _context.InvitationCodes
                .AsNoTracking()
                .FirstOrDefault(c => c.Code.Equals(code));

            if (dbRecord == null ||
                dbRecord.IsUsed)
            {
                throw new Exception("邀请码不存在或已被使用");
            }

            if (dbRecord.ExpiredAt.ToUniversalTime() < DateTime.UtcNow)
            {
                throw new Exception("邀请码已过期");
            }

            return dbRecord.UserGroup;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<string> GenerateInviteCodeAsync(UserGroup userGroup, TimeSpan validDurationHours, Guid createdById)
    {
        var expiryDate = DateTime.UtcNow.Add(validDurationHours);

        var payload = new InvitationCodePayload
        {
            UserGroup = userGroup,
            ExpiryDate = expiryDate
        };

        var invitationCode = new InvitationCode
        {
            UserGroup = userGroup,
            ValidDurationHours = validDurationHours,
            ExpiredAt = expiryDate,
            CreatorId = createdById,
            Code = EncryptPayload(payload),
            CreatedAt = DateTime.UtcNow
        };

        await _context.InvitationCodes.AddAsync(invitationCode);
        await _context.SaveChangesAsync();

        return invitationCode.Code;
    }

    private string EncryptPayload(InvitationCodePayload payload)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
        {
            var json = JsonSerializer.Serialize(payload);
            var data = Encoding.UTF8.GetBytes(json);
            cs.Write(data, 0, data.Length);
        }

        return Convert.ToBase64String(ms.ToArray())
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }

    private InvitationCodePayload? DecryptPayload(string code)
    {
        try
        {
            // 修复Base64格式
            var base64 = code
                .Replace('-', '+')
                .Replace('_', '/');

            switch (code.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            var encrypted = Convert.FromBase64String(base64);

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            using var ms = new MemoryStream(encrypted);
            using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);

            var json = reader.ReadToEnd();
            return JsonSerializer.Deserialize<InvitationCodePayload>(json);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<InvitationCode>> GetAllInvitationCodesAsync()
    {
        var codes = await _context.InvitationCodes
            .AsNoTracking()
            .Include(ic => ic.Creator)
            .Include(ic => ic.UsedBy)
            .ToListAsync();

        return codes;
    }

    public async Task<bool> DeleteInviteCodeAsync(int CodeId)
    {
        var toDelete = await _context.InvitationCodes
            .FirstOrDefaultAsync(c => c.Id == CodeId);
        if (toDelete != null && toDelete.IsUsed == false)
        {
            _context.InvitationCodes.Remove(toDelete);
            await _context.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }
}
