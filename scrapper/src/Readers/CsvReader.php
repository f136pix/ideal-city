<?php

class CsvReader
{
    protected $file;
    protected $delimiter;
    protected $records;

    public function __construct($filename, $delimiter = ',')
    {
        if (!file_exists($filename) || !is_readable($filename)) {
            throw new Exception("File not readable.");
        }

        $this->file = fopen($filename, 'r');
        $this->delimiter = $delimiter;
        $this->records = $this->getRecords();
    }

    public function getRecords()
    {
        $result = [];
        while (($line = fgetcsv($this->file, 1000, $this->delimiter)) !== FALSE) {
            $result[] = $line;
        }
        fclose($this->file);
        return $result;
    }

    public function processData(callable $callback)
    {
        
        foreach ($this->records as $record) {
            $callback($record);
        }
    }
}