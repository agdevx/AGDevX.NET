# AGDevX .NET

AGDevX .NET is a collection of C# extension methods, utilities, and other helpful, foundational code that can help make developing applications more elegant.

# Features

The sections below describe AGDevX .NET's capabilities.

## Assemblies

### Assembly Extensions

- `FullNameStartsWithPrefix`: Determines if an assembly's FullName begins with the provided string prefix
- `FullNameStartsWithPrefixes`: Determines if an assembly's FullName begins with any of the provided string prefixes

### Assembly Utility

- `GetAssemblies`: Recursively retrieves all assemblies referenced by the provided parent assembly or the currently executing assembly
- `AssemblyNameStartsWithAnyPrefix`: Determines if an assembly's FullName begins with any of the provided prefixes

## Attributes

### Attribute Extensions

- `GetDescription`: Retrieves the value of the DescriptionAttribute for an objects of type T

## DateTimes

### DateTime Extensions

- `SpecifyKind`: Specifies the Kind of the DateTime

## Enums

### Enum Extensions

- `IsOneOf`: Determines if an Emum exists in a list of Enums

### EnumStringValueAttribute

Decorator to provide a string value to an Enum since C# does not support Enums with a string value

- `StringValue` (extension): Retrieves the string value for an Enum decorated with the EnumStringValueAttribute

## Exceptions

- `CodedApplicationException`: Base exception from which to inherit for coded application-specific exceptions. This inherits from ApplicationException and adds the concept of a "Code" that can provide additional context into the origin of the exception.
- `CodedArgumentNullException` Base exception from which to inherit for coded null argument exceptions. This inherits from ArgumentNullException and adds the concept of a "Code" that can provide additional context into the origin of the exception.
- `AcquireTokenException`: Exception to throw if an error occurs while acquiring a token from an authorization server
- `ApplicationStartupException`: Exception to throw during application start up if an error occurs during the application startup process
- `ClaimNotFoundException`: Exception to throw if a claim is unable to be retrieved from a ClaimsPrincipal
- `ExtensionMethodException`: Exception to throw if an error occurs during the execution of an extension method when no other more-specific exceptions are available
- `ExtensionMethodParameterNullException`: Exception to throw if a required parameter was not provided to an extension method
- `MissingRequiredClaimException`: Exception to throw if a required Claim is missing from a ClaimsPrincipal
- `NotAuthorizedException`: Exception to throw if the ClaimsPrincipal is attempting to access a resource or perform a function that it is not allowed to

### Exception Detail

Information relevant to a developer that has been extracted from an execption and put into an easy to consume model

- `GetExceptionDetail` (Exception extension method): Recursively retrieves structured information about an exception

## Guids

### Guid Extensions

- `IsEmpty`: Determines if a Guid is empty
- `IsNotEmpty`: Determines if a Guid is not empty
- `IsNull`: Determines if a Guid is null
- `IsNotNull`: Determines if a Guid is not null
- `IsNullOrEmpty`: Determines if a Guid is null or empty
- `IsNotNullNorEmpty`: Determines if a Guid is not null and not empty

## IEnumerables

### Dictionary Extensions

- `ReverseKeysAndValues`: Switches the place of the Dictionary's Keys and Values
- `Concatenate`: Appends one Dictionary to another

### IEnumerable Extensions

- `IsNullOrEmpty`: Determines if an IEnumerable is null or empty
- `HasCommonStringElement`: Determines if two string IEnumerables have a common element
- `ToDataTable`: Converts an IEnumerable of type T to a DataTable with the provided column name
- `ContainsIgnoreCase`: Determines if a string IEnumerable contains the provided string
- `AnySafe`: Determines if there are any records in an IEnumerable (will not blow up on null IEnumerables)

## Objects

### Object Extensions

- `IsNull`: Determines if an object is null
- `IsNotNull`: Determines if an object is not null

## Security

- `Auth0ClaimType`: Claim Types as defined by Auth0
- `CustomClaimType`: Claim Types as defined by AGDevX
- `JwtClaimType`: Standard JWT Claim Types

### ClaimsPrincipal Extensions

Extensions for pulling Claims out of a ClaimsPrincipal. Each claim has a `Get` variant (throws `ClaimNotFoundException` if missing) and a `TryGet` variant (returns null if missing).

#### Standard JWT Claims

- `GetIssuer` / `TryGetIssuer`: Returns the Issuer (iss) claim value
- `GetSubject` / `TryGetSubject`: Returns the Subject (sub) claim value
- `GetAudiences` / `TryGetAudiences`: Returns the Audience (aud) claim values as a list
- `GetExpiration` / `TryGetExpiration`: Returns the Expiration (exp) claim value as an integer
- `GetNotBefore` / `TryGetNotBefore`: Returns the NotBefore (nbf) claim value as an integer
- `GetIssuedAt` / `TryGetIssuedAt`: Returns the IssuedAt (iat) claim value as an integer
- `GetJwtId` / `TryGetJwtId`: Returns the JWT ID (jti) claim value

#### OpenID Connect Profile Claims

- `GetName` / `TryGetName`: Returns the Name claim value
- `GetGivenName` / `TryGetGivenName`: Returns the Given Name claim value
- `GetFamilyName` / `TryGetFamilyName`: Returns the Family Name claim value
- `GetMiddleName` / `TryGetMiddleName`: Returns the Middle Name claim value
- `GetNickname` / `TryGetNickname`: Returns the Nickname claim value
- `GetPreferredUsername` / `TryGetPreferredUsername`: Returns the Preferred Username claim value
- `GetProfile` / `TryGetProfile`: Returns the Profile claim value
- `GetPicture` / `TryGetPicture`: Returns the Picture claim value
- `GetWebsite` / `TryGetWebsite`: Returns the Website claim value
- `GetGender` / `TryGetGender`: Returns the Gender claim value
- `GetBirthdate` / `TryGetBirthdate`: Returns the Birthdate claim value as a DateTime
- `GetZoneInfo` / `TryGetZoneInfo`: Returns the ZoneInfo claim value
- `GetLocale` / `TryGetLocale`: Returns the Locale claim value
- `GetUpdatedAt` / `TryGetUpdatedAt`: Returns the UpdatedAt claim value as an integer

#### OpenID Connect Contact Claims

- `GetEmail` / `TryGetEmail`: Returns the Email claim value
- `GetEmailVerified` / `TryGetEmailVerified`: Returns the EmailVerified claim value as a boolean
- `GetPhoneNumber` / `TryGetPhoneNumber`: Returns the PhoneNumber claim value
- `GetPhoneNumberVerified` / `TryGetPhoneNumberVerified`: Returns the PhoneNumberVerified claim value as a boolean
- `GetAddress` / `TryGetAddress`: Returns the Address claim value as a JsonElement

#### OpenID Connect Authentication Claims

- `GetAuthorizedParty` / `TryGetAuthorizedParty`: Returns the Authorized Party (azp) claim value
- `GetNonce` / `TryGetNonce`: Returns the Nonce claim value
- `GetAuthTime` / `TryGetAuthTime`: Returns the Auth Time claim value as an integer
- `GetAccessTokenHash` / `TryGetAccessTokenHash`: Returns the Access Token Hash (at_hash) claim value
- `GetCodeHash` / `TryGetCodeHash`: Returns the Code Hash (c_hash) claim value
- `GetAuthenticationContextClassReference` / `TryGetAuthenticationContextClassReference`: Returns the Authentication Context Class Reference (acr) claim value
- `GetAuthenticationMethodsReference` / `TryGetAuthenticationMethodsReference`: Returns the Authentication Methods Reference (amr) claim value
- `GetSessionId` / `TryGetSessionId`: Returns the Session ID (sid) claim value

#### OAuth 2.0 Claims

- `GetScopes` / `TryGetScopes`: Returns the Scope claim values as a list
- `GetClientId` / `TryGetClientId`: Returns the Client ID claim value
- `GetRoles` / `TryGetRoles`: Returns the Roles claim values as a list

#### Custom / Provider-Specific Claims

- `GetExternalId` / `TryGetExternalId`: Returns an external identifier (tries Subject, then UserId)
- `GetIsActive` / `TryGetIsActive`: Returns the IsActive claim value as a boolean
- `GetGrantType` / `TryGetGrantType`: Returns the Grant Type claim value (Auth0-specific)

## Strings

### String Extensions

- `EqualsIgnoreCase`: Determines if two string are equal ignoring case
- `StartsWithIgnoreCase`: Determines if a string starts with another string ignoring case
- `ContainsIgnoreCase`: Determines if a string is contained within another string ignoring case
- `IsNullOrWhiteSpace`: Determines if a string is null or whitespace
- `IsNotNullNorWhiteSpace`: Determines if a string is not null and not whitespace
- `IsWhiteSpace`: Determines if a string only consists of whitespace
- `IsNotWhiteSpace`: Determines if a string contains at least one character that is not whitespace
- `IsEmpty`: Determines if a string is empty
- `IsNotEmpty`: Determines if a string is not empty
- `NullIfNullOrWhiteSpace`: Returns null if the string is null or whitespace. Otherwise, returns the string.

