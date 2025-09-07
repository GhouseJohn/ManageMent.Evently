using BuidingBlock.Application.Messaging;
using BuidingBlock.Domain;
using Module.User.Application.Abstractions.Data;
using Module.User.Domain.Users;

namespace Module.User.Application.Users;
public record CreateuserCommandRequest(string FirstName, string Email, string LastName)
                        : ICommand<Result<CreateuserCommandResponse>>;
public record CreateuserCommandResponse(Guid Id);
internal sealed class CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateuserCommandRequest, Result<CreateuserCommandResponse>>
{
    public async Task<Result<CreateuserCommandResponse>> Handle(CreateuserCommandRequest request, CancellationToken cancellationToken)
    {

        try
        {
            var Ouser = UserProperties.Create(
                            request.Email,
                            request.FirstName,
                            request.LastName);
            userRepository.Insert(Ouser);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(new CreateuserCommandResponse(Ouser.Id));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }

    }
}
