# Use Case 1
## Description
This API provides paging features for the Stripe test API.
Features:
- fetch the balance;
- fetch the list of transactions;
- fetch the results devided by pages.

## Running locally
To run the application locally switch to the UC2 folder where UC2.sln solution file is located and run the command from the console:
```
dotnet run
```

## Example URLs
- https://<hostname:port>/stripe/balance?pageNumber=2&pageSize=3
- https://<hostname:port>/stripe/balancetransactions?pageNumber=1&pageSize=10