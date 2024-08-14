package f136.pix.payment_service.contracts.users.createUser;

import f136.pix.payment_service.entity.users.model.User;
import jakarta.validation.constraints.NotNull;

public record CreateUserResponse(@NotNull Long id, @NotNull String username) {
    public CreateUserResponse(User user) {
        this(user.getId(), user.getName());
    }
}


