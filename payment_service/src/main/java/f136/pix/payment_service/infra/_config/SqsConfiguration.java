package f136.pix.payment_service.infra._config;

import com.amazonaws.regions.Regions;
import com.amazonaws.services.sqs.AmazonSQSAsync;
import com.amazonaws.services.sqs.AmazonSQSAsyncClientBuilder;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class SqsConfiguration {
    @Bean
    public AmazonSQSAsync amazonSQSClient() {
        return AmazonSQSAsyncClientBuilder.standard()
                .withRegion(Regions.SA_EAST_1)
                .build();
    }
}
