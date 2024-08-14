package f136.pix.payment_service.infra._config;

import f136.pix.payment_service.entity.users.gateway.IUserGateway;
import f136.pix.payment_service.infra.users.db.UserRepository;
import f136.pix.payment_service.infra.users.gateway.UserDatabaseGateway;
import f136.pix.payment_service.usecases.users.createUser.CreateUserUseCase;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class ApplicationBeanConfiguration {

    // Whatever we request a IUserGateway, receiving it DB implementation
    // User repository extends JPA repository, already autowired by Spring
    @Bean
    public IUserGateway userGateway(UserRepository userRepository) {
        return new UserDatabaseGateway(userRepository);
    }

    @Bean
    public CreateUserUseCase createUserUseCase(IUserGateway userGateway) {
        return new CreateUserUseCase(userGateway);
    }
}
