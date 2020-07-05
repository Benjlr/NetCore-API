# NetCore-API
A C# REST API built using .Net Core

GET https://user-accounts-api.azurewebsites.net/users/listusers
Returns all users

POST https://user-accounts-api.azurewebsites.net/users/createuser
Inserts the user embedded the JSON content of the http request 
returns bad request if the user has any null fields, if the user email already exists or if the user salary or expenses is less than zero
returns user if successful

GET https://user-accounts-api.azurewebsites.net/users/getuser/myEmailAddress@email.com
returns the user with the specified email address
returns bad request if the user does not exist

GET https://user-accounts-api.azurewebsites.net/accounts/ListAccounts
returns a list of all accounts

POST https://user-accounts-api.azurewebsites.net/accounts/createaccount
If a user is posted in the request content, an account is created with the users email address
returns accout if successful
returns bad request if the the salary-expenses of the user is less than 1000
returns bad request if the user already has an account
returns bad request if the user does not exist in the user database, ensure the user is first created by posting a user to https://user-accounts-api.azurewebsites.net/users/createuser

GET https://user-accounts-api.azurewebsites.net/accounts/getaccount/myEmailAddress@email.com
if successful returns the accoutn with the specified email address
returns bad request if the account does not exist

Hosted at: https://user-accounts-api.azurewebsites.net
