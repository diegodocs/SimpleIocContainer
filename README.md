# Simple IoC Container - .NET9 

This is a community asset to accelerate understanding and development Inversion of Control Pattern (IoC) principle.

## Summary

This project covers concepts about:  

- [Dependency Injection](http://en.wikipedia.org/wiki/Dependency_injection)
- [SOLID Principles](http://en.wikipedia.org/wiki/SOLID_%28object-oriented_design%29)

## .Net Version

- [.NET 9.0](https://dotnet.microsoft.com/en-us/download)

## 3rd Party NuGet Packages

- XUnit

## Development Tools

- Visual Studio Code
- GIT Bash
- GitHub(Repos, Actions)

## How to use it

```Powershell
var builder = new ContainerBuilder();
builder.Register<IFileHandler, MockFileHandler>();
var container = builder.Build();
var fileHandler = container.Resolve<IFileHandler>();
```

## Restore, Build and Test

```Powershell
dotnet restore
dotnet build
dotnet test

```

## You shouldn't find

- Binaries committed to source control.
- Unnecessary project or library references or third party frameworks.
- Many "try" blocks - code defensively and throw exceptions if something is wrong.
- Third party APIs exposed via public interfaces.
