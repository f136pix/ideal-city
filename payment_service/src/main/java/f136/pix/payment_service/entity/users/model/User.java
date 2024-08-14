package f136.pix.payment_service.entity.users.model;

import f136.pix.payment_service.entity._common.models.AbstractEntity;

public class User extends AbstractEntity<Long> {

    private String name;

    public User(String username) {
        this.name = username;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}
