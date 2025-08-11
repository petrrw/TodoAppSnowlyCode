# To-Do API (.NET) + React client
Repozitář obsahuje backend část - jednoduché API v .NET8 (složka /api) s základními CRUD endpointy dle zadání a react clienta (/client)
Backend jsem se snažil alespoň základně rozdělit do 3 vrstvé architektury
1. Presentation layer - obsahuje API, resp. zprostředkovává komunikaci s klienty a následně dále zbytek zodpovědnosti nechává na business vrstvě (kromě logiky spojené přímo s prezenčační vrstvou, např. vracení errorů v správném formátu -
v případě validační chyby je vrácen formát tak, aby z klienta bylo jednoduše rozpoznatelné, k jaké vlastnosti itemu se daná error hláška vztahuje.)
3. Business layer - aplikace neoplývá nějakou velkou business logikou, každopádně je to vrstva která by ji měla obsahovat - jsou zde tedy validace, service/manager pro práci s ToDo itemy s kterou se pracuje pod rozhraním tak,
aby se dala implementace případně jednodušeji nahradit. Zároveň je registrována v DI a injektovaná tak, aby si závislost na ní nemuseli konzumenti (v tomto případě tedy jenom controllery) vytvářet sami..
4. Data layer - obstarává presistenci, konkrétně je využit EF code-first approach s využitím repository patternu, pod kterým se přístup k DB přes EF schovává, čímž je opět docíleno toho, že lze v případě potřeby využití jiné technologie pro práci s DB
naimplementovat nové repository a upravit registraci v DI, díky čemuž pak aplikace jednoduše začně využívat tuto novou implementaci.
V API by se ještě daly vrstvy lépe separovat - např. prezentační vrstva by nemusela vůbec pracovat s ToDoItem modelem z datové vrstvy, namísto toho by bylo vhodnější aby business vrstva měla ještě svoje business třídy. Celkově lepšího rozdělení by
šlo dosáhnout např. využitím jiné architektury, např. CleanCode, ale architektura by dle mého měla především co nejlépe sloužit účelům použití a v tomto případě mi kvůli své jednoduchosti přišla vhodnější méně složitá 3 vrstvá.

# Testy
Do .sln jsou přidány jednoduché UT pro test logiky validace a servisy pro práci s ToDo itemy a IT pro end-to-end test celého api. Testy se se při úpravách díky regresi snáze odhalí případné zanesené chyby.
Ideální by samozřejmě bylo mít pokryto více funkcionality, jde spíše o náčrt.

# React client (/client)
Přidán byl i velmi jednoduchý react-client, který je schopen provolávat endpointy API a umožnit tak jednoduchou práci s ToDo itemy.
Konkrétně se skládá z pár komponent - ToDoList.jsx, který vykresluje předané itemy (ToDoItem.jsx). Interakce s rodičovskou komponentou je umožněna pomocí callback funkcí, které se rovněž předávají v props - umožnují v rodičovské komponentě
reagovat na vytvoření/smazání/úpravu itemů - v tomto případě je reakcí odpovídající komunikace s API, která je zapoudřena v třídě ApiClient.js

# Spuštění přes docker compose
Pro jednodušší spuštění všech částí aplikace (backend api, frontend-client, DB) je vytvořen docker compose - nad složkou /docker stačí spustit klasicky docker compose up. Po buildu image a nastartování containerů je klient dostupný
pod http://localhost:5001 a api pod http://localhost:5000. Aplikování migrací si aplikace pro jednoduchost spuštění provede sama. V případě spouštění běz dockeru je potřeba upravit si DB connection string v appsettings.json.
Client má url backendu zadanou v App.js na řádku 9 pomocí process.env.REACT_APP_BACKENDURL - v případě spouštění mimo docker tedy může být potřeba upravit.




