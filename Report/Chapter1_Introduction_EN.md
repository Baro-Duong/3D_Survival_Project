> **⚠️ GHI CHÚ - XÓA KHỐI NÀY TRƯỚC KHI NỘP**
>
> - Ngôi thứ ba, giọng bị động - thống nhất với Chương 3 và Chương 6.
> - Trích dẫn RMIT Harvard. Chương này dùng lại nguồn **Fullerton (2018)** đã có ở mục 6.4.
> - ⚠️ Số commit ở §1.4 ghi **29** tại thời điểm viết - chạy `git rev-list --count HEAD` và cập nhật lại ngay trước khi nộp.
> - §1.5 **cố ý ngắn** theo đúng yêu cầu đề bài ("Short evaluation only"); đánh giá đầy đủ ở §6.4 và Chương 7.

---

# CHAPTER 1 - INTRODUCTION

## 1.1 Introduction to the Project Subject

The survival game is a genre in which the player is placed in a hostile environment with minimal starting resources and must sustain themselves by gathering materials, crafting tools and managing the basic needs of their character. What distinguishes the genre from others is that the principal threat comes not from an opponent but from the environment and from scarcity itself: the player loses not by being defeated, but by running out.

**WildBound** is a first-person survival game developed for this final-year project. The player is stranded on an isolated island carrying nothing but a cooking pot and an empty bottle, with no prospect of resupply. Three statistics determine survival - health, hunger and thirst - each of which declines continuously over time and can be restored only through the corresponding survival activity. To sustain them, the player must gather wood and stone, craft an axe and a pickaxe, build a fire pit, boil water and cook meat, while contending with the wildlife inhabiting the island. The game has no victory condition; its sole performance metric is the length of time the player manages to stay alive.

The title **WildBound** combines two elements that reflect the design of the game directly. *Wild* denotes untamed nature and the condition of surviving within it, while *Bound* carries the sense of being confined, tied to a fixed space - the island the player cannot leave. Together they express the central premise: escape is not available, only adaptation. The title additionally bears a phonetic resemblance to the name of the project's author.

The rationale for choosing this subject derives from the technical character of the survival genre. Unlike genres in which most of the effort is invested in pre-authored content, a survival game is composed of numerous small systems that run continuously and depend upon one another: the character's statistics affect their capacity to move, that capacity affects the rate of gathering, and the rate of gathering in turn determines whether the player can restore their statistics in time. It is precisely this quality that makes the genre a suitable vehicle for practising the design and implementation of software systems, rather than merely the assembly of visuals and scripted content. This is the competence the project sets out to develop.

---

## 1.2 Project Objectives

### General Aim

To build a first-person survival game that is complete at the level of its core loop, in which every game system is designed and implemented within the project itself rather than reproduced from an existing tutorial.

### Specific Objectives

1. **An inventory and hotbar system** supporting storage, drag-and-drop and item stacking, with a defined order of priority for filling slots.
2. **A crafting system** based on recipes with quantified ingredients, accompanied by a reference interface so that the player need not memorise them.
3. **A survival statistics system** comprising health, hunger and thirst, in which the statistics influence one another rather than operating independently.
4. **A multi-step environmental interaction chain**, specifically the process of treating water and preparing food at a fire pit.
5. **A resource gathering system with dedicated tools**, in which each tool possesses finite durability and acts only upon its corresponding target.
6. **Creature artificial intelligence**, encompassing free movement in the ordinary state and pursuit behaviour once provoked.
7. **A closed resource economy**, calculated so as to guarantee that the player cannot enter an unrecoverable dead-end state.
8. **A complete interface system**, comprising a main menu, an in-game interface and a closing screen that records the player's achievement.

### Non-functional Objectives

Alongside the functional objectives, the project sets two requirements concerning technical quality. First, all game balance parameters must be centralised in a single location and separated from the logic-processing code, so that adjusting difficulty does not require modifying the program. Second, the code architecture must permit new items, recipes and tools to be added at low cost, rather than obliging existing work to be rewritten.

### Out of Scope

Three systems originally envisaged were deliberately removed when the project scope was narrowed: crop cultivation, a day-night cycle and a weather system. This decision was taken to ensure that the core systems were finished properly, rather than leaving several systems simultaneously incomplete. The reasoning behind the decision and its consequences are examined in Chapter 7.

---

## 1.3 Project Plan

### Phases

The project ran from January to August 2026 and was carried out in five phases:

| Phase | Period | Principal activity |
|---|---|---|
| Learning the platform | 17/01 - 14/03/2026 | Becoming familiar with Unity through online tutorials; building a first prototype |
| Change of direction | 14/03 - 04/04/2026 | Departing from the tutorials to redesign systems independently; building terrain suited to the intended gameplay |
| Restructuring | 04/04 - 28/05/2026 | Transition to individual work; resolving render pipeline compatibility problems |
| Core system development | 28/05 - 16/07/2026 | Inventory, hotbar, crafting, combat, creature AI, environmental interaction |
| Completion | 16/07 - 15/08/2026 | Main menu, player onboarding, parameter balancing, report writing |

Progress was monitored through the university's milestone system, in combination with the commit history of the Git repository, which served as a development log at the granularity of individual changes.

### Two Decisions That Shaped the Plan

**Departing from online tutorials (14 March 2026).** The opening phase of the project was carried out by following a series of game development tutorials on a video-sharing platform. This approach permitted rapid familiarity with the tools but soon revealed its limits: the operations were reproduced without the reasoning behind each design choice, and difficulties arose as soon as a requirement fell outside the tutorial's scope, since no reference solution then remained. As the project called for systems built to its own design intentions, continuing to follow tutorials became an obstacle. From this point onward, the systems were rewritten from their foundations.

**Transition to individual work (4 April 2026).** The project was initially registered as the work of a three-person group. After the opening phase, the effectiveness of that collaboration proved unsatisfactory and overall progress suffered. The proposal to separate was put forward, with the aim of recovering control over both the schedule and the quality of the product. The decision substantially increased the individual workload, but in exchange removed any dependence upon the progress of others - a consideration of some weight for a project with a fixed deadline.

Both decisions increased the workload in the short term, yet both were judged necessary, since together they moved the project from reproduction towards independent design, which is the academic requirement of a final-year project.

---

## 1.4 Project Outcomes

### Deliverable

The product of this project is a working game, complete at the level of its core loop. The player launches from the main menu, reads the tutorial, enters the game, gathers resources, crafts tools, prepares food and water, contends with creatures, and concludes the session at a screen recording their survival time, from which they may either restart or return to the main menu. No link in this chain has been left unfinished or substituted with simulated data.

### Systems Completed

All eight specific objectives set out in Section 1.2 have been implemented: seven in full, and one - creature artificial intelligence - at a basic level, since pathfinding has not been implemented. A point-by-point comparison against each objective is presented in Section 6.4.2.

Beyond the originally intended scope, three further items were also completed:

- **A rock-mining system operated with the pickaxe**, extending the supply of minerals and introducing an additional crafting branch.
- **A higher-tier creature variant** (the alpha rabbit), with doubled statistics and the ability to detect the player independently, introducing a second tier of risk into the game.
- **A paginated tutorial overlay** on the main menu, addressing the difficulty new players face with mechanisms that are hard to discover unaided.

### Quantitative Figures

| Item | Figure |
|---|---|
| Source files written for the project | 32 C# files |
| Total lines of code | Approximately 3,200 |
| Centralised balance parameters | More than 50, held in a single configuration asset |
| Crafting recipes | 3 |
| Commits in the repository | 29 *(to be updated before submission)* |
| Scenes in the product | 2 (main menu and gameplay) |

---

## 1.5 Project Evaluation

This section offers a summary assessment only; the full evaluation is presented in Section 6.4 and in Chapter 7.

**With regard to what was achieved**, the most significant outcome is a core loop that is complete and fully playable - the project's foremost objective. The resource economy was, moreover, designed through an explicit balancing of supply against demand rather than by intuition, and the code architecture demonstrated its extensibility by accommodating three out-of-scope additions at a late stage of the project.

**With regard to limitations**, the product has no save functionality, no audio, and neither the day-night cycle nor the weather system originally envisaged. Creature artificial intelligence likewise remains at a basic level.

**The most serious limitation, however, lies not in the product but in the process of evaluating it**: all testing was carried out internally within the project, with no external players involved. This means that the present balance parameters reflect the perception of someone who already knew every mechanism in advance, and their suitability for a new player remains unverified (Fullerton 2018).

Taken as a whole, the project fulfils the scope defined for it following the narrowing described above, with most remaining limitations belonging to the category of features not yet added rather than existing systems functioning incorrectly.

---

# REFERENCES - CHAPTER 1

> The source below already appears in the list for Section 6.4 and is repeated here only for ease of reference.

Fullerton T (2018) *Game design workshop: a playcentric approach to creating innovative games*, 4th edn, CRC Press, Boca Raton.
