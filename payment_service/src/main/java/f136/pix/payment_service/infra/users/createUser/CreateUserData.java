package f136.pix.payment_service.infra.users.dto;

import f136.pix.payment_service.infra.users.validation.UniqueUsername;
import f136.pix.payment_service.usecases.users.createUser.ICreateUserData;
import jakarta.validation.constraints.NotBlank;

public record CreateUserData(
        @UniqueUsername // (message = "Username already exists")
        @NotBlank
        String username
) implements ICreateUserData {
}
