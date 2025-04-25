Based on `custom policies/simplest sso/SimplestSsoCustomPolicy.xml`

Read https://learn.microsoft.com/en-us/azure/active-directory-b2c/custom-policies-series-store-user

The flow:
- read the user using `<TechnicalProfile Id="AAD-UserRead">`. It sets `objectId` claim that indicates that user is found
- if `objectId` is not set, create user using `<TechnicalProfile Id="AAD-UserWrite">`


