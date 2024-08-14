package f136.pix.payment_service.infra.users.createUser;

import f136.pix.payment_service.contracts.users.createUser.CreateUserRequest;
import f136.pix.payment_service.contracts.users.createUser.CreateUserResponse;
import f136.pix.payment_service.usecases.users.createUser.CreateUserUseCase;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.servlet.function.RouterFunction;
import org.springframework.web.servlet.function.RouterFunctions;

import static org.springframework.web.servlet.function.RequestPredicates.POST;

@RestController
public class CreateUserController {
    private final CreateUserUseCase createUserUseCase;

    // DI in ApplicationBeanConfiguration.java
    public CreateUserController(CreateUserUseCase createUserUseCase) {
        this.createUserUseCase = createUserUseCase;
    }

    @PostMapping("/users")
    @ResponseStatus(HttpStatus.CREATED)
    public CreateUserResponse createCustomer(@Valid @RequestBody CreateUserRequest dados) {
        return new CreateUserResponse(createUserUseCase.execute(dados));
    }
}