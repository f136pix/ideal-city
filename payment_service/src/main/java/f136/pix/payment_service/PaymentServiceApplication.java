package f136.pix.payment_service;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class PaymentServiceApplication {

    public static void main(String[] args) {
        System.out.println("Payment Service is running...");
        SpringApplication.run(PaymentServiceApplication.class, args);
    }
}
