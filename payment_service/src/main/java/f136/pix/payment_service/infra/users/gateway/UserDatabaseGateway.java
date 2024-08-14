package f136.pix.payment_service.infra.users.gateway;

import f136.pix.payment_service.entity.users.gateway.IUserGateway;
import f136.pix.payment_service.entity.users.model.User;
import f136.pix.payment_service.infra.users.db.UserRepository;
import f136.pix.payment_service.infra.users.db.UserSchema;

import java.util.Optional;

// Class does the conversion between the domain model user and the db schema user, and implements the dbs operations
public class UserDatabaseGateway implements IUserGateway {
    private final UserRepository userRepository;

    public UserDatabaseGateway(UserRepository userRepository) {
        this.userRepository = userRepository;
    }


    public Optional<User> findByName(String name) {
        return this.userRepository.findByName(name).map(UserSchema::toUser);
    }

    @Override
    public User create(User user) {
        return this.userRepository.save(new UserSchema(user)).toUser();
    }

    @Override
    public void delete(Long id) {
        this.userRepository.deleteById(id);
    }

    @Override
    public Optional<User> findById(Long id) {
        return Optional.empty();
    }
}
