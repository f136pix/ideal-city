package f136.pix.payment_service.entity.users.exceptions;

import jakarta.persistence.EntityNotFoundException;

public class UserNotFoundException extends EntityNotFoundException {
   
    public UserNotFoundException() {
        super("No user with provided data was found!");
    }
}