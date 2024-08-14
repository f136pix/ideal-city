package f136.pix.payment_service.infra._config.db.repository;

import f136.pix.payment_service.infra._config.db.schema.UserSchema;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface UserRepository extends JpaRepository<UserSchema, Long> {
    Optional<UserSchema> findByName(String username);
}
