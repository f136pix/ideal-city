<?php
require CONFIG_ROOT . '/config.php';

# x54 - average monthly net salary
# x28 - public transportation
# x33 - gasoline
#x1 - x53 - general cost of living
class CityData
{
    public $cityName;
    public $country;
    public $costOfLivingIndex;
    public $publicTransportation;
    public $gasoline;
    public $averageMonthlyNetSalary;
    public $weather;


    public function __construct(string $city, string $country, array $costOfLivingIndex, string $publicTransportation, string $gasoline, string $averageMonthlyNetSalary, string $dataQuality, $weather = null)
    {
        $this->cityName = $city;
        $this->country = $country;
        $costOfLivingIndex = array_filter($costOfLivingIndex);
        $arrSize = count($costOfLivingIndex);
        if ($arrSize > 0 && $dataQuality == "1") {
            $this->costOfLivingIndex = $this->transform(array_sum($costOfLivingIndex) / $arrSize);
        } else {
            $this->costOfLivingIndex = null;
        }
        $this->publicTransportation = $publicTransportation;
        $this->gasoline = $gasoline;
        $this->averageMonthlyNetSalary = $averageMonthlyNetSalary;
        $this->weather = $weather;
    }

    private function transform($x)
    {
        # highest cost of living in dataset is 5134.8294545455, so it is 1
        return ($x / HIGHEST_COST_OF_LIVING);
    }

}

# Oslo Norway 19.588754716981

# Rio Verde Brazil 9.6023078431373