using ExcellentCvWriter.SharedKernel.Application.ServiceLifetimes;
using ExcellentCvWriter.SharedKernel.Application.Time;

namespace ExcellentCvWriter.SharedKernel.Infrastructure.Time;

/// <summary>
/// Represents the system time interface.
/// </summary>
public sealed class SystemTime : ISystemTime, ITransient
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}
