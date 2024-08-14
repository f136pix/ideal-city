package f136.pix.payment_service.contracts.users.createUser;

import f136.pix.payment_service.usecases.users.createUser.ICreateUserRequest;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;

public record CreateUserRequest(
        @NotNull
        String username
) implements ICreateUserRequest {
}
