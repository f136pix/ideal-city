package f136.pix.payment_service.infra.users._common.validation;

import jakarta.validation.ConstraintValidator;
import jakarta.validation.ConstraintValidatorContext;
import org.springframework.beans.factory.annotation.Autowired;

public class UniqueUsernameValidator implements ConstraintValidator<UniqueUsername, String> {
    @Autowired
    private UserRepository repository;

    @Override
    public boolean isValid(String username, ConstraintValidatorContext constraintValidatorContext) {
        if (repository != null) {
            Optional<UserSchema> usuario = repository.findByUsername(username);
            return usuario.isEmpty();
        }
        return true;
    }
} 
