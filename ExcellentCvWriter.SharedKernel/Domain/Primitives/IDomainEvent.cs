using MediatR;

namespace ExcellentCvWriter.SharedKernel.Domain.Primitives;

public interface IDomainEvent : INotification
{
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the occurred on date and time.
    /// </summary>
    DateTime OccurredOnUtc { get; }
}