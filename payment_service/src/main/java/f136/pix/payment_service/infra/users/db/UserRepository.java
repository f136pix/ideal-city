package f136.pix.payment_service.infra.users.db;

import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface UserRepository extends JpaRepository<UserSchema, Long> {
    Optional<UserSchema> findByName(String username);
}
