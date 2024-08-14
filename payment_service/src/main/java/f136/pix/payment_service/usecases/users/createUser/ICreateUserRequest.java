package f136.pix.payment_service.usecases.users.createUser;

import org.springframework.scheduling.support.SimpleTriggerContext;

// DTO (Data Transfer Object) interface to invert spring boot lib dependencies,
// respecting the decoupling of the use cases layer.
public interface ICreateUserData {
    String username();
}
