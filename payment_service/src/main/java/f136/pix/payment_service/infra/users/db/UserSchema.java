package f136.pix.payment_service.infra._config.db.schema;

import f136.pix.payment_service.entity.users.model.User;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.validation.constraints.NotNull;

@Entity
public class UserSchema extends AbstractEntitySchema<Long> {
    
    @NotNull
    @Column(nullable = false)
    private String name;

    public UserSchema() {

    }

    public UserSchema(User user) {
        super();
        this.name = user.getName();
    }


    public User toUser() {
        User user = new User(this.name);

        user.setId(this.getId());
        return user;
    }
}
