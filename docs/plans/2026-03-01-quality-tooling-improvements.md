# Quality & Tooling Improvements Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Close testing gaps, improve API design (IReadOnlyList), complete XML documentation, add build tooling (SourceLink, global.json, Directory.Build.props, PublicApiAnalyzers), enhance CI (coverage), and add README badges.

**Architecture:** Single branch `ag/quality-tooling-improvements` with logical commits. Tasks are ordered to avoid conflicts: build tooling first, then code changes, then tests, then docs/badges last.

**Tech Stack:** C# / .NET 10 + .NET 8 / xUnit / GitHub Actions

---

## Task 1: Add `Directory.Build.props` and update csprojs

Extract shared properties from both project files into a solution-level `Directory.Build.props`.

**Files:**
- Create: `Directory.Build.props`
- Modify: `AGDevX/AGDevX.csproj`
- Modify: `AGDevX.Tests/AGDevX.Tests.csproj`

**Step 1: Create `Directory.Build.props`**

```xml
<Project>

  <PropertyGroup>
    <TargetFrameworks>net8.0;net10.0</TargetFrameworks>
    <ImplicitUsings>disable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```

**Step 2: Remove extracted properties from `AGDevX/AGDevX.csproj`**

Remove these three lines (they're now in Directory.Build.props):
```xml
<TargetFrameworks>net8.0;net10.0</TargetFrameworks>
<ImplicitUsings>disable</ImplicitUsings>
<Nullable>enable</Nullable>
```

**Step 3: Remove extracted properties from `AGDevX.Tests/AGDevX.Tests.csproj`**

Remove the same three lines.

**Step 4: Build and run tests**

Run: `dotnet build AGDevX.slnx && dotnet test --verbosity normal`
Expected: Clean build, all tests pass on both TFMs

**Step 5: Commit**

```
chore: extract shared properties to Directory.Build.props
```

---

## Task 2: Add `global.json`

Pin the SDK version to prevent drift across contributors and CI.

**Files:**
- Create: `global.json`

**Step 1: Create `global.json`**

```json
{
  "sdk": {
    "version": "10.0.103",
    "rollForward": "latestPatch"
  }
}
```

**Step 2: Build to verify SDK resolves**

Run: `dotnet --version && dotnet build AGDevX.slnx`
Expected: SDK 10.0.103 (or later patch), clean build

**Step 3: Commit**

```
chore: add global.json to pin SDK version
```

---

## Task 3: Add SourceLink

Enable source debugging for NuGet consumers.

**Files:**
- Modify: `AGDevX/AGDevX.csproj`

**Step 1: Add SourceLink package reference to `AGDevX/AGDevX.csproj`**

Add to the existing `<ItemGroup>` (or create a new one above the `None Include` items):
```xml
<ItemGroup>
  <PackageReference Include="Microsoft.SourceLink.GitHub" Version="10.0.103" PrivateAssets="All" />
</ItemGroup>
```

**Step 2: Build to verify**

Run: `dotnet build AGDevX.slnx -c Release`
Expected: Clean build

**Step 3: Commit**

```
chore: add SourceLink for NuGet source debugging
```

---

## Task 4: Add PublicApiAnalyzers

Track public API surface to prevent accidental breaking changes.

**Files:**
- Modify: `AGDevX/AGDevX.csproj`
- Create: `AGDevX/PublicAPI.Shipped.txt`
- Create: `AGDevX/PublicAPI.Unshipped.txt`

**Step 1: Add PublicApiAnalyzers package reference to `AGDevX/AGDevX.csproj`**

Add to the same `<ItemGroup>` as SourceLink:
```xml
<PackageReference Include="Microsoft.CodeAnalysis.PublicApiAnalyzers" Version="3.3.4" PrivateAssets="All" />
```

**Step 2: Create empty `PublicAPI.Shipped.txt`**

```
#nullable enable
```

(Just the nullable directive — nothing shipped yet.)

**Step 3: Build to get the full list of unshipped API diagnostics**

Run: `dotnet build AGDevX/AGDevX.csproj -c Release 2>&1`

The build will produce RS0016 warnings for every public symbol. Collect these.

**Step 4: Create `PublicAPI.Unshipped.txt` with all current public API**

Populate from the RS0016 warnings. The file should contain every public type, method, property, constructor, and enum member. Format example:
```
#nullable enable
AGDevX.Assemblies.AssemblyExtensions
static AGDevX.Assemblies.AssemblyExtensions.FullNameStartsWithPrefix(this System.Reflection.Assembly! assembly, string? prefix) -> bool
...
```

**Step 5: Build to verify no RS0016 warnings remain**

Run: `dotnet build AGDevX/AGDevX.csproj -c Release`
Expected: Clean build

**Step 6: Commit**

```
chore: add PublicApiAnalyzers to track public API surface
```

---

## Task 5: Update CLAUDE.md dependency convention

**Files:**
- Modify: `.claude/CLAUDE.md`

**Step 1: Update the "Zero external dependencies" section**

Change:
```markdown
### Zero external dependencies
- This library depends only on the .NET runtime — no NuGet packages
- Keep it that way unless there's a compelling reason to add one
```

To:
```markdown
### Zero runtime dependencies
- This library has no runtime NuGet dependencies — consumers get no transitive packages
- Build-time tooling (SourceLink, analyzers) is acceptable via `PrivateAssets="All"`
- Keep runtime dependencies at zero unless there's a compelling reason to add one
```

**Step 2: Commit**

```
docs: update dependency convention to distinguish runtime from build-time
```

---

## Task 6: Change collection return types to `IReadOnlyList<string>`

**Files:**
- Modify: `AGDevX/Security/ClaimsPrincipalExtensions.cs:67,76,776,785,819,828`

**Step 1: Update `GetAudiences` return type (line 67)**

Change:
```csharp
    public static List<string> GetAudiences(this ClaimsPrincipal claimsPrincipal)
```
To:
```csharp
    public static IReadOnlyList<string> GetAudiences(this ClaimsPrincipal claimsPrincipal)
```

**Step 2: Update `TryGetAudiences` return type (line 76)**

Change:
```csharp
    public static List<string>? TryGetAudiences(this ClaimsPrincipal claimsPrincipal)
```
To:
```csharp
    public static IReadOnlyList<string>? TryGetAudiences(this ClaimsPrincipal claimsPrincipal)
```

**Step 3: Update `GetScopes` return type (line 776)**

Change:
```csharp
    public static List<string> GetScopes(this ClaimsPrincipal claimsPrincipal)
```
To:
```csharp
    public static IReadOnlyList<string> GetScopes(this ClaimsPrincipal claimsPrincipal)
```

**Step 4: Update `TryGetScopes` return type (line 785)**

Change:
```csharp
    public static List<string>? TryGetScopes(this ClaimsPrincipal claimsPrincipal)
```
To:
```csharp
    public static IReadOnlyList<string>? TryGetScopes(this ClaimsPrincipal claimsPrincipal)
```

Also update the implementation to return an array (since `.ToList()` returns `List<T>` which implements `IReadOnlyList<T>`, this actually already works — but for Scopes and Roles, the `.Split(' ').ToList()` can stay as-is since `List<T>` implements `IReadOnlyList<T>`).

**Step 5: Update `GetRoles` return type (line 819)**

Change:
```csharp
    public static List<string> GetRoles(this ClaimsPrincipal claimsPrincipal)
```
To:
```csharp
    public static IReadOnlyList<string> GetRoles(this ClaimsPrincipal claimsPrincipal)
```

**Step 6: Update `TryGetRoles` return type (line 828)**

Change:
```csharp
    public static List<string>? TryGetRoles(this ClaimsPrincipal claimsPrincipal)
```
To:
```csharp
    public static IReadOnlyList<string>? TryGetRoles(this ClaimsPrincipal claimsPrincipal)
```

**Step 7: Update XML docs for these 6 methods**

Change `as a list` to `as a read-only list` in all 6 XML doc summaries.

**Step 8: Add `using System.Collections.Generic;` if not already present**

Check the using statements at the top of the file — it's already there.

**Step 9: Update test assertions**

In `ClaimsPrincipalExtensionsGetTests.cs` and `ClaimsPrincipalExtensionsTryGetTests.cs`, the `.Count` property is available on `IReadOnlyList<T>` so assertions like `Assert.Equal(2, result.Count)` will still compile. No test changes needed.

**Step 10: Build and run tests**

Run: `dotnet build AGDevX.slnx && dotnet test --verbosity normal`
Expected: Clean build, all tests pass

**Step 11: Commit**

```
feat: return IReadOnlyList<string> from collection claim methods

GetAudiences, GetScopes, GetRoles and their TryGet variants now return
IReadOnlyList<string> instead of List<string>. Breaking change acceptable
in alpha — prevents consumers from mutating returned collections.
```

---

## Task 7: Add `CodedArgumentNullExceptionTests.cs`

**Files:**
- Create: `AGDevX.Tests/Exceptions/CodedArgumentNullExceptionTests.cs`

**Step 1: Write the test file**

Follow the exact pattern from `CodedApplicationExceptionTests.cs`, but adapted for `CodedArgumentNullException` (which takes `argumentName` instead of `message` in its first string parameter):

```csharp
using System;
using AGDevX.Exceptions;
using Xunit;

namespace AGDevX.Tests.Exceptions;

public sealed class CodedArgumentNullExceptionTests
{
    public class When_throwing_a_CodedArgumentNullException
    {
        [Fact]
        public void And_has_correct_default_code_then_assert_true()
        {
            //-- Arrange
            var defaultCode = "CODED_ARGUMENT_NULL_EXCEPTION";

            //-- Assert
            Assert.Equal(defaultCode, new CodedArgumentNullException().Code);
        }

        [Fact]
        public void And_has_correct_code_then_assert_true()
        {
            //-- Arrange
            var code = "ex";

            //-- Assert
            Assert.Equal(code, new CodedArgumentNullException("arg", code).Code);
        }

        [Fact]
        public void And_has_correct_argument_name_then_assert_true()
        {
            //-- Arrange
            var argumentName = "testArgument";

            //-- Assert
            Assert.Equal(argumentName, new CodedArgumentNullException(argumentName).ParamName);
        }

        [Fact]
        public void And_should_have_inner_exception_then_make_sure_it_has_inner_exception()
        {
            //-- Arrange
            var message = "Test message";
            var innerExceptionMessage = "Inner exception message";
            var innerException = new Exception(innerExceptionMessage);

            //-- Assert
            Assert.Equal(message, new CodedArgumentNullException(message, innerException).Message);
            Assert.Same(innerException, new CodedArgumentNullException(message, innerException).InnerException);
        }

        [Fact]
        public void And_should_have_inner_exception_then_make_all_properties_are_correct()
        {
            //-- Arrange
            var argumentName = "testArgument";
            var code = "ex";
            var innerExceptionMessage = "Inner exception message";
            var innerException = new Exception(innerExceptionMessage);

            //-- Assert
            Assert.Equal(argumentName, new CodedArgumentNullException(argumentName, code, innerException).ParamName);
            Assert.Equal(code, new CodedArgumentNullException(argumentName, code, innerException).Code);
            Assert.Same(innerException, new CodedArgumentNullException(argumentName, code, innerException).InnerException);
        }
    }
}
```

Note: `CodedArgumentNullException` inherits from `ArgumentNullException`, so the first string parameter is `argumentName` (accessible via `.ParamName`), not `message`. The `.Message` property prepends "Value cannot be null." to the parameter name. The 2-string-arg constructor is `(argumentName, code)`, not `(message, code)`.

**Step 2: Run tests**

Run: `dotnet test AGDevX.Tests --filter "FullyQualifiedName~CodedArgumentNullExceptionTests" --verbosity normal`
Expected: 5 tests pass

**Step 3: Commit**

```
test: add CodedArgumentNullException unit tests
```

---

## Task 8: Add enum string value tests

**Files:**
- Create: `AGDevX.Tests/Security/Auth0ClaimTypeTests.cs`
- Create: `AGDevX.Tests/Security/CustomClaimTypeTests.cs`
- Create: `AGDevX.Tests/Security/JwtClaimTypeTests.cs`

**Step 1: Create `Auth0ClaimTypeTests.cs`**

```csharp
using AGDevX.Enums;
using AGDevX.Security;
using Xunit;

namespace AGDevX.Tests.Security;

public sealed class Auth0ClaimTypeTests
{
    public class When_calling_StringValue
    {
        [Theory]
        [InlineData(Auth0ClaimType.GrantType, "gty")]
        public void Then_return_expected_string_value(Auth0ClaimType claimType, string expected)
        {
            //-- Act
            var result = claimType.StringValue();

            //-- Assert
            Assert.Equal(expected, result);
        }
    }
}
```

**Step 2: Create `CustomClaimTypeTests.cs`**

```csharp
using AGDevX.Enums;
using AGDevX.Security;
using Xunit;

namespace AGDevX.Tests.Security;

public sealed class CustomClaimTypeTests
{
    public class When_calling_StringValue
    {
        [Theory]
        [InlineData(CustomClaimType.RequestIp, "request_ip")]
        [InlineData(CustomClaimType.AppMetadata, "app_metadata")]
        [InlineData(CustomClaimType.CreatedAt, "created_at")]
        [InlineData(CustomClaimType.UserId, "user_id")]
        [InlineData(CustomClaimType.IsActive, "isActive")]
        public void Then_return_expected_string_value(CustomClaimType claimType, string expected)
        {
            //-- Act
            var result = claimType.StringValue();

            //-- Assert
            Assert.Equal(expected, result);
        }
    }
}
```

**Step 3: Create `JwtClaimTypeTests.cs`**

```csharp
using AGDevX.Enums;
using AGDevX.Security;
using Xunit;

namespace AGDevX.Tests.Security;

public sealed class JwtClaimTypeTests
{
    public class When_calling_StringValue
    {
        [Theory]
        [InlineData(JwtClaimType.Issuer, "iss")]
        [InlineData(JwtClaimType.Subject, "sub")]
        [InlineData(JwtClaimType.Audience, "aud")]
        [InlineData(JwtClaimType.Expiration, "exp")]
        [InlineData(JwtClaimType.NotBefore, "nbf")]
        [InlineData(JwtClaimType.IssuedAt, "iat")]
        [InlineData(JwtClaimType.JwtId, "jti")]
        [InlineData(JwtClaimType.Name, "name")]
        [InlineData(JwtClaimType.GivenName, "given_name")]
        [InlineData(JwtClaimType.FamilyName, "family_name")]
        [InlineData(JwtClaimType.MiddleName, "middle_name")]
        [InlineData(JwtClaimType.Nickname, "nickname")]
        [InlineData(JwtClaimType.PreferredUsername, "preferred_username")]
        [InlineData(JwtClaimType.Profile, "profile")]
        [InlineData(JwtClaimType.Picture, "picture")]
        [InlineData(JwtClaimType.Website, "website")]
        [InlineData(JwtClaimType.Email, "email")]
        [InlineData(JwtClaimType.EmailVerified, "email_verified")]
        [InlineData(JwtClaimType.Gender, "gender")]
        [InlineData(JwtClaimType.Birthdate, "birthdate")]
        [InlineData(JwtClaimType.ZoneInfo, "zoneinfo")]
        [InlineData(JwtClaimType.Locale, "locale")]
        [InlineData(JwtClaimType.PhoneNumber, "phone_number")]
        [InlineData(JwtClaimType.PhoneNumberVerified, "phone_number_verified")]
        [InlineData(JwtClaimType.Address, "address")]
        [InlineData(JwtClaimType.UpdatedAt, "updated_at")]
        [InlineData(JwtClaimType.AuthorizedParty, "azp")]
        [InlineData(JwtClaimType.Nonce, "nonce")]
        [InlineData(JwtClaimType.AuthTime, "auth_time")]
        [InlineData(JwtClaimType.AccessTokenHash, "at_hash")]
        [InlineData(JwtClaimType.CodeHash, "c_hash")]
        [InlineData(JwtClaimType.AuthenticationContextClassReference, "acr")]
        [InlineData(JwtClaimType.AuthenticationMethodsReference, "amr")]
        [InlineData(JwtClaimType.SessionId, "sid")]
        [InlineData(JwtClaimType.Scope, "scope")]
        [InlineData(JwtClaimType.ClientId, "client_id")]
        [InlineData(JwtClaimType.Roles, "roles")]
        public void Then_return_expected_string_value(JwtClaimType claimType, string expected)
        {
            //-- Act
            var result = claimType.StringValue();

            //-- Assert
            Assert.Equal(expected, result);
        }
    }
}
```

**Step 4: Run tests**

Run: `dotnet test AGDevX.Tests --filter "FullyQualifiedName~ClaimTypeTests" --verbosity normal`
Expected: 43 tests pass (1 + 5 + 37)

**Step 5: Commit**

```
test: add dedicated enum string value tests for claim types
```

---

## Task 9: Add `GetExceptionDetail` test for plain `Exception` overload

**Files:**
- Modify: `AGDevX.Tests/Exceptions/ExceptionDetailTests.cs`

**Step 1: Add a new nested test class for the plain Exception overload**

Add inside `When_calling_GetExceptionDetail`:

```csharp
public class And_called_on_a_plain_Exception
{
    [Fact]
    public void Then_return_exception_detail_with_provided_code()
    {
        //-- Arrange
        var code = "CUSTOM_CODE";
        var message = "Something went wrong";
        var exception = new InvalidOperationException(message);

        //-- Act
        ExceptionDetail exceptionDetail;
        try
        {
            throw exception;
        }
        catch (Exception ex)
        {
            exceptionDetail = ex.GetExceptionDetail(code);
        }

        //-- Assert
        Assert.Equal(code, exceptionDetail.Code);
        Assert.Equal(message, exceptionDetail.Message);
        Assert.NotNull(exceptionDetail.StackFrames);
        Assert.Null(exceptionDetail.InnerException);
    }
}
```

**Step 2: Run tests**

Run: `dotnet test AGDevX.Tests --filter "FullyQualifiedName~ExceptionDetailTests" --verbosity normal`
Expected: 5 tests pass (4 existing + 1 new)

**Step 3: Commit**

```
test: add GetExceptionDetail test for plain Exception overload
```

---

## Task 10: Add XML docs to all undocumented public members

**Files:**
- Modify: `AGDevX/Exceptions/CodedApplicationException.cs`
- Modify: `AGDevX/Exceptions/CodedArgumentNullException.cs`
- Modify: `AGDevX/Exceptions/AcquireTokenException.cs`
- Modify: `AGDevX/Exceptions/ApplicationStartupException.cs`
- Modify: `AGDevX/Exceptions/ClaimNotFoundException.cs`
- Modify: `AGDevX/Exceptions/ExtensionMethodException.cs`
- Modify: `AGDevX/Exceptions/ExtensionMethodParameterNullException.cs`
- Modify: `AGDevX/Exceptions/MissingRequiredClaimException.cs`
- Modify: `AGDevX/Exceptions/NotAuthorizedException.cs`
- Modify: `AGDevX/Exceptions/ExceptionDetail.cs`
- Modify: `AGDevX/Enums/EnumStringValueAttribute.cs`

**Step 1: Add constructor XML docs to `CodedApplicationException.cs`**

For the base classes (`CodedApplicationException` and `CodedArgumentNullException`), use these patterns:

```csharp
/// <summary>
/// Initializes a new instance of the <see cref="CodedApplicationException"/> class with the default code.
/// </summary>
public CodedApplicationException()

/// <summary>
/// Initializes a new instance of the <see cref="CodedApplicationException"/> class with a specified error message.
/// </summary>
/// <param name="message">The error message that explains the reason for the exception.</param>
public CodedApplicationException(string message)

/// <summary>
/// Initializes a new instance of the <see cref="CodedApplicationException"/> class with a specified error message and code.
/// </summary>
/// <param name="message">The error message that explains the reason for the exception.</param>
/// <param name="code">A code providing additional context into the origin of the exception.</param>
public CodedApplicationException(string message, string code)

/// <summary>
/// Initializes a new instance of the <see cref="CodedApplicationException"/> class with a specified error message and inner exception.
/// </summary>
/// <param name="message">The error message that explains the reason for the exception.</param>
/// <param name="innerException">The exception that is the cause of the current exception.</param>
public CodedApplicationException(string message, Exception innerException)

/// <summary>
/// Initializes a new instance of the <see cref="CodedApplicationException"/> class with a specified error message, code, and inner exception.
/// </summary>
/// <param name="message">The error message that explains the reason for the exception.</param>
/// <param name="code">A code providing additional context into the origin of the exception.</param>
/// <param name="innerException">The exception that is the cause of the current exception.</param>
public CodedApplicationException(string message, string code, Exception innerException)
```

Also add to the `Code` property:
```csharp
/// <summary>
/// A code providing additional context into the origin of the exception.
/// </summary>
public virtual string Code { get; set; } = "CODED_APPLICATION_EXCEPTION";
```

**Step 2: Add constructor XML docs to `CodedArgumentNullException.cs`**

Same pattern but adapted for `argumentName` parameter:

```csharp
/// <summary>
/// Initializes a new instance of the <see cref="CodedArgumentNullException"/> class with the default code.
/// </summary>
public CodedArgumentNullException()

/// <summary>
/// Initializes a new instance of the <see cref="CodedArgumentNullException"/> class with a specified argument name.
/// </summary>
/// <param name="argumentName">The name of the argument that caused the exception.</param>
public CodedArgumentNullException(string argumentName)

/// <summary>
/// Initializes a new instance of the <see cref="CodedArgumentNullException"/> class with a specified argument name and code.
/// </summary>
/// <param name="argumentName">The name of the argument that caused the exception.</param>
/// <param name="code">A code providing additional context into the origin of the exception.</param>
public CodedArgumentNullException(string argumentName, string code)

/// <summary>
/// Initializes a new instance of the <see cref="CodedArgumentNullException"/> class with a specified message and inner exception.
/// </summary>
/// <param name="message">The error message that explains the reason for the exception.</param>
/// <param name="innerException">The exception that is the cause of the current exception.</param>
public CodedArgumentNullException(string message, Exception innerException)

/// <summary>
/// Initializes a new instance of the <see cref="CodedArgumentNullException"/> class with a specified argument name, code, and inner exception.
/// </summary>
/// <param name="argumentName">The name of the argument that caused the exception.</param>
/// <param name="code">A code providing additional context into the origin of the exception.</param>
/// <param name="innerException">The exception that is the cause of the current exception.</param>
public CodedArgumentNullException(string argumentName, string code, Exception innerException)
```

Also add to the `Code` property:
```csharp
/// <summary>
/// A code providing additional context into the origin of the exception.
/// </summary>
```

**Step 3: Add constructor XML docs to all 7 derived exception classes**

For each derived exception (`AcquireTokenException`, `ApplicationStartupException`, `ClaimNotFoundException`, `ExtensionMethodException`, `ExtensionMethodParameterNullException`, `MissingRequiredClaimException`, `NotAuthorizedException`), follow the same 5-constructor pattern using `<see cref="ClassName"/>` and the class's appropriate parameter names (e.g., `ExtensionMethodParameterNullException` uses `argumentName` since it inherits from `CodedArgumentNullException`).

**Step 4: Add XML docs to `ExceptionDetail` properties**

```csharp
/// <summary>
/// A code identifying the type or origin of the exception.
/// </summary>
public required string Code { get; set; }

/// <summary>
/// The exception error message.
/// </summary>
public required string Message { get; set; }

/// <summary>
/// The parsed stack frames from the exception, or <see langword="null"/> if stack frames were not included.
/// </summary>
public IEnumerable<StackFrameModel>? StackFrames { get; set; }

/// <summary>
/// The inner exception detail, or <see langword="null"/> if there is no inner exception.
/// </summary>
public ExceptionDetail? InnerException { get; set; }
```

**Step 5: Add XML docs to `StackFrameModel` properties**

```csharp
/// <summary>
/// Structured representation of a single stack frame.
/// </summary>
public sealed class StackFrameModel
{
    /// <summary>
    /// The line number in the source code file where the frame originated.
    /// </summary>
    public int LineNumber { get; set; }

    /// <summary>
    /// The method signature where the frame originated.
    /// </summary>
    public string? Method { get; set; }

    /// <summary>
    /// The fully qualified class name where the frame originated.
    /// </summary>
    public string? Class { get; set; }

    /// <summary>
    /// The full name of the assembly containing the frame.
    /// </summary>
    public string? AssemblyName { get; set; }

    /// <summary>
    /// The file path of the assembly containing the frame.
    /// </summary>
    public string? AssemblyFile { get; set; }

    /// <summary>
    /// The source code file path where the frame originated.
    /// </summary>
    public string? CodeFile { get; set; }
}
```

**Step 6: Add XML doc to `EnumStringValueAttribute.Value`**

In `AGDevX/Enums/EnumStringValueAttribute.cs`, add before the `Value` property:

```csharp
/// <summary>
/// The string value associated with the decorated enum field.
/// </summary>
public string Value { get; }
```

**Step 7: Build to verify no XML doc warnings**

Run: `dotnet build AGDevX.slnx`
Expected: Clean build

**Step 8: Commit**

```
docs: add XML documentation to all undocumented public members

Covers exception constructors, ExceptionDetail/StackFrameModel properties,
CodedApplicationException.Code, CodedArgumentNullException.Code, and
EnumStringValueAttribute.Value.
```

---

## Task 11: Add code coverage to CI

**Files:**
- Modify: `.github/workflows/build-test-create-nuget-package.yml`

**Step 1: Update the test step to collect coverage**

Change:
```yaml
      - name: Run Unit Tests
        run: dotnet test --no-build --verbosity normal
```

To:
```yaml
      - name: Run Unit Tests
        run: dotnet test --no-build --verbosity normal --collect:"XPlat Code Coverage" --results-directory ./coverage

      - name: Check Code Coverage Threshold
        if: matrix.os == 'ubuntu-latest'
        shell: bash
        run: |
          # Find the coverage file (Cobertura XML)
          COVERAGE_FILE=$(find ./coverage -name "coverage.cobertura.xml" | head -1)
          if [ -z "$COVERAGE_FILE" ]; then
            echo "::error::No coverage file found"
            exit 1
          fi
          # Extract line coverage rate from Cobertura XML
          LINE_RATE=$(grep -oP 'line-rate="\K[^"]+' "$COVERAGE_FILE" | head -1)
          COVERAGE_PCT=$(echo "$LINE_RATE * 100" | bc -l | xargs printf "%.1f")
          echo "Line coverage: ${COVERAGE_PCT}%"
          THRESHOLD=80
          if (( $(echo "$COVERAGE_PCT < $THRESHOLD" | bc -l) )); then
            echo "::error::Code coverage ${COVERAGE_PCT}% is below ${THRESHOLD}% threshold"
            exit 1
          fi
          echo "Coverage ${COVERAGE_PCT}% meets ${THRESHOLD}% threshold"
```

Note: Coverage threshold check runs only on ubuntu-latest to avoid running it 3x and dealing with Windows/macOS shell differences.

**Step 2: Commit**

```
ci: add code coverage collection and 80% threshold enforcement
```

---

## Task 12: Add README badges

**Files:**
- Modify: `README.md`

**Step 1: Add badges at the top of the README**

Add immediately after `# AGDevX .NET` (line 1):

```markdown
# AGDevX .NET

[![Build](https://github.com/agdevx/AGDevX.NET/actions/workflows/build-test-create-nuget-package.yml/badge.svg)](https://github.com/agdevx/AGDevX.NET/actions/workflows/build-test-create-nuget-package.yml)
[![NuGet](https://img.shields.io/nuget/v/AGDevX.svg)](https://www.nuget.org/packages/AGDevX)
```

**Step 2: Commit**

```
docs: add build status and NuGet version badges to README
```

---

## Task 13: Update `PublicAPI.Unshipped.txt` with IReadOnlyList changes

After the IReadOnlyList changes in Task 6, the public API file will be stale.

**Files:**
- Modify: `AGDevX/PublicAPI.Unshipped.txt`

**Step 1: Build and check for RS0016/RS0017 warnings**

Run: `dotnet build AGDevX/AGDevX.csproj -c Release 2>&1`

**Step 2: Update `PublicAPI.Unshipped.txt`**

Remove old `List<string>` signatures, add new `IReadOnlyList<string>` signatures.

**Step 3: Build to verify clean**

Run: `dotnet build AGDevX/AGDevX.csproj -c Release`
Expected: Clean build

**Step 4: Commit**

```
chore: update PublicAPI.Unshipped.txt for IReadOnlyList changes
```

---

## Task 14: Final verification

**Step 1: Run full build and test suite**

Run: `dotnet build AGDevX.slnx -c Release && dotnet test --verbosity normal`
Expected: Clean build, all tests pass on both net8.0 and net10.0

**Step 2: Verify no remaining warnings**

Run: `dotnet build AGDevX.slnx -c Release -warnaserror 2>&1`
Check for any remaining warnings.

**Step 3: Push and create PR**

```bash
git push -u origin ag/quality-tooling-improvements
gh pr create --title "Quality and tooling improvements" --body "$(cat <<'EOF'
## Summary
- Add Directory.Build.props for shared project properties
- Add global.json to pin SDK version
- Add SourceLink for NuGet source debugging
- Add PublicApiAnalyzers to track public API surface
- Return IReadOnlyList<string> from collection claim methods (breaking, alpha)
- Add CodedArgumentNullExceptionTests
- Add dedicated enum string value tests for Auth0/Custom/JWT claim types
- Add GetExceptionDetail test for plain Exception overload
- Add XML docs to all undocumented public members (constructors, properties)
- Add code coverage collection and 80% threshold in CI
- Add build status and NuGet version badges to README
- Update CLAUDE.md dependency convention

## Test plan
- [ ] All existing tests pass on net8.0 and net10.0
- [ ] New CodedArgumentNullException tests pass (5 tests)
- [ ] New enum string value tests pass (43 tests)
- [ ] New ExceptionDetail plain Exception test passes
- [ ] IReadOnlyList return types compile and tests pass
- [ ] PublicApiAnalyzers produces clean build
- [ ] CI workflow runs successfully
EOF
)"
```

---

## Execution Order

Tasks 1-5 are build tooling/config (independent of code changes).
Task 6 is the API change.
Tasks 7-9 are new tests (independent of each other).
Task 10 is XML docs (independent).
Task 11-12 are CI/README (independent).
Task 13 depends on Tasks 4 + 6 (PublicAPI update).
Task 14 is final verification.

**Parallelizable groups:**
- Wave 1: Tasks 1, 2, 3, 5 (config/tooling, independent files)
- Wave 2: Task 4 (PublicApiAnalyzers — needs csproj from Task 3)
- Wave 3: Tasks 6, 7, 8, 9, 10, 11, 12 (code changes, all touch different files)
- Wave 4: Task 13 (PublicAPI update after Task 6)
- Wave 5: Task 14 (final verification)
