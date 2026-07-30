> **⚠️ GHI CHÚ — XÓA KHỐI NÀY TRƯỚC KHI NỘP**
>
> - Ngôi thứ ba, giọng bị động — thống nhất với Chương 3.
> - `📌 [IMAGE n]` = vị trí chèn ảnh chụp màn hình.
> - **KHÔNG chạy QuillBot lên các khối code** — chỉ paraphrase phần văn xuôi. Sau khi paraphrase, kiểm tra lại tên riêng và thuật ngữ trong dấu backtick (`MonoBehaviour`, `ScriptableObject`, `Quaternion.Slerp`, `Time.deltaTime`, `GetComponentInParent`, `WildBound`) — công cụ paraphrase thường thay thế sai những từ này.
> - Số dòng trong khối code phải khớp với số dòng được nhắc trong phần giải thích — nếu chỉnh sửa code, nhớ cập nhật lại số dòng trong văn bản.

---

# 6.2 Product Features

This section presents the principal features of WildBound through screenshots captured from the running product. The seven features selected cover the complete core loop of the game, from the opening screen through the processes of resource gathering and crafting, to combat situations and the conclusion of a play session.

---

## 6.2.1 Main Menu

📌 **[IMAGE 1]** *The main menu showing the game title, the Play button and the blurred background scene.*

The main menu is the entry point of the game, comprising the product title positioned centrally with the Play button below it. The background is an actual view of the island, observed through a camera that automatically pans horizontally around a fixed point before transitioning to a different vantage point by means of a fade effect.

The entire background scene is blurred using a Depth of Field effect. The purpose of this design choice is to create visual depth on the main menu without drawing attention away from the interactive elements. Because the interface layer is rendered in Screen Space Overlay mode, it falls outside the scope of post-processing effects applied to the camera, and consequently the game title and controls retain complete sharpness against the blurred background.

---

## 6.2.2 In-game Interface

📌 **[IMAGE 2]** *The in-game view showing all interface components together with the interaction prompt text.*

The in-game interface is organised on the principle of positioning all information at the periphery of the screen, keeping the central region — where the player observes and operates — unobstructed.

The components comprise three survival status bars (health, thirst and hunger) accompanied by their specific numerical values; a hotbar of eight slots along the lower edge, in which the currently selected slot is marked with a yellow highlight; a survival timer in the corner of the screen; and an interaction prompt that appears centrally when the player directs their view towards an interactable object.

Displaying both the status bar and the numerical value simultaneously is a deliberate decision: the bar allows the level of danger to be recognised rapidly through peripheral vision, whereas the numerical value supports precise decision-making, such as when weighing whether to sprint based on the quantity of water remaining.

---

## 6.2.3 Inventory and Crafting Panel

📌 **[IMAGE 3]** *The open inventory interface, showing stacked items with quantity indicators and an item being dragged.*

The inventory interface is opened with the E key and comprises two functional areas. The storage area contains twenty-six slots in total, of which the first eight simultaneously serve as the hotbar slots and are always filled first. The crafting area consists of two input slots, an output slot and a button that executes the craft.

Items of the same type are stacked within a single slot, with the quantity displayed in the corner of that slot. This same display position is reused to indicate remaining durability for tools, where the durability value takes precedence when the item in question is a tool.

Items are moved between slots by means of a drag-and-drop operation. The craft button becomes enabled only when both input slots contain the correct item types in sufficient quantities according to a defined recipe.

---

## 6.2.4 Tool Library

📌 **[IMAGE 4]** *The tool library panel displaying recipes with their ingredients and the Choose button.*

The tool library is a reference panel listing all crafting recipes available in the game, paginated at four recipes per page. Each entry displays the icons of the two ingredient types together with the required quantities, and the icon of the resulting product.

This feature was introduced to address a usability problem: if crafting recipes are not presented anywhere within the game, the player is obliged either to memorise them or to experiment at random, which produces a sense of being stuck.

Each recipe is accompanied by a Choose button. When activated, the system automatically locates the required quantity of each ingredient within the inventory, transfers them into the two input slots and closes the reference panel. It should be emphasised that this operation moves the player's existing items rather than generating new ones; where insufficient ingredients are held, the operation is not performed.

---

## 6.2.5 The Cooking Interaction Chain

📌 **[IMAGE 5]** *The player holding raw meat and pressing the F key at the fire pit, with the cooking progress displayed as a percentage.*

The cooking interaction chain is the most complex interaction system in WildBound and simultaneously the system that connects the game's various resource streams together.

The system operates on the combination of the item the player is currently holding and the object towards which the player is directing their view. The same fire pit yields different interaction options depending on the item in hand: holding a pot of dirty water enables water to be boiled, holding an empty bottle at a fire pit that has finished boiling enables water to be collected, holding raw meat enables cooking, and holding a stick or a rock enables the fire pit's durability to be replenished.

The meat-cooking operation specifically requires the F key to be held continuously for ten seconds. Throughout this period the progress is displayed as a percentage and is reset should the player release the key or look away. The mechanism of a held key with progress feedback was chosen in preference to a single button press in order to convey that cooking requires time, and simultaneously to oblige the player to remain stationary — a deliberately introduced interval of vulnerability within the design.

---

## 6.2.6 Combat and Damage Feedback

📌 **[IMAGE 6]** *A rabbit pursuing the player, together with the full-screen red flash effect at the moment damage is received. If a moment showing the alpha rabbit attacking can be captured, that image is preferable, as it depicts both creature variants.*

The rabbit is the only creature in WildBound and functions simultaneously as a food source and as a threat. The game features two variants of this creature.

**The ordinary rabbit** moves randomly in four directions and does not attack unprompted. However, as soon as it is attacked for the first time, it enters a permanently aggressive state: it turns towards the player, pursues at a speed higher than its ordinary movement rate, and attacks on a fixed cycle once within range. This design converts hunting into a decision carrying risk, rather than an entirely safe operation: the player must weigh the health that may be lost against the food to be gained.

**The alpha rabbit** is a rare variant, distinguished by its larger size and a conspicuous reddish-pink colouring. It possesses double the health, damage and pursuit speed of an ordinary rabbit, and yields double the quantity of meat when killed. The most significant difference lies in its behaviour: the alpha rabbit **turns aggressive of its own accord as soon as the player enters its detection radius**, rather than waiting to be attacked first. This radius is wider than its actual attack range, meaning that it detects the player from a distance and then closes in.

At most one alpha rabbit exists on the map at any given time. Once killed, the system must produce three ordinary rabbit spawns before a replacement is generated.

The distinction between the two variants introduces a second tier of risk into the game. The ordinary rabbit represents a safe food source provided the player accepts a small loss of health, whereas the alpha rabbit constitutes a high-reward target that obliges the player to prepare in advance, both in terms of current health and of the tool being carried. Its markedly different colour and size are a deliberate decision, intended to allow the player to recognise the threat from a distance and to decide independently whether to engage or avoid it.

When the player receives damage — whether from a rabbit bite or from the depletion of water and food — the entire screen flashes red briefly and then fades. This feedback mechanism was introduced following the observation that, when relying on the health bar alone, players readily fail to notice that they are losing health while concentrating on observing their surroundings.

---

## 6.2.7 Death Screen

📌 **[IMAGE 7]** *The death screen showing the red background, the Game Over text, the survival time and the two control buttons.*

When the player's health reaches zero, the death screen is activated. A red background layer fades in over approximately two seconds rather than appearing instantaneously, in order to produce a softer transition.

At the same time, all of the player's control systems — movement, camera rotation, attacking, hotbar selection and interaction — are disabled, and the mouse cursor is released so that the player may operate the on-screen buttons.

The survival time for that session is frozen and displayed directly beneath the closing text, while the timer on the main interface is hidden. This is the sole performance metric in the game: as WildBound has no victory condition, survival time serves as the measure of the player's effectiveness and as the motivation for attempting a further session. Two control buttons allow a new session to be started or the main menu to be returned to.

---

# 6.3 Product Implementation

This section analyses seven representative pieces of code from WildBound. The pieces were selected on the principle that each should illustrate a distinct technique, so as to reflect the range of technical problems addressed during development. All code has been condensed to retain only the essential portions; omitted sections are marked with `// ...`.

---

## 6.3.1 The State-Preserving Object Replacement Pattern

**The problem to be solved.** The fire pit in WildBound has three states with differing appearances: an ordinary fire pit, a fire pit currently boiling, and a fire pit that has completed boiling. Similarly, the bush has two states: bearing berries and harvested. The requirement was that the three-dimensional model should change correspondingly when the state changes, while all progress data — remaining durability, the number of scoops taken, the regrowth timer — must be preserved.

```csharp
 1  private void SpawnReplacement(GameObject prefab)
 2  {
 3      if (prefab == null) { Debug.LogError("Prefab is null in FirePitManager!"); return; }
 4      GameObject replacement = Instantiate(prefab, transform.position, transform.rotation);
 5
 6      FirePitManager newFP = replacement.GetComponent<FirePitManager>();
 7      if (newFP != null)
 8      {
 9          newFP.state      = state;        // transfer the current state
10          newFP.scoopCount = scoopCount;   // transfer the scoop counter
11          newFP.config     = config;
12          newFP.uses       = uses;         // transfer the remaining durability
13      }
14      else
15      {
16          Debug.LogError(prefab.name + " is missing a FirePitManager component");
17      }
18      Destroy(gameObject);
19  }
```

**Technical explanation.** This method implements the state machine pattern in combination with prefab swapping (Nystrom 2014). Line 4 creates the new instance at the exact position and rotation of the existing one. Lines 9 to 12 transfer the data to the new instance. Line 18 destroys the original.

**Design rationale.** An alternative approach would have been to retain a single object and swap only its rendering component. However, the fire pit states differ not merely in their models but also in their particle effects, collision positions and child object structures, which makes partial swapping both more complex and more error-prone than wholesale replacement.

The most noteworthy aspect is the structure of the block spanning lines 6 to 17. In the initial version, the `else` branch at line 14 did not exist, while the `Destroy` call at line 18 was nonetheless always executed. The consequence was that, where the target prefab lacked a `FirePitManager` component, the data transfer was silently skipped yet the original object was still destroyed, leaving behind a replacement object that could no longer be interacted with. This fault occurred twice in the project before the error message at line 16 was introduced. The lesson drawn is that every `GetComponent` call on a newly instantiated object requires a branch handling the case in which the result is null.

It should be noted further that the code above does **not** copy prefab references to the new instance. All prefab references are held centrally on a single manager object within the scene. This arrangement eliminates entirely the risk of an instance inadvertently referencing the very object about to be destroyed — a fault that did occur and is discussed in Section 3.1.4.

The same code structure is applied again in the bush system, differing only in the data type and the set of variables transferred. That a single design pattern serves two systems of differing natures demonstrates the generality of the solution.

---

## 6.3.2 The Crafting Recipe Matching Algorithm

**The problem to be solved.** The crafting system must determine which recipe corresponds to the two ingredients the player has placed in the input slots. A difficulty arises when several recipes use the same pair of ingredients but differ in the quantities required: the axe requires one stick and two rocks, whereas the fire pit requires four sticks and three rocks. An algorithm selecting the first matching recipe invariably returns the axe, even when the player has supplied sufficient ingredients for the fire pit.

```csharp
 1  public void CheckRecipe()
 2  {
 3      string item1 = input1Slot.ItemName;
 4      string item2 = input2Slot.ItemName;
 5      matchedRecipe   = null;
 6      int bestSpecificity = -1;   // total ingredients of the best matching recipe so far
 7
 8      foreach (CraftingRecipe recipe in allRecipes)
 9      {
10          bool straightMatch = recipe.input1Name == item1 && recipe.input2Name == item2
11              && HasEnough(input1Slot, recipe.input1Count)
12              && HasEnough(input2Slot, recipe.input2Count);
13
14          // The order in which ingredients are placed does not affect the result
15          bool swappedMatch = !straightMatch
16              && recipe.input1Name == item2 && recipe.input2Name == item1
17              && HasEnough(input1Slot, recipe.input2Count)
18              && HasEnough(input2Slot, recipe.input1Count);
19
20          if (!straightMatch && !swappedMatch) continue;
21
22          int specificity = recipe.input1Count + recipe.input2Count;
23          if (specificity > bestSpecificity)   // prefer the more demanding recipe
24          {
25              bestSpecificity = specificity;
26              matchedRecipe   = recipe;
27              inputsSwapped   = swappedMatch;
28          }
29      }
30      craftButton.interactable = (matchedRecipe != null);
31  }
```

**Technical explanation.** Rather than halting at the first matching recipe, the algorithm traverses the entire list and records the best match according to a quantitative criterion. This criterion is termed *specificity* and is calculated as the total number of ingredients the recipe requires (line 22). The recipe with the higher specificity is given precedence (line 23).

Lines 10 to 18 address a separate usability requirement: the player need not be concerned with the order in which ingredients are placed. Each recipe is tested in both orientations, and the `inputsSwapped` variable records which orientation matched so that the subsequent consumption stage deducts the correct quantity from the correct slot.

**Design rationale.** An alternative would have been to require every recipe to use a unique pair of ingredients, thereby prohibiting two recipes from sharing ingredients. Such an approach resolves the conflict but severely constrains the game's design space, since in a survival game with a finite number of resource types it is entirely natural for several tools to be crafted from wood and stone. The specificity-based solution preserves that flexibility while also conforming to player intuition: having expended more materials, a player expects to receive a correspondingly greater product.

---

## 6.3.3 The Survival Simulation Loop

**The problem to be solved.** The survival statistics system must simulate the relationship between the three statistics of health, thirst and hunger in real time, while generating continuous resource pressure upon the player.

```csharp
 1  private void Update()
 2  {
 3      bool isSprinting = playerMovement != null && playerMovement.isSprinting;
 4      bool isRegenerating = currentHP < config.maxHP
 5          && currentThirst > config.hpRegenThreshold
 6          && currentHunger > config.hpRegenThreshold;
 7
 8      float thirstDrain = config.thirstDrainRate;
 9      if (isSprinting)    thirstDrain += config.thirstSprintBonus;
10      if (isRegenerating) thirstDrain += config.thirstDrainRegenBonus;  // regeneration has a cost
11      currentThirst = Mathf.Max(0, currentThirst - thirstDrain * Time.deltaTime);
12
13      float hungerDrain = config.hungerDrainRate;
14      if (isRegenerating) hungerDrain += config.hungerDrainRegenBonus;
15      currentHunger = Mathf.Max(0, currentHunger - hungerDrain * Time.deltaTime);
16
17      if (currentThirst <= 0) TakeDamage(config.hpDrainWhenNoThirst * Time.deltaTime);
18      if (currentHunger <= 0) TakeDamage(config.hpDrainWhenNoHunger * Time.deltaTime);
19
20      if (isRegenerating)
21          currentHP = Mathf.Min(config.maxHP, currentHP + config.hpRegenRate * Time.deltaTime);
22
23      if (currentHP <= 0 && !isDead)
24      {
25          isDead = true;
26          if (DeadScreen.Instance != null) DeadScreen.Instance.Show();
27      }
28      UpdateUI();
29  }
```

**Technical explanation.** Every statistic transformation is multiplied by `Time.deltaTime`, the real elapsed time since the preceding frame. This ensures that the rate of statistic depletion is identical across all hardware configurations, irrespective of the frame rate achieved. Were this multiplication omitted, a player on high-performance hardware would lose water several times faster than one on lower-performance hardware.

The `Mathf.Max` and `Mathf.Min` functions are used to constrain statistics within their valid range, preventing negative values or values exceeding the defined maximum.

**Design rationale.** The essential game-design element lies in the `isRegenerating` variable and the manner in which it is employed at lines 10 and 14. Automatic health regeneration occurs only when both water and food exceed fifty per cent, and, more significantly, the regeneration process increases the depletion rate of those very statistics.

This design converts health regeneration into a trade-off rather than a cost-free mechanism. A severely injured player is obliged to choose between pausing to recover — which entails consuming their reserves of water and food more rapidly — and continuing to operate at low health. Were regeneration to occur unconditionally, the entire resource pressure of the game would be nullified, since the player would need only to wait for all damage to be erased.

Lines 17 and 18 implement the penalty for depleted statistics, at differing severities: running out of water inflicts three times the damage of running out of food. This disparity reflects the differing urgency of the two needs and simultaneously guides the player's order of priority when choosing which resource to seek.

---

## 6.3.4 Singleton Architecture and Data-driven Configuration

**The problem to be solved.** Many systems within the game require access to one another: the interaction system must add items to the inventory, and the combat system must deduct the player's health. Were each class obliged to hold direct references to every other class, the number of references requiring manual assignment in the editor would increase very rapidly.

```csharp
 1  public class InventorySystem : MonoBehaviour
 2  {
 3      public static InventorySystem Instance { get; set; }
 4
 5      private void Awake()
 6      {
 7          if (Instance != null && Instance != this)
 8              Destroy(gameObject);   // destroy the duplicate, retain a single instance only
 9          else
10              Instance = this;
11      }
12      // ...
13  }
```

In parallel, all balance parameters are separated from the source code and stored in a configuration asset:

```csharp
 1  [CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
 2  public class GameConfig : ScriptableObject
 3  {
 4      [Header("Player Stats - HP Regen")]
 5      public float hpRegenRate      = 5f;
 6      public float hpRegenThreshold = 50f;   // both Thirst AND Hunger must exceed this
 7
 8      [Header("FirePit Durability")]
 9      public int   firePitMaxUses     = 50;
10      public int   firePitBoilUseCost = 10;
11      public int   stickRepairUses    = 2;
12      public int   rockRepairUses     = 5;
13      // ... approximately forty further parameters
14  }
```

**Technical explanation.** The Singleton pattern ensures that a class has only one instance and provides a global point of access to it (Gamma et al. 1994). The static property at line 3 serves as that access point, while the block within `Awake()` guarantees uniqueness by destroying any duplicates. Any class within the project may therefore call upon the inventory system without declaring a reference to it.

The `GameConfig` asset implements the principle of data-driven design. Its public fields appear directly within the editor, permitting every balance parameter to be adjusted without recompiling the source code.

**Design rationale and limitations.** The Singleton pattern is criticised in much of the software architecture literature, principally for three reasons: it introduces global state, it conceals the dependency relationships between classes and thereby makes code harder to read, and it obstructs automated testing (Nystrom 2014).

The pattern was nonetheless selected for WildBound owing to the particular characteristics of the project: the game supports a single player, each system inherently exists as only one instance, and a codebase of thirty-one files remains within the comprehension of one individual. Within that context, the cost of constructing a full dependency injection system would exceed the benefit obtained.

It must nevertheless be acknowledged that this represents a genuine limitation should the project be extended. In the event that a multiplayer mode were developed, the assumption that each system is unique would no longer hold, and much of the current architecture would require redesign. This matter is discussed further in Section 7.3.

---

## 6.3.5 Context-dependent Interaction Chains

**The problem to be solved.** A single object within the game world must produce different behaviours according to the item the player is holding. The fire pit supports as many as six distinct interactions, all of which must be handled without rendering the source code unmanageable.

```csharp
 1  private void Update()
 2  {
 3      if (InventorySystem.Instance.isOpen) return;
 4
 5      string heldItem = GetHeldItemName();      // the item currently held
 6      RaycastHit hit;
 7      bool hasHit = Physics.Raycast(playerCamera.position, playerCamera.forward,
 8                                    out hit, interactRange);
 9
10      HandleInteractionText(heldItem, hasHit, hit);   // decides WHAT IS DISPLAYED
11      HandleInput(heldItem, hasHit, hit);             // decides WHAT IS PROCESSED
12  }
13
14  private void HandleInput(string heldItem, bool hasHit, RaycastHit hit)
15  {
16      if (!hasHit) { isCooking = false; cookHoldTime = 0f; return; }
17      string hitTag = hit.collider.tag;
18
19      // Pot + Water  ->  Pot of dirty water
20      if (heldItem == "Pot" && hitTag == "Water" && Input.GetKeyDown(KeyCode.Mouse0))
21      { ReplaceHeldItem("Pot", "DirtyWaterPot"); return; }
22
23      // Raw meat + Fire pit  ->  hold F for 10 seconds  ->  Cooked meat
24      if (heldItem == "RawMeat" && hitTag == "FirePit")
25      {
26          FirePitManager fp = hit.collider.GetComponent<FirePitManager>();
27          if (fp == null || fp.uses <= 0)      // a worn-out fire pit cannot cook
28          { isCooking = false; cookHoldTime = 0f; return; }
29
30          if (Input.GetKey(KeyCode.F))
31          {
32              isCooking = true;
33              cookHoldTime += Time.deltaTime;
34              if (cookHoldTime >= config.cookRequiredTime)
35              {
36                  ConsumeOneAndAdd("CookedMeat");
37                  fp.ConsumeCookUse();          // deduct the fire pit's durability
38                  isCooking = false; cookHoldTime = 0f;
39              }
40          }
41          else { isCooking = false; cookHoldTime = 0f; }   // releasing the key resets progress
42          return;
43      }
44      // ... four further interaction combinations
45  }
```

**Technical explanation.** In every frame, a ray is cast from the camera along the view direction (line 7) in order to determine which object the player is facing. Behaviour is then determined by the combination of the held item's name and the tag of the object the ray has struck.

Structurally, the separation into two distinct methods at lines 10 and 11 is noteworthy. `HandleInteractionText` determines only which prompt text is displayed, while `HandleInput` handles only key input. This separation is necessary because the two responsibilities have different activation conditions: the prompt must be displayed continuously while the player is facing the object, whereas the action is executed only at the moment a key is pressed.

The held-key mechanism is implemented at lines 30 to 41: the accumulator `cookHoldTime` increases in real time and is reset to zero as soon as the player releases the key or looks away.

**Design rationale.** An important architectural lesson emerged during the construction of this system. In the initial version, both this class and the class responsible for displaying object names directly enabled, disabled and wrote content to the **same** interface object, while Unity provides no guarantee regarding execution order between classes within a single frame. The consequence was that the two classes continually overwrote one another's results, producing the symptom of the prompt text disappearing unpredictably.

The problem was addressed by establishing the principle that **each interface object has exactly one owning class**. The owning class is the only class permitted to alter that object's display state; other classes may only set a flag, which the owner then reads in order to make its own determination. This principle eliminates the dependency on execution order entirely.

---

## 6.3.6 Logic Reuse Across Multiple Tools

**The problem to be solved.** The axe and the pickaxe inflict identical damage upon creatures and share the same durability-consumption mechanism, but differ in the objects upon which they may act: the axe fells trees, while the pickaxe mines rock. The requirement was to handle what they have in common without duplicating source code.

```csharp
 1  private void TryAttack()
 2  {
 3      if (Time.time - lastAttackTime < config.attackCooldown) return;
 4      lastAttackTime = Time.time;
 5
 6      Ray ray = cam.ScreenPointToRay(Input.mousePosition);
 7      RaycastHit hit;
 8      if (Physics.Raycast(ray, out hit, config.attackRange))
 9      {
10          string heldName     = GetHeldItemName();
11          bool holdingAxe     = heldName == "Axe";
12          bool holdingPickaxe = heldName == "Pickaxe";
13          bool holdingTool    = holdingAxe || holdingPickaxe;   // unify both tools
14
15          RabbitHealth rabbit = hit.collider.GetComponentInParent<RabbitHealth>();
16          if (rabbit != null)
17          {
18              rabbit.TakeDamage(holdingTool ? config.toolAttackDamage : config.attackDamage);
19              if (holdingTool) ConsumeToolDurability();
20              return;
21          }
22
23          Tree tree = hit.collider.GetComponentInParent<Tree>();
24          if (tree != null && holdingAxe)          // only the axe can fell a tree
25          { tree.Chop(); ConsumeToolDurability(); return; }
26
27          BigRock bigRock = hit.collider.GetComponentInParent<BigRock>();
28          if (bigRock != null && holdingPickaxe)   // only the pickaxe can mine rock
29          { bigRock.Mine(); ConsumeToolDurability(); return; }
30
31          Bush bush = hit.collider.GetComponentInParent<Bush>();
32          if (bush != null) { bush.TryHarvest(); return; }   // harvesting requires no tool
33      }
34  }
```

**Technical explanation.** Line 13 unifies the two tool types into a single boolean variable. As a result, the damage handling at line 18 and the durability consumption at line 19 are written once and apply to both tools. The difference between them appears only at lines 24 and 28, where each tool is associated with its corresponding target.

Lines 3 and 4 implement the cooldown interval between successive attacks, preventing the player from inflicting continuous damage by clicking rapidly.

The `GetComponentInParent` method is used in preference to `GetComponent` at lines 15, 23, 27 and 31. The reason is that the collision components of these objects reside on child objects rather than on the root object carrying the controlling script; searching only the object struck by the ray would therefore always return a null result.

**Design rationale.** This structure embodies the principle of avoiding code duplication. Its value was verified directly during development: the pickaxe was introduced in the final stage of the project, and its integration required only one additional boolean variable and one additional conditional branch, while the entire damage and durability behaviour functioned immediately without modification.

The order of the checks from line 15 to line 31 is likewise a deliberate decision. Creatures are tested first because they are mobile and frequently overlap stationary objects within the field of view; were trees or rocks tested first, a player could inadvertently fell a tree while attempting to attack a rabbit standing beside its trunk.

---

## 6.3.7 Cinematic Transition Control Through a State Machine

**The problem to be solved.** The main menu required an automatic camera that remains stationary at one position and pans horizontally to display the scene, then moves to a different vantage point and repeats indefinitely. The transition between positions had to be concealed so that the viewer does not perceive an abrupt jump.

```csharp
 1  private enum CamState { Panning, FadingOut, FadingIn }
 2
 3  private void Update()
 4  {
 5      stateTimer += Time.deltaTime;
 6      switch (state)
 7      {
 8          case CamState.Panning:
 9              float panT = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(stateTimer / panDuration));
10              transform.rotation = Quaternion.Slerp(panStartRot, panEndRot, panT);
11              if (stateTimer >= panDuration) { state = CamState.FadingOut; stateTimer = 0f; }
12              break;
13
14          case CamState.FadingOut:
15              SetOverlayAlpha(Mathf.Clamp01(stateTimer / fadeDuration));
16              if (stateTimer >= fadeDuration)
17              {
18                  currentIndex = (currentIndex + 1) % waypoints.Length;
19                  MoveToWaypoint(currentIndex);   // reposition while the screen is black
20                  state = CamState.FadingIn; stateTimer = 0f;
21              }
22              break;
23
24          case CamState.FadingIn:
25              SetOverlayAlpha(1f - Mathf.Clamp01(stateTimer / fadeDuration));
26              if (stateTimer >= fadeDuration) { state = CamState.Panning; stateTimer = 0f; }
27              break;
28      }
29  }
```

**Technical explanation.** The entire behaviour is organised into three cyclical states declared at line 1. A single timing variable drives all three states; upon each state transition this variable is reset to zero.

Line 10 employs `Quaternion.Slerp` rather than `Vector3.Lerp`. This spherical interpolation is designed specifically for rotations and ensures that the camera turns along the shortest arc at a constant angular velocity, avoiding the distortion that can arise when interpolating linearly across Euler angles.

Line 9 applies `Mathf.SmoothStep` to the interpolation factor. This function transforms linear progress into an S-shaped curve, causing the camera movement to begin slowly, accelerate through the middle and decelerate towards the end. The result is a movement that reads as cinematic rather than possessing the mechanical quality of a constant rotation rate.

Line 19 is the crux of the entire mechanism: the camera is repositioned precisely at the moment the screen has faded to complete darkness. The jump is thereby concealed entirely from the viewer.

**Design rationale.** The alternative would have been to move the camera continuously between vantage points. This approach was rejected because a straight path between any two points may pass through terrain or objects, producing visual defects whose correction would require an additional pathfinding system for the camera — an amount of work disproportionate to the value obtained on a menu screen.

With regard to implementation, the timer-based approach within `Update()` was chosen in preference to Unity's Coroutines. Both are technically viable, but the former maintains consistency with the remainder of the project, in which every time-based process — the water boiling duration, the bush regrowth interval, the creature spawning cycle — is implemented in the same manner. This consistency reduces the cognitive cost of subsequently rereading the source code.

---

# ADDITIONAL REFERENCES FOR SECTION 6.3

> The sources below supplement the list already provided in Chapter 3.

Gamma E, Helm R, Johnson R and Vlissides J (1994) *Design patterns: elements of reusable object-oriented software*, Addison-Wesley, Boston.

Nystrom R (2014) *Game programming patterns*, Genever Benning, Game Programming Patterns website, accessed 29 July 2026. https://gameprogrammingpatterns.com/
