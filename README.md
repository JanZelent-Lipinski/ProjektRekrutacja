# ProjektRekrutacja – aplikacja WPF (.NET Framework 4.8)

## Opis projektu

Aplikacja desktopowa napisana w technologii WPF (.NET Framework 4.8) z wykorzystaniem wzorca MVVM oraz Entity Framework 6.

Celem aplikacji jest połączenie z bazą danych systemu magazynowo-handlowego i umożliwienie wyszukiwania produktów na podstawie fragmentu nazwy lub kodu kreskowego. Wyniki prezentowane są w formie tabeli zawierającej podstawowe informacje o towarach.

## Funkcjonalności

* wyszukiwanie produktów po nazwie lub kodzie kreskowym (w czasie rzeczywistym)
* prezentacja danych w tabeli (DataGrid)
* integracja z bazą danych SQL (Subiekt Nexo)
* agregacja danych z wielu tabel (stan magazynowy, jednostka, waluta)
* możliwość czyszczenia pola wyszukiwania
* zastosowanie wzorca MVVM

## Zakres danych

Dla każdego produktu wyświetlane są:

* nazwa
* cena
* waluta
* stan magazynowy
* jednostka miary
* kod kreskowy

## Technologie

* C#
* WPF (Windows Presentation Foundation)
* .NET Framework 4.8
* Entity Framework 6
* SQL Server
* MVVM

## Wymagania

### Środowisko

* Visual Studio 2022 lub nowsze
* .NET Framework 4.8

### Baza danych

* zainstalowany system Subiekt Nexo 
* dostęp do SQL Server (np. lokalna instancja INSERTNEXO)

## Konfiguracja

W pliku App.config należy ustawić poprawny connection string:

```xml
<connectionStrings>
  <add name="SubiektDb"
       connectionString="Server=NAZWA_SERWERA;Database=NAZWA_BAZY;Trusted_Connection=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

Przykład:

```
Server=JANZELENT\INSERTNEXO;Database=Nexo_Demo_1;Trusted_Connection=True;
```

## Uruchomienie

1. Otworzyć projekt w Visual Studio
2. Ustawić poprawny connection string
3. Upewnić się, że baza danych jest dostępna
4. Uruchomić projekt 

## Architektura

Projekt wykorzystuje wzorzec MVVM:

* Model – reprezentacja danych z bazy (np. Asortyment, Waluta)
* ViewModel – logika aplikacji, zapytania LINQ, filtrowanie danych
* View – interfejs użytkownika (XAML)

## Uwagi techniczne

* aplikacja korzysta z rzeczywistej struktury bazy Subiekt Nexo
* dane są pobierane dynamicznie z bazy danych
* zastosowano zapytania LINQ tłumaczone na SQL przez Entity Framework
* wykorzystano JOIN oraz agregacje (SUM) do pobierania danych z wielu tabel


