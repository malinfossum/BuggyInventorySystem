# Bug-oversikt — BuggyInventorySystem

Oversikt over feilene som ble funnet og rettet i debugging-oppgaven, pluss
kvalitetsforbedringene som ble gjort etter tilbakemelding fra læreren (se nederst).
Gruppert per fil, med opprinnelig feil → fiks. Referert med metodenavn (linjenumre
flytter seg når koden endres).

## Status

- **Bygg:** 0 advarsler, 0 feil
- **Verifisert ende-til-ende:** kjøp → vis gull/inventar → selg → søk

```
Sword - 30          riktig pris i butikk (ingen −5)
Item purchased
Gold: 70            100 − 30  → kjøp trekker riktig
Items: 1            vare lagt til, telling riktig
Item sold
Gold: 100           70 + 30   → salg gir riktig
Items: 0            vare fjernet (ikke hele lista tømt)
Search "sword" →    Sword     case-insensitivt søk treffer
```

---

## Telling

| Kategori | Antall |
|---|---|
| Logiske bugs funnet og fikset | **20** |
| Kvalitets-/arkitekturforbedringer (lærerens tilbakemelding) | 8 |
| Konsistens-forbedringer utover bugs (case-insensitivt søk) | 2 |
| Null-advarsler ryddet (for grønt bygg) | 9 |

**Fordeling fikset:** Item.cs 4 · Player.cs 9 · Shop.cs 4 · Game.cs 3 = **20 bugs**

Oppgaven anslår ca. 30 bugs (22 "tydelige"). Vi fant og fikset 20 logiske bugs.
Etter tilbakemelding fra læreren (alle hovedbugs funnet) ble i tillegg 8
kvalitets-/arkitekturforbedringer gjort — se egen seksjon nederst.

---

## Fiksede bugs

### Item.cs
| Sted | Feilen | Fiks |
|---|---|---|
| Konstruktør | `Name = "Unknown"` — ignorerte `name`-parameteren, alle varer het "Unknown" | `Name = name;` |
| Konstruktør | `IsRare = false` — ignorerte `isRare`, Magic Ring ble aldri rar | `IsRare = isRare;` |
| `Price` | `{ get; set; }` — offentlig setter, pris kunne endres utenfra | `{ get; private set; }` |
| `IsRare` | `{ get; set; }` — offentlig setter | `{ get; private set; }` |

### Player.cs
| Sted | Feilen | Fiks |
|---|---|---|
| Konstruktør | `_gold = 0` — ignorerte `gold`-parameteren, spiller startet med 0 | `_gold = gold;` |
| `AddGold` | `_gold -= amount` — trakk fra i stedet for å legge til | `_gold += amount;` |
| `RemoveGold` | `_gold += amount` — la til i stedet for å trekke fra | `_gold -= amount;` |
| `CanAfford` | Omvendt logikk + feil grense (`if (_gold > price) return false`) | `return _gold >= price;` |
| `AddItem` | La bare til når `item == null` — ekte varer ble aldri lagt til | `_inventory.Add(item);` (alltid) |
| `RemoveItem` | `_inventory.Clear()` — tømte HELE inventaret ved salg | `_inventory.Remove(item);` |
| `FindItem` | `if (item.Name != name)` — returnerte første vare som IKKE matchet | `if (item.Name == name)` → case-insensitiv* |
| `ShowInventory` | `"Items: " + 0` — hardkodet, viste alltid 0 | `"Items: " + _inventory.Count` |
| `ShowGold` | `_gold * 2` — viste dobbel verdi | `_gold` |

### Shop.cs
| Sted | Feilen | Fiks |
|---|---|---|
| `ShowItems` | `i < _items.Count - 1` — siste vare (Potion) ble aldri vist | `i < _items.Count` |
| `ShowItems` | `Price - 5` — viste falsk pris i butikken | `Price` |
| `FindItem` | `item.Name.ToLower() == name` — kun venstre side i småbokstaver, matchet aldri | begge sider `.ToLower()`* |
| `Search` | `if (!item.Name.Contains(search))` — viste alle varer som IKKE matchet | fjernet `!` → case-insensitiv* |

### Game.cs
| Sted | Feilen | Fiks |
|---|---|---|
| `SellItem` | `_player.RemoveGold(...)` — salg trakk fra gull i stedet for å gi | `_player.AddGold(item.Price)` |
| `SellItem` | Manglet `else` — ugyldig salg ga ingen tilbakemelding | la til `else { "Item not found" }` |
| `Start` (switch) | Manglet `default` — ugyldig menyvalg ble ignorert stille | la til `default: "Invalid choice"` |

---

## Tillegg utover de rene bugs

- **\*Case-insensitivt søk/finn** (3 steder): `Shop.FindItem` var en reell bug
  (asymmetrisk `ToLower`). `Player.FindItem` og `Search` ble gjort case-insensitive
  for konsistens, slik at "sword" og "Sword" begge treffer.
- **Null-advarsler ryddet** (alle 9 i bygg-panelet): `Console.ReadLine() ?? ""`
  (4 steder) og returtype `Item?` på begge `FindItem`. Dette er opprydding, ikke
  blant de logiske bugs.

---

## Forbedringer etter tilbakemelding fra læreren

Læreren bekreftet at alle hovedbugs var funnet, og foreslo 8 kvalitets-/arkitektur-
forbedringer. Alle er nå implementert:

| lærerens punkt | Hva som ble gjort |
|---|---|
| 1. Kjøpte items fjernes ikke fra shop | `Shop.RemoveItem()` kalles ved kjøp |
| 2. Inventory kan få duplikater | løst av #1 — varen er borte fra shop og kan ikke kjøpes på nytt |
| 3. Inventory mutable via Item reference | `Item` er immutable (private set); inventar og shop eksponeres som `IReadOnlyList<Item>` |
| 4. Rare item brukes aldri | rare varer vises med `(Rare)` i shop, inventar og søk |
| 5. Ingen historikk / logging | ny `History`-klasse logger hvert kjøp/salg; eget menyvalg viser loggen |
| 6 + 8. Separation of concerns / UI blandet med logikk | all `Console`-I/O flyttet til ny `View`-klasse; `Item`/`Player`/`Shop` er nå ren data + logikk; `Game` orkestrerer |
| 7. Search burde returnere List | `Shop.Search()` returnerer `List<Item>`; `View` viser resultatet |

I tillegg: salg legger varen tilbake i shop (symmetrisk med punkt 1), og `Menu.cs` er
foldet inn i `View.cs`. Prosjektet bygger fortsatt rent (0 advarsler, 0 feil) og hele
flyten (kjøp → fjernet fra shop → inventar → selg → tilbake i shop → søk → historikk)
er testet.
