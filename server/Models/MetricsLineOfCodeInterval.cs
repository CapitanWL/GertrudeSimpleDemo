using System;
using System.Collections.Generic;

namespace server.Models;

public partial class MetricsLineOfCodeInterval
{
    public int MetricsLineOfCodeIntervalId { get; set; }

    public int? LineOfCodeIntervalId { get; set; }

    public int? MetricId { get; set; }

    public virtual Metric? Metric { get; set; }
}
