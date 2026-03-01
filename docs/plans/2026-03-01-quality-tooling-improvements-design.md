# Quality & Tooling Improvements Design

**Date:** 2026-03-01
**Status:** Approved
**Branch:** Single branch with logical commits

## Goal

Close testing gaps, improve API design, complete XML documentation, add build tooling, and enhance CI — all in one cohesive effort.

## Decisions

- **Dependency policy updated:** "Zero runtime dependencies" replaces "zero external dependencies." Build-time tooling (`PrivateAssets="All"`) is acceptable.
- **IReadOnlyList scope:** Both Get and TryGet variants change for consistency.
- **Coverage threshold:** 80% line coverage, enforced in CI.
- **NuGet publishing:** Excluded from this effort (separate task).

---

## 1. Testing Gaps

### CodedArgumentNullExceptionTests.cs

New test file with 5 `[Fact]` tests (one per constructor overload), matching the pattern of all other exception test files. Nested `When_constructing` class with `//-- Arrange`, `//-- Act`, `//-- Assert` comments.

### Enum String Value Tests

New files in `AGDevX.Tests/Security/`:
- `Auth0ClaimTypeTests.cs`
- `CustomClaimTypeTests.cs`
- `JwtClaimTypeTests.cs`

Each uses `[Theory]/[InlineData]` to assert every enum's `.StringValue()` matches the expected spec string (e.g., `JwtClaimType.Subject` → `"sub"`).

### GetExceptionDetail for Plain Exception

Add test in `ExceptionDetailTests.cs` that directly calls `exception.GetExceptionDetail("CODE")` on a regular `Exception` (not `CodedApplicationException`).

---

## 2. API: IReadOnlyList Return Types

Change public return types on 6 methods:
- `GetAudiences` / `TryGetAudiences`: `List<string>` → `IReadOnlyList<string>`
- `GetScopes` / `TryGetScopes`: `List<string>` → `IReadOnlyList<string>`
- `GetRoles` / `TryGetRoles`: `List<string>` → `IReadOnlyList<string>`

Private `GetClaimValues<T>` keeps returning `List<T>` internally (`List<T>` implements `IReadOnlyList<T>`). Update test assertions accordingly.

Breaking change — acceptable in alpha.

---

## 3. XML Documentation

Add `<summary>` tags to all undocumented public members:
- 9 exception classes × 5 constructors = 45 constructors
- `ExceptionDetail`: Code, Message, StackFrames, InnerException properties
- `ExceptionDetail.StackFrameModel`: LineNumber, Method, Class, AssemblyName, AssemblyFile, CodeFile properties
- `EnumStringValueAttribute.Value` property
- `CodedApplicationException.Code` and `CodedArgumentNullException.Code` properties

---

## 4. Build Tooling

### SourceLink

Add `Microsoft.SourceLink.GitHub` to AGDevX.csproj with `PrivateAssets="All"`. Enables NuGet consumers to step into library source during debugging.

### global.json

Pin SDK to 10.0.103 with `"rollForward": "latestPatch"`. Prevents SDK version drift across contributors and CI.

### Directory.Build.props

Extract shared properties from both csprojs:
- `TargetFrameworks` (net8.0;net10.0)
- `ImplicitUsings` (disable)
- `Nullable` (enable)

Each csproj retains only its unique properties.

### PublicApiAnalyzers

Add `Microsoft.CodeAnalysis.PublicApiAnalyzers` to AGDevX.csproj with `PrivateAssets="All"`. Create:
- `PublicAPI.Shipped.txt` — empty (nothing shipped yet)
- `PublicAPI.Unshipped.txt` — full current API surface

API moves from Unshipped to Shipped at first GA release.

---

## 5. CI: Code Coverage

Add `--collect:"XPlat Code Coverage"` to the test step. Enforce 80% line coverage threshold — fail the build if below.

---

## 6. README Badges

Add at the top of README.md:
- GitHub Actions build status badge
- NuGet version badge (shields.io)

---

## 7. CLAUDE.md Convention Update

Update "Zero external dependencies" section to "Zero runtime dependencies" with a note that build-time tooling is acceptable via `PrivateAssets="All"`.
