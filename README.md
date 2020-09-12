# MalExporter
simple tool to import xml data and export it to csv or into an postgresql database

## Build
Visual Studio or dotnet build can be used to build the project.

## Dependencies
* CliFX
* Npgsql

## Usage
### CSV
```MALExporter.exe csv <filename> <tag> <csvfile>``` convertes the data in the ```<tag>``` blocks of the xml file into an equivalent csv file.

 If ```--force``` is added to the command a existing csv file will be overwritten. If ```-f``` is added a renaming scheme of the form ```xmlfield=csvfield,...``` can be added which rewrites the xml column ```xmlfield``` into ```csvfield``` while exporting.

### SQL
```MalExporter.exe sql <filename> <tag> <sqlconnectionstring> <tablename>``` convertes the data in the ```<tag>``` block of the xml file into an equivalent PostgreSQL table.

 The ```<filename>```and ```<tag>``` parameters work in the same way as in the csv part. 
 ```<sqlconnectionstring>``` has to be a a Npgsql-compliant connection string to a PostgreSQL database. ```<tablename>``` is the table which is created by MalExporter for the export.

 As in the CSV part ```--force``` can be added to the command to overwrite an existing PostgreSQL table. If ```-f``` is added a renaming scheme of the form ```xmlfield=sqlfield,...``` can be added which rewrites the xml column ```xmlfield``` into ```sqlfield``` while exporting.

## Examples
### First example
Let ```test.xml``` be the following XML file:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<root>
	<child>
		<test>first</test>
		<test2>second</test2>
	</child>
	<child>
		<test>third</test>
		<test2>forth</test2>
	</child>
</root>
```

```MalExporter.exe csv test.xml child test.csv``` will create the following CSV file:
```csv
test,test2
first,second
third,forth
```

### Second example
It is also possible to only export some of the columns. Let ```test2.xml``` be the following XML file:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<root>
	<child>
		<test>first</test>
		<test2>second</test2>
        <test3>test3</test3>
	</child>
	<child>
		<test>third</test>
		<test2>forth</test2>
        <test3>test3-2</test3>
	</child>
</root>
```

```MalExporter.exe csv test2.xml child test2.csv -f test,test3=rename3``` will create the following CSV file:
```csv
test,rename3
first,test3
third,test3-2
```

It can be seen that only the xml values for test and test3 were exported into the csv file and the name of the test3 column was renamed to rename3.