> **⚠️ GHI CHÚ — XÓA KHỐI NÀY TRƯỚC KHI NỘP**
>
> - Ngôi thứ ba, giọng bị động — thống nhất với 6.2 và 6.3.
> - Trích dẫn RMIT Harvard. Mục này thêm 2 nguồn mới: **Fullerton (2018)** và **Schell (2019)** — nhớ gộp vào danh sách tham khảo tổng.
> - ⚠️ Bảng ở §6.4.3 có ba ô `[CONFIRM]` — mở GameConfig trong Unity đọc giá trị thật rồi điền.
> - Sau khi paraphrase bằng QuillBot: giữ nguyên các con số trong bảng, tên riêng **WildBound**, và các mốc tham chiếu mục (**6.3.1 / 6.3.4 / 6.3.6 / 6.4.1 / 6.4.3**) — công cụ paraphrase hay đổi số thành chữ.

---

## 6.4 Evaluation of the Product

### 6.4.1 Method and Criteria of Evaluation

Before the results are presented, the method employed and its limitations should be stated explicitly. All testing of WildBound was carried out by the developer alone, without the participation of external players. This means that every judgement concerning the game's difficulty, pacing and comprehensibility originates from the perspective of someone already familiar with each internal mechanism — a perspective differing fundamentally from that of a first-time player. In the game design literature, testing with genuine players is regarded as an irreplaceable stage, precisely because a developer loses the capacity to experience their own product as a newcomer would (Fullerton 2018).

Owing to this limitation, the evaluation below is divided into two groups carrying different degrees of reliability:

- **Objectively verifiable claims**: judgements that can be confirmed through measurable data, through the structure of the source code, or through the development process as it actually occurred. This group covers functional completeness, the soundness of the resource economy, and the extensibility of the architecture.
- **Subjective claims**: judgements concerning player experience, appropriate difficulty and engagement. These carry indicative value only and would require confirmation through testing with genuine players.

Four criteria are applied in evaluating the product: the degree of functional completion measured against the stated objectives, the soundness of the resource economy design, the quality of the code architecture in terms of extensibility, and the quality of feedback provided to the player.

---

### 6.4.2 Degree of Functional Completion

Measured against the specific objectives set out in Chapter 1, the outcomes are as follows:

| Stated objective | Outcome | Notes |
|---|---|---|
| Inventory and hotbar with drag-and-drop and stacking | Achieved | 8 hotbar slots plus 24 inventory slots |
| Crafting system with quantified recipes | Achieved | Three recipes; matching algorithm based on specificity |
| Interdependent survival statistics | Achieved | Health, thirst and hunger with conditional regeneration |
| Multi-step environmental interaction chain | Achieved | Six distinct interactions with the fire pit |
| Resource gathering with dedicated tools and durability | Achieved | Axe and pickaxe, each with its own target |
| Creature artificial intelligence | Partially achieved | Wandering and pursuit behaviour present; no pathfinding |
| Closed resource economy resistant to dead-end states | Achieved | See the analysis in Section 6.4.3 |
| Complete interface: main menu, HUD, death screen | Achieved | An additional creature variant was implemented beyond the original scope |

The core loop of the game is complete and fully playable: the player launches from the main menu, gathers resources, crafts tools, prepares food and water, engages creatures in combat, and concludes the session with a clearly defined performance metric. No link in this chain has been left unfinished or simulated with placeholder data.

It should be noted that three items beyond the original scope were also completed: the rock-mining system operated with the pickaxe, the higher-tier creature variant with its independent player-detection behaviour, and the paginated tutorial overlay presented on the main menu.

---

### 6.4.3 Soundness of the Resource Economy

This is the aspect most amenable to objective verification, since it is determined entirely by configuration values rather than by perception.

**Structure of supply.** Every resource type in WildBound has at least two independent sources, so that the loss of one source cannot produce a complete impasse:

| Resource | First source | Second source | Replenishment mechanism |
|---|---|---|---|
| Stick | Harvesting bushes (with berries) | Chopping trees with the axe | Bushes regrow on a fixed cycle |
| Rock | Collected from the map | Mining the boulder with the pickaxe | The boulder produces rock on a fixed cycle |
| Meat | Hunting ordinary rabbits | Hunting the alpha rabbit (double yield) | Burrows spawn rabbits periodically |
| Berries | Harvesting bushes | — | Bushes regrow on a fixed cycle |
| Clean water | Boiling dirty water at the fire pit | — | Dependent on fire pit durability |

**The initial tool cost problem.** The three existing crafting recipes require:

| Recipe | Sticks | Rocks | Total ingredients |
|---|---|---|---|
| Axe | 1 | 1 | 2 |
| Pickaxe | 1 | 2 | 3 |
| Fire pit | 5 | 4 | 9 |

The player begins with no tools and must therefore rely on the two sources that require none: harvesting bushes and collecting rock from the map. With a cost of two ingredients for the axe and three for the pickaxe, the threshold for obtaining an initial toolset lies within reach during the opening minutes, which avoids stranding the player at the outset. The fire pit, by contrast, with nine ingredients in total, becomes feasible only once tools are available. This produces a natural progression: manual gathering, then tool crafting, then large-scale extraction, then fire pit construction, and finally access to clean water and cooked food.

**Dead-end prevention.** The most serious risk in any crafting system with consumption is that the player expends finite resources on poor choices and enters an unrecoverable state. WildBound addresses this risk through two safety nets operating independently of the player's actions:

- **The boulder produces one unit of rock on a fixed cycle**, regardless of whether the player mines it. This means that even if all rock has been consumed and the player no longer possesses a pickaxe with which to mine, the rock supply still recovers.
- **Bushes regrow after a fixed interval**, guaranteeing that the supply of sticks and berries is never permanently exhausted.

Both mechanisms were designed prior to implementation, arising from an explicit balancing of supply against demand rather than being patched in after problems emerged.

**Consumption of surplus resources.** A problem symmetrical to the dead-end state is the accumulation of useless surplus: once the player has crafted sufficient tools, further sticks and rocks serve no purpose. WildBound resolves this by permitting sticks and rocks to be fed into the fire pit to restore its durability, converting surplus resources into extended structure lifespan.

| Fuel material | Durability restored | Notes |
|---|---|---|
| Stick | [CONFIRM] points | Abundant source, lower restoration value |
| Rock | [CONFIRM] points | Scarcer, higher restoration value |
| Fire pit maximum durability | [CONFIRM] points | Ceiling when refuelling |

The difference in restoration value between the two materials reflects their relative scarcity accurately, presenting the player with a meaningful choice rather than a mechanical action.

---

### 6.4.4 Quality of the Code Architecture

The extensibility of the architecture has been confirmed not merely in theory but through the development process itself. Two features added at a late stage of the project served as a natural test:

**The pickaxe.** This is a complete tool with its own three-dimensional model, durability, crafting recipe and extraction target. Integrating its combat and durability logic required only one additional boolean variable and one additional conditional branch within the attack-handling class, as presented in Section 6.3.6. The entire damage and durability mechanism functioned immediately without modification.

**The higher-tier creature variant.** The alpha rabbit possesses different health, damage and pursuit speed, together with a player-detection behaviour that the ordinary rabbit lacks. This variant was built entirely by adding a single control flag and a multiplier to the existing class, without creating a subclass and without duplicating any code.

**Centralisation of balance parameters.** More than fifty parameters governing game behaviour are stored in a single configuration asset, entirely separated from the logic-processing code. Adjusting difficulty, resource depletion rates or creature strength can be performed through the Unity interface without recompilation. This is a precondition for any serious balancing process undertaken subsequently, and equally a precondition for conducting testing with genuine players.

**Reuse of design patterns.** The state-preserving object replacement pattern presented in Section 6.3.1 is applied to two systems of differing natures: the fire pit with three states and the bush with two. That a single design pattern serves two unrelated systems indicates that the level of abstraction achieved is substantive rather than nominal.

---

### 6.4.5 Quality of Player Feedback

Every significant action in WildBound produces immediate visual feedback:

| Situation | Feedback | Purpose |
|---|---|---|
| Damage received | Red screen flash, then fade | Signals health loss while the player is focused on the environment |
| Cooking in progress | Percentage progress indicator | Shows that the operation is running and how long remains |
| Holding a tool | Durability value on the icon | Warns that the tool is close to breaking |
| Looking at a creature | Name and current health | Distinguishes the ordinary rabbit from the alpha before engaging |
| Looking at an interactable object | Prompt text describing the action | Removes the need to memorise key bindings |
| Looking at the fire pit | Remaining durability | Informs the decision whether to add fuel |
| Before the first session | Tutorial overlay on the main menu | Conveys the rules that cannot be inferred by experimentation |

The governing principle is that the player should understand the game state without recourse to external documentation. The tutorial overlay described in Section 6.2.2 serves the same principle at the point of entry: rather than supplying a separate manual, the rules least amenable to discovery through play — above all the conditions under which health regenerates — are presented within the game itself before the first session begins. The red-flash feedback mechanism illustrates this: it was introduced after observing that, when relying on the health bar alone, players readily fail to notice that they are losing health while concentrating on their surroundings.

It must be emphasised, however, that the entirety of this section falls within the subjective group defined in Section 6.4.1. Whether these feedback mechanisms are genuinely intelligible to a new player has not been verified.

---

### 6.4.6 Remaining Limitations

The limitations are divided into three groups according to their nature, since their severity and their routes to resolution differ.

**Group 1 — Features not yet built**

*No save or load functionality.* This is the most substantial limitation in feature terms. The entirety of the player's progress exists in memory and is discarded on exit. Consequently every session must begin afresh, which restricts the game to short sessions and diminishes the value of accumulating resources over the longer term.

*No day–night cycle or weather system.* Both systems formed part of the original intention but were removed when the project scope was narrowed. Their absence leaves the environment comparatively static: conditions in the first minute and the thirtieth minute are identical, save for the player's own depleted statistics.

*The game has no audio.* There is no background music, no sound effects for actions, and no audio warning cue. This is a considerable omission in experiential terms, since audio is the second most important feedback channel after vision, particularly in the survival genre where signalling a threat outside the field of view carries significant weight (Schell 2019).

*Content confined to a single map.* There are no new regions to explore and no long-term objective beyond extending survival time. This limits the motivation to replay once the player has grasped all the mechanics.

**Group 2 — Existing systems implemented at a basic level**

*Creature artificial intelligence.* The rabbit moves in four fixed directions and pursues the player in a straight line, without pathfinding capability. A creature may consequently become stuck when an obstacle lies between itself and the player. Unity's navigation package is already present in the project but has not been used, owing to time constraints.

*The crafting system is restricted to two ingredients.* The current recipe structure permits the combination of exactly two ingredient types, which constrains the recipe design space and makes expansion towards more elaborate recipes difficult.

*Limited coherence of visual style.* Because several asset packages by different authors were used, the models differ somewhat in their level of detail and modelling style. This follows directly from the decision to prioritise time for system design — a conscious trade-off, but a limitation of the finished product nonetheless.

*Singleton-based architecture.* As analysed in Section 6.3.4, this pattern suits the present scale but would become an obstacle were the project extended to a multiplayer mode.

**Group 3 — Limitations of the evaluation process**

*No testing with external players.* This is the most serious limitation in methodological terms, and differs in kind from the two groups above: those concern things not yet built, whereas this one means that all current balance parameters reflect the perception of a single individual — one who already knew every mechanism in advance. Values such as statistic depletion rates, tool durability and creature health may prove too easy or too demanding for a new player, and there is at present no means of determining which.

---

### 6.4.7 Overall Assessment

Measured against the scope originally defined, WildBound achieves its central objective: a survival game with a complete loop, systems operating in connection with one another, a resource economy designed on a reasoned basis, and a codebase sound enough to support continued development.

A notable observation arising from the classification of limitations is that most fall into the category of **features not yet added** rather than **existing systems functioning incorrectly**. This distinction carries practical significance: what has been built operates as designed, and most limitations could be addressed by building upon the present architecture rather than redesigning it. The single exception is the Singleton architecture, which would require revision were a multiplayer mode introduced.

Were the project to be continued, a reasonable order of priority would be: testing with genuine players in order to establish a basis for balance adjustment, then the addition of save functionality, and only then the expansion of content. The rationale for this ordering is that the first two items determine the quality of what already exists, whereas expanding content increases volume without improving the core.

---

# ADDITIONAL REFERENCES FOR SECTION 6.4

> The sources below supplement the lists already provided in Chapter 3 and Section 6.3.

Fullerton T (2018) *Game design workshop: a playcentric approach to creating innovative games*, 4th edn, CRC Press, Boca Raton.

Schell J (2019) *The art of game design: a book of lenses*, 3rd edn, CRC Press, Boca Raton.
