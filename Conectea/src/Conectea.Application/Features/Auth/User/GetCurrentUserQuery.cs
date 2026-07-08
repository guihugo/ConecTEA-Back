using Conectea.Application.Features.Users.Me;
using MediatR;

public record GetCurrentUserQuery : IRequest<CurrentUserResponse>;