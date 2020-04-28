# Driver With Logger

This is a `1.7.2` version of the `Neo4j.Driver`. With an `IDriverLogger` implementation. 

## How to run

```
dotnet build
dotnet run 
```

## Known issues

The `ConsoleDriverLogger` has an issue in it's formatting which means certain messages are not formatted properly.