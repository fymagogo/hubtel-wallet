# hubtel-wallet
 API service to be used to manage a user's wallet on the Hubtel app

## Specs
 - .Net6 Framework
 - PostgreSQL Database
 - Clean Architecture


### Migrations

- Running in _Package Manager Console_, change directory to `HubtelWallet` and set default project to `HubtelWallet.API`

_To add migrations_
```
dotnet ef migrations add InitialMigration -c AppDbContext --project .\HubtelWallet.Infrastructure -s .\HubtelWallet.API -o .\Persistence\Migrations
```

_To remove migrations_
```
dotnet ef migrations remove --project .\HubtelWallet.Infrastructure -c AppDbContext -s .\HubtelWallet.API
```
