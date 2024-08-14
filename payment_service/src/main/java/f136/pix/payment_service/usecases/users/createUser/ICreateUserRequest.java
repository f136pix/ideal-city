package f136.pix.payment_service.usecases.users.createUser;

// DTO (Data Transfer Object) interface to invert spring boot lib dependencies,
// respecting the decoupling of the use cases layer.
public interface ICreateUserRequest {
    String username();
}
