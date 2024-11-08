using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Metric
{
    public int MetricsId { get; set; }

    public int? MetricTypeId { get; set; }

    public bool IsIgnored { get; set; }

    public virtual ICollection<MetricsLineOfCodeInterval> MetricsLineOfCodeIntervals { get; set; } = new List<MetricsLineOfCodeInterval>();

    public virtual ICollection<ProjectMetric> ProjectMetrics { get; set; } = new List<ProjectMetric>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
