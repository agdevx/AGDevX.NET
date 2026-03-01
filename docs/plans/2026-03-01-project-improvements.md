# AGDevX.NET Project Improvements Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Implement 9 improvement branches covering project setup, code enhancements, tooling, and release prep.

**Architecture:** Each task is an independent branch off `main`. Branches that touch unrelated files can be parallelized via worktree isolation. Version bump goes last.

**Tech Stack:** .NET 10, C#, xUnit, GitHub Actions

---

## Pre-Work: Update Comment Convention in Docs

**Not a branch — just a file edit (outside the repo).**

**File:** `C:\Users\bfgei\.claude\docs\.NET_C#.md`

**Change:**
```
- Single-line comments: `//== This is a comment` (two slashes, two equals, one space)
+ Single-line comments: `//-- This is a comment` (two slashes, two dashes, one space)
```

Also update all `//==` references elsewhere in the file (there are examples in the Comment Format and Comment Preservation sections):
- Line 181: `//== Bad - unnecessary wrapping` → `//-- Bad - unnecessary wrapping`
- Line 186: `//== Good - single line` → `//-- Good - single line`
- Line 190: `//== Bad - verbose null check` → `//-- Bad - verbose null check`
- Line 201: `//== Good - concise null guard` → `//-- Good - concise null guard`
- Line 227: `//== This is a single-line comment explaining why, not what` → `//-- This is a single-line comment explaining why, not what`
- Line 77: `//-- Always write notes in CLAUDE.md about this application` (already correct)

---

## Task 1: Create `.claude/NOTES.md`

**Branch:** `ag/config/project-notes`

**Files:**
- Create: `.claude/NOTES.md`

**Step 1: Create the branch**
```bash
git checkout main
git checkout -b ag/config/project-notes
```

**Step 2: Create `.claude/NOTES.md`**

```markdown
# NOTES

## Tech Debt

- [ ] Document all ClaimsPrincipal extensions in README (there are 40+ Get/TryGet pairs; README only lists the section header)

## Observations

- ClaimsPrincipal test coverage appears complete for all existing Get/TryGet pairs (40 pairs, ~1627 lines of tests)
- README tech debt item "Unit tests to cover all ClaimsPrincipal extensions" may be outdated — verify and remove if so
- Feature branch `ag/NullIfNullOrWhiteSpace` exists with completed implementation; `[NotNullIfNotNull]` attribute is semantically incorrect and should be removed when merging
```

**Step 3: Commit**
```bash
git add .claude/NOTES.md
git commit -m "Add .claude/NOTES.md with tech debt tracking"
```

**Step 4: Push and create PR**
```bash
git push -u origin ag/config/project-notes
gh pr create --title "Add .claude/NOTES.md" --body "Track tech debt and observations for the project."
```

---

## Task 2: Create `.claude/CLAUDE.md`

**Branch:** `ag/config/project-claude-md`

**Files:**
- Create: `.claude/CLAUDE.md`

**Step 1: Create the branch**
```bash
git checkout main
git checkout -b ag/config/project-claude-md
```

**Step 2: Create `.claude/CLAUDE.md`**

```markdown
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

### Zero external dependencies
- This library depends only on the .NET runtime — no NuGet packages
- Keep it that way unless there's a compelling reason to add one
```

**Step 3: Commit**
```bash
git add .claude/CLAUDE.md
git commit -m "Add project-level .claude/CLAUDE.md with conventions"
```

**Step 4: Push and create PR**
```bash
git push -u origin ag/config/project-claude-md
gh pr create --title "Add project-level CLAUDE.md" --body "Document project-specific conventions that override global defaults."
```

---

## Task 3: Implement ClaimsPrincipal TODOs

**Branch:** `ag/feature/claims-principal-todos`

**Files:**
- Modify: `AGDevX/Security/ClaimsPrincipalExtensions.cs`
- Modify: `AGDevX.Tests/Security/ClaimsPrincipalExtensionsGetTests.cs`
- Modify: `AGDevX.Tests/Security/ClaimsPrincipalExtensionsTryGetTests.cs`

### Birthdate: Change return type from string to DateTime

The JWT `birthdate` claim (OIDC spec) uses format `YYYY-MM-DD`. Parse it to `DateTime`.

**Step 1: Create the branch**
```bash
git checkout main
git checkout -b ag/feature/claims-principal-todos
```

**Step 2: Write failing tests for Birthdate**

In `ClaimsPrincipalExtensionsTryGetTests.cs`, update the Birthdate test class:
- Test that a valid date string `"1990-06-15"` returns `new DateTime(1990, 6, 15)`
- Test that a missing claim returns `null`
- Test that an unparseable string returns `null`

In `ClaimsPrincipalExtensionsGetTests.cs`, update the Birthdate test class:
- Test that a valid date string returns `DateTime`
- Test that a missing claim throws `ClaimNotFoundException`

**Step 3: Run tests to verify they fail**
```bash
dotnet test AGDevX.Tests --filter "FullyQualifiedName~Birthdate" -v n
```

**Step 4: Implement Birthdate changes**

Change `TryGetBirthdate`:
```csharp
public static DateTime? TryGetBirthdate(this ClaimsPrincipal claimsPrincipal)
{
    var value = claimsPrincipal.GetClaimValue<string>(JwtClaimType.Birthdate.StringValue());
    if (value == null) return null;
    return DateTime.TryParse(value, out var date) ? date : null;
}
```

Change `GetBirthdate`:
```csharp
public static DateTime GetBirthdate(this ClaimsPrincipal claimsPrincipal)
{
    return claimsPrincipal.TryGetBirthdate()
                ?? throw new ClaimNotFoundException($"A Birthdate claim was not found");
}
```

Add `using System.Globalization;` if needed for date parsing.

**Step 5: Run tests to verify they pass**
```bash
dotnet test AGDevX.Tests --filter "FullyQualifiedName~Birthdate" -v n
```

**Step 6: Commit Birthdate changes**
```bash
git add -A
git commit -m "Implement Birthdate DateTime parsing in ClaimsPrincipal extensions"
```

### Address: Change return type from string to JsonElement

The JWT `address` claim (OIDC spec) is a JSON object. Parse it to `JsonElement`.

**Step 7: Write failing tests for Address**

In both test files, update Address test classes:
- Test that a valid JSON string `{"street_address":"123 Main St","locality":"Anytown"}` returns a `JsonElement` with accessible properties
- Test that a missing claim returns `null` (TryGet) or throws (Get)

**Step 8: Run tests to verify they fail**
```bash
dotnet test AGDevX.Tests --filter "FullyQualifiedName~Address" -v n
```

**Step 9: Implement Address changes**

Add `using System.Text.Json;` to ClaimsPrincipalExtensions.cs.

Change `TryGetAddress`:
```csharp
public static JsonElement? TryGetAddress(this ClaimsPrincipal claimsPrincipal)
{
    var value = claimsPrincipal.GetClaimValue<string>(JwtClaimType.Address.StringValue());
    if (value == null) return null;
    try
    {
        return JsonDocument.Parse(value).RootElement;
    }
    catch (JsonException)
    {
        return null;
    }
}
```

Change `GetAddress`:
```csharp
public static JsonElement GetAddress(this ClaimsPrincipal claimsPrincipal)
{
    return claimsPrincipal.TryGetAddress()
                ?? throw new ClaimNotFoundException($"An Address claim was not found");
}
```

**Step 10: Run tests to verify they pass**
```bash
dotnet test AGDevX.Tests --filter "FullyQualifiedName~Address" -v n
```

**Step 11: Commit Address changes**
```bash
git add -A
git commit -m "Implement Address JSON deserialization in ClaimsPrincipal extensions"
```

**Step 12: Push and create PR**
```bash
git push -u origin ag/feature/claims-principal-todos
gh pr create --title "Implement Birthdate and Address claim parsing" --body "$(cat <<'EOF'
## Summary
- Birthdate: parse string claim to DateTime (TryParse, returns null on failure)
- Address: parse JSON string claim to JsonElement (returns null on parse failure)
- Both follow existing Get/TryGet pattern

## Test plan
- [ ] Birthdate: valid date string returns DateTime
- [ ] Birthdate: missing claim returns null / throws
- [ ] Birthdate: unparseable string returns null
- [ ] Address: valid JSON returns JsonElement
- [ ] Address: missing claim returns null / throws
- [ ] Address: invalid JSON returns null
- [ ] All existing tests still pass
EOF
)"
```

---

## Task 4: Verify and Update ClaimsPrincipal Test Coverage

**Branch:** `ag/test/claims-principal-coverage`

**Files:**
- Modify: `AGDevX.Tests/Security/ClaimsPrincipalExtensionsGetTests.cs` (if gaps found)
- Modify: `AGDevX.Tests/Security/ClaimsPrincipalExtensionsTryGetTests.cs` (if gaps found)
- Modify: `README.md` (update tech debt section)

**Step 1: Create the branch**
```bash
git checkout main
git checkout -b ag/test/claims-principal-coverage
```

**Step 2: Audit test coverage**

Compare the list of public methods in `ClaimsPrincipalExtensions.cs` against test classes in both test files. For each Get/TryGet pair, verify:
- Success case tested
- Missing claim case tested
- For value types (int, bool): conversion tested
- For fallback claims (Subject, Email, etc.): fallback tested
- For collection claims (Scopes, Roles): split logic tested

**Step 3: Add any missing tests following existing patterns**

Use the `When_calling_XXX` nested class pattern with `//-- Arrange`, `//-- Act`, `//-- Assert` comments.

**Step 4: Update README tech debt**

If coverage is verified complete, remove the "Unit tests to cover all ClaimsPrincipal extensions" line from the Tech Debt section. Also update or expand the ClaimsPrincipal extensions documentation section if needed.

**Step 5: Run full test suite**
```bash
dotnet test AGDevX.Tests -v n
```

**Step 6: Commit and PR**
```bash
git add -A
git commit -m "Verify ClaimsPrincipal test coverage and update README"
git push -u origin ag/test/claims-principal-coverage
gh pr create --title "Verify ClaimsPrincipal test coverage" --body "Audit and update test coverage for all 40 Get/TryGet pairs. Update README tech debt."
```

---

## Task 5: Finish NullIfNullOrWhiteSpace Feature

**Branch:** `ag/feature/null-if-null-or-whitespace`

**Files:**
- Modify: `AGDevX/Strings/StringExtensions.cs`
- Modify: `AGDevX.Tests/Strings/StringExtensionsTests.cs`
- Modify: `README.md`
- Modify: `LICENSE.txt`

The existing branch `ag/NullIfNullOrWhiteSpace` has a complete implementation with one bug: the `[NotNullIfNotNull(nameof(str))]` attribute is semantically incorrect — `"   ".NullIfNullOrWhiteSpace()` returns null even though input is not null.

**Step 1: Recreate branch from main with the fix**
```bash
git checkout main
git checkout -b ag/feature/null-if-null-or-whitespace
```

**Step 2: Cherry-pick the existing commits**
```bash
git cherry-pick 554896d c118bec
```
If conflicts occur, resolve them.

**Step 3: Fix the `[NotNullIfNotNull]` attribute bug**

In `StringExtensions.cs`, remove the `[NotNullIfNotNull(nameof(str))]` attribute from `NullIfNullOrWhiteSpace`. The method signature should be:
```csharp
public static string? NullIfNullOrWhiteSpace(this string? str)
```
Not:
```csharp
public static string? NullIfNullOrWhiteSpace([NotNullIfNotNull(nameof(str))] this string? str)
```

**Step 4: Run tests**
```bash
dotnet test AGDevX.Tests --filter "FullyQualifiedName~NullIfNullOrWhiteSpace" -v n
```

**Step 5: Commit the fix**
```bash
git add -A
git commit -m "Remove incorrect NotNullIfNotNull attribute from NullIfNullOrWhiteSpace"
```

**Step 6: Push and create PR**
```bash
git push -u origin ag/feature/null-if-null-or-whitespace
gh pr create --title "Add NullIfNullOrWhiteSpace string extension" --body "$(cat <<'EOF'
## Summary
- Returns null if string is null or whitespace, otherwise returns original string
- Removed incorrect [NotNullIfNotNull] attribute (whitespace strings return null despite being non-null)
- Includes tests and README update

## Test plan
- [ ] Null input returns null
- [ ] Empty string returns null
- [ ] Whitespace-only string returns null
- [ ] Non-whitespace string returns original string
EOF
)"
```

---

## Task 6: Add `.editorconfig`

**Branch:** `ag/config/editorconfig`

**Files:**
- Create: `.editorconfig`

**Step 1: Create the branch**
```bash
git checkout main
git checkout -b ag/config/editorconfig
```

**Step 2: Create `.editorconfig`**

Match existing project conventions (4-space indentation, file-scoped namespaces, explicit accessibility, no regions, var everywhere):

```ini
root = true

[*]
indent_style = space
indent_size = 4
end_of_line = crlf
charset = utf-8
trim_trailing_whitespace = true
insert_final_newline = true

[*.md]
trim_trailing_whitespace = false

[*.{csproj,slnx,props,targets}]
indent_size = 2

[*.{json,yml,yaml}]
indent_size = 2

[*.cs]
# Namespace preferences
csharp_style_namespace_declarations = file_scoped:warning

# var preferences
csharp_style_var_for_built_in_types = true:suggestion
csharp_style_var_when_type_is_apparent = true:suggestion
csharp_style_var_elsewhere = true:suggestion

# Accessibility modifiers
dotnet_style_require_accessibility_modifiers = always:warning

# Expression-level preferences
csharp_style_expression_bodied_methods = when_on_single_line:suggestion
csharp_style_expression_bodied_constructors = false:suggestion
csharp_style_expression_bodied_properties = true:suggestion

# Nullable reference types
dotnet_diagnostic.CS8600.severity = warning
dotnet_diagnostic.CS8601.severity = warning
dotnet_diagnostic.CS8602.severity = warning
dotnet_diagnostic.CS8603.severity = warning
dotnet_diagnostic.CS8604.severity = warning

# Organize usings
dotnet_sort_system_directives_first = true
dotnet_separate_import_directive_groups = false

# Naming conventions
dotnet_naming_rule.private_fields_should_be_camel_case.severity = suggestion
dotnet_naming_rule.private_fields_should_be_camel_case.symbols = private_fields
dotnet_naming_rule.private_fields_should_be_camel_case.style = camel_case_underscore
dotnet_naming_symbols.private_fields.applicable_kinds = field
dotnet_naming_symbols.private_fields.applicable_accessibilities = private
dotnet_naming_style.camel_case_underscore.required_prefix = _
dotnet_naming_style.camel_case_underscore.capitalization = camel_case
```

**Step 3: Run build to verify no new warnings break things**
```bash
dotnet build AGDevX.slnx
```

**Step 4: Commit and PR**
```bash
git add .editorconfig
git commit -m "Add .editorconfig with project coding conventions"
git push -u origin ag/config/editorconfig
gh pr create --title "Add .editorconfig" --body "Codify existing coding conventions: 4-space indentation, file-scoped namespaces, var usage, explicit accessibility modifiers."
```

---

## Task 7: Enable Roslyn Code Analyzers

**Branch:** `ag/config/code-analyzers`

**Files:**
- Modify: `AGDevX/AGDevX.csproj`

**Step 1: Create the branch**
```bash
git checkout main
git checkout -b ag/config/code-analyzers
```

**Step 2: Add analyzer configuration to AGDevX.csproj**

Add to the main `<PropertyGroup>`:
```xml
<EnableNETAnalyzers>true</EnableNETAnalyzers>
<AnalysisLevel>latest-recommended</AnalysisLevel>
```

**Step 3: Build and check for warnings**
```bash
dotnet build AGDevX.slnx -c Release 2>&1
```

**Step 4: Fix any analyzer warnings that surface**

Address warnings one by one. Common ones to expect:
- CA1062 (null parameter validation) — may conflict with extension method pattern
- CA1303 (string literals) — suppress if not localizing

If specific rules conflict with project conventions, suppress them in the csproj or via `<NoWarn>`.

**Step 5: Run full test suite**
```bash
dotnet test AGDevX.Tests -v n
```

**Step 6: Commit and PR**
```bash
git add -A
git commit -m "Enable Roslyn code analyzers at latest-recommended level"
git push -u origin ag/config/code-analyzers
gh pr create --title "Enable Roslyn code analyzers" --body "Enable .NET analyzers at latest-recommended level. Fix or suppress any warnings that conflict with project conventions."
```

---

## Task 8: Uncomment NuGet Publishing in CI

**Branch:** `ag/config/ci-nuget-publish`

**Files:**
- Modify: `.github/workflows/build-test-create-nuget-package.yml`

**Step 1: Create the branch**
```bash
git checkout main
git checkout -b ag/config/ci-nuget-publish
```

**Step 2: Uncomment the NuGet push step (lines 52-53)**

Change:
```yaml
#- name: Push to NuGet.org
#  run: dotnet nuget push ${{ env.PACKAGE_OUTPUT_DIRECTORY }}\*.nupkg -s ${{ env.NUGET_API_URL }} -k ${{ secrets.NUGET_API_KEY }}
```

To:
```yaml
- name: Push to NuGet.org
  run: dotnet nuget push ${{ env.PACKAGE_OUTPUT_DIRECTORY }}\*.nupkg -s ${{ env.NUGET_API_URL }} -k ${{ secrets.NUGET_API_KEY }}
```

**NOTE:** This step requires the `NUGET_API_KEY` secret to be configured in GitHub repository settings. The workflow will fail on the push step until that secret exists.

**Step 3: Commit and PR**
```bash
git add .github/workflows/build-test-create-nuget-package.yml
git commit -m "Enable NuGet package publishing in CI workflow"
git push -u origin ag/config/ci-nuget-publish
gh pr create --title "Enable NuGet publishing in CI" --body "$(cat <<'EOF'
## Summary
- Uncomment the NuGet push step in the build workflow

## Prerequisites
- NUGET_API_KEY secret must be configured in GitHub repository settings
- Without the secret, the push step will fail
EOF
)"
```

---

## Task 9: Bump Version to 0.1.0-alpha

**Branch:** `ag/config/version-bump`

**DO THIS LAST — after all other branches are merged.**

**Files:**
- Modify: `AGDevX/AGDevX.csproj` (lines 19, 21, 22)

**Step 1: Create the branch**
```bash
git checkout main
git pull
git checkout -b ag/config/version-bump
```

**Step 2: Update version properties in AGDevX.csproj**

Change:
```xml
<Version>0.0.1-alpha</Version>
...
<AssemblyVersion>0.0.0.1</AssemblyVersion>
<FileVersion>0.0.0.1</FileVersion>
```

To:
```xml
<Version>0.1.0-alpha</Version>
...
<AssemblyVersion>0.1.0.0</AssemblyVersion>
<FileVersion>0.1.0.0</FileVersion>
```

**Step 3: Build to verify**
```bash
dotnet build AGDevX.slnx -c Release
```

**Step 4: Commit and PR**
```bash
git add AGDevX/AGDevX.csproj
git commit -m "Bump version to 0.1.0-alpha"
git push -u origin ag/config/version-bump
gh pr create --title "Bump version to 0.1.0-alpha" --body "Version bump reflecting new features, tooling, and project maturity."
```

---

## Execution Order

**Wave 1 (parallel — independent files):** Tasks 1, 2, 5, 6, 7, 8
**Wave 2 (sequential — ClaimsPrincipal files):** Tasks 3, then 4
**Wave 3 (last — after all merges):** Task 9
