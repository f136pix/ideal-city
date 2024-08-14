package f136.pix.payment_service.infra._config.validation;

import f136.pix.payment_service.entity.users.gateway.IUserGateway;
import f136.pix.payment_service.entity.users.model.User;
import jakarta.validation.ConstraintValidator;
import jakarta.validation.ConstraintValidatorContext;
import org.springframework.beans.factory.annotation.Autowired;

import java.util.Optional;

public class UniqueUsernameValidator implements ConstraintValidator<UniqueUsername, String> {
    @Autowired
    private IUserGateway repository;

    @Override
    public boolean isValid(String name, ConstraintValidatorContext constraintValidatorContext) {
        if (repository != null) {
            Optional<User> usuario = repository.findByName(name);
            return usuario.isEmpty();
        }
        return true;
    }
} 
