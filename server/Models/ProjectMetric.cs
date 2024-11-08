using System;
using System.Collections.Generic;

namespace server.Models;

public partial class ProjectMetric
{
    public int ProjectMetricsId { get; set; }

    public int? ProjectId { get; set; }

    public int? MetricsId { get; set; }

    public virtual Metric? Metrics { get; set; }

    public virtual Project? Project { get; set; }
}
