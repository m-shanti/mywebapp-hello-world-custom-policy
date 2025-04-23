- input login
- the policy add claims `login` and some additional claim `lastname`
- sso should work
  - When I remove appllication cookie `.AspNetCore.Cookies` then roundtrip to B2C will occur (invisible) and I will enter the same application without entering credentials
  - I can enter other application without entering credentials
- sso doesn't require saving any objects in B2C directory

# Remarks
- In example https://learn.microsoft.com/en-us/azure/active-directory-b2c/custom-policies-series-hello-world we set `<Protocol Name="None" />`. What does it mean? Here https://learn.microsoft.com/en-us/azure/active-directory-b2c/jwt-issuer-technical-profile we have: *The Name attribute of the Protocol element needs to be set to OpenIdConnect.*
- Orchestration step has one technical profiles referenced `<OrchestrationStep Order="1" Type="SendClaims" CpimIssuerTechnicalProfileReferenceId="JwtIssuer" />` that is of the type 'None'
- And Relying party has other `<TechnicalProfile Id="HelloWorldPolicyProfile">` that is of the type `OpenIdConnect`
- For comparison in Ocean custom policy
  - User journey has technical profile `<UserJourney Id="AgentUserADJourney" DefaultCpimIssuerTechnicalProfileReferenceId="JwtIssuer">` that is of the type `OpenIdConnect` and that has connected `<UseTechnicalProfileForSessionManagement ReferenceId="SM-jwt-issuer" />`
  - Relying party has technical profile `<TechnicalProfile Id="PolicyProfile">` of the type `OpenIdConnect`
