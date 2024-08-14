package f136.pix.payment_service.entity.users.gateway;

import f136.pix.payment_service.entity.users.model.User;

import java.util.Optional;

public interface UserGateway {
    User create(User user);
    void delete(Long id);
    Optional<User> findById(Long id);
}
