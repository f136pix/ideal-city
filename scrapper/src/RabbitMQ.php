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

        $this->channel->exchange_declare(RABBITMQ_DEFAULT_EXCHANGE, "direct", false, true, false);
        $this->channel->queue_bind(RABBITMQ_QUEUE, RABBITMQ_DEFAULT_EXCHANGE, "city.create");
        $this->channel->queue_declare(RABBITMQ_QUEUE, false, false, false, false);
    }

    public function publishMessage(string $json_message, string $routing_key)
    {
        $this->HoldIfQeueIsFull();
        
        $amqpMessage = new AMQPMessage($json_message,
            ['content_type' => 'application/json', 'delivery_mode' => AMQPMessage::DELIVERY_MODE_PERSISTENT]);

        $wasPosted = $this->channel->basic_publish($amqpMessage, RABBITMQ_DEFAULT_EXCHANGE, $routing_key, true);
        if ($wasPosted == true) {
            echo "Published City :  $json_message" . PHP_EOL;
        }
    }

    private function HoldIfQeueIsFull()
    {
        $maxQueueSize = 100; // Set your maximum queue size
        $waitTime = 5; // Time to wait in seconds

        $queue = $this->channel->queue_declare(RABBITMQ_QUEUE, false, false, false, false);
        $messageCount = $queue[1];

        // Checking if the queue doesnt have too much messages
        while ($messageCount >= $maxQueueSize) {
            echo "Queue is full, waiting for messages to be consumed...\n";
            echo "Tehere are currently $messageCount messages.\n";
            sleep($waitTime);
            $queue = $this->channel->queue_declare(RABBITMQ_QUEUE, false, false, false, false);
            $messageCount = $queue[1];
        }

        return;
    }

    public function __destruct()
    {
        $this->channel->close();
        $this->connection->close();
    }
}
