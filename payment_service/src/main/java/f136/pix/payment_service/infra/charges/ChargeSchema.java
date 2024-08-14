package f136.pix.payment_service.infra.charges;

import jakarta.persistence.Entity;
import jakarta.persistence.Id;

import java.util.UUID;

@Entity
public class ChargeSchema {
    public ChargeSchema() {

    }
    
    // This constructor is used to convert a domain object to a schema object
    ChargeSchema(f136.pix.payment_service.usecases.charges.Charge charge) {
    }
    
    @Id
    private UUID Id;
    
}
