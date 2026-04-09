# Museum MVC (ASP.NET Core)

[![Build](https://github.com/DimSelyutin/selyutin-dbdevelpment-lab1/actions/workflows/build.yml/badge.svg)](https://github.com/DimSelyutin/selyutin-dbdevelpment-lab1/actions/workflows/build.yml)

Учебный проект по дисциплине: ASP.NET Core MVC + EF Core + SQL Server.

## Предметная область

- Фонды (экспонаты: описание, эпоха, материал, признак реставрации)
- Залы (номер, название, ответственный смотритель)
- Экскурсии (название, тематика, длительность, цена)
- Экскурсоводы (ФИО, языки)
- График экскурсий (дата, время, экскурсия, гид, группа)

## Реализовано в проекте

- ASP.NET Core MVC приложение (`Museum.Web`)
- EF Core контекст и миграции (`Data/MuseumContext.cs`, `Data/Migrations`)
- Внедрение зависимостей (`MuseumContext` регистрируется в `Program.cs`)
- Middleware инициализации БД тестовыми данными (`Middleware/DatabaseSeedMiddleware.cs`)
- Контроллеры и представления для 3+ таблиц:
  - `ExhibitsController`
  - `GuidesController`
  - `TourSchedulesController`
- Для таблицы на стороне «многие» (`TourSchedules`) отображаются смысловые значения связанных таблиц (название экскурсии, ФИО гида, название группы), а не коды FK

## Дополнительные функции

- Расписание экскурсий на текущую неделю: `/TourSchedules/CurrentWeek`
- Экспонаты на реставрации: `/Exhibits/UnderRestoration`
- Загруженность экскурсоводов: `/Guides/Workload`

## Кэширование ответа

Используется профиль кэширования `MuseumCache` и атрибут `ResponseCache`.

Для варианта `N=15`:

- `Duration = 2*N + 240 = 270` секунд

Кэширование включено для списковых/отчетных действий контроллеров.

## Проверка ускорения через DevTools

1. Откройте страницу, например `/TourSchedules/CurrentWeek`.
2. В `Network` (Chrome/Firefox DevTools) зафиксируйте время первого запроса.
3. Обновите страницу повторно в интервале 270 секунд.
4. Сравните время ответа и заголовки кэширования (`Cache-Control`, `Age` при наличии прокси).
5. Повторный запрос должен обрабатываться быстрее за счет кэширования ответа.

## Запуск локально

```bash
dotnet restore Museum.Web/Museum.Web.csproj
dotnet build Museum.Web/Museum.Web.csproj
dotnet run --project Museum.Web/Museum.Web.csproj
```

## Миграции EF Core

Создание миграции:

```bash
dotnet ef migrations add InitialCreate --project Museum.Web/Museum.Web.csproj --startup-project Museum.Web/Museum.Web.csproj --output-dir Data/Migrations
```

Применение миграций:

```bash
dotnet ef database update --project Museum.Web/Museum.Web.csproj --startup-project Museum.Web/Museum.Web.csproj
```

## CI/CD

Workflow `build.yml` запускается на каждое изменение (`push`, `pull_request`) и собирает проект на двух платформах:

- `ubuntu-latest`
- `windows-latest`
