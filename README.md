# NetCore-API
A C# REST API built using .Net Core

GET https://user-accounts-api.azurewebsites.net/users/listusers
- Returns all users.

GET https://user-accounts-api.azurewebsites.net/users/getuser/myEmailAddress@email.com
- returns the user with the specified email address.
- returns bad request if the user does not exist.

POST https://user-accounts-api.azurewebsites.net/users/createuser
- Inserts the user embedded the JSON content of the http request.
- Returns bad request if the user has any null fields, if the user email already exists, or if the user salary or expenses is less than zero.
- Returns user if successful.

GET https://user-accounts-api.azurewebsites.net/accounts/ListAccounts
- Returns a list of all accounts.

GET https://user-accounts-api.azurewebsites.net/accounts/getaccount/myEmailAddress@email.com
- If successful, returns the account with the specified email address.
- Returns bad request if the account does not exist.

POST https://user-accounts-api.azurewebsites.net/accounts/createaccount
- If a user is posted in the request content, an account is created with the users email address.
- Returns account if successful.
- Returns bad request if the the salary-expenses of the user is less than 1000
- Returns bad request if the user already has an account
- Returns bad request if the user does not exist in the user database

Hosted at: https://user-accounts-api.azurewebsites.net

Sample UserModel http Payload for JSON content body:
{
    "EmailAddress": "test@email.com",
    "Name": "Test",
    "Salary": 9000,
    "Expenses": 6500
}
Sample AccountModel:
{
    "AccountOwner": "test@email.com",
    "Amount": 1000,
}
