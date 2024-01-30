# Cron Parser

This app can be used to parse an input Cron string into a human readable format

For example, the following cron string: `*/15 0 1,15 * 1-5 /usr/bin/find` will result in:
```
minute 0 15 30 45
hour 0
day of month 1 15
month 1 2 3 4 5 6 7 8 9 10 11 12
day of week 1 2 3 4 5
command /usr/bin/find
```

## Requirements
The .Net 6 SDK is required to build and run this app.
It can be downloaded here: https://dotnet.microsoft.com/en-us/download/dotnet/6.0

## Building
Build the solution using `dotnet build`
Navigate to the root directory, open a command window, and enter: `dotnet build src/CronParser.Runner/CronParser.Runner.csproj`

## Running
### Windows
In your console window, from the root directory, navigate to `src/CronParser.Runner/bin/Debug/net6.0`

Now you can run the app via the command: `./CronParser.Runner.exe`

### Linux/MacOS
In your console window, from the root directory, navigate to `src/CronParser.Runner/bin/Debug/net6.0`

Now you can run the app via the command: `./CronParser.Runner`

## Project Structure
The solution is split into 2 source projects and 2 test projects.

The core parser logic is located in `CronParser.Engine`

A command line runner to use the engine is located in `CronParser.Runner`

There are 2 test projects, one for unit testing the engine (`CronParser.Engine.Tests`) with mocked dependencies, and one for integration tests of the engine, using real dependencies (`CronParser.Engine.IntegrationTests`)

Code has been documented where required, and SOLID principles have been obeyed to keep classes small and segregated.
