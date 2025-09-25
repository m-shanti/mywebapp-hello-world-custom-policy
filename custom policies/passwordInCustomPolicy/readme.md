Problem przekazywania hasła pomiędzy krokami. MS pilnuje, aby claimy typu `password` nie wychodziły z kroku, w którym powstają. Jest na to obejście przy pomocy ClaimTransformation, ale w nim hasło widać w App Insight.

- `hiddenPasswordNew.xml` - pobranie hasła, dwa kolejne kroki i wydanie tokenu. Pokazuje, ze hasło nie pojawia się w logach.
- `hiddenPasswordNewAppInsight.xml` - dodanie App Insight
- `hiddenPasswordNewAccountWriteAppInsight.xml` - dodanie zapisu uzytkownika. Uzytkownik powstaje, ale jest "Disabled", bo nie dostal hasła. Hasło nie wyszło z pierwszego kroku.
  - `hiddenPasswordNewAccountAndPasswordWriteAppInsight.xml` - wstawienie zapisu hasła na koniec i podłaczenie do niego zapisu uzytkownika. W takim przypadku haslo zapisue sie poprawnie.
  - `hiddenPasswordNewAccountWritePasswordPassingAppInsight.xml` - dodane ClaimTransformations, które przepisuja hasło do claim `plaintextPassword` i przekazują az do konca. Uzytkownik jest poprawny. W logach App Insight przy DeveloperMode="Development" lub "Debugging" widać hasło

Referencje:

*The password claim value is a very important piece of information, so be very careful how you
handle it in your custom policy. For a similar reason, Azure AD B2C treats the password claim value
as a special value. When you collect the password claim value in the self-asserted technical
profile, that value is only available within the same technical profile or within a validation
technical profiles that are referenced by that same self-asserted technical profile. Once execution
of that self-asserted technical profile completes, and moves to another technical profile, the value
is lost.*

https://learn.microsoft.com/en-us/azure/active-directory-b2c/custom-policies-series-store-user

Implementacja przez ClaimTransformation:
https://stackoverflow.com/questions/51892902/can-you-pass-password-claims-between-steps-in-azure-ad-b2c-custom-policies
