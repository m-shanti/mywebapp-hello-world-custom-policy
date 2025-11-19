It's implementation of Client Credentials.
Based on https://github.com/azure-ad-b2c/samples/blob/master/policies/client_credentials_flow/ClientCredentialsFlow.xml but without inheritance:
```
  <BasePolicy>
    <TenantId>yourtenant.onmicrosoft.com</TenantId>
    <PolicyId>B2C_1A_TrustFrameworkExtensions</PolicyId>
  </BasePolicy>
```