# Interpolated Logging

**Extensions to Logging Libraries to write Log Messages using Interpolated Strings without losing Structured Property Names** 

Current Status:

Library | Status | NuGet Package
------------ | ------------- | -------------
[**Serilog**](https://github.com/Drizin/InterpolatedLogging/tree/main/src/InterpolatedLogging.Serilog) | Working | [NuGet](https://www.nuget.org/packages/InterpolatedLogging.Serilog/)
[**Microsoft.Extensions.Logging**](https://github.com/Drizin/InterpolatedLogging/tree/main/src/InterpolatedLogging.Microsoft.Extensions.Logging) | Working | [NuGet](https://www.nuget.org/packages/InterpolatedLogging.Microsoft.Extensions.Logging/)
NLog | Pending |
log4net | Pending |

Most logging libraries support **structured logging**:

```cs
logger.Info("User {UserName} created Order {OrderId} at {Date}, operation took {OperationElapsedTime}ms", 
    name, orderId, DateTime.Now, elapsedTime);
```

This means that our logs will get not only plain strings but also the structured data, allowing us to search for specific property values (e.g. search for `OrderId="123"` to trace some order, or search for `OperationElapsedTime>1000` to find slow operations).  

The problem with this approach is that it's easy to put the wrong number of parameters or wrong order of parameters (the parameters at the end are **positional**, they are not matched with the message by their names).

If you just use regular interpolated strings you lose the benefit of structured logging, since the logging library won't know the names of each property:

```cs
logger.Info($"User {UserName} created Order {OrderId} at {Date}, operation took {OperationElapsedTime}ms");
```

This library solves this problem by creating extensions to popular logging libraries which allow us to use string interpolation (easier to write) and yet set the name of the properties:

```cs
logger.InterpolatedInfo($"User {new { UserName = name }} created Order {new { OrderId = orderId}} at {new { Date = now }}, operation took {new { OperationElapsedTime = elapsedTime }}ms");
```

# Serilog Quickstart

1. Install the [NuGet package InterpolatedLogging.Serilog](https://www.nuget.org/packages/InterpolatedLogging.Serilog)
1. Start using like this:
```cs
using Serilog; // for easier use our extensions use the same namespace of Serilog
// ...

logger.InterpolatedInformation($"User {new { UserName = name }} created Order {new { OrderId = orderId}} at {new { Date = now }}, operation took {new { OperationElapsedTime = elapsedTime }}ms");
// there are also extensions for Debug, Verbose,  etc, and also the overloads which take an Exception

// in plain Serilog this would be equivalent of:
//logger.Information("User {UserName} created Order {OrderId} at {Date}, operation took {OperationElapsedTime}ms", name, orderId, DateTime.Now, elapsedTime);
```

In Serilog there's the `@` destructuring operator which makes a single property be stored with its internal structure (instead of just invoking `ToString()` and saving the serialized property). You can still use that operator by using the `@` outside of the interpolation:

```cs
var input = new { Latitude = 25, Longitude = 134 };
logger.Information($"Processed @{ new { SensorInput = input }} in { new { TimeMS = time}:000} ms.");
// in plain Serilog this would be equivalent of:
//logger.Information("Processed {@SensorInput} in {TimeMS:000}ms.", input, time);
```

## Raw strings

If you want to embed raw strings in your messages (don't want them to be saved as structured properties), you don't need to create an anonymous object and you can just use the **raw modifier**:

```cs
logger.InterpolatedInformation($"User {new { UserName = name }} logged as {role:raw}");
```

# Collaborate

This is a brand new project, and your contribution can help a lot.  

**Would you like to collaborate?**  

Please submit a pull request or if you prefer you can [create an issue](https://github.com/Drizin/InterpolatedLogging/issues) or [contact me](http://drizin.io/pages/Contact/) to discuss your idea.

## License
MIT License
