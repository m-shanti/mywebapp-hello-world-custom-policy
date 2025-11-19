# ClientCredentialsCustomPolicy.xml

It's implementation of Client Credentials.
Based on https://github.com/azure-ad-b2c/samples/blob/master/policies/client_credentials_flow/ClientCredentialsFlow.xml but without inheritance:
```
  <BasePolicy>
    <TenantId>yourtenant.onmicrosoft.com</TenantId>
    <PolicyId>B2C_1A_TrustFrameworkExtensions</PolicyId>
  </BasePolicy>
```

# ClientCredentialsCustomPolicyPlusCustomParam.xml

Claim resolvers, this is it!
https://learn.microsoft.com/en-us/azure/active-directory-b2c/claim-resolver-overview

```
    <ClaimsProvider>
      <DisplayName>Runtime parameters</DisplayName>
      <TechnicalProfiles>
        <TechnicalProfile Id="Get-Parameters">
          <DisplayName>Profile to fill claims with parameter values</DisplayName>
          <Protocol Name="Proprietary"
            Handler="Web.TPEngine.Providers.ClaimsTransformationProtocolProvider, Web.TPEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" />
          <Metadata>
            <Item Key="IncludeClaimResolvingInClaimsHandling">true</Item>
          </Metadata>
          <OutputClaims>
            <OutputClaim ClaimTypeReferenceId="OnBehalfOf" DefaultValue="{oauth-kv:OnBehalfOf}"
              AlwaysUseDefaultValue="true" />
          </OutputClaims>
        </TechnicalProfile>
      </TechnicalProfiles>
    </ClaimsProvider>
```

and most important line

```
            <OutputClaim ClaimTypeReferenceId="OnBehalfOf" DefaultValue="{oauth-kv:OnBehalfOf}"
```

The solution from
https://blog.wojtek.pro/aad-b2c-quick-tips-query-string-parameters/

Client Cr request:
```
POST https://samplemwb2ctenant.b2clogin.com/samplemwb2ctenant.onmicrosoft.com/B2C_1A_ClientCredentialsCustomPolicyPlusCustomParam_H/oauth2/v2.0/token
Content-Type: application/x-www-form-urlencoded

client_id=f8f43193-dc83-49f2-a16a-27137bdd9767
    &scope=https://samplemwb2ctenant.onmicrosoft.com/753fd60d-bf11-48d8-9ef4-31ab1e3aac21/.default
    &client_secret=***
    &grant_type=client_credentials
&OnBehalfOf=user123
```
