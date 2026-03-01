# AGDevX.NET

A foundational C# NuGet package — extension methods, utilities, and helpers.

## Project-Specific Conventions

### This is a library, not an application
- No Api, Services, Auth, Data, or Shared projects — just the single class library + test project
- The standard AGDevX.{SolutionName} multi-project structure does not apply here

### Primary constructors are not applicable
- All classes are static extension method containers or exception types
- Neither pattern benefits from primary constructors

### XML documentation is required on all public members
- This overrides the global "minimal/avoid" XML doc preference
- As a public NuGet package, consumers rely on IntelliSense documentation
- Every public method, class, enum, and property must have XML docs

### Zero runtime dependencies
- This library has no runtime NuGet dependencies — consumers get no transitive packages
- Build-time tooling (SourceLink, analyzers) is acceptable via `PrivateAssets="All"`
- Keep runtime dependencies at zero unless there's a compelling reason to add one
