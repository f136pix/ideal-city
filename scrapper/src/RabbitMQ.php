<?php

use PhpAmqpLib\Connection\AMQPStreamConnection;
use PhpAmqpLib\Message\AMQPMessage;

class RabbitMQ
{
    private $connection;
    private $channel;

    // ctor
    public function __construct()
    {
        $host = RABBITMQ_HOST;
        $port = RABBITMQ_PORT;
        $user = RABBITMQ_USER;
        $password = RABBITMQ_PASSWORD;

        $connection = new AMQPStreamConnection($host, $port, $user, $password);
        $this->channel = $connection->channel();

        $this->channel->queue_declare(RABBITMQ_QUEUE, false, false, false, false);
    }

    public function publishMessage(string $json_message, string $routing_key)
    {
        $amqpMessage = new AMQPMessage($json_message,
            ['content_type' => 'application/json', 'delivery_mode' => AMQPMessage::DELIVERY_MODE_PERSISTENT]);
        
        $this->channel->basic_publish($amqpMessage, RABBITMQ_QUEUE, $routing_key, true);
    }

    public function __destruct()
    {
        $this->channel->close();
        $this->connection->close();
    }
}
