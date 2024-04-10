# CryptLearn.Server
Celem pracy było stworzenie interaktywnej platformy internetowej do nauki kryptografii, umożliwiającej implementację algorytmów w postaci kodu. Platforma oferuje moduły edukacyjne składające się z części teoretycznej i praktycznej. Teoria zawiera kluczowe informacje, natomiast praktyka obejmuje przykładowe rozwiązanie, szablon rozwiązania i testy. Platforma umożliwia używanie różnych języków programowania, początkowo Python 3, z opcją dodawania kolejnych przez administratorów. Użytkownicy mają różne prawa w systemie, od których zależą dostępne funkcjonalności. Aplikacja w wersji podstawowej umożliwia tworzenie kont, logowanie, tworzenie i edycję modułów edukacyjnych, rozwiązywanie zadań w wybranych językach programowania i weryfikację rozwiązań w zabezpieczonym kontenerze. Serwis webowy posiada niezbędne mechanizmy bezpieczeństwa, chroniące przed nieautoryzowanym dostępem do danych. Projekt ma potencjał rozwoju poprzez dodawanie języków programowania, rozwijanie funkcji feedbacku, ulepszanie bezpieczeństwa i interfejsu użytkownika oraz do rozwinięcia mechanizmu praw użytkowników.


## Wymagania
- .Net 7
- MSSQL Server 16

## Jak uruchomić

1. Skonfiguruj odpowiednie parametry w pliku `./src/Bootstraper/appsettings.json`
1. Uruchom aplikację `dotnet run --launch-profile https --project .\src\Bootstraper\Bootstraper.csproj`

Sprawdź działanie aplikacji przechodząc do ścieżki `https://localhost:7299/swagger`, powinny się tam pojawić wszystkie endpointy aplikacji.

Zmiana adresu oraz portu uruchomienia aplikacji jest dostępna w pliku `./src/Bootstraper/Properties/launchSettings.json`
