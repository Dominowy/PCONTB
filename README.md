# PLATFORMA CROWDFUNDINGOWA OPARTA NA TECHNOLOGII BLOCKCHAIN

### 1. Wprowadzenie

#### 1.1 Opis Systemu

Celem jest utworzenie aplikacji pozwalającej użytkownikowi wspieranie ciekawych projektów używając do tego kryptowalut. W aplikacji użytkownik będzie mieć możliwość przeglądania projektów podzielonych na różne kategorie/dziedziny, obserwować interesujące go projekty, udzielać się na forum projektu, wystawiać ocenę a następnie jeżeli sie zdecyduje to wesprzeć go.

#### 1.2 Słownik Pojęć

-Użytkownik – osoba, która korzysta z programu/strony.

-Baza danych – program, który gromadzi dane i ułatwia dostęp do nich. Dzięki temu możliwa jest centralizacja danych.

-Serwer (host) – główny komputer, na którym rezydują programy i który udostępnia komputerom-klientom swoją funkcjonalność poprzez przeglądarkę internetową.

-Przeglądarka internetowa – program, który łączy się z serwerem, pobiera i wyświetla stronę internetową

-Klient – rozumiany w dwójnasób – jako osoba zlecająca firmie stworzenie projektu oraz jako komputer, który korzysta z usług innego komputera (serwera).

-Architektura klient <-> serwer – forma komunikacji upraszczających model wzorcowy ISO OSI z siedmiu do 3 warstw: fizycznej, łącza danych oraz sesji realizowanej za pomocą protokołu zamówienie – odpowiedź.  Centralizuje usługi, dzięki czemu firma korzysta zawsze z tej samej wersji oprogramowania. W naszym przypadku usługą tą jest udostępnianie stron html.

-Architektura – schemat ogólny budowy systemu komputerowego lub jego części, określający jego elementy, układy ich łączące i zasady współpracy między nimi.

#### 2.1 Obiekty Binesowe

| Nazwa:| Discover <img width=1000/>|         
|:------|:----------------------------------------------------------------------|
| Opis: | Miejsce, w którym użytkownik może przeglądać dostępne projekty oraz ich kampanie, sprawdzać cele, postępy oraz użytkowników odpowiedzialnych za projekt. |

| Nazwa:| Project managment <img width=1000/>|         
|:------|:----------------------------------------------------------------------|
| Opis: | Miejsce, w którym użytkownik może tworzyć nowe projekty lub zarządzać istniejącymi (edycja, usuwanie, aktualizacja statusów itp.). |

| Nazwa:| User settings <img width=1000/>|         
|:------|:----------------------------------------------------------------------|
| Opis: | Miejsce, w którym użytkownik może edytować ustawienia swojego konta, zarządzać profilem oraz preferencjami. |

| Nazwa:| Admin/Moderator Panel <img width=1000/>|         
|:------|:----------------------------------------------------------------------|
| Opis: | Panel administracyjny dla moderatorów i administratorów, umożliwiający zarządzanie użytkownikami i zgłoszeniami. |

| Nazwa:| Ledger(portfel) <img width=1000/>|         
|:------|:----------------------------------------------------------------------|
| Opis: | Moduł odpowiedzialny za zarządzanie finansami: przegląd salda, historia transakcji, wpłaty, wypłaty oraz integrację z systemem płatności. |

#### 2.2 Aktorzy Biznesowi

| Nazwa:| Użytkownik Niezalogowany <img width=1000/>                            |
|:------|:----------------------------------------------------------------------|
| Opis: |Użytkownik który nie zalogował sie lub nie wykreował swojego konta, <br> może przeglądać dostępne projekty. |

| Nazwa:| Użytkownik Zalogowany <img width=1000/>                               |
|:------|:----------------------------------------------------------------------|
| Opis: |Użytkownik który wykreował swoje konto ale także się zalogował ma pełny dostęp do oferowanej treśći. |

| Nazwa:| Moderator          <img width=1000/>                                  |
|:------|:----------------------------------------------------------------------|
| Opis: | Użytkownik z uprawnieniami administracyjnymi w ograniczonym zakresie. Może wspierać użytkowników, moderować treści i zgłoszenia, ale nie zarządza całym systemem. |

| Nazwa:| Administrator          <img width=1000/>                    |
|:------|:----------------------------------------------------------------------|
| Opis: | Użytkownik z najwyższym poziomem uprawnień. Ma dostęp do wszystkich funkcji systemu, zarządza użytkownikami oraz konfiguracją systemu. |

### 3.Wymagania

#### 3.1 Wymagania Funkcjonalne

| ID:        | 1                   <img width=1000/>                                 |
|:-----------|:----------------------------------------------------------------------|
| Nazwa:     |Tworzenie konta                                                        |
| Priorytet: |Wysoki                                                                 |
| Rola:      |Użytkownik                                                             |
| Opis:      |Aplikcja oferuje tworzenie konta poprzez podane przez użytkownika dane.|

| ID:        | 2                   <img width=1000/>                                                                       | 
|:-----------|:------------------------------------------------------------------------------------------------------------|
| Nazwa:     |Logowanie                                                                                                    |
| Priorytet: |Wysoki                                                                                                       |
| Rola:      |Wszyscy                                                                                                      |
| Opis:      |Za pomocą podanych przy logowaniu danych uwierzytelniającyh, użytkownik uzyskuje dostęp do swojego konta.    |

| ID:        | 3                 <img width=1000/>                                                                         | 
|:-----------|:------------------------------------------------------------------------------------------------------------|
| Nazwa:     |Zarządzanie kontem                                                                                           |
| Priorytet: |Wysoki                                                                                                       |
| Rola:      |Wszyscy                                                                                                      |
| Opis:      |Użytkownik może aktualizować swoje dane osobowe, hasło oraz ustawienia profilu.                              |

| ID:        | 4                <img width=1000/>                                                                          | 
|:-----------|:------------------------------------------------------------------------------------------------------------|
| Nazwa:     |Wyszukiwane/Przeglądanie kampani                                                                             |
| Priorytet: |Wysoki                                                                                                       |
| Rola:      |Wszyscy                                                                                                      |
| Opis:      |Użytkownik może wyszukiwać i przeglądać dostępne kampanie oraz szczegóły projektów.                          |

| ID:        | 5               <img width=1000/>                                                                           | 
|:-----------|:------------------------------------------------------------------------------------------------------------|
| Nazwa:     |Dodawanie projektów                                                                                          |
| Priorytet: |Wysoki                                                                                                       |
| Rola:      |Użytkownik                                                                                                   |
| Opis:      |Użytkownik może tworzyć nowe projekty.                                                                       |

| ID:        | 6               <img width=1000/>                                                                           | 
|:-----------|:------------------------------------------------------------------------------------------------------------|
| Nazwa:     |Wspieranie projektów                                                                                         |
| Priorytet: |Wysoki                                                                                                       |
| Rola:      |Użytkownik                                                                                                   |
| Opis:      |Użytkownik może finansowo wspierać projekty poprzez system płatności.                                        |

| ID:        | 7               <img width=1000/>                                                                           | 
|:-----------|:------------------------------------------------------------------------------------------------------------|
| Nazwa:     |Zarządzanie projektami                                                                                       |
| Priorytet: |Wysoki                                                                                                       |
| Rola:      |Użytkownik                                                                                                   |
| Opis:      |Użytkownik może edytować, aktualizować i usuwać własne projekty. Dodawać kollaboratorów, zdjęcia oraz opisy. |

| ID:        | 8              <img width=1000/>                                                                            | 
|:-----------|:------------------------------------------------------------------------------------------------------------|
| Nazwa:     |Moderowanie aplikacji                                                                                        |
| Priorytet: |Wysoki                                                                                                       |
| Rola:      |Moderator                                                                                                    |
| Opis:      |Moderator może przeglądać zgłoszenia, zarządzać treściami i wspierać użytkowników.                           |

| ID:        | 9             <img width=1000/>                                                                             | 
|:-----------|:------------------------------------------------------------------------------------------------------------|
| Nazwa:     |Administrowanie aplikacji                                                                                    |
| Priorytet: |Wysoki                                                                                                       |
| Rola:      |Administrator                                                                                                |
| Opis:      |Administrator zarządza użytkownikami oraz konfiguracją systemu.                                              |

| ID:        | 10             <img width=1000/>                                                                            | 
|:-----------|:------------------------------------------------------------------------------------------------------------|
| Nazwa:     |Przegląd portfela (Ledger)                                                                                   |
| Priorytet: |Wysoki                                                                                                       |
| Rola:      |Użytkownik                                                                                                   |
| Opis:      |Użytkownik może przeglądać saldo, historię transakcji i dokonywać operacji finansowych.                      |

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
| Opis:      |Platforma ma być przyjazna dla użytkownika. Interfejs musi być nowoczesny i przejrzysty. |

| ID:        | 3                      <img width=1000/>                                   |
|:-----------|:---------------------------------------------------------------------------|
| Nazwa:     |Intergralność danych                                                        |
| Priorytet: |Średni                                                                      |
| Opis:      |Zabezpieczenie przed nieautoryzowaną zmianą danych przez użycie szyfrowania haseł oraz implementacja szyfrowanego połączenia https. |

| ID:        | 4                       <img width=1000/>                                  |
|:-----------|:---------------------------------------------------------------------------|
| Nazwa:     |Przeładowanie Stron                                                         |
| Priorytet: |Średni                                                                      |
| Opis:      |Platforma powinna wczytywać się raz (brak przeładowywania podstron).        |

### 4. Technologia i Architektura

#### 4.1. Technologie
-ASP.NET

-VUE.JS

-VITE.JS

-PostgreSQL

-Solana

#### 4.2. Opis Architektury

System zostanie podzielony na dwie oddzielne aplikacje Panel oraz Ledger.

1. Panel - jest to część z którą to użytkownik będzie wchodził w interakcję podzielona jest na backend oraz frontend.
- Backend - ASP.NET - napisane zostanie w nim WEB.API mechanika strony, połączenie z bazą danych oraz połączenie z Ledgerem. Backend zostanie podzielony na warstwy zgodnie z czystą architekturą.
- Frontend - VUE.JS - zostanie w nim stworzony interfejs użytkownika w komunikacji z backendem będzie pośredniczył VITE.JS w którym zostanie skonfigurowane proxy.
- Baza danych - PostgreSQL - odpowiedzialny z przechowywanie danych o projektach oraz użytkownikach
2. Ledger - w nim znajdą się informację o płatnościach oraz transakcjach backend będzie wymieniał z nim dane na temat transakcji.

![image](https://github.com/user-attachments/assets/0e996372-d122-495d-b421-495c441211e9)

![image](https://github.com/user-attachments/assets/d6a14862-fe50-425d-8d22-e14a0f15cd12)


### 5. Lista przypdków użycia

|Funkcjonalność | Użytkownik | Moderator | Administrator|
|:-------------:|:----------:|:------------:|:------------:|
|Rejestracja | + | - | - |
|Logowanie | + | + | + |
|Wylogowanie sesji| + | + | + |
|Aktualizacja danych konta | + | + | + |
|Zmiana hasła konta | + | + | + |
|Zamknięcie konta | + | + | + |
|Wyświetlanie użytkownika | + | + | + |
|Zarządzanie sesjami | + | + | + |
|Wyszukiwanie projektów | + | + | + |
|Utworzenie projektów | + | - | - |
|Edycja projektów | + | - | - |
|Usuwanie projektów | + | - | - |
|Dodawanie kollaboratów | + | - | - |
|Zarządzanie kollaboratorami | + | - | - |
|Usuwanie kollaboratorów | + | - | - |
|Dodawanie zdjęć projektów | + | - | - |
|Edycja zdjęć projektów | + | - | - |
|Usuwanie zdjęć projektów | + | - | - |
|Tworzenie opisu projektów | + | - | - |
|Edycja opisu projektów | + | - | - |
|Wsparcie projektów | + | - | - |
|Wypłata środków z kampanii | + | - | - |
|Przegląd portfela z kampanii | + | - | - |
|Dodawanie użytkowników | - | - | + |
|Blokada użytkowników | + | + | + |
|Odblokowanie użytkowników | - | + | + |
|Zmiana uprawnień użytkownika | - | - | + |


### 6. Przypadki użycia

### Rejestracja

* **Cel**: 
        Celem tej funkcjonalności jest rejestracja konta dla nowego użytkownika w celu możliwości zalogowania.
        
* **Aktorzy**:
        Administrator
                        
* **Główny Scenariusz**:
      
        - Krok 1: Użytkownik przechodzi do logowanie.

        - Krok 2: Klika przycisk link Rejestruj
        
        - Krok 3: Użytkownik wypełnia wymagane dane w formularzu.
        
        - Krok 4: Użytkownik klika przycisk "Rejestruj".
        
        - Krok 5: Sprawdzana jest poprawność wprowadzonych danych.
        
        - Krok 6: Otrzymuje informację o pozytywnym wykreowaniu konta.

* **Rozszerzenia**:
        
        - Krok 5.1: Otrzymuje informację o wprowadzeniu niepoprawnych danych lub się powtarzają.

        - Krok 5.2: System prosi o ponowne wprowadzenie danych.
        
* **Przykład**:

<p align="center">
  <img src="https://user-images.githubusercontent.com/56208135/165714270-a657857e-57c7-4bc1-9fb2-f04f71dafa3b.png" />
</p>
        
---
### Logowanie

* **Cel**: 
       Celem tej funkcjonalności jest umożliwienie użytkownikowi/moderatorowy/administratorowi uzyskania dostępu.
        
* **Aktorzy**:
        Użytkownik/Moderator/Administrator
                
* **Główny Scenariusz**:
      
        - Krok 1: Użytkownik/Moderator/Administrator wyświetla się ekran z możliwością zarejestrowania lub zalogowania.
        
        - Krok 2: Użytkownik/Moderator/Administrator wypełnia dane w formularzu logowania.
        
        - Krok 3: Sprawdzana jest poprawność wprowadzonych danych.
        
        - Krok 4: Użytkownik/Moderator/Administrator zostaje pomyślnie zalogowany.

* **Rozszerzenia**:
        
        - Krok 3.1: Otrzymuje informację o wprowadzeniu niepoprawnych danych lub się powtarzają.

        - Krok 3.2: System prosi o ponowne wprowadzenie danych.

* **Przykład**:

<p align="center">
  <img src="https://user-images.githubusercontent.com/56208135/165717473-0ae1b1a6-0fd9-4f63-aae7-2277e8e0637c.png" />
</p>

---
### Wylogowanie

* **Cel**: 
        Celem tej funkcjonalności jest umożliwienie użytkownikowi/moderatorowt/administratorowi wylogowanie się z systemu tym samym zakończenie sesji korzystania z sytemu i zabezpieczenie swojego konta.
     
* **Aktorzy**:
        Użytkownik/Moderator/Administrator
                
* **Główny Scenariusz**:

        - Krok 1: Użytkownik/Moderator/Administrator klika przycisk wyloguj.

        - Krok 2: Zamykana jest sesja określonego Użytkownik/Moderator/Administrator.

        - Krok 3: Komunikat o poprawynm wylogowaniu.
        
       
* **Przykład**:

<p align="center">
  <img src="https://user-images.githubusercontent.com/56208135/165771164-1de77e13-8ec6-43b5-8e27-7a90679790b2.png" />
</p>

---
### Aktualizacja danych konta

* **Cel**: 
        Celem tej funkcjonalności jest umożliwienie użytkownikowi/moderatorowt/administratorowi zmiany danych(e-mail, nazwa) użytkownika.
     
* **Główny Aktor**:
        Administrator
                
* **Główny Scenariusz**:

        - Krok 1: Użytkownik/Moderator/Administrator przechodzi do ustawień.

        - Krok 2: Wprowadza nowe dane.
       
        - Krok 3: Użytkownik/Moderator/Administrator zapisuje.
        
        - Krok 4: Sprawdzana jest poprawność wprowadzonych danych.
        
        - Krok 5: Komunikat o poprawnej operacji.

* **Rozszerzenia**:
        
        - Krok 5.1: Otrzymuje informację o wprowadzeniu niepoprawnych danych lub się powtarzają.

        - Krok 5.2: System prosi o ponowne wprowadzenie danych.
       
* **Przykład**:

<p align="center">
  <img src="https://user-images.githubusercontent.com/56208135/165722805-7997efdf-9c1e-46c6-b46d-e8fef952a646.png" />
</p>

---
###Zmiana hasła konta

* **Cel**:  
  Celem tej funkcjonalności jest umożliwienie użytkownikowi/moderatorowi/administratorowi zmiany hasła konta.

* **Główny Aktor**:  
  Użytkownik

* **Główny Scenariusz**:
  - Krok 1: Użytkownik/Moderator/Administrator przechodzi do ustawień.
  - Krok 2: Wprowadza stare i nowe hasło.
  - Krok 3: Zapisuje.
  - Krok 4: Sprawdzana jest poprawność starego hasła i siła nowego.
  - Krok 5: Komunikat o poprawnej operacji.

* **Rozszerzenia**:
  - Krok 4.1: Niepoprawne stare hasło – informacja o błędzie.
  - Krok 4.2: Zbyt słabe hasło – system prosi o mocniejsze.

* **Przykład**:
  
---
### Zamknięcie konta

* **Cel**:  
  Umożliwienie użytkownikowi trwałego zamknięcia konta.

* **Główny Aktor**:  
  Użytkownik

* **Główny Scenariusz**:
  - Krok 1: Użytkownik przechodzi do ustawień konta.
  - Krok 2: Wybiera opcję zamknięcia konta.
  - Krok 3: Potwierdza decyzję.
  - Krok 4: System usuwa lub dezaktywuje konto.
  - Krok 5: Komunikat o poprawnej operacji.

* **Rozszerzenia**:
  - Krok 3.1: Użytkownik anuluje proces – konto pozostaje aktywne.
  - Krok 4.1: Konto powiązane z aktywnymi kampaniami – informacja o konieczności ich zamknięcia.
    
* **Przykład**:
  
---
### Wyświetlanie użytkownika

* **Cel**:  
  Umożliwienie przeglądania publicznego profilu innego użytkownika.

* **Główny Aktor**:  
  Użytkownik

* **Główny Scenariusz**:
  - Krok 1: Użytkownik otwiera kampanię lub projekt.
  - Krok 2: Kliknięcie na nazwę użytkownika przekierowuje do jego profilu.
  - Krok 3: System wyświetla dane publiczne profilu.

* **Rozszerzenia**:
  - Krok 3.1: Konto ukryte lub zablokowane – informacja o braku dostępu.
    
* **Przykład**:
  
---
###  Zarządzanie sesjami

* **Cel**:  
  Umożliwienie zarządzania aktywnymi sesjami logowania.

* **Główny Aktor**:  
  Użytkownik

* **Główny Scenariusz**:
  - Krok 1: Użytkownik przechodzi do ustawień bezpieczeństwa.
  - Krok 2: Przegląda listę aktywnych sesji.
  - Krok 3: Wybiera zakończenie jednej lub wszystkich sesji.
  - Krok 4: System wylogowuje wskazane sesje.
  - Krok 5: Komunikat o poprawnej operacji.

* **Rozszerzenia**:
  - Krok 3.1: Błąd połączenia z serwerem – informacja o błędzie.
  - Krok 4.1: Część sesji już wygasła – system informuje użytkownika.

* **Przykład**:
  
---
###  Wyszukiwanie projektów

* **Cel**:  
  Umożliwienie użytkownikowi przeszukiwania bazy projektów.

* **Główny Aktor**:  
  Użytkownik

* **Główny Scenariusz**:
  - Krok 1: Użytkownik przechodzi do sekcji Discover.
  - Krok 2: Wprowadza frazę lub ustawia filtry.
  - Krok 3: System wyświetla wyniki wyszukiwania.
  - Krok 4: Użytkownik przegląda szczegóły projektów.
  - Krok 5: Może przejść do wsparcia lub kontaktu z twórcą.

* **Rozszerzenia**:
  - Krok 3.1: Brak wyników – wyświetlany komunikat.
  - Krok 2.1: Wprowadzono niedozwolone znaki – błąd walidacji.
 
* **Przykład**:
