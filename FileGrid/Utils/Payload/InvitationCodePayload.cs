using System;

namespace FileGrid.Utils.Class;

public class InvitationCodePayload
{
    public UserGroup UserGroup { get; set; }
    public DateTime ExpiryDate { get; set; }
}
