# 3D_Survival_Project (Unity 6, URP)

First-person survival/farming game, built on top of Mike's Code "3D Survival Game" tutorial
but with custom-redesigned systems. Actively developed by the project owner with Claude's help.

Note: this folder was previously named `3D_Farming_Project` (renamed mid-development).
Both `3D_Farming_Project.sln` and `3D_Survival_Project.sln` may exist side by side — harmless,
Unity just regenerated the solution file after the rename.

## Architecture conventions

- **Singleton pattern**: `InventorySystem.Instance`, `CraftingSystem.Instance`, `SelectionManager.Instance`,
  `HotbarSelection.Instance`, `PlayerStats.Instance`, `ToolLibraryUI.Instance`, `ReferenceManager.Instance`.
  No null-checks at most call sites — missing scene object/wrong load order throws NRE.
- **Resources.Load by name**: UI item prefabs live in `Assets/Resources/[ItemName]`, world (3D, droppable)
  prefabs live in `Assets/Resources/WorldItems/[ItemName]`. Item identity is a bare string match against
  GameObject name (`.Replace("(Clone)","")`) — renaming a prefab silently breaks matching.
- **ItemData component**: attached to every item UI prefab. Holds `itemName`, `maxStack`, `isConsumable`,
  `hungerRestore`, `thirstRestore`. Used to distinguish the actual item from the child `StackText` in a slot.
- **GameConfig ScriptableObject** (`Assets/_Project/Settings/GameConfig.asset`, script at
  `Scripts/Core/Gameconfig.cs`): centralizes all tunable numbers (speed, drain rates, damage, rabbit stats...).
  Scripts read `config.field` instead of hardcoding. Must be dragged into each MonoBehaviour's Inspector
  slot manually — no central auto-wiring.
- **ReferenceManager**: singleton holding the Canvas reference used by `DragDrop`.
- **Tag conventions**: `"Slot"` = inventory/hotbar slot, `"Player"` = player, `"Water"` = water object,
  `"FirePit"` = firepit.
- **Layer conventions**: `Ground` layer = terrain, excluded from `SelectionManager`'s raycast via `ignoreLayer`.

## Completed systems

- **Inventory + Hotbar**: 8 hotbar slots (filled first) + 18 inventory slots, scanned via `PopulateSlotList()`.
- **Drag & Drop**: `DragDrop.cs` + `ItemSlot.cs`, `RaycastAll` to detect the slot under the cursor.
  `ItemSlot.Item` getter finds the child with an `ItemData` component (skips `StackText`).
- **Stacking**: Apple/Rock/Stick max 10, count shown via a `StackText` TMP_Text child,
  `RefreshStackDisplay()` called after every operation.
- **Crafting**: `CraftingRecipe` ScriptableObject (input1+input2=output), `CraftingSystem`, `CraftingSlot`
  (Input1/Input2/Output). Craft button lights up when both inputs match a recipe.
- **ToolLibrary**: `ToolLibraryUI` paginates 4 recipes/page. `RecipeSlotUI`'s Choose button moves the real
  item from inventory into the crafting slots (doesn't spawn a new one), then closes the panel.
- **PlayerStats**: HP/Thirst/Hunger. Thirst drains over time + sprint bonus, Hunger drains slower.
  Thirst empty → HP -3/s, Hunger empty → HP -1/s.
- **Sprint**: hold LeftShift + move forward, `isSprinting` public property.
- **HotbarSelection**: keys 1-8 + scroll wheel, yellow highlight on selected slot. Q drops the item
  (spawns the WorldItems prefab). F consumes (Apple: +10 Hunger/+5 Thirst; WaterBottle: +50 Thirst,
  then reverts to empty Bottle).
- **PlayerAttack**: left-click raycast from camera forward, damages `RabbitHealth` on hit.
- **RabbitHealth + AI_Movement**: rabbit has HP, aggros when hit, drops RawMeat on death,
  wanders in 4 random directions.
- **PotInteraction** (fire pit / cooking flow): hold Pot + look at Water → "Take Dirty Water" →
  click turns it into DirtyWaterPot. Hold DirtyWaterPot + look at FirePit → "Boil Water" → click
  removes the pot, FirePit changes state. Hold Bottle + look at a BoiledWaterFirePit → "Scoop Water" →
  creates WaterBottle. Hold RawMeat + look at FirePit → hold F for 10s → CookedMeat.
- **FirePitManager**: state machine Normal → Boiling → BoiledWater. 30s boil timer, tracks 3 scoops,
  reverts to Normal + drops a Pot world item after the 3rd scoop.

## Active dev scene

`SmallIslandScene.unity` is the scene actually being played/tested (not `FarmScene`/`TestScene`).
**It's saved in Unity's Binary serialization format** (unlike `FarmScene.unity`/`TestScene.unity`,
which are Text/YAML) — grepping GUIDs as hex text won't find components in it (binary stores GUIDs as
raw bytes, not ASCII hex). Class names (`m_EditorClassIdentifier`) are still greppable as plain text
(`grep -a -o "Assembly-CSharp::[A-Za-z_]*" file.unity`) since those are always stored as strings even
in binary mode — useful for confirming a component exists, but field *values* aren't recoverable this
way. When in doubt about live Inspector state in this scene, ask for a screenshot instead of trying to
grep the file.

## PotInteraction / FirePitManager cooking chain — debugged and working as of 2026-07-17

Long debugging session (multiple root causes, fixed one at a time):

1. **`SpawnReplacement()` fails silently if the target prefab lacks a `FirePitManager` component**
   ([FirePitManager.cs](Assets/_Project/Scripts/Core/FirePitManager.cs)): `Instantiate` succeeds,
   `replacement.GetComponent<FirePitManager>()` returns null, the `if (newFP != null)` block is
   skipped (no state/prefab-ref copy), but `Destroy(gameObject)` **still runs unconditionally** —
   leaving a component-less replacement that can never transition or be interacted with again. Bit
   the project twice (once on `boilingFirePitPrefab`/`boiledFirePitPrefab`, once on `firePitPrefab`).
   Now logs `Debug.LogError` when this happens — check Console first before re-diagnosing this class
   of bug.
2. **`ScoopWater()` never reset `state` back to `Normal`** after the 3rd scoop — it swapped the visual
   prefab but the new instance inherited the old `BoiledWater` state via the copy-forward logic in
   `SpawnReplacement`, so scooping (and dropping a Pot) kept re-triggering forever. Fixed: `state =
   FirePitState.Normal;` is now set before calling `SpawnReplacement(firePitPrefab)`.
3. **Self-referencing prefab field gotcha**: `firePitPrefab` on the FirePit *scene instance* (not the
   asset) was dragged from the Hierarchy instead of the Project window, creating a reference to the
   live scene GameObject rather than the stable prefab asset. It looked completely normal right up
   until `StartBoiling()` destroyed that very object — at which point every downstream
   `SpawnReplacement` copy of that field showed `Missing (Game Object)` in the Inspector (Unity's
   "was valid, now destroyed" indicator — different from `None`/never-assigned). **Lesson**: always
   drag prefab references from the **Project window**, never from the Hierarchy/Scene view, even when
   the field is meant to self-reference the prefab you're currently editing.
4. **`InteractableObject` doubles as "show my name" AND "let me be picked up + destroyed on click"**,
   with no way to opt out. Any structure (FirePit, trees, ...) that has this component just for the
   `SelectionManager` name-label was also silently destroyable — `Update()`'s `Destroy(gameObject)`
   ran unconditionally on left-click whenever `SelectionManager.onTarget`/`selectedObject` pointed at
   it, race-firing alongside `PotInteraction`'s own Mouse0 handling. Fixed by adding a public
   `isPickupable` bool (default `true`, preserves existing item behavior) that gates the whole
   pickup block — **must be manually unticked in the Inspector on every non-item object** that has
   `InteractableObject` attached (FirePit and friends).
5. **Pot-eject-on-3rd-scoop physics**: `AddForce(Vector3.up * f, ForceMode.Impulse)` was too weak if
   the Pot's Rigidbody has non-trivial mass (impulse ÷ mass). Switched to directly setting
   `rb.linearVelocity = Vector3.up * potEjectForce` (Unity 6 renamed `.velocity` →
   `.linearVelocity`), mass-independent. Also bumped the spawn height offset from `0.5f` to `2f` above
   the firepit position — spawning too close to the just-replaced FirePit's collider triggered a
   strong physics depenetration push that could override the intended upward velocity.
6. Water needs an actual **Collider** (a `BoxCollider` with `Is Trigger = true` works) for
   `Physics.Raycast` to detect it at all — a visual-only mesh is invisible to raycasts. Also applies
   generally: raycast/trigger-tag checks only see whichever GameObject the Collider itself lives on,
   not tags on a parent.

## In progress / not fully tested

- **`AI_Movement` turn-around-on-Water**: added `OnTriggerEnter` (reverses `walkDirection` 180° on
  touching a `"Water"`-tagged collider) but not yet confirmed working even with a Kinematic Rigidbody
  added to the rabbit. Suspect the Rigidbody was added to a child mesh object rather than the same
  GameObject as the `AI_Movement` script (root) — Unity only routes trigger messages to the
  GameObject holding the Rigidbody in a compound-collider hierarchy. `RabbitHealth`'s raycast
  detection already needs `GetComponentInParent` from `SelectionManager`, confirming the rabbit's
  Collider lives on a child, not the root — same layout likely applies here. Needs Rigidbody
  relocated to the root object (same GameObject as `AI_Movement`) if not already there.
- **SelectionManager text stuck hidden — two bugs found and fixed, 2026-07-14/15**:
  1. Root cause was NOT the Ignore Layer field (user confirmed via Inspector screenshot it was already
     correctly set to `Ground, Water`). First bug: `PotInteraction.HandleInteractionText()` set
     `SelectionManager.Instance.overrideText = true` via `ShowText()` when looking at a Pot/Water/FirePit
     combo, but its early-out `if (!hasHit) return;` skipped calling `HideText()` when the raycast
     subsequently hit nothing — leaving `overrideText` stuck `true` forever. Fixed by calling
     `HideText()` before that early return.
  2. Deeper architectural bug, found after the above fix still didn't resolve it: `SelectionManager` and
     `PotInteraction` both run `Update()` every frame and both directly toggle
     `SetActive()`/write `.text` on the **same shared** `interaction_Info_UI` GameObject, with no script
     execution order defined (no `ProjectSettings/ScriptExecutionOrder.asset`, no
     `[DefaultExecutionOrder]`). Whenever the player's held item + raycast target didn't match any Pot
     condition, `PotInteraction.HideText()` unconditionally called
     `interaction_Info_UI.SetActive(false)` — which could run *after* `SelectionManager.Update()` in the
     same frame and stomp its `SetActive(true)`/text write (e.g. looking at an Apple would show
     `Text Input: "Apple"` in the Inspector — proving the script ran — while the GameObject itself ended
     up inactive and invisible in Game view, because Inspector still shows field values on inactive
     objects). Fixed by removing `SetActive(false)` from `PotInteraction.HideText()` — it now only
     resets `overrideText = false` and lets `SelectionManager` (the actual owner of
     `interaction_Info_UI`) decide active/inactive itself next frame based on its own raycast. Not yet
     re-tested in-Editor after this second fix.
  - **Lesson**: don't let two different scripts directly call `SetActive`/write fields on a UI object
    they don't "own" — route control through a single owner (here, `SelectionManager`) and have other
    scripts only flip a flag/queue that the owner reads.

## Known bugs

- `InventorySystem.FindNextEmptySlot()` returns `new GameObject()` instead of `null` when full —
  currently masked because callers check `isFull`/`CheckIfFull()` first, but it's a latent trap
  (leaks stray GameObjects if ever called while full).
- ~~Duplicate `PlayerMovement.cs` causing CS0111~~ — **resolved/stale**: verified only one
  `PlayerMovement.cs` exists under `Assets/_Project`; the only other matches are unrelated sample
  scripts inside `Library/PackageCache` (Cinemachine/HDRP samples), which aren't compiled into the project.
- `CraftingSlot.OnBeginDrag` used to call `ExecuteEvents.ExecuteHierarchy`, causing a StackOverflow —
  fixed, do not reintroduce.

## Don't do it this way (lessons learned)

- **`childCount` to detect an item in a slot**: `StackText` is a child, so `childCount > 0` even when
  the slot is empty. Always use `ItemSlot.Item` (getter that looks for an `ItemData` component).
- **`GetChild(0)` to grab the item**: grabs `StackText` instead. Use `ItemSlot.Item`.
- **Sphere Collider around the player for interaction range**: too complex, breaks the existing system.
  Use raycast + tag instead.
- **`ExecuteEvents.ExecuteHierarchy` inside `CraftingSlot.OnBeginDrag`**: causes an infinite loop. Don't
  reintroduce.
- **`SetActive(false)` on the ToolLibrary root GameObject at Start**: an inactive object's Awake/Start
  never runs, so `onClick` never gets wired up. Fix: put a child Panel and toggle that instead of the
  parent.
- **Dragging a prefab reference from the Hierarchy instead of the Project window**: creates a
  reference to the live scene instance, not the stable asset — looks identical in the Inspector until
  that instance gets destroyed, then shows `Missing (Game Object)`. Always drag from Project window.
- **`GetComponent<T>()` on an `Instantiate`d replacement without checking for null**: if the target
  prefab is missing the expected script, the null check gets skipped silently and
  `Destroy(gameObject)` still runs — the object effectively "disappears" into a dumb, script-less
  replacement. Always `Debug.LogError` on the else-branch, not just on the null-prefab-argument case.
- **`Resources.Load<T>()` passed straight into `Instantiate` without a null check**: throws
  `ArgumentException` if the name doesn't match a file in `Resources/` exactly. Always load into a
  local variable and check for null first (see `InventorySystem.AddToInvetory`,
  `PotInteraction.ReplaceHeldItem` for the pattern).

## Items

- **UI prefabs** (`Resources/`): Apple, Axe, Rock, Stick, Pot, DirtyWaterPot, BoilledWaterPot, Bottle,
  WaterBottle, RawMeat, CookedMeat, Empty_Pot, FirePit.
- **World prefabs** (`Resources/WorldItems/`): Apple, Rock, Stick, Pot, RawMeat, CookedMeat, FirePit,
  BoillingWaterFirePit(1), BoilledWaterFirePit(1).

## Script layout

- `Scripts/Core/`: AI_Movement, CraftingRecipe, CraftingSlot, CraftingSystem, FirePitManager, ItemData,
  PotInteraction, RabbitHealth, RecipeSlotUI, ReferenceManager, SelectionManager, ToolLibraryUI,
  Gameconfig, Craftingrecipe/Craftingslot/Craftingsystem (note: some file names use lowercase, e.g.
  `Craftingrecipe.cs`, `Gameconfig.cs` — check actual casing before referencing).
- `Scripts/UI/`: DragDrop, InventorySystem, ItemSlot, Itemdata, Hotbarselection.
- `Scripts/Player/`: MouseMovement, PlayerAttack, PlayerMovement, Playerstats.
- `Scripts/Interaction/`: InteractableObject, Potinteraction.
- `Scripts/Mobs/`: AI_Movement, RabbitHealth.
- `Settings/` (or `Assets/_Project/GameConfig.asset`): GameConfig ScriptableObject asset.

## Scenes

- `Scenes/Main/FarmScene.unity`, `IslandScene.unity`, `SmallIslandScene.unity` — world/gameplay scene
  candidates (naming suggests iteration on the map).
- `Scenes/UI/MenuScene.unity` — main menu.
- `Scenes/Dev/test.unity`, `TestScene.unity` — developer test scenes.

## Notable gap

Despite the survival/farming premise, there is **no crop planting/growth system** in the codebase yet
(no scripts for soil, seeds, or plant growth over time). If the farming loop is a goal, this is
likely the biggest missing core system.
