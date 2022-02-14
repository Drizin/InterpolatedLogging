rmdir /s /q "%HOMEDRIVE%%HOMEPATH%\.nuget\packages\InterpolatedLogging.Microsoft.Extensions.Logging"
rmdir /s /q "%HOMEDRIVE%%HOMEPATH%\.nuget\packages\InterpolatedLogging.NLog"
rmdir /s /q "%HOMEDRIVE%%HOMEPATH%\.nuget\packages\InterpolatedLogging.Serilog"

if not exist packages-local mkdir packages-local

dotnet clean
dotnet build -c release InterpolatedLogging.Microsoft.Extensions.Logging\InterpolatedLogging.Microsoft.Extensions.Logging.csproj
REM dotnet test  InterpolatedLogging.Microsoft.Extensions.Logging.Tests\InterpolatedLogging.Microsoft.Extensions.Logging.Tests.csproj
REM Looks like deterministic build works randomly (fails most of the times). Looks like msbuild works better?
REM dotnet pack InterpolatedLogging.Microsoft.Extensions.Logging\InterpolatedLogging.Microsoft.Extensions.Logging.csproj -c release -o .\packages-local\ /p:ContinuousIntegrationBuild=true -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe" /t:Pack InterpolatedLogging.Microsoft.Extensions.Logging\ /p:targetFrameworks="netstandard2.0;net472" /p:Configuration=Release /p:IncludeSymbols=true /p:SymbolPackageFormat=snupkg /p:PackageOutputPath=..\packages-local\ /verbosity:minimal /p:ContinuousIntegrationBuild=true

dotnet clean
dotnet build -c release InterpolatedLogging.NLog\InterpolatedLogging.NLog.csproj
REM dotnet test  InterpolatedLogging.NLog.Tests\InterpolatedLogging.NLog.Tests.csproj
REM Looks like deterministic build works randomly (fails most of the times). Looks like msbuild works better?
REM dotnet pack InterpolatedLogging.NLog\InterpolatedLogging.NLog.csproj                                                 -c release -o .\packages-local\ /p:ContinuousIntegrationBuild=true /p:IncludeSymbols=true /p:SymbolPackageFormat=snupkg
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe" /t:Pack InterpolatedLogging.NLog\                         /p:targetFrameworks="netstandard2.0;net472" /p:Configuration=Release /p:IncludeSymbols=true /p:SymbolPackageFormat=snupkg /p:PackageOutputPath=..\packages-local\ /verbosity:minimal /p:ContinuousIntegrationBuild=true

dotnet clean
dotnet build -c release InterpolatedLogging.Serilog\InterpolatedLogging.Serilog.csproj
REM dotnet test  InterpolatedLogging.Serilog.Tests\InterpolatedLogging.Serilog.Tests.csproj
REM Looks like deterministic build works randomly (fails most of the times). Looks like msbuild works better?
REM dotnet pack InterpolatedLogging.Serilog\InterpolatedLogging.Serilog.csproj                                           -c release -o .\packages-local\ /p:ContinuousIntegrationBuild=true /p:IncludeSymbols=true /p:SymbolPackageFormat=snupkg
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe" /t:Pack InterpolatedLogging.Serilog\                      /p:targetFrameworks="netstandard2.0;net472" /p:Configuration=Release /p:IncludeSymbols=true /p:SymbolPackageFormat=snupkg /p:PackageOutputPath=..\packages-local\ /verbosity:minimal /p:ContinuousIntegrationBuild=true
