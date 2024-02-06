# Przygotowanie projektu egzaminacyjnego
Sklonuj projekt z repozytorium https://github.com/siwoncezary/EgzaminyASP.NET.git i uzupełnij go wykonując kolejne zadania.
W trakcie wykonywania zadań w klasie tworzonego kontrolera, serwisu i modelu umieść na początku pliku w komentarzu swoje dane:

`
// <Imię> <Nazwisko> <nrAlbumu>
`

np.

`
// Jan Abecki 2334
`
# Zadanie 1 (1 pkt)
Zdefiniuj klasę modelu notatki `Note` w folderze `Models` składającej się z właściwości:
- `Title`: tytułu (od 3 do 20 znaków), który jest też identyfikatorem
- `Content`: treści (od 10 do 2000 znaków)
- `Deadline`: daty ważności (do którego dnia i godziny powinna być notatka udostępniana) 
Utwórz widok formularza dla tego modelu z uwzględnieniem komunikatów błędów dla każdego pola. Pole
`Content` w formularzy musi być elementem typu `textarea`! Etykiety pól formularz to odpowiednio:
- `Tytuł`
- `Treść`
- `Data ważności`

Utwórz klasę kontrolera o odpowiedniej nazwie, a w nim metodę akcji, która zwraca utworzony widok formularza po wysłaniu 
żądania do ścieżki `/Exam/Create`.

# Zadanie 2 (2 pkt)
Utwórz metodę akcji, która zwraca widok z listą notatek po wysłaniu żądania do ścieżki `Exam/Index` . Na razie umieść w widoku tylko tytuł w znaczniku H1: 
"Lista notatek". 

Zdefiniuj drugą metodę w kontrolerze, która odbiera dane z formularza notatki wysłane metodą `post` do ściezki `/Exam/create`.
Dane notatki są poprawne, jeśli pola spełniają podane warunki w zadaniu 1 oraz data ważności jest późniejsza
o co najmniej jedną godzinę od bieżącej daty.

Jeśli data ważności jest niepoprawna to zgłoś błąd daty ważności poniższą metodą:
`ModelState.AddModelError(<nazwa-pola-daty-ważnosci>, "Czas ważności musi być o godzinę późniejszy od bieżącego czasu!");`

W miejscu <nazwa-pola-daty-ważnosci> wpisz nazwę właściwości modelu.
Datę bieżącą pobierz z serwisu `DefaultDateProvider`, której kod znajduje się poniżej.
```
public class DefaultDateProvider: IDateProvider
{
    public DateTime CurrentDate { get => DateTime.Now; }
}
```
Samodzielnie zdefiniuj interfejs `IDateProvider` z metodą, którą implementuje powyższa klasa. 
Dodaj odpowiedni wiersz rejestrujący klasę `DefaultDateProvider` jako implementację interfejsu 
`IDateProvider`.
Jeśli notatka jest poprawna to zatwierdzenie formularza przekierowuje do ścieżki `/Exam/Index`. 

# Zadanie  3 (2 pkt)
Zdefiniuj klasę serwisu o nazwie `NoteService` z trzema metodami:
- `Add()`: dodanie notatki, która zapisuje notatkę w pamięci np. dodaje do listy, słownika
- `GelAll()`: pobranie listy ważnych notatek, która zwraca tylko te notatki, których data ważności jest mniejsza od bieżącej daty
- `GetById()`: zwrócenie jednej, ważnej notatki na podstawie jego tytułu.

Serwis powinien mieć zależność do serwisu  `IDateProvider` i z tego serwisu pobierać czas bieżący. 
Zarejestruj serwis w kontenerze IOC dobierając odpowiedni zasięg. 
Uzupełnij kontroler o zapisywanie poprawnej notatki do utworzonego serwisu.

# Zadanie 4 (1 pkt)
Uzupełnij metodę akcji wraz z jej widokiem listy notatek, aby wyświetlała tylko ważne notatki w 
liście typu `<ul>`. Każdy element listy typu `<li>` powinien zawierać tylko tytuł notatki w postaci linku, który kieruje do
ścieżki `/Exam/Details/{tytuł notatki}`.
Przykład linku kierującego do treści notatki o tytule "Sprawdzian"
`/Exam/Details/Sprawdzian`.

# Zadanie 5 (1 pkt)
Zdefiniuj metodę `Details`, aby zwracała widok z tytułem notatki w elemencie `<h1>`, pod tytułem należy umieścić treść notatki
w elemencie `<div>`, który zawiera paragrafy (w elemencie `<p>`). Każdy paragraf zawiera jeden wiersz tekstu treści notatki.
Na dole umieść link powrotu do listy notatek.

# Przesłanie na serwer
Wykonaj archiwum projektu zawierające tylko pliki źródłowe (oprócz katalogów `bin` i `obj`) i prześlij jako plik potwierdzający wykonanie zadania egzaminacyjnego. 





