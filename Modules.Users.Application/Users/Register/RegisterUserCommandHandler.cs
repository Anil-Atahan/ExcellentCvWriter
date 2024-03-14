using ExcellentCvWriter.SharedKernel.Common.Results;
using MediatR;
using Modules.Users.Application.Authentication;
using Modules.Users.Domain;
using Modules.Users.Domain.Users;

namespace Modules.Users.Application.Users.Register;

/// <summary>
/// Represents the <see cref="RegisterUserCommand"/> handler.
/// </summary>
internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterUserCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="authenticationService">The authentication service</param>
    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork, 
        IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _authenticationService = authenticationService;
    }

    /// <inheritdoc />
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var isEmailExists = await _userRepository.IsEmailExistsAsync(request.Email, cancellationToken);
        if (isEmailExists.Value)
        {
            return Result.Failure<Guid>(UserErrors.EmailIsNotUnique);
        }
        
        var user = User.Create(
            new UserId(Guid.NewGuid()),
            request.Email,
            request.FirstName,
            request.LastName);
        
        var identityProviderId = await _authenticationService.RegisterAsync(
            user,
            request.Password,
            cancellationToken);

        user.SetIdentityProviderId(identityProviderId);
        
        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id.Value);
    }
}
