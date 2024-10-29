package f136.pix.payment_service.infra.health;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

@RestController
public class CheckHealthController {
    
    public CheckHealthController() {
    }

    @GetMapping("/health")
    @ResponseStatus(HttpStatus.OK)
    public String Check() {
        return "App OK";
    }
}


