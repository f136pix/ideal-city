using Application.Authentication.Commands;
using Application.Authentication.Common;
using Contracts.Authentication;
using Mapster;

namespace Application._Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
        {
            // maps each property to the exactly corresponding
            // src -> dest
            config.NewConfig<RegisterUserRequest, RegisterUserCommand>();

            config.NewConfig<AuthenticationResult, RegisterResponse>()
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.Token, src => src.Token);
            
            


        }
}