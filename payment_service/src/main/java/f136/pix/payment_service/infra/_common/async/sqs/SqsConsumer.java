package f136.pix.payment_service.infra._common.async.sqs;

import com.amazonaws.handlers.AsyncHandler;
import com.amazonaws.services.sqs.AmazonSQSAsync;
import com.amazonaws.services.sqs.model.Message;
import com.amazonaws.services.sqs.model.ReceiveMessageRequest;
import com.amazonaws.services.sqs.model.ReceiveMessageResult;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Service;

import java.io.Console;

@Service
public class SqsConsumer {

    @Value("${aws.queueName}")
    private String queueName;
    private String queueUrl;
    private final AmazonSQSAsync amazonSQSClient;

    public SqsConsumer(AmazonSQSAsync amazonSQSClient) {
        this.amazonSQSClient = amazonSQSClient;
    }

    @Scheduled(fixedDelay = 5000) // It runs every 5 seconds.
    public void consumeMessages() {
        try {
            amazonSQSClient.receiveMessageAsync(new ReceiveMessageRequest(getQueueUrl()), new AsyncHandler<ReceiveMessageRequest, ReceiveMessageResult>() {
                @Override
                public void onError(Exception exception) {
                    System.out.println("Error: " + exception.getMessage());
                }

                @Override
                public void onSuccess(ReceiveMessageRequest request, ReceiveMessageResult receiveMessageResult) {
                    if (!receiveMessageResult.getMessages().isEmpty()) {
                        Message message = receiveMessageResult.getMessages().get(0);
                        System.out.print("Message: " + message.getBody());

                        ObjectMapper objectMapper = new ObjectMapper();

                        try {
                            PublishableMessage publishableMessage = objectMapper.readValue(message.getBody(), PublishableMessage.class);
                            System.out.println("Processing message " + publishableMessage.getId());
                            amazonSQSClient.deleteMessage(getQueueUrl(), message.getReceiptHandle());
                        } catch (Exception e) {
                            System.out.println("Error deserializing message: " + e.getMessage());
                        }

//                        amazonSQSClient.deleteMessage(queueUrl, message.getReceiptHandle());
                    }
                }
            });

        } catch (Exception e) {
            System.out.println("Error: " + e.getMessage());
        }
    }
    
    private String getQueueUrl () {
        if (this.queueUrl == null) {
            this.queueUrl = amazonSQSClient.getQueueUrl(this.queueName).getQueueUrl();
        }
        return this.queueUrl;
    }
}
