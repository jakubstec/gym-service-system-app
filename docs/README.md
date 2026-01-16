# Gym Service System App

## Opis aplikacji

Aplikacja do zarządzania systemem rezerwacji zajęć fitness w siłowni. System umożliwia:
- Rezerwację zajęć grupowych i treningów personalnych
- Zarządzanie biletami i pakietami usług
- Automatyczne powiadomienia dla pracowników o nowych rezerwacjach
- Różne strategie cenowe (normalny, student, senior)

## Użyte wzorce projektowe

### 1. **Strategy Pattern** (Strategia)
**Zastosowanie:** System różnych strategii cenowych dla biletów
- `IPriceStrategy` - interfejs strategii
- Implementacje:
  - `NormalPriceStrategy` - cena standardowa
  - `StudentDiscountStrategy` - 20% zniżki dla studentów
  - `SeniorDiscountStrategy` - 30% zniżki dla seniorów

### 2. **Observer Pattern** (Obserwator)
**Zastosowanie:** Powiadamianie pracowników o zdarzeniach w systemie
- `IObserver` - interfejs obserwatora
- `Employee` implementuje `IObserver`
- `GymSystemController` zarządza obserwatorami i wysyła powiadomienia

### 3. **Factory Pattern** (Fabryka)
**Zastosowanie:** Tworzenie obiektów sesji treningowych
- `SessionFactory` - statyczna klasa fabryki
- Metody:
  - `CreateGroupClass()` - tworzy zajęcia grupowe
  - `CreatePersonalTraining()` - tworzy trening personalny

### 4. **Builder Pattern** (Budowniczy)
**Zastosowanie:** Konstrukcja złożonych obiektów biletów
- `TicketBuilder` - budowniczy biletów
  - `AddPool()` - dodaje basen
  - `AddSauna()` - dodaje saunę
  - `ApplyDiscount()` - uwzględnia rabat
  - `Build()` - tworzy finalny obiekt
