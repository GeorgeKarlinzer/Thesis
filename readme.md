# CryptLearn.Server
Część backendowa rozszerzalnej platformy do nauki algorytmów kryptograficznych.


## Wymagania
- .Net 7
- MSSQL Server 16

## Jak uruchomić

1. Skonfiguruj odpowiednie parametry w pliku `./src/Bootstraper/appsettings.json`
1. Uruchom aplikację `dotnet run --launch-profile https --project .\src\Bootstraper\Bootstraper.csproj`

Sprawdź działanie aplikacji przechodząc do ścieżki `https://localhost:7299/swagger`, powinny się tam pojawić wszystkie endpointy aplikacji.

Zmiana adresu oraz portu uruchomienia aplikacji jest dostępna w pliku `./src/Bootstraper/Properties/launchSettings.json`