package f136.pix.payment_service.infra.users.createUser;

import f136.pix.payment_service.infra._config.validation.UniqueUsername;
import f136.pix.payment_service.usecases.users.createUser.ICreateUserRequest;
import jakarta.validation.constraints.NotBlank;

public record CreateUserData(
        @UniqueUsername // (message = "Username already exists")
        @NotBlank
        String username
) implements ICreateUserRequest {
}
