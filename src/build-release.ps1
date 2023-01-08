
$msbuild = ( 
	"$Env:programfiles\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\msbuild.exe",
	"$Env:programfiles\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\msbuild.exe",
	"$Env:programfiles\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\msbuild.exe",
    "$Env:programfiles (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\msbuild.exe",
    "$Env:programfiles (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\msbuild.exe",
    "$Env:programfiles (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\msbuild.exe",
    "$Env:programfiles (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\msbuild.exe",
    "$Env:programfiles (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\msbuild.exe",
    "$Env:programfiles (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\msbuild.exe",
    "$Env:programfiles (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\msbuild.exe",
    "${Env:ProgramFiles(x86)}\MSBuild\14.0\Bin\MSBuild.exe",
    "${Env:ProgramFiles(x86)}\MSBuild\13.0\Bin\MSBuild.exe",
    "${Env:ProgramFiles(x86)}\MSBuild\12.0\Bin\MSBuild.exe"
) | Where-Object { Test-Path $_ } | Select-Object -first 1



Remove-Item -Recurse -Force -ErrorAction Ignore ".\packages-local"
Remove-Item -Recurse -Force -ErrorAction Ignore "$env:HOMEDRIVE$env:HOMEPATH\.nuget\packages\InterpolatedLogging.Microsoft.Extensions.Logging"
Remove-Item -Recurse -Force -ErrorAction Ignore "$env:HOMEDRIVE$env:HOMEPATH\.nuget\packages\InterpolatedLogging.NLog"
Remove-Item -Recurse -Force -ErrorAction Ignore "$env:HOMEDRIVE$env:HOMEPATH\.nuget\packages\InterpolatedLogging.Serilog"

$configuration = "Release";


dotnet clean
& $msbuild ".\InterpolatedLogging.Microsoft.Extensions.Logging\InterpolatedLogging.Microsoft.Extensions.Logging.csproj" `
		   /t:Restore /t:Build /t:Pack                                                                                  `
		   /p:PackageOutputPath="..\..\packages-local\"                                                                 `
		   '/p:targetFrameworks="netstandard2.0;net472;net5.0"'                                                         `
		   /p:Configuration=$configuration                                                                              `
		   /p:IncludeSymbols=true                                                                                       `
		   /p:SymbolPackageFormat=snupkg                                                                                `
		   /verbosity:minimal                                                                                           `
		   /p:ContinuousIntegrationBuild=true

# dotnet test  InterpolatedLogging.Microsoft.Extensions.Logging.Tests\InterpolatedLogging.Microsoft.Extensions.Logging.Tests.csproj

dotnet clean
& $msbuild ".\InterpolatedLogging.NLog\InterpolatedLogging.NLog.csproj" `
		   /t:Restore /t:Build /t:Pack                                  `
		   /p:PackageOutputPath="..\..\packages-local\"                 `
		   '/p:targetFrameworks="netstandard2.0;net472;net5.0"'         `
		   /p:Configuration=$configuration                              `
		   /p:IncludeSymbols=true                                       `
		   /p:SymbolPackageFormat=snupkg                                `
		   /verbosity:minimal                                           `
		   /p:ContinuousIntegrationBuild=true
# dotnet test  InterpolatedLogging.NLog.Tests\InterpolatedLogging.NLog.Tests.csproj

dotnet clean
& $msbuild ".\InterpolatedLogging.Serilog\InterpolatedLogging.Serilog.csproj"  `
		   /t:Restore /t:Build /t:Pack                                         `
		   /p:PackageOutputPath="..\..\packages-local\"                        `
		   '/p:targetFrameworks="netstandard2.0;net472;net5.0"'                `
		   /p:Configuration=$configuration                                     `
		   /p:IncludeSymbols=true                                              `
		   /p:SymbolPackageFormat=snupkg                                       `
		   /verbosity:minimal                                                  `
		   /p:ContinuousIntegrationBuild=true
# dotnet test  InterpolatedLogging.Serilog.Tests\InterpolatedLogging.Serilog.Tests.csproj
