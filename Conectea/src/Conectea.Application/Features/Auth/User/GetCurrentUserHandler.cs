using Conectea.Application.Features.Users.Me;
using Conectea.Application.Interfaces;
using MediatR;

public class GetCurrentUserHandler: IRequestHandler<GetCurrentUserQuery, CurrentUserResponse>
{
    private readonly ICurrentUser _currentUser;
    private readonly IIdentityService _identityService;

    public GetCurrentUserHandler(
        ICurrentUser currentUser,
        IIdentityService identityService)
    {
        _currentUser = currentUser;
        _identityService = identityService;
    }

    public async Task<CurrentUserResponse> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        return await _identityService.GetCurrentUserAsync(_currentUser.UserId);
    }
}