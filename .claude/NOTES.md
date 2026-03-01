# NOTES

## Tech Debt

- [ ] Document all ClaimsPrincipal extensions in README (there are 40+ Get/TryGet pairs; README only lists the section header)

## Observations

- ClaimsPrincipal test coverage appears complete for all existing Get/TryGet pairs (40 pairs, ~1627 lines of tests)
- README tech debt item "Unit tests to cover all ClaimsPrincipal extensions" may be outdated — verify and remove if so
- Feature branch `ag/NullIfNullOrWhiteSpace` exists with completed implementation; `[NotNullIfNotNull]` attribute is semantically incorrect and should be removed when merging
