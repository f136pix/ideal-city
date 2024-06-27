<?php

use PhpAmqpLib\Message\AMQPMessage;

require PROJECT_ROOT . '/Models/CityData.php';
require PROJECT_ROOT . '/RabbitMQ.php';

class ProcessCsvService
{
    private CostOfLivingCsvReader $csvReader;
    private RabbitMQ $asyncQueue;

    public $greatest = 0;

    function __construct()
    {
        $this->asyncQueue = new RabbitMQ();
    }

    public function processCityData($record)
    {
        //        string $city, string $country, array $costOfLivingIndex, string $publicTransportation, string $gasoline, string $averageMonthlyNetSalary, $weather = null
        $costOfLivingIndex = array_slice($record, 3, 55);

        $city = new CityData($record[1], $record[2], $costOfLivingIndex, $record[31], $record[36], $record[57], $record[58]);

        echo "Processing city: " . $city->cityName . " " . $city->country . " " . $city->costOfLivingIndex . " " . PHP_EOL;

//        do on dotnet service
//        $response = $this->http->getRequest('/weather', ['city' => $city->cityName]); 
//        $weather = json_decode($response, true);
//        $city->weather = $weather['weather'];

        $this->postToAsyncQueue($city);

    }

    private function postToAsyncQueue(CityData $city)
    {
        if ($city->costOfLivingIndex == null) {
            echo "City cost of living index from $city->cityName $city->country is null, skipping" . PHP_EOL;
            return;
        }

       $json_message = json_encode($city);
        
//        $this->asyncQueue->publishMessage(json_encode($city), 'city.create');
        $this->asyncQueue->publishMessage($json_message, 'city.create');
        sleep(20);
    }
}