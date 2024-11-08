using System;
using System.Collections.Generic;

namespace server.Models;

public partial class UserKey
{
    public int UserKeyId { get; set; }

    public int? UserId { get; set; }

    public byte[] Password { get; set; } = null!;

    public byte[] Salt { get; set; } = null!;

    public virtual User? User { get; set; }
}
