<?php
//reads and proccess csv data =>
//search city with same name in weatherApi =>
//if necessary does the same when classifiyng the continents
define('PROJECT_ROOT', __DIR__);
define('CONFIG_ROOT', dirname(__DIR__));

require CONFIG_ROOT . '/vendor/autoload.php';

require 'Readers/CsvReader.php';
require 'Service/ProcessCsvService.php';


$service = new ProcessCsvService();
$csv = new CsvReader("../dataset/cost-of-living.csv");

$csv->processData([$service, 'processCityData']);



