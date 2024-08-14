package f136.pix.payment_service.usecases.users.createUser;

import f136.pix.payment_service.entity.users.gateway.IUserGateway;
import f136.pix.payment_service.entity.users.model.User;


public class CreateUserUseCase {
    private final IUserGateway userGateway;
    
    public CreateUserUseCase(IUserGateway userGateway) {
        this.userGateway = userGateway;
    }
    
    public User execute(ICreateUserRequest data) {
        User user = new User(data.username());
        
        return user;
    } 
}



