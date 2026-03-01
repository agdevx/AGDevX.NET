# Quality Improvements Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Fix bugs, add missing XML docs, improve test assertions, add multi-targeting, update CI, and complete README documentation.

**Architecture:** These are independent quality improvements across the existing codebase. No new projects or architectural changes. Each task is self-contained.

**Tech Stack:** C# / .NET 10 + .NET 8 / xUnit

---

### Task 1: Fix `IsWhiteSpace`/`IsNotWhiteSpace` bug

The methods only check for space characters (`' '`), not tabs, newlines, or other Unicode whitespace.

**Files:**
- Modify: `AGDevX/Strings/StringExtensions.cs:88,98`
- Modify: `AGDevX.Tests/Strings/StringExtensionsTests.cs:205,262`

**Step 1: Add failing test cases for tab/newline whitespace**

In `AGDevX.Tests/Strings/StringExtensionsTests.cs`, add `\t` and `\n` InlineData to the `When_calling_IsWhiteSpace` true cases and `When_calling_IsNotWhiteSpace` false cases:

```csharp
// In When_calling_IsWhiteSpace, add to the "return_true" test:
[InlineData("\t")]
[InlineData("\n")]
[InlineData("\t \n")]

// In When_calling_IsNotWhiteSpace, add to the "return_false" test:
[InlineData("\t")]
[InlineData("\n")]
[InlineData("\t \n")]
```

**Step 2: Run tests to verify they fail**

Run: `dotnet test --filter "FullyQualifiedName~When_calling_IsWhiteSpace|FullyQualifiedName~When_calling_IsNotWhiteSpace" --no-build --verbosity normal`
Expected: FAIL — `"\t".IsWhiteSpace()` returns false

**Step 3: Fix implementation**

In `AGDevX/Strings/StringExtensions.cs`, change:

Line 88 — `IsWhiteSpace`:
```csharp
// FROM:
return str != string.Empty && str.All(s => s == ' ');
// TO:
return str != string.Empty && str.All(char.IsWhiteSpace);
```

Line 98 — `IsNotWhiteSpace`:
```csharp
// FROM:
return str == string.Empty || !str.All(s => s == ' ');
// TO:
return str == string.Empty || !str.All(char.IsWhiteSpace);
```

**Step 4: Build and run tests to verify they pass**

Run: `dotnet build && dotnet test --verbosity normal`
Expected: All tests PASS

**Step 5: Commit**

```
fix: use char.IsWhiteSpace instead of checking only for space character

IsWhiteSpace/IsNotWhiteSpace only detected spaces, not tabs, newlines,
or other Unicode whitespace characters.
```

---

### Task 2: Remove `[AllowNull]` from parameters that throw on null

`[AllowNull]` signals to callers that null is acceptable, but these methods throw on null. Remove the attribute so the API contract matches the behavior.

**Files:**
- Modify: `AGDevX/Strings/StringExtensions.cs:17,34,51`
- Modify: `AGDevX/IEnumerables/IEnumerableExtensions.cs:33,77`

**Step 1: Remove `[AllowNull]` from throwing parameters**

In `StringExtensions.cs`, remove `[AllowNull]` from `string2` parameter on:
- `EqualsIgnoreCase` (line 17)
- `StartsWithIgnoreCase` (line 34)
- `ContainsIgnoreCase` (line 51)

In `IEnumerableExtensions.cs`, remove `[AllowNull]` from:
- `enumerable2` on `HasCommonStringElement` (line 33)
- `str` on `ContainsIgnoreCase` (line 77)

**Step 2: Build and run tests**

Run: `dotnet build && dotnet test --verbosity normal`
Expected: All tests PASS (behavior unchanged, only nullability annotation corrected)

**Step 3: Commit**

```
fix: remove [AllowNull] from parameters that throw on null

The attribute signaled to callers that null was acceptable, but the
methods immediately throw. Now the API contract matches behavior.
```

---

### Task 3: Remove unnecessary `$` string interpolation in ClaimsPrincipalExtensions

All throw expressions use `$"..."` with no interpolated expressions.

**Files:**
- Modify: `AGDevX/Security/ClaimsPrincipalExtensions.cs`

**Step 1: Remove `$` prefix from all non-interpolated strings**

Replace all `$"A ... claim was not found"` with `"A ... claim was not found"` (34 occurrences).

**Step 2: Build and run tests**

Run: `dotnet build && dotnet test --verbosity normal`
Expected: All tests PASS

**Step 3: Commit**

```
cleanup: remove unnecessary $ string interpolation prefix

None of the exception message strings contained interpolated expressions.
```

---

### Task 4: Document `TryGetAddress` JsonDocument disposal trade-off

**Files:**
- Modify: `AGDevX/Security/ClaimsPrincipalExtensions.cs:419-431`

**Step 1: Add XML doc comment explaining the trade-off**

Update the existing `TryGetAddress` XML doc to note the disposal trade-off:

```csharp
/// <summary>
/// Returns the Address claim value parsed as a <see cref="JsonElement"/>,
/// or <see langword="null"/> if the claim is missing or the value is not valid JSON.
/// </summary>
/// <remarks>
/// The underlying <see cref="JsonDocument"/> is not disposed because <see cref="JsonElement"/>
/// becomes invalid after its parent document is disposed. Callers should not hold long-lived
/// references to the returned value in memory-sensitive scenarios.
/// </remarks>
```

**Step 2: Commit**

```
docs: document TryGetAddress JsonDocument disposal trade-off
```

---

### Task 5: Add missing XML docs to ClaimsPrincipal methods

36 of the 40 Get/TryGet method pairs lack XML documentation. Only Birthdate and Address have them.

**Files:**
- Modify: `AGDevX/Security/ClaimsPrincipalExtensions.cs`

**Step 1: Add XML docs to all undocumented public methods**

Every `Get{Claim}` method gets:
```csharp
/// <summary>
/// Returns the {Claim} claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
/// </summary>
```

Every `TryGet{Claim}` method gets:
```csharp
/// <summary>
/// Returns the {Claim} claim value, or <see langword="null"/> if the claim is missing.
/// </summary>
```

For methods returning `List<string>` (Audiences, Scopes, Roles):
```csharp
/// <summary>
/// Returns the {Claim} claim values as a list. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
/// </summary>
```

For methods returning `int` (Expiration, NotBefore, IssuedAt, UpdatedAt, AuthTime):
```csharp
/// <summary>
/// Returns the {Claim} claim value as an integer. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
/// </summary>
```

For methods returning `bool` (EmailVerified, PhoneNumberVerified, IsActive):
```csharp
/// <summary>
/// Returns the {Claim} claim value as a boolean. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
/// </summary>
```

**Step 2: Build to verify no XML doc warnings**

Run: `dotnet build`
Expected: Clean build

**Step 3: Commit**

```
docs: add XML documentation to all ClaimsPrincipal extension methods

As a public NuGet package, all public methods need IntelliSense docs.
```

---

### Task 6: Fix xUnit analyzer warnings in exception tests

Exception tests use `Assert.True(x.Equals(y))` and `Assert.True(x == y)` instead of `Assert.Equal` / `Assert.Same`, producing poor failure messages.

**Files:**
- Modify: `AGDevX.Tests/Exceptions/AcquireTokenExceptionTests.cs`
- Modify: `AGDevX.Tests/Exceptions/ApplicationStartupExceptionTests.cs`
- Modify: `AGDevX.Tests/Exceptions/ClaimNotFoundExceptionTests.cs`
- Modify: `AGDevX.Tests/Exceptions/CodedApplicationExceptionTests.cs`
- Modify: `AGDevX.Tests/Exceptions/ExtensionMethodExceptionTests.cs`
- Modify: `AGDevX.Tests/Exceptions/ExtensionMethodParameterNullExceptionTests.cs`
- Modify: `AGDevX.Tests/Exceptions/MissingRequiredClaimExceptionTests.cs`
- Modify: `AGDevX.Tests/Exceptions/NotAuthorizedExceptionTests.cs`

**Step 1: Replace assertion patterns across all 8 files**

Pattern replacements:
- `Assert.True(new XException().Code.Equals(defaultCode))` -> `Assert.Equal(defaultCode, new XException().Code)`
- `Assert.True(new XException("msg", code).Code.Equals(code))` -> `Assert.Equal(code, new XException("msg", code).Code)`
- `Assert.True(new XException(message).Message.Equals(message))` -> `Assert.Equal(message, new XException(message).Message)`
- `Assert.True(new XException(message, innerException).Message.Equals(message))` -> `Assert.Equal(message, new XException(message, innerException).Message)`
- `Assert.True(new XException(message, innerException).InnerException == innerException)` -> `Assert.Same(innerException, new XException(message, innerException).InnerException)`
- Similar patterns for the 3-arg constructor variants

**Step 2: Build and run tests**

Run: `dotnet build && dotnet test --verbosity normal`
Expected: All tests PASS, analyzer warnings gone

**Step 3: Commit**

```
test: use Assert.Equal/Assert.Same instead of Assert.True for value comparisons

Fixes xUnit analyzer warnings and produces better failure messages.
```

---

### Task 7: Add net8.0 multi-targeting

Consumers on .NET 8 LTS cannot use the library. Add `net8.0` as a target.

**Files:**
- Modify: `AGDevX/AGDevX.csproj:4`
- Modify: `AGDevX.Tests/AGDevX.Tests.csproj:4`

**Step 1: Update main project to multi-target**

In `AGDevX/AGDevX.csproj`, change:
```xml
<!-- FROM: -->
<TargetFramework>net10.0</TargetFramework>
<!-- TO: -->
<TargetFrameworks>net8.0;net10.0</TargetFrameworks>
```

**Step 2: Update test project to multi-target**

In `AGDevX.Tests/AGDevX.Tests.csproj`, change:
```xml
<!-- FROM: -->
<TargetFramework>net10.0</TargetFramework>
<!-- TO: -->
<TargetFrameworks>net8.0;net10.0</TargetFrameworks>
```

**Step 3: Build and run tests on both targets**

Run: `dotnet build && dotnet test --verbosity normal`
Expected: Build succeeds for both TFMs, all tests pass on both

**Step 4: Commit**

```
feat: add net8.0 multi-targeting

Enables consumers on .NET 8 LTS to use the library alongside .NET 10.
```

---

### Task 8: Enable push-to-main trigger in CI

Merges to main are not validated by CI.

**Files:**
- Modify: `.github/workflows/build-test-create-nuget-package.yml:6-9`

**Step 1: Uncomment push trigger for main**

```yaml
# FROM:
on:
  pull_request:
  #push:
  #  tags:
  #    - 'v*'

# TO:
on:
  pull_request:
  push:
    branches:
      - main
```

Note: Keep the tag-based push commented out (that's for NuGet publishing).

**Step 2: Commit**

```
ci: enable CI on push to main

Ensures merges to main are validated, not just pull requests.
```

---

### Task 9: Document ClaimsPrincipal extensions in README

The README's ClaimsPrincipal section is a one-line stub.

**Files:**
- Modify: `README.md:102-104`

**Step 1: Expand the ClaimsPrincipal section**

Replace the stub with a full listing of all Get/TryGet pairs, grouped by claim category. Follow the same format as other README sections (bullet list with method name and description).

**Step 2: Remove the duplicate tech debt line at the bottom of the README**

Line 123 (`- Document all ClaimsPrincipal extensions (there are many)`) can be removed since this task completes it.

**Step 3: Commit**

```
docs: document all ClaimsPrincipal extension methods in README
```

---

### Task 10: Clean up stale NOTES.md

**Files:**
- Modify: `.claude/NOTES.md`

**Step 1: Remove stale items**

- Remove the feature branch observation about `ag/NullIfNullOrWhiteSpace` (already merged)
- Remove the README tech debt item about ClaimsPrincipal (completed in Task 9)
- Remove the observation about "README tech debt item about unit tests may be outdated" (it is)
- Keep only items that are still relevant (if any)

**Step 2: Commit**

```
chore: clean up stale NOTES.md items
```
