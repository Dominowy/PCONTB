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

| Nazwa:| Home <img width=1000/>|         
|:------|:----------------------------------------------------------------------|
| Opis: | mStrona główna miejsce, w którym użytkownik może przeglądać najnowsze projekty oraz najczęściej odwiedzane. |

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

#### 2.2 Aktorzy Biznesowi

| Nazwa:| Użytkownik Niezalogowany <img width=1000/>                            |
|:------|:----------------------------------------------------------------------|
| Opis: |Użytkownik który nie zalogował sie lub nie wykreował swojego konta, <br> może przeglądać dostępne projekty a także po połączeniu z swoim portfelem może wesprzeć projekt anonimowo. |

| Nazwa:| Użytkownik Zalogowany <img width=1000/>                               |
|:------|:----------------------------------------------------------------------|
| Opis: |Użytkownik który wykreował swoje konto ale także się zalogował ma pełny dostęp do oferowanej treśći |

| Nazwa:| Moderator          <img width=1000/>                                  |
|:------|:----------------------------------------------------------------------|
| Opis: | Użytkownik z uprawnieniami administracyjnymi w ograniczonym zakresie. Może wspierać użytkowników, ale nie zarządza całym systemem. |

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

- Backend - ASP.NET - napisane zostanie w nim WEB.API mechanika strony, połączenie z bazą danych oraz blockchainem. Backend zostanie podzielony na warstwy zgodnie z czystą architekturą.
- Frontend - VUE.JS - zostanie w nim stworzony interfejs użytkownika w komunikacji z backendem będzie pośredniczył VITE.JS w którym zostanie skonfigurowane proxy.
- Baza danych - PostgreSQL - odpowiedzialny z przechowywanie danych o projektach, użytkownikach oraz danych pomocniczych
- Blockchain - Solana - poprzez smart contract użytkownik dokonuje wpłat na cel projektu a także przetrzymywane są w nim dane na temat transakcji oraz wspierających

<img width="1169" height="1055" alt="image" src="https://github.com/user-attachments/assets/fabc017e-7b5b-4908-9a27-5205da83cdfa" />

### 5. Lista przypdków użycia

|Funkcjonalność | Użytkownik Niezalogowany | Użytkownik | Moderator | Administrator|
|:-------------:|:------------------------:|:----------:|:------------:|:------------:|
|Rejestracja | + | - | - | - |
|Logowanie | + | - | - | - |
|Wylogowanie | - | + | + | + |
|Aktualizacja danych konta | - | + | + | + |
|Zmiana hasła konta | - | + | + | + |
|Blokada konta | - | + | + | + |
|Wyświetlanie użytkownika | + | + | + | + |
|Zarządzanie sesjami | - | + | + | + |
|Wyświetlanie projektu | + | + | + | + |
|Utworzenie projektu | - | + | - | - |
|Edycja projektu | - | + | - | - |
|Uruchomienie kampanii | - | + | - | - |
|Wsparcie kampanii | + | + | - | - |
|Wypłata środków kampanii | - | + | - | - |
|Zwrot środków kampanii | + | + | - | - |
|Wyświetlenie transakcji | + | + | - | - |
|Dodawanie komentarzy | + | + | - | - |
|Wyświetlenie komentarzy | + | + | - | - |
|Dodawanie obserwacji projektu | - | + | - | - |
|Usuwanie obserwacji projektu | - | + | - | - |
|Blokowanie komentarzy | - | - | + | + |
|Dodawanie użytkownika | - | - | - | + |
|Blokada użytkownika | - | - | + | + |
|Odblokowanie użytkownika | - | - | + | + |
|Zmiana uprawnień użytkownika | - | - | - | + |
|Dodawanie kategorii | - | - | - | + |
|Edycja kategorii | - | - | - | + |
|Usuwanie kategorii | - | - | - | + |
|Dodanie lokalizacji | - | - | - | + |
|Edycja lokalizacji | - | - | - | + |
|Usuwanie lokalizacji | - | - | - | + |


### 6. Przypadki użycia

### Rejestracja

* **Cel**: 
        Celem tej funkcjonalności jest rejestracja konta dla nowego użytkownika w celu możliwości zalogowania.
        
* **Główny Aktor**:
        Użytkownik/Moderator/Administrator
                        
* **Główny Scenariusz**:
  
```
- Krok 1: Użytkownik/Moderator/Administrator przechodzi do logowanie.
- Krok 2: Klika przycisk link Rejestruj
- Krok 3: Użytkownik/Moderator/Administrator wypełnia wymagane dane w formularzu.
- Krok 4: Użytkownik/Moderator/Administrator klika przycisk "Rejestruj".
- Krok 5: Sprawdzana jest poprawność wprowadzonych danych.
- Krok 6: Otrzymuje informację o pozytywnym wykreowaniu konta.
```

* **Rozszerzenia**:
  
```
- Krok 5.1: Otrzymuje informację o wprowadzeniu niepoprawnych danych lub powtarzających się.
- Krok 5.2: System prosi o ponowne wprowadzenie danych.
```
        
* **Przykład**:

<p align="center">
        <img width="721" height="309" alt="Rejestracja drawio" src="https://github.com/user-attachments/assets/8e43c2b4-7de4-427e-8434-194f9ea4588e" />
</p>
        
---
### Logowanie

* **Cel**:
        Celem tej funkcjonalności jest umożliwienie użytkownikowi/moderatorowi/administratorowi uzyskania dostępu.
        
* **Główny Aktor**:
        Użytkownik/Moderator/Administrator
                
* **Główny Scenariusz**:
```
      
- Krok 1: Użytkownik/Moderator/Administrator wyświetla się ekran z możliwością zalogowania.
- Krok 2: Użytkownik/Moderator/Administrator wypełnia dane w formularzu logowania.
- Krok 3: Sprawdzana jest poprawność wprowadzonych danych.
- Krok 4: Użytkownik/Moderator/Administrator zostaje pomyślnie zalogowany.
```

* **Rozszerzenia**:
```
- Krok 3.1: Otrzymuje informację o wprowadzeniu niepoprawnych danych lub powtarzającyh się.
- Krok 3.2: System prosi o ponowne wprowadzenie danych.
```

* **Przykład**:

<p align="center">
        <img width="721" height="309" alt="Logowanie drawio" src="https://github.com/user-attachments/assets/2c766f44-9b49-47f6-bb0b-3d22c53978eb" />
</p>

---
### Wylogowanie

* **Cel**:
        Celem tej funkcjonalności jest umożliwienie użytkownikowi/moderatorowt/administratorowi wylogowanie się z systemu tym samym zakończenie sesji i zabezpieczenie swojego konta.
     
* **Główny Aktor**:
        Użytkownik/Moderator/Administrator
                
* **Główny Scenariusz**:

```
- Krok 1: Użytkownik/Moderator/Administrator klika przycisk wyloguj.
- Krok 2: Zamykana jest sesja określonego Użytkownik/Moderator/Administrator.
- Krok 3: Komunikat o poprawynm wylogowaniu.
```

* **Przykład**:

<p align="center">
        <img width="721" height="309" alt="Wylogowanie drawio" src="https://github.com/user-attachments/assets/040e1cae-4db3-4f81-9060-94eda56efd16" />
</p>

---
### Aktualizacja danych konta

* **Cel**:
        Celem tej funkcjonalności jest umożliwienie użytkownikowi/moderatorowt/administratorowi zmiany danych(e-mail, nazwa) użytkownika.
     
* **Główny Aktor**:
        Użytkownik/Moderator/Administrator
                
* **Główny Scenariusz**:
  
```
- Krok 1: Użytkownik/Moderator/Administrator przechodzi do ustawień.
- Krok 2: Wprowadza nowe dane.
- Krok 3: Użytkownik/Moderator/Administrator zapisuje.
- Krok 4: Sprawdzana jest poprawność wprowadzonych danych.     
- Krok 5: Komunikat o poprawnej operacji.
```

* **Rozszerzenia**:
  
```
- Krok 5.1: Otrzymuje informację o wprowadzeniu niepoprawnych danych lub powtarzających się.
- Krok 5.2: System prosi o ponowne wprowadzenie danych.
```
       
* **Przykład**:

<p align="center">
        <img width="721" height="309" alt="Aktualizacja danych konta drawio" src="https://github.com/user-attachments/assets/6dce3c6a-616d-4ff8-ba0d-7d60d9297c51" />
</p>

---
### Zmiana hasła konta

* **Cel**:
        Celem tej funkcjonalności jest umożliwienie użytkownikowi/moderatorowi/administratorowi zmiany hasła konta.

* **Główny Aktor**:
        Użytkownik/Moderator/Administrator

* **Główny Scenariusz**:
  
```
- Krok 1: Użytkownik/Moderator/Administrator przechodzi do ustawień.
- Krok 2: Wprowadza stare i nowe hasło.
- Krok 3: Zapisuje.
- Krok 4: Sprawdzana jest poprawność starego hasła i siła nowego.
- Krok 5: Komunikat o poprawnej operacji.
```

* **Rozszerzenia**:
  
```
- Krok 4.1: Otrzymuje informację o wprowadzeniu niepoprawnych danych. 
```

* **Przykład**:

<p align="center">
        <img width="721" height="309" alt="Zmiana hasła konta drawio" src="https://github.com/user-attachments/assets/510a85e4-1533-43a8-952e-7467180fd042" />
</p>
  
---
### Blokada konta

* **Cel**:
        Umożliwienie Użytkownik/Moderator/Administrator blokady swojego konta.

* **Główny Aktor**:
        Użytkownik/Moderator/Administrator

* **Główny Scenariusz**:

```
- Krok 1: Użytkownik/Moderator/Administrator przechodzi do ustawień konta.
- Krok 2: Wybiera opcję blokady konta.
- Krok 3: Potwierdza decyzję.
- Krok 4: System dezaktywuje konto.
- Krok 5: Komunikat o poprawnej operacji.
- Krok 6: Wylogowanie wszystkich aktywnych sesji.
```

* **Rozszerzenia**:

```
- Krok 5.1: Informacja o powiązanyiach z aktywnymi kampaniami - brak możliwości zamknięcia konta.
```

* **Przykład**:

<p align="center">
        <img width="721" height="309" alt="Blokda konta drawio" src="https://github.com/user-attachments/assets/f784954f-3461-49f8-a639-ded70cf03797" />
</p>
  
---
### Wyświetlanie użytkownika

* **Cel**:
        Umożliwienie przeglądania publicznego profilu innego użytkownika.

* **Główny Aktor**:
        Użytkownik

* **Główny Scenariusz**:

```
- Krok 1: Użytkownik otwiera strone projektu.
- Krok 2: Kliknięcie na nazwę użytkownika przekierowuje do jego profilu.
- Krok 3: System wyświetla dane publiczne profilu.
```

* **Rozszerzenia**:

```
- Krok 3.1: Konto ukryte lub zablokowane – informacja o braku dostępu.
```

* **Przykład**:

<p align="center">
        <img width="721" height="309" alt="Wyświetlanie użytkownika drawio" src="https://github.com/user-attachments/assets/5fe65956-51fc-4da0-808b-66690230008f" />
</p>
  
---
### Zarządzanie sesjami

* **Cel**:
        Umożliwienie zarządzania aktywnymi sesjami logowania.

* **Główny Aktor**:
        Użytkownik/Moderator/Administrator

* **Główny Scenariusz**:

```
- Krok 1: Użytkownik przechodzi do ustawień konta.
- Krok 2: Przegląda listę aktywnych sesji.
- Krok 3: Wybiera zakończenie jednej lub wszystkich sesji.
- Krok 4: System wylogowuje wskazane sesje.
- Krok 5: Komunikat o poprawnej operacji.
```

* **Rozszerzenia**:

```
- Krok 3.1: Błąd połączenia z serwerem – informacja o błędzie.
- Krok 4.1: Część sesji już wygasła – system informuje użytkownika.
```

* **Przykład**:

<p align="center">
        <img width="721" height="309" alt="Zarządzanie sesjami drawio" src="https://github.com/user-attachments/assets/d1b843d0-d86f-4e76-90ca-f7329e7d6f1e" />
</p>
  
---
### Wyświetlanie projektu

* **Cel**:
        Umożliwienie Użytkownikowi/Moderatorowi/Administratorowi przeglądanie projektu.

* **Główny Aktor**:
        Użytkownik/Moderator/Administrator

* **Główny Scenariusz**:

```
- Krok 1: Wprowadza frazę lub ustawia filtry.
- Krok 2: System wyświetla wyniki wyszukiwania.
- Krok 3: Użytkownik wybiera projekt z listy.
- Krok 4: Użytkownik przegląda szczegóły projektó.u
- Krok 5: Może przejść do wsparcia lub kontaktu z twórcą.
```

* **Rozszerzenia**:
  
```
- Krok 3.1: Brak wyników – wyświetlany komunikat.
```

* **Przykład**:

<p align="center">
        <img width="721" height="309" alt="Wyświetlanie projektu drawio" src="https://github.com/user-attachments/assets/702a2a5e-09af-434b-b4df-79ac056d0f5d" />
</p>

---
### Utworzenie projektu

* **Cel**:
        Umożliwienie użytkownikowi utworzenia nowego projektu.

* **Główny Aktor**:
        Użytkownik

* **Główny Scenariusz**:
  
```
- Krok 1: Użytkownik przechodzi do strony swoich projektów".
- Krok 2: Wybiera opcję „Dodaj”.
- Krok 3: Wprowadza wymagane dane (nazwa, opis, kategorie, itp.).
- Krok 4: Zapisuje projekt.
- Krok 5: Projekt jest widoczny w systemie.
```

* **Rozszerzenia**:

```
- Krok 3.1: Niepełne dane – komunikat o błędzie.
- Krok 4.1: Błąd serwera – projekt nie został zapisany.
```

* **Przykład**:

<p align="center">
        <img width="721" height="309" alt="Utworzenie projektu drawio" src="https://github.com/user-attachments/assets/018bc402-aac7-409f-bd7a-750b30b9691b" />
</p>

---
### Edycja projektu

* **Cel**:
  Umożliwienie użytkownikowi aktualizacji danych istniejącego projektu.

* **Główny Aktor**:
        Użytkownik

* **Główny Scenariusz**:

```
- Krok 1: Użytkownik otwiera swój projekt.
- Krok 2: Wybiera opcję „Edytuj”.
- Krok 3: Wprowadza zmiany w danych.
- Krok 4: Zapisuje zmiany.
- Krok 5: Projekt zostaje zaktualizowany.
```

* **Rozszerzenia**:
  
```
 - Krok 3.1: Niepoprawne dane – system zgłasza błąd.
- Krok 4.1: Brak połączenia z serwerem – zmiany niezapisane.
```

* **Przykład**:

<p align="center">
        <img width="721" height="309" alt="Edycja projektu drawio" src="https://github.com/user-attachments/assets/0db2a881-aaa4-4840-9502-98cd983a7427" />
</p>

---

### Uruchomienie kampanii

* **Cel**:
  Umożliwienie użytkownikowi rozpoczęcie kampanii.

* **Główny Aktor**:
        Użytkownik

* **Główny Scenariusz**:

```
- Krok 1: Użytkownik łączy się ze swoim portfelem.
- Krok 2: Uruchamian strone edycji projektu.
- Krok 3: Wprowadza cel oraz czas trwania.
- Krok 4: Potwierdza operacje.
- Krok 5: Kampanie zostaje uruchomion.
```

* **Rozszerzenia**:
  
```
- Krok 4.1: Niepoprawne dane – system zgłasza błąd.

```

* **Przykład**:

<p align="center">
        <img width="721" height="371" alt="Uruchomienie kampanii drawio" src="https://github.com/user-attachments/assets/2c94b4ea-b93d-4477-9915-bde9b71f00f0" />
</p>

---

### Wsparcie kampanii

* **Cel**:
        Umożliwienie użytkownikowi wsparcia finansowego dla wybranego projektu.

* **Główny Aktor**:
        Użytkownik

* **Główny Scenariusz**:

```
- Krok 1: Użytkownik łączy się ze swoim portfelem.
- Krok 2: Użytkownik wybiera projekt.
- Krok 3: Wybiera kwotę wsparcia.
- Krok 4: Potwierdza operację.
- Krok 5: System przetwarza transakcję i informuje o sukcesie.
```

* **Rozszerzenia**:

```
  - Krok 5.1: Brak środków – transakcja anulowana.
```

* **Przykład**:

<p align="center">
        <img width="721" height="371" alt="Wsparcie kampanii drawio" src="https://github.com/user-attachments/assets/1ff03ab9-ce95-4326-a145-758ab2e51edf" />
</p>

---
### Wypłata środków z kampanii

* **Cel**:
        Pozwala twórcy projektu wypłacić zgromadzone środki z kampanii po osiągnięciu celu.

* **Główny Aktor**:
        Użytkownik

* **Główny Scenariusz**:
  
```
- Krok 1: Użytkownik łączy się ze swoim portfelem.
- Krok 2: Użytkownik otwiera strone edycji projektu.
- Krok 3: Wybiera opcję „Wypłać środki”.
- Krok 4: Kontrakt realizuje wypłate.
```

* **Rozszerzenia**:
  
```
- Krok 4.1: Kampania jeszcze trwa – brak możliwości wypłaty.

```

* **Przykład**:

<p align="center">
        <img width="721" height="371" alt="Wypłata środków z kampanii drawio" src="https://github.com/user-attachments/assets/873f086f-3ab1-4429-a0d7-154677fd4c91" />
</p>

---

### Zwrot środków z kampanii

* **Cel**:
        Umożliwienie użytkownikowi wypłaty środków z kampanii po nie spełnieniu celu oraz przekroczeniu czasy.

* **Główny Aktor**:
        Użytkownik

* **Główny Scenariusz**:

```
- Krok 1: Użytkownik przechodzi sekcję „Portfel”.
- Krok 2: System wyświetla saldo oraz listę transakcji.
```
* **Rozszerzenia**:

```
- Krok 2.1: Błąd API – brak dostępu do danych portfela.
```
* **Przykład**:

<p align="center">
        <img width="721" height="371" alt="Zwrot środków z kampanii drawio" src="https://github.com/user-attachments/assets/7944542e-040c-41d8-887d-77c3e5c88b87" />
</p>

---
### Wyświetlanie transakcji

* **Cel**:
        Umożliwienie użytkownikowi wypłaty środków z kampanii po nie spełnieniu celu oraz przekroczeniu czasy.

* **Główny Aktor**:
        Użytkownik

* **Główny Scenariusz**:

```
- Krok 1: Użytkownik przechodzi sekcję „Portfel”.
- Krok 2: System wyświetla saldo oraz listę transakcji.
```
* **Rozszerzenia**:

```
- Krok 2.1: Błąd API – brak dostępu do danych portfela.
```
* **Przykład**:

<p align="center">
        <img width="721" height="371" alt="Wyświetlanie transakcji drawio" src="https://github.com/user-attachments/assets/45c577f8-917b-4755-ab69-e39ed0bc19fd" />
</p>

---

### Dodawanie komentarzy

* **Cel**:
        Umożliwienie użytkownikowi wypłaty środków z kampanii po nie spełnieniu celu oraz przekroczeniu czasy.

* **Główny Aktor**:
        Użytkownik

* **Główny Scenariusz**:

```
- Krok 1: Użytkownik przechodzi sekcję „Portfel”.
- Krok 2: System wyświetla saldo oraz listę transakcji.
```
* **Rozszerzenia**:

```
- Krok 2.1: Błąd API – brak dostępu do danych portfela.
```
* **Przykład**:

<p align="center">
        <img width="721" height="309" alt="Dodawanie komentarzy drawio" src="https://github.com/user-attachments/assets/0f5c85a7-7b3a-4c8c-a82b-29d64cc8900e" />
</p>

---

### Wyświetlanie komentarzy

* **Cel**:
        Umożliwienie użytkownikowi wypłaty środków z kampanii po nie spełnieniu celu oraz przekroczeniu czasy.

* **Główny Aktor**:
        Użytkownik

* **Główny Scenariusz**:

```
- Krok 1: Użytkownik przechodzi sekcję „Portfel”.
- Krok 2: System wyświetla saldo oraz listę transakcji.
```
* **Rozszerzenia**:

```
- Krok 2.1: Błąd API – brak dostępu do danych portfela.
```
* **Przykład**:

<p align="center">
        <img width="721" height="309" alt="Wyświetlanie komentarzy drawio" src="https://github.com/user-attachments/assets/08e9fdca-c96f-4e02-9f40-02d1d58a519e" />
</p>

---

### Dodanie obserwacji projektu

* **Cel**: 
        Celem tej funkcjonalności jest rejestracja konta dla nowego użytkownika w celu możliwości zalogowania.
        
* **Główny Aktor**:
        Użytkownik/Moderator/Administrator
                        
* **Główny Scenariusz**:
  
```
- Krok 1: Użytkownik/Moderator/Administrator przechodzi do logowanie.
- Krok 2: Klika przycisk link Rejestruj
- Krok 3: Użytkownik/Moderator/Administrator wypełnia wymagane dane w formularzu.
- Krok 4: Użytkownik/Moderator/Administrator klika przycisk "Rejestruj".
- Krok 5: Sprawdzana jest poprawność wprowadzonych danych.
- Krok 6: Otrzymuje informację o pozytywnym wykreowaniu konta.
```

* **Rozszerzenia**:
  
```
- Krok 5.1: Otrzymuje informację o wprowadzeniu niepoprawnych danych lub powtarzających się.
- Krok 5.2: System prosi o ponowne wprowadzenie danych.
```
        
* **Przykład**:

<p align="center">
        <img width="721" height="309" alt="Dodanie obserwacji projektu drawio" src="https://github.com/user-attachments/assets/aa37f2c6-d87e-4c09-92e9-d14f11c49e93" />
</p>
        
---

### Usuwanie obserwacji projektu

* **Cel**: 
        Celem tej funkcjonalności jest rejestracja konta dla nowego użytkownika w celu możliwości zalogowania.
        
* **Główny Aktor**:
        Użytkownik/Moderator/Administrator
                        
* **Główny Scenariusz**:
  
```
- Krok 1: Użytkownik/Moderator/Administrator przechodzi do logowanie.
- Krok 2: Klika przycisk link Rejestruj
- Krok 3: Użytkownik/Moderator/Administrator wypełnia wymagane dane w formularzu.
- Krok 4: Użytkownik/Moderator/Administrator klika przycisk "Rejestruj".
- Krok 5: Sprawdzana jest poprawność wprowadzonych danych.
- Krok 6: Otrzymuje informację o pozytywnym wykreowaniu konta.
```

* **Rozszerzenia**:
  
```
- Krok 5.1: Otrzymuje informację o wprowadzeniu niepoprawnych danych lub powtarzających się.
- Krok 5.2: System prosi o ponowne wprowadzenie danych.
```
        
* **Przykład**:

<p align="center">
        <img width="721" height="309" alt="Usuwanie obserwacji projektu drawio" src="https://github.com/user-attachments/assets/68594851-159a-42ec-b401-d5c0fecb49e7" />
</p>
        
---

### Blokowanie komentarzy

* **Cel**:
        Umożliwienie użytkownikowi wypłaty środków z kampanii po nie spełnieniu celu oraz przekroczeniu czasy.

* **Główny Aktor**:
        Użytkownik

* **Główny Scenariusz**:

```
- Krok 1: Użytkownik przechodzi sekcję „Portfel”.
- Krok 2: System wyświetla saldo oraz listę transakcji.
```
* **Rozszerzenia**:

```
- Krok 2.1: Błąd API – brak dostępu do danych portfela.
```
* **Przykład**:

<p align="center">
        <img width="726" height="309" alt="Blokowanie komentarzy drawio" src="https://github.com/user-attachments/assets/9c564d07-ae1c-47ef-b299-c4ef7ef3319a" />
</p>

---

### Dodawanie użytkownika

* **Cel**:
        Administrator może ręcznie dodać nowego użytkownika do systemu.

* **Główny Aktor**:
        Administrator

* **Główny Scenariusz**:

```
- Krok 1: Administrator otwiera panel zarządzania użytkownikami.
- Krok 2: Wprowadza dane nowego użytkownika.
- Krok 3: Zatwierdza dodanie.
- Krok 4: Użytkownik pojawia się w systemie.
```

* **Rozszerzenia**:
```
- Krok 2.1: Użytkownik o tym adresie e-mail już istnieje.
```
* **Przykład**:

<p align="center">
        <img width="726" height="309" alt="Dodawanie użytkownika drawio" src="https://github.com/user-attachments/assets/ae7e746f-37d7-41a7-860c-6bf28f4aaef6" />
</p>

---
### Blokada użytkownika

* **Cel**:
        Zablokowanie konta użytkownika w przypadku naruszenia regulaminu.

* **Główny Aktor**:
        Moderator, Administrator

* **Główny Scenariusz**:

```
- Krok 1: Otwiera profil użytkownika.
- Krok 2: Wybiera opcję „Zablokuj konto”.
- Krok 3: Potwierdza decyzję.
- Krok 4: Użytkownik traci dostęp do konta.
```

* **Rozszerzenia**:
  
```
- Krok 3.1: Próba zablokowania administratora – operacja niedozwolona.
```

* **Przykład**:

<p align="center">
        <img width="755" height="309" alt="Blokada użytkownika drawio" src="https://github.com/user-attachments/assets/91a1f153-2323-4dca-b99c-bc3c3adf27fe" />
</p>

---
### Odblokowanie użytkownika

* **Cel**:
        Przywrócenie dostępu do zablokowanego konta.

* **Główny Aktor**:
        Moderator, Administrator

* **Główny Scenariusz**:
  
```
  - Krok 1: Otwiera profil zablokowanego użytkownika.
  - Krok 2: Wybiera opcję „Odblokuj”.
  - Krok 3: Potwierdza.
  - Krok 4: Konto zostaje aktywowane.
```

* **Rozszerzenia**:
```
- Krok 2.1: Konto nie jest zablokowane – operacja niepotrzebna.
```

* **Przykład**:

<p align="center">
        <img width="726" height="309" alt="Odblokowanie użytkownika drawio" src="https://github.com/user-attachments/assets/753980b7-75ce-4510-a116-a875bebb2673" />
</p>
  
---

### Zmiana uprawnień użytkownika

* **Cel**:
        Administrator może zmienić poziom dostępu danego użytkownika (np. nadać rolę moderatora).

* **Główny Aktor**:
        Administrator

* **Główny Scenariusz**:
  
 ```
 - Krok 1: Administrator otwiera profil użytkownika.
 - Krok 2: Wybiera nowy poziom uprawnień.
 - Krok 3: Zatwierdza zmianę.
 - Krok 4: Uprawnienia zostają zaktualizowane.
```
* **Rozszerzenia**:
  
```
- Krok 2.1: Próba nadania wyższych uprawnień niż własne – operacja odrzucona.
```

* **Przykład**:

<p align="center">
        <img width="726" height="309" alt="Zmiana uprawnień użytkownika drawio" src="https://github.com/user-attachments/assets/b7a70872-f2c8-4c4f-b09a-84ca32125933" />
</p>

---

### Dodawanie kategorii  
* **Cel**:  Administrator może dodać nową kategorię do systemu.  
* **Główny Aktor**:  Administrator  
* **Główny Scenariusz**:
  
```
- Krok 1: Administrator otwiera panel zarządzania kategoriami.  
- Krok 2: Wybiera opcję "Dodaj kategorię".  
- Krok 3: Wprowadza nazwę i ewentualny opis kategorii.  
- Krok 4: Zatwierdza dodanie.  
- Krok 5: Kategoria zostaje dodana do systemu.  
  ```  
* **Rozszerzenia**:
  
```
- Krok 3.1: Wprowadzono pustą nazwę – wyświetlany jest komunikat o błędzie.  
- Krok 4.1: Kategoria o podanej nazwie już istnieje – operacja odrzucona.  
```

* **Przykład**:

<p align="center">
        <img width="726" height="309" alt="Dodawanie kategorii drawio" src="https://github.com/user-attachments/assets/bf057b2e-e076-42ab-998e-c68368dacc0a" />
</p>

---

### Edycja kategorii  
* **Cel**:  Administrator może edytować istniejącą kategorię.  
* **Główny Aktor**:  Administrator  
* **Główny Scenariusz**:
  
```
- Krok 1: Administrator otwiera listę kategorii.  
- Krok 2: Wybiera kategorię do edycji.  
- Krok 3: Modyfikuje dane kategorii.  
- Krok 4: Zatwierdza zmiany.  
- Krok 5: Kategoria zostaje zaktualizowana.  
```
* **Rozszerzenia**:
   
```
- Krok 3.1: Zmiana nazwy na istniejącą – operacja odrzucona.  
```

* **Przykład**:

<p align="center">
        <img width="726" height="309" alt="Edycja kategorii drawio" src="https://github.com/user-attachments/assets/c1254879-881d-4278-a650-71937dc5c567" />
</p>

---

### Usuwanie kategorii  
* **Cel**:  Administrator może usunąć kategorię z systemu.  
* **Główny Aktor**:  Administrator  
* **Główny Scenariusz**:
  
```
- Krok 1: Administrator otwiera listę kategorii.  
- Krok 2: Wybiera kategorię do usunięcia.  
- Krok 3: Potwierdza usunięcie.  
- Krok 4: Kategoria zostaje usunięta z systemu.  
```  
* **Rozszerzenia**:
  
```
- Krok 2.1: Kategoria jest powiązana z aktywnymi danymi – operacja odrzucona.  
```

 * **Przykład**:

<p align="center">
        <img width="726" height="309" alt="Usuwanie kategorii drawio" src="https://github.com/user-attachments/assets/85fa2916-61c0-4b0e-be4f-79fb734c0228" />
</p>

---

### Dodanie lokalizacji  
* **Cel**:  Administrator może dodać nową lokalizację do systemu.  
* **Główny Aktor**:  Administrator  
* **Główny Scenariusz**:
  
```
- Krok 1: Administrator otwiera panel zarządzania lokalizacjami.  
- Krok 2: Wybiera opcję "Dodaj lokalizację".  
- Krok 3: Wprowadza dane lokalizacji.  
- Krok 4: Zatwierdza dodanie.  
- Krok 5: Lokalizacja zostaje dodana.  
```  
* **Rozszerzenia**:
  
```
- Krok 3.1: Niepełne dane – wyświetlany jest komunikat o błędzie.  
- Krok 4.1: Lokalizacja już istnieje – operacja odrzucona.  
```

* **Przykład**:

<p align="center">
        <img width="726" height="309" alt="Dodanie lokalizacji drawio" src="https://github.com/user-attachments/assets/97ef6f95-9740-4ae4-8e51-869acc5d3c9c" />
</p>

---

### Edycja lokalizacji  
* **Cel**:  Administrator może edytować dane istniejącej lokalizacji.  
* **Główny Aktor**:  Administrator  
* **Główny Scenariusz**:
  
```
- Krok 1: Administrator otwiera listę lokalizacji.  
- Krok 2: Wybiera lokalizację do edycji.  
- Krok 3: Modyfikuje dane lokalizacji.  
- Krok 4: Zatwierdza zmiany.  
- Krok 5: Dane lokalizacji zostają zaktualizowane.  
```  
* **Rozszerzenia**:
  
```
- Krok 3.1: Błędny format danych – operacja odrzucona.  
```

* **Przykład**:

<p align="center">
        <img width="726" height="309" alt="Edycja lokalizacji drawio" src="https://github.com/user-attachments/assets/55aa9c4d-36c0-439f-a124-5759ca7a2cda" />
</p>

---

### Usuwanie lokalizacji  
* **Cel**:  Administrator może usunąć lokalizację z systemu.  
* **Główny Aktor**:  Administrator  
* **Główny Scenariusz**:
  
```
- Krok 1: Administrator otwiera listę lokalizacji.  
- Krok 2: Wybiera lokalizację do usunięcia.  
- Krok 3: Potwierdza usunięcie.  
- Krok 4: Lokalizacja zostaje usunięta.  
```  
* **Rozszerzenia**:
  
```
- Krok 2.1: Lokalizacja jest przypisana do aktywnych zasobów – operacja odrzucona.  
```

* **Przykład**:

<p align="center">
        <img width="726" height="309" alt="Usuwanie lokalizacji drawio" src="https://github.com/user-attachments/assets/c47a1120-486c-473a-b6d0-b6a6890b0768" />
</p>

