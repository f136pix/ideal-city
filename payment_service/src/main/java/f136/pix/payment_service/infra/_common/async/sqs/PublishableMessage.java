package f136.pix.payment_service.infra._common.async.sqs;

import com.fasterxml.jackson.annotation.JsonProperty;
import lombok.Data;

@Data
public class PublishableMessage {
    @JsonProperty("Id")
    private String Id;

    @JsonProperty("EventType")
    private String EventType;

    @JsonProperty("Data")
    private Object Data;
}