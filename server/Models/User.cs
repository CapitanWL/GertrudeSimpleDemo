using System;
using System.Collections.Generic;

namespace server.Models;

/// <summary>
/// Пользователь.
/// </summary>
public partial class User
{
    public int UserId { get; set; }

    public string Nickname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<UserKey> UserKeys { get; set; } = new List<UserKey>();
}
