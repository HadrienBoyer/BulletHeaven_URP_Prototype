# Services — Description of `Assets/Scripts/Services/`

Purpose

Services centralize cross-cutting concerns such as economy, purchases, cosmetics and analytics. They provide a single point of access and make it easier to swap implementations (stubs → real integrations).

Key service files

- `EconomyService.cs` — Manages soft/hard currencies and wallets. Uses `CurrencyDefinition` and `EconomyConfig` ScriptableObjects for identifiers and balances. Exposes APIs to credit/debit currency and to query balances.

- `IAPService.cs` — Abstraction for in-app purchases. Current project ships with a stub interface to allow offline testing. Replace or extend this with Unity IAP or UGS implementation.

- `CosmeticService.cs` — Tracks unlocked cosmetics, equips items on the player, and interfaces with UI to show previews.

- `AnalyticsService.cs` — Lightweight analytics event emitter. In the prototype it's a stub designed to be replaced by an analytics provider.

Integration notes

- Services are usually instantiated as singletons during `SceneBootstrap`.
- To swap the IAP implementation, implement the `IAPService` interface and register it during bootstrap.
- `EconomyService` and `CosmeticService` persist data via `SaveService`.

Testing & stubs

- The project includes stub implementations that let you exercise economy and cosmetics without real purchases.
- Use the stubbed `IAPService` for local testing. When integrating with a real store, ensure product IDs in `IAPProduct` match store dashboard entries.
