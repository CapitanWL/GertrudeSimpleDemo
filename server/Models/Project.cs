using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Project
{
    public int ProjectId { get; set; }

    public string Name { get; set; } = null!;

    public string? Patch { get; set; }

    public int? LanguageId { get; set; }

    public int? MetricsId { get; set; }

    public virtual ICollection<History> HistoryProject1s { get; set; } = new List<History>();

    public virtual ICollection<History> HistoryProject2s { get; set; } = new List<History>();

    public virtual Language? Language { get; set; }

    public virtual Metric? Metrics { get; set; }

    public virtual ICollection<ProjectMetric> ProjectMetrics { get; set; } = new List<ProjectMetric>();
}
