<?php

use PhpAmqpLib\Connection\AMQPStreamConnection;

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

    public function publishMessage($message, string $routing_key)
    {
        $this->channel->basic_publish($message, RABBITMQ_QUEUE, $routing_key, true );
    }

    public function __destruct()
    {
        $this->channel->close();
        $this->connection->close();
    }
}
