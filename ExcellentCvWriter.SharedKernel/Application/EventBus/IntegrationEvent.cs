namespace ExcellentCvWriter.SharedKernel.Application.EventBus;

/// <summary>
/// Represents the abstract integration event primitive.
/// </summary>
public abstract record IntegrationEvent(Guid Id, DateTime OccurredOnUtc) : IIntegrationEvent;
