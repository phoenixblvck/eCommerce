# My Awesome Online shop
## Running Api
run dotnet program `dotnet watch --no-hot-reload`
to add migrations `dotnet ef migrations add InitialCreate -p Infrastructure -s API -o Data/Migrations`
to run migrations `dotnet ef database update -p Infrastructure -s API`