using System;
using System.Collections.Generic;

namespace server.Models;

public partial class History
{
    public int HistoryId { get; set; }

    public string Name { get; set; } = null!;

    public int? Project2Id { get; set; }

    public int? Project1Id { get; set; }

    public virtual Project? Project1 { get; set; }

    public virtual Project? Project2 { get; set; }
}
