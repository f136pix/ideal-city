package f136.pix.payment_service.usecases.users.createUser;

import f136.pix.payment_service.entity.users.exceptions.UserNotFoundException;
import f136.pix.payment_service.entity.users.gateway.IUserGateway;
import f136.pix.payment_service.entity.users.model.User;
import org.springframework.transaction.annotation.Transactional;

import java.util.Optional;

public class CreateUserUseCase {
    private final IUserGateway userGateway;

    public CreateUserUseCase(IUserGateway userGateway) {
        this.userGateway = userGateway;
    }

    @Transactional
    public User execute(ICreateUserRequest data) {
        User userOptional = userGateway.findByName(data.username())
                .orElseThrow(UserNotFoundException::new);

        User user = new User(data.username());
        userGateway.create(user);

        return user;
    }
}



