rmdir /s /q "%HOMEDRIVE%%HOMEPATH%\.nuget\packages\InterpolatedLogging.Microsoft.Extensions.Logging"
rmdir /s /q "%HOMEDRIVE%%HOMEPATH%\.nuget\packages\InterpolatedLogging.NLog"
rmdir /s /q "%HOMEDRIVE%%HOMEPATH%\.nuget\packages\InterpolatedLogging.Serilog"

if not exist packages-local mkdir packages-local

dotnet build -c release InterpolatedLogging.Microsoft.Extensions.Logging\InterpolatedLogging.Microsoft.Extensions.Logging.csproj
REM dotnet test  InterpolatedLogging.Microsoft.Extensions.Logging.Tests\InterpolatedLogging.Microsoft.Extensions.Logging.Tests.csproj
dotnet pack -c release InterpolatedLogging.Microsoft.Extensions.Logging\InterpolatedLogging.Microsoft.Extensions.Logging.csproj

dotnet build -c release InterpolatedLogging.NLog\InterpolatedLogging.NLog.csproj
REM dotnet test  InterpolatedLogging.NLog.Tests\InterpolatedLogging.NLog.Tests.csproj
dotnet pack -c release InterpolatedLogging.NLog\InterpolatedLogging.NLog.csproj

dotnet build -c release InterpolatedLogging.Serilog\InterpolatedLogging.Serilog.csproj
REM dotnet test  InterpolatedLogging.Serilog.Tests\InterpolatedLogging.Serilog.Tests.csproj
dotnet pack -c release InterpolatedLogging.Serilog\InterpolatedLogging.Serilog.csproj