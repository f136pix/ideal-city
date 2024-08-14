package f136.pix.payment_service.usecases.charges.createCharge;

import f136.pix.payment_service.usecases._common.interfaces.IChargeRepository;
import f136.pix.payment_service.usecases.charges.Charge;
import f136.pix.payment_service.contracts.charges.createCharge.CreateChargeRequest;

public class CreateChargeInteractor {
    final IChargeRepository chargeRepository;

    public CreateChargeInteractor(IChargeRepository chargeRepository) {
        this.chargeRepository = chargeRepository;
    }

    public Charge createCharge(CreateChargeRequest request) {
        
        // Perform domain validation in Charge object
        Charge charge = new Charge();
        
        chargeRepository.save(charge);
        
        return charge;
    }
}
