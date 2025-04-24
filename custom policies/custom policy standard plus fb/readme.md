This is efect of tutorial: [Create and run your own custom policies in Azure Active Directory B2C](https://learn.microsoft.com/en-us/azure/active-directory-b2c/custom-policies-series-overview)

# TODO

- connect the policy with angular app
- refresh token check
- remove refresh tokens on logout
    - [Azure B2C revoke refresh tokens][1]
    - [Invalidate all refresh tokens for a user][2]
    - [user: invalidateAllRefreshTokens][3]
    - [Refresh token revocation in Azure AD B2C Custom    - Policy][4]
    - [Refresh token revocation in Azure AD B2C][5]
    - [A B2C IEF Custom Policy which invalidates all SSO session across all devices after the users refresh token has been revoked][6]

[1]: https://learn.microsoft.com/en-us/answers/questions/75189/azure-b2c-revoke-refresh-tokens
[2]: https://learn.microsoft.com/en-us/previous-versions/azure/ad/graph/api/users-operations#invalidate-all-refresh-tokens-for-a-user
[3]: https://learn.microsoft.com/en-us/graph/api/user-invalidateallrefreshtokens?view=graph-rest-beta&tabs=http
[4]: https://stackoverflow.com/questions/55166134/refresh-token-revocation-in-azure-ad-b2c-custom-policy
[5]: https://stackoverflow.com/questions/54887573/refresh-token-revocation-in-azure-ad-b2c
[6]: https://github.com/azure-ad-b2c/samples/tree/master/policies/revoke-sso-sessions