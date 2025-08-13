This policy is based on `custom policies/save user to ad b2c/SaveUserToAdB2cCustomPolicy.xml`.

This policy is prepapared based on [Azure AD B2C MFA with TOTP using any Authenticator app][1]

First important question - **Can I enable B2C MFA when the user is not stored in B2C directory?**

We will save user object but not as **a user** but rather as **user profile**.

There are two ways:
- create from scratch starting from [TOTP display control][2]
- use custom policy defined in [Azure AD B2C MFA with TOTP using any Authenticator app][1]

This solution uses second approach. Due to this all TrustFramework parent policies are used.

In `custom policies/mfa authenticator totp/TrustFrameworkExtensions_TOTP.xml` users journey `SignUpOrSignInTOTP` is defined. In this solution this journey won't be used as we have our own steps reading or writing from/to AAD. Insted we will use only subjourneys `TotpFactor-Input` and `TotpFactor-Verify`.

Remember to place your tenant Id in the top of each file.

**Remark** 

When you set `PolicyId="TOTPCP_TrustFrameworkExtensions"` the result policy will have `PolicyId="B2C_1A_TOTPCP_TrustFrameworkExtensions"` and with such Id must be referenced.

**Error**

Activity Type: **Get available strong authentication devices**
Status reason: **The service received a bad request**

**Solution**

In all places referring to user you must change the id you reference by:
```
          <InputClaims>
            <InputClaim ClaimTypeReferenceId="login" PartnerClaimType="signInNames.userName" />
          </InputClaims>
```

Here [Bad request when trying to get available strong authentication devices][3] I read that in app insights there should be message like that `A partner claim with id 'userPrincipalName' is not found however is required in technical profile 'AzureMfa-GetAvailableDevices' - but I don't find it.


`

[Define a Microsoft Entra ID multifactor authentication technical profile in an Azure AD B2C custom policy][4]

[Enable multifactor authentication in Azure Active Directory B2C][5] - pack of custom policies from MS. This is MFA out of the box B2C. Full magical integrated with B2C user objects.

[DisplayControls][6] - among the others VerificationControl that validates one time codes. May be used separately without "box MFA"

[1]: https://github.com/azure-ad-b2c/samples/tree/master/policies/totp
[2]: https://learn.microsoft.com/en-us/azure/active-directory-b2c/display-control-time-based-one-time-password
[3]: https://stackoverflow.com/questions/72576057/bad-request-when-trying-to-get-available-strong-authentication-devices
[4]: https://learn.microsoft.com/en-us/azure/active-directory-b2c/multi-factor-auth-technical-profile
[5]: https://learn.microsoft.com/en-us/azure/active-directory-b2c/multi-factor-authentication?pivots=b2c-custom-policy
[6]: https://learn.microsoft.com/en-us/azure/active-directory-b2c/display-controls