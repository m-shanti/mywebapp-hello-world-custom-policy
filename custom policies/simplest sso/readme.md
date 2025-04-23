- input login
- the policy add claims `login` and some additional claim `lastname`
- sso should work
  - I can enter the same application in other tab without entering credentials
  - I can enter other application without entering credentials
- sso doesn't require saving any objects in B2C directory

# Remarks
- In example https://learn.microsoft.com/en-us/azure/active-directory-b2c/custom-policies-series-hello-world we set `<Protocol Name="None" />`. What does it mean? Here https://learn.microsoft.com/en-us/azure/active-directory-b2c/jwt-issuer-technical-profile we have: *The Name attribute of the Protocol element needs to be set to OpenIdConnect.*
- Orchestration step has one technical profiles referenced `<OrchestrationStep Order="1" Type="SendClaims" CpimIssuerTechnicalProfileReferenceId="JwtIssuer" />` that is of the type 'None'
- And Relying party has other `<TechnicalProfile Id="HelloWorldPolicyProfile">` that is of the type `OpenIdConnect`
