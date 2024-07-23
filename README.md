# PLATFORMA CROWDFUNDINGOWA OPARTA NA TECHNOLOGII BLOCKCHAIN

### 1. Wprowadzenie

#### 1.1 Opis Systemu

Celem jest utworzenie aplikacji pozwalającej użytkownikowi wspieranie ciekawych projektów używając do tego kryptowalut. W aplikacji użytkownik będzie mieć możliwość przeglądania projektów podzielonych na różne kategorie/dziedziny, obserwować interesujące go projekty, udzielać się na forum projektu, wystawiać ocenę a następnie jeżeli sie zdecyduje to wesprzeć go.

#### 1.2 Słownik Pojęć

-Użytkownik – osoba, która korzysta z programu/strony.

-System – program, który ma za zadanie ułatwić pracę firmy.

-Baza danych – program, który gromadzi dane i ułatwia dostęp do nich. Dzięki temu możliwa jest centralizacja danych.

-Serwer (host) – główny komputer, na którym rezydują programy i który udostępnia komputerom-klientom swoją funkcjonalność poprzez przeglądarkę internetową.

-Przeglądarka internetowa – program, który łączy się z serwerem, pobiera i wyświetla stronę internetową

-Klient – rozumiany w dwójnasób – jako osoba zlecająca firmie stworzenie projektu oraz jako komputer, który korzysta z usług innego komputera (serwera).

-Architektura klient – serwer – forma komunikacji upraszczających model wzorcowy ISO OSI z siedmiu do 3 warstw: fizycznej, łącza danych oraz sesji realizowanej za pomocą protokołu zamówienie – odpowiedź.  Centralizuje usługi, dzięki czemu firma korzysta zawsze z tej samej wersji oprogramowania. W naszym przypadku usługą tą jest udostępnianie stron html.

-architektura – schemat ogólny budowy systemu komputerowego lub jego części, określający jego elementy, układy ich łączące i zasady współpracy między nimi.

#### 2.1 Obiekty Binesowe

| Nazwa:|  <img width=1000/>|         
|:------|:----------------------------------------------------------------------|
| Opis: | |

| Nazwa:|  <img width=1000/>|         
|:------|:----------------------------------------------------------------------|
| Opis: | |

| Nazwa:| <img width=1000/>|         
|:------|:----------------------------------------------------------------------|
| Opis: | |

#### 2.2 Aktorzy Biznesowi

| Nazwa:| Użytkownik Niezalogowany <img width=1000/>                            |
|:------|:----------------------------------------------------------------------|
| Opis: |Użytkownik który nie zalogował sie lub nie wykreował swojego konta, <br> może przeglądać dostępne projekty. |

| Nazwa:| Użytkownik Zalogowany <img width=1000/>                               |
|:------|:----------------------------------------------------------------------|
| Opis: |Użytkownik który wykreował swoje konto ale także się zalogował ma pełny dostęp do oferowanej treśći. |

| Nazwa:| Administrator          <img width=1000/>                              |
|:------|:----------------------------------------------------------------------|
| Opis: |Posiada pełen dostęp do serwisu. Służy jako pomoc techniczna dla użytkowników. |

### 3.Wymagania

#### 3.1 Wymagania Funkcjonalne

| ID:        | 1                   <img width=1000/>                                 |
|:-----------|:----------------------------------------------------------------------|
| Nazwa:     |Tworzenie konta                                                        |
| Priorytet: |Wysoki                                                                 |
| Rola:      |Wszyscy                                                                |
| Opis:      |Aplikcja oferuje tworzenie konta poprzez podane przez użytkownika dane.|

| ID:        | 2                   <img width=1000/>                                                                       | 
|:-----------|:------------------------------------------------------------------------------------------------------------|
| Nazwa:     |Logowanie                                                                                                    |
| Priorytet: |Wysoki                                                                                                       |
| Rola:      |Wszyscy                                                                                                      |
| Opis:      |Za pomocą podanych przy logowaniu danych uwierzytelniającyh, użytkownik uzyskuje dostęp do swojego konta.    |

#### 3.2 Wymagania Niefunkcjonalne

| ID:        | 1                       <img width=1000/>                                  |
|:-----------|:---------------------------------------------------------------------------|
| Nazwa:     |Podział odpowiedzalnośći                                                    |
| Priorytet: |Wysoki                                                                      |
| Opis:      |Platforma ma być podzielona na część frontendową jak i backendową. |

| ID:        | 2                       <img width=1000/>                                  |
|:-----------|:---------------------------------------------------------------------------|
| Nazwa:     |Łatwość w użyciu                                                            |
| Priorytet: |Wysoki                                                                      |
| Opis:      |Platforma ma być przyjazna dla użytkownika. Interfejs musi być nowoczesny i przejrzysty.                                                                              |

| ID:        | 3                      <img width=1000/>                                   |
|:-----------|:---------------------------------------------------------------------------|
| Nazwa:     |Intergralność danych                                                        |
| Priorytet: |Średni                                                                      |
| Opis:      |Zabezpieczenie przed nieautoryzowaną zmianą danych przez użycie szyfrowania haseł oraz implementacja szyfrowanego połączenia https.                                   |

| ID:        | 4                       <img width=1000/>                                  |
|:-----------|:---------------------------------------------------------------------------|
| Nazwa:     |Przeładowanie Stron                                                         |
| Priorytet: |Średni                                                                      |
| Opis:      |Platforma powinna wczytywać się raz (brak przeładowywania podstron).        |

### 4. Technologia i Architektura

#### 4.1. Technologie
-ASP.NET

-React.JS

-MySQL

#### 4.2. Opis Architektury

![image](https://user-images.githubusercontent.com/56208135/139593932-508362fd-074d-4bef-b898-e03149c3f736.png)

-Za frontend będzie odpowiadać Reacj.JS w którym zostanie napisany interfejs użytkownika z którym to będzie mógł wchodzić w interakcję.

-Za backend odpowiada ASP.NET w którym zostanie napisany Web API. W ASP.NET zostanie wykonana mechanika strony oraz połączenie z bazą danych. 

-Za przechowywanie danych będzie odpowiadać baza danych MySQL. Będą się w niej znajodwać informacje o klientach, subksrybentach oraz książkach.


![image](https://user-images.githubusercontent.com/54260979/142664020-e094f156-357d-4a81-a958-3389dd7c21a1.png)

### 5. Lista przypdków użycia

W systemie istnieją dwa rodzaje użytkowników: Administrator oraz Użytkownik. Poniższa tabela przedstawia funkcjonalności poszczególnych ról.

|Funkcjonalność | Użytkownik | Administrator|
|:-------------:|:----------:|:------------:|
|Rejestracja użytkownika | + | - |
|Logowanie | + | + |
|Wylogowanie  | + | + |
|Edycja danych profilu | + | + |
|Wyszukiwanie pozycji | + | + |
|Dodanie recenzji | + | - | 
|Dodanie pozycji do koszyka | + | - |
|Usuwanie pozycji z koszyka | + | - |
|Złożenie zamówienia | + | - | 
|Historia zamówień | + | - |
|Kontakt z administratorem | + | - |
|Dodawanie pozycji do systemu | - | + | 
|Modyfikowanie pozycji z systemu | - | + |
|Usuwanie pozycji z sytemu | - | + |
|Przeglądanie użytkowników | - | + | 
|Usuwanie użytkownika | - | + |
|Przyjmowanie zgłoszeń | - | + |

### 6. Przypadki użycia

### Tworzenie Konta Użytkownika

* **Cel**: 
        Celem tej funkcjonalności jest stworzenie konta dla nowego użytkownika w celu możliwości zalogowania się do systemu.
        
* **Aktorzy**:
        Administrator
                        
* **Główny Scenariusz**:
      
        - Krok 1: Administrator przechodzi do panelu administracyjnego.

        - Krok 2: Administrator klika przycisk "Dodaj Użytkownika"
        
        - Krok 3: Administrator wypełnia wymagane dane w formularzu.
        
        - Krok 4: Administrator klika przycisk "Zatwierdź".
        
        - Krok 5: Sprawdzana jest poprawność wprowadzonych danych.
        
        - Krok 6: Otrzymuje informację o pozytywnym wykreowaniu konta.

* **Rozszerzenia**:
        
        - Krok 5.1: Otrzymuje informację o wprowadzeniu niepoprawnych danych lub się powtarzają.

        - Krok 5.2: System prosi o ponowne wprowadzenie danych.
        
* **Przykład**:

<p align="center">
  <img src="https://user-images.githubusercontent.com/56208135/165714270-a657857e-57c7-4bc1-9fb2-f04f71dafa3b.png" />
</p>
        
- - - 

### Logowanie

* **Cel**: 
       Celem tej funkcjonalności jest umożliwienie użytkownikowi/administratorowi uzyskania dostępu do systemu.
        
* **Aktorzy**:
        Administrator/Użytkownik   
                
* **Główny Scenariusz**:
      
        - Krok 1: Użytkownikowi/Administratorowi wyświetla się ekran z możliwością zarejestrowania lub zalogowania.
        
        - Krok 2: Użytkownik/Administrator wypełnia dane w formularzu logowania.
        
        - Krok 3: Sprawdzana jest poprawność wprowadzonych danych.
        
        - Krok 4: Użytkownik/Administrator zostaje pomyślnie zalogowany.

* **Rozszerzenia**:
        
        - Krok 3.1: Otrzymuje informację o wprowadzeniu niepoprawnych danych lub się powtarzają.

        - Krok 3.2: System prosi o ponowne wprowadzenie danych.

* **Przykład**:

<p align="center">
  <img src="https://user-images.githubusercontent.com/56208135/165717473-0ae1b1a6-0fd9-4f63-aae7-2277e8e0637c.png" />
</p>

- - - 

### Wylogowanie

* **Cel**: 
        Celem tej funkcjonalności jest umożliwienie użytkownikowi/administratorowi wylogowanie się z systemu tym samym zakończenie sesji korzystania z sytemu i zabezpieczenie swojego konta.
     
* **Aktorzy**:
        Administrator/Użytkownik
                
* **Główny Scenariusz**:

        - Krok 1: Użytkownik/Administrator klika przycisk wyloguj.

        - Krok 2: Zamykana jest sesja określonego użytkownika/administratora.

        - Krok 3: Komunikat o poprawynm wylogowaniu.
        
       
* **Przykład**:

<p align="center">
  <img src="https://user-images.githubusercontent.com/56208135/165771164-1de77e13-8ec6-43b5-8e27-7a90679790b2.png" />
</p>

- - -
### Aktualizacja danych użytkownika

* **Cel**: 
        Celem tej funkcjonalności jest umożliwienie administratorowi zmiany danych(e-mail, hasło, operator) użytkownika.
     
* **Główny Aktor**:
        Administrator
                
* **Główny Scenariusz**:

        - Krok 1: Administrator przechodzi do panelu administracyjnego.

        - Krok 2: Wybiera użytkownika któremu chce zmienić dane.

        - Krok 3: Wprowadza nowe dane. 
       
        - Krok 4: Administrator zatwierdza.
        
        - Krok 5: Sprawdzana jest poprawność wprowadzonych danych.
        
        - Krok 6: Komunikat o poprawnej operacji.

* **Rozszerzenia**:
        
        - Krok 5.1: Otrzymuje informację o wprowadzeniu niepoprawnych danych lub się powtarzają.

        - Krok 5.2: System prosi o ponowne wprowadzenie danych.
       
* **Przykład**:

<p align="center">
  <img src="https://user-images.githubusercontent.com/56208135/165722805-7997efdf-9c1e-46c6-b46d-e8fef952a646.png" />
</p>

---
