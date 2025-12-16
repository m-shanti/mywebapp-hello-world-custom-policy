# Description

Custom policies gets:
- `message`
- `bearerToken`

from the user and send them to mock server
- http://restfullprovider-get.free.beeceptor.com/{message}

`message` is sent in Url and `bearerToken` is sent as `Authorization` header.

You can run custom policy and see result in beeceptor https://app.beeceptor.com/console/restfullprovider-get:
- it gets `message` from Url
- and Authorization header


![beeceptor](beeceptor.png)



[x] Visual Studio

[x] Simple hello

[x] Prepare mock on https://free.beeceptor.com

[x] PoC on passing values in GET

[ ] PoC on passing Correlation ID

https://learn.microsoft.com/en-us/azure/active-directory-b2c/troubleshoot?pivots=b2c-custom-policy

```
{
  "exp": 1765921450,
  "nbf": 1765917850,
  "ver": "1.0",
  "iss": "https://samplemwb2ctenant.b2clogin.com/08beb0c6-8f0a-44e8-9771-54d58fbf5009/v2.0/",
  "sub": "dffgd",
  "aud": "f8f43193-dc83-49f2-a16a-27137bdd9767",
  "acr": "b2c_1a_callrestfulprovidersendcorrelationid_b",
  "nonce": "defaultNonce",
  "iat": 1765917850,
  "auth_time": 1765917850,
  "message": "dffgd",
  "correlationId": "1492b04d-e4a0-4aa7-9906-99421d031de0"
}
```

https://learn.microsoft.com/en-us/azure/active-directory-b2c/claim-resolver-overview