> **⚠️ GHI CHÚ - XÓA KHỐI NÀY TRƯỚC KHI NỘP**
>
> - Toàn bộ chương viết ở **ngôi thứ ba, giọng bị động**, thống nhất với bản tiếng Việt.
> - Trích dẫn theo **RMIT Harvard** (không dấu phẩy giữa tên và năm).
> - `📌 [IMAGE]` = vị trí chèn ảnh chụp màn hình.
> - Bảng mục 3.6.2 đã điền đủ tên nhà phát hành. Ba tên gói bị cắt ngắn trên Asset Store cần xác nhận lại tên đầy đủ: **Toony Kitchen & Ingredients**, **Interiors FREE**, **Low Poly Trees - Free Nature Pack**.
> - Sau khi paraphrase bằng QuillBot, **kiểm tra lại các thuật ngữ kỹ thuật và tên riêng** (`MonoBehaviour`, `ScriptableObject`, `Raycast Target`, `WildBound`, tên gói asset) - công cụ paraphrase thường thay thế sai những từ này.

---

# CHAPTER 3 - TECHNOLOGY AND TOOLS

## 3.0 Chapter Introduction

This chapter presents the technologies and tools used to develop WildBound. Each technology is discussed in three parts: its technical nature, the reasoning behind its selection within the context of this project, and the specific way in which it was applied to the product. This approach is intended to demonstrate that the technological choices made in this project were deliberate, rather than the result of arbitrarily adopting whichever tools happened to be available.

---

## 3.1 Unity 6 Game Engine

### 3.1.1 Overview and Rationale for Selection

A game engine is an integrated software framework that provides the foundational systems required for game development, including graphics rendering, physics simulation, audio processing, asset management and a scripting environment (Gregory 2018). The use of a game engine allows the development process to concentrate on designing game mechanics rather than rebuilding fundamental technical systems from the ground up.

Unity is currently among the most widely adopted engines in the games industry. According to a report analysing more than 13,000 titles on the Steam platform, 51% of the games released during 2024 were developed using Unity (Video Game Insights 2025). This scale of adoption carries a direct practical implication for the project: the more widely an engine is used, the greater the volume of documentation, tutorials and technical discussion available to draw upon.

Unity was selected as the development platform for WildBound for three principal reasons.

First, Unity provides a complete development ecosystem - including a visual editor, a physics system, an animation system, terrain construction tools and a user interface framework - within a single unified environment. This is particularly significant for an individual project, in which the entire workload is undertaken by one person and there is no capacity to build foundational technical systems independently.

Second, the large scale of the user community, combined with comprehensive official documentation, considerably reduces the time required to resolve technical problems. As the project was carried out independently, without direct consultation with colleagues when technical faults arose, the ability to research documentation autonomously became a determining factor in maintaining progress.

Third, the Unity Asset Store provides an extensive library of graphical resources. This made it possible to use pre-existing three-dimensional models and to devote the majority of the available time to designing and implementing the game systems, which constitute the academic focus of this project.

The version used is **Unity 6 (6000.3.10f1)**. Selecting the most recent version available at the time the project commenced was intended to take advantage of the engine's latest performance improvements and features, thereby optimising the final product. The trade-off associated with this decision is that the majority of existing tutorials and online instructional material were produced for earlier versions, with the result that certain interface operations and component names did not correspond exactly. In such cases, the equivalent procedure for the version in use was established by consulting the official Unity documentation directly.

### 3.1.2 The GameObject-Component Architecture

Unity is built upon a component-based architecture. Within this model, every entity in the game world is a `GameObject` - in itself merely an empty container carrying no functionality. All behaviour and properties are added by attaching `Component` objects to that GameObject (Unity Technologies 2025a).

This model differs fundamentally from the multi-level inheritance hierarchies characteristic of traditional object-oriented programming. Rather than constructing a rigid inheritance tree, independent components are composed together to produce the desired entity. The advantage of this approach lies in its flexibility: a behaviour written once as a component can be reused across entirely different categories of object without introducing complex inheritance relationships (Nystrom 2014).

In WildBound, this principle is clearly demonstrated by the `InteractableObject` component. This component is responsible for displaying the name of an object when the player directs their view towards it, and is attached to several categories of object of fundamentally different natures: collectable items (rocks, sticks, apples), choppable trees, harvestable bushes, minable boulders and the fire pit. Under an inheritance-based model, these objects would be difficult to consolidate under a single common parent class, given how substantially their functions differ.

### 3.1.3 The Script Lifecycle

Unity controls the execution of code through a sequence of methods that are invoked automatically in a defined order, known as the script lifecycle. The three most significant of these are `Awake()`, `Start()` and `Update()` (Unity Technologies 2025a).

A critical characteristic to note is that Unity guarantees that **the `Awake()` method of every object completes execution before any `Start()` method begins**. Conversely, Unity does **not** guarantee the order in which `Awake()` executes across different scripts.

Understanding this distinction precisely carries considerable practical significance. During the development of WildBound, a fault arose in which the button opening the Tool Library interface failed to respond on the first click. The cause was identified as the button's event registration being placed within `Start()`, while a separate script deactivated the object containing that button within `Awake()`. Because a deactivated object does not execute `Start()`, the button's event was never registered. The fault was resolved by moving the event registration to `Awake()` and relocating the deactivation operation to `Start()`, thereby exploiting Unity's ordering guarantee correctly and eliminating the indeterminacy entirely.

### 3.1.4 Prefabs

A Prefab is a mechanism that allows a GameObject, together with its complete configuration, to be stored as an independent asset from which multiple copies can subsequently be instantiated at runtime (Unity Technologies 2025a). Prefabs underpin every dynamic object-spawning system in WildBound: the items dropped when a tree is chopped, the rabbits spawned periodically from burrows, and the state variants of the fire pit are all implemented using Prefabs.

A noteworthy technical issue relating to the assignment of Prefab references was identified during development. When a reference is assigned by dragging from the Hierarchy window - that is, pointing to an instance currently existing within the scene - rather than from the Project window (pointing to the original Prefab asset), that reference continues to appear valid in the interface until the referenced instance is destroyed at runtime. Beyond that point, the entire chain of references inherited from it becomes invalid. The lesson drawn is that Prefab references must always be assigned from the Project window in order to guarantee stability.

### 3.1.5 ScriptableObject

A ScriptableObject is a data class that permits information to be stored as an asset independent of any scene, without needing to be attached to a GameObject (Unity Technologies 2025a). This mechanism forms the basis for implementing the principle of data-driven design, in which configuration data is entirely decoupled from the code that processes logic.

WildBound applies ScriptableObject for two purposes. First, the `GameConfig` asset centralises all game balance parameters - movement speed, stamina depletion rates, damage values, tool durability, resource regeneration intervals and creature statistics. As a result, adjusting the difficulty of the game can be performed entirely through the Unity interface without modifying or recompiling any source code. Second, each crafting recipe is stored as a separate `CraftingRecipe` asset, allowing new recipes to be introduced without altering the existing crafting system.

### 3.1.6 Terrain Construction Tools

The terrain of the island in WildBound was constructed using Unity's built-in Terrain system, employing two fundamental tools: raising and lowering the surface elevation to shape the overall form of the island, and painting surface materials to distinguish areas of grass, soil and pathways.

📌 [IMAGE] *Insert a screenshot of the island terrain in the Scene view, or of the Terrain tool panel.*

---

## 3.2 Universal Render Pipeline (URP)

### 3.2.1 The Concept of a Render Pipeline

A render pipeline is the sequence of processing stages an engine performs in order to convert the three-dimensional data of a scene - comprising geometry, materials, lighting and camera position - into the two-dimensional image displayed on screen (Gregory 2018). Unity provides three distinct pipelines, each targeting a different category of user (Unity Technologies 2025b):

- **Built-in Render Pipeline**: the legacy pipeline, offering limited customisation.
- **Universal Render Pipeline (URP)**: designed to balance visual quality against performance, operating reliably across a wide range of hardware platforms.
- **High Definition Render Pipeline (HDRP)**: oriented towards the highest visual fidelity, requiring powerful hardware and suitable only for desktop and dedicated console platforms.

### 3.2.2 Rationale for Selecting URP

URP was selected for WildBound for three reasons. First, its hardware requirements are appropriate for a low-poly survival game, which does not demand the highest tier of visual fidelity. Second, URP incorporates a built-in post-processing system through the Volume mechanism, allowing visual effects to be applied without installing additional extension packages. Third, URP is the pipeline recommended by Unity as the default choice for new projects, which means that the supporting documentation and resources available for it are the most extensive.

### 3.2.3 Pipeline Compatibility Problems in Practice

One of the most substantial technical problems encountered during the middle phase of the project was incompatibility between render pipelines. The problem arose because asset packages downloaded from the Asset Store had been built by their publishers for different pipelines: some for Built-in, some for URP and others for HDRP. As each pipeline employs its own set of shaders, and these sets are mutually incompatible, objects using shaders inappropriate to the active pipeline are rendered in a distinctive magenta colour - the indicator that Unity has been unable to resolve the corresponding shader.

The problem was resolved by consolidating the entire project onto URP and converting the materials of incompatible assets to URP shaders. The technical lesson drawn is that the render pipeline must be determined at the project initialisation stage, and that every asset should be checked for the pipeline it supports before being imported into the project. This constitutes an architectural constraint of Unity that introductory tutorials frequently fail to address adequately.

### 3.2.4 Application of Post-processing in the Product

In WildBound, the URP Volume system is used to produce a Depth of Field effect in Gaussian mode on the main menu screen. This effect blurs the entire background scene, ensuring that the game title and interface controls in the foreground retain their sharpness and hold the player's attention. Because the interface is rendered in Screen Space Overlay mode, it is unaffected by post-processing effects applied to the camera.

📌 [IMAGE] *Insert a screenshot of the main menu screen with the blurred background.*

---

## 3.3 C# and .NET

C# is an object-oriented programming language developed by Microsoft, combining type safety with a highly expressive syntax (Microsoft 2025). Unity uses C# as its sole official scripting language, and consequently the entire logic of WildBound is written in this language.

The total volume of source code developed within this project amounts to **32 script files comprising approximately 3,200 lines of code**. The object-oriented programming features employed include the following:

- **Inheritance**: every behavioural script inherits from Unity's `MonoBehaviour` base class, through which the engine automatically invokes the methods of the script lifecycle.
- **Data encapsulation**: properties that must be readable externally but modifiable only from within their own class are declared with asymmetric access, for example `public int selectedIndex { get; private set; }` in the hotbar management class.
- **Enumerations**: used to represent finite state sets explicitly and in a type-safe manner, in preference to integers or string literals. WildBound defines enumerations for the fire pit state, the bush state, the crafting slot type and the main menu camera state.
- **Properties with custom accessor logic**: the `ItemSlot` class exposes an `Item` property that iterates through child objects to locate the component carrying item data. This addresses a practical problem: each inventory slot may contain several child objects, including the element that displays the stack quantity, so retrieval by positional index would return an incorrect result.
- **Interfaces**: Unity's event-handling interfaces, such as `IBeginDragHandler`, `IDragHandler`, `IDropHandler` and `IPointerEnterHandler`, were implemented to construct the item drag-and-drop mechanism within the inventory.
- **Static members**: used for the Singleton design pattern and for globally shared state, such as the variable holding the item currently being dragged between slots.

---

## 3.4 Unity UI (uGUI) and TextMeshPro

The entire user interface of WildBound is built using the Unity UI (uGUI) system, encompassing the survival status bars, the hotbar, the inventory, the crafting panel, the recipe library, the death screen and the main menu.

The foundation of this system is the `Canvas` object together with the `RectTransform` component, which positions interface elements through an anchoring mechanism so that layouts display correctly across differing screen resolutions (Unity Technologies 2025a). User interaction is processed through the `EventSystem`, the subsystem responsible for casting rays from the cursor position in order to determine which interface element is being interacted with.

TextMeshPro is used for all textual content within the game. In comparison with the legacy Text component, TextMeshPro employs the Signed Distance Field technique, which allows text to retain sharpness under magnification and supports a broader range of advanced formatting options (Unity Technologies 2025a).

A technical issue of considerable practical value emerged during the construction of the inventory interface. In order to display item quantities and tool durability, a text element was overlaid on top of the item icon. However, because the `Raycast Target` property of the text element is enabled by default, this element intercepted mouse events directed at the icon beneath it, with the result that drag-and-drop operations could only be performed in the regions the text did not cover. The issue was addressed by disabling the `Raycast Target` property on all interface elements serving a purely display function. The general principle derived from this is that, within Unity's interface system, an element's capacity to receive mouse events and its display role are two independent properties, and each must be configured deliberately.

---

## 3.5 Git and GitHub

### 3.5.1 The Role of Version Control

A version control system is a tool that records the complete history of changes made to source code, permitting versions to be compared, earlier states to be restored, and parallel work to proceed across multiple development branches (Chacon and Straub 2014). For a project extending from January to August 2026, adopting version control was a necessary condition for controlling the risk of data loss and for enabling changes that produced unforeseen faults to be reverted.

A distinction should be drawn between Git and GitHub: Git is a distributed version control system operating on the local machine, whereas GitHub is an online hosting service providing a remote copy of the repository (Chacon and Straub 2014).

### 3.5.2 The Workflow Applied in the Project

The workflow adopted in WildBound follows the standard cycle of reviewing the status of changes, staging those changes, creating a commit accompanied by a descriptive message, and synchronising with the remote repository. At the time of writing, the project has recorded **28 commits** in the repository `Baro-Duong/3D_Survival_Project`.

The naming convention applied to commits was to describe precisely the work carried out, rather than employing generic labels. As a result, the commit history simultaneously functions as a development log that can serve as evidence when comparing actual progress against the project plan.

### 3.5.3 The Problem of Asset Volume

A practical difficulty arose during the first synchronisation. Because a Unity project contains a substantial volume of binary graphical assets, the total size of the initial commit exceeded **GitHub's 2GB limit per push operation**, causing the operation to be rejected. The situation was resolved by dividing the commit into two separate commits and synchronising them sequentially.

This incident illustrates a characteristic peculiar to applying version control to game projects: unlike conventional software projects, in which source code accounts for the majority of the repository size, in a game project it is the graphical assets that dominate. This necessitates configuring the `.gitignore` file to exclude directories generated automatically by Unity - such as `Library/`, `Temp/` and `obj/` - which can be regenerated and therefore need not be stored.

A further observation of some value emerged during the audit of asset usage conducted in the final stage of the project (presented in Section 3.6): the largest asset package in the repository was found to be **entirely unused** in the product. This indicates that control over which assets are imported into a project should be exercised regularly from the outset, rather than allowing them to accumulate.

---

## 3.6 Third-party Assets

### 3.6.1 Principles Governing Use

WildBound uses free graphical asset packages from the Unity Asset Store for the visual content of the game. This decision arose from the allocation of available resources: the project was undertaken by one individual over approximately seven months, and its academic focus was defined as **the design and implementation of game systems** rather than three-dimensional modelling. Using pre-existing assets made it possible to concentrate the available time on the core of the project.

The scope of this use should be stated explicitly: **the third-party packages supply only three-dimensional models, materials, textures and animations. The entire operational logic of the game - including the inventory, crafting, survival statistics, combat, creature artificial intelligence and the cooking interaction chain - was designed and programmed within this project.**

With regard to licensing, the asset packages used are distributed under the Standard Unity Asset Store End User License Agreement. This licence permits assets to be used in both non-commercial and commercial products on a royalty-free basis, provided that the assets are embedded within the product and are not redistributed as standalone items (Unity Technologies 2025c).

### 3.6.2 Inventory of Assets Used

In order to ensure the accuracy of this report, the inventory below was established by auditing the project's asset reference graph, starting from the actual scenes and from the assets loaded dynamically at runtime, rather than by listing every package that had at some point been downloaded. The audit revealed that, of the packages downloaded, only a proportion were genuinely used in the final product.

| Functional group | Asset package | Publisher | Content used |
|---|---|---|---|
| Terrain | Fantasy Landscape | PXLTIGER | Ground surface materials (grass, pathways) |
| | Fantasy Skybox FREE | Render Knight | Supplementary surface textures for terrain |
| Vegetation | Yughues Free Palm Trees | Nobiax / Yughues | Five palm tree model variants |
| | Fantasy Landscape | PXLTIGER | Birch tree model |
| | Idyllic Fantasy Nature | Edenity | Harvestable bush model, vegetation shader |
| | Low Poly Trees - Free Nature Pack | Nebula | Decorative bush models |
| Minerals | Free Pack - Rocks Stylized | PolyOne Studio | Minable boulder model |
| | Fantasy Landscape | PXLTIGER | Small rock model |
| Water | Simple Water Shader URP | IgniteCoders | Water surface and reflection effects |
| Creatures | White Rabbit | Niwashi Games | Rabbit model and three animations (idle, run, death) |
| Tools | Low-Poly Forest Survival Starter Pack | Devtricked | Fire pit and pickaxe models |
| | Low Poly Fantasy Warrior | asoliddev | Axe model |
| Items | Rustic Series: a Pot | NZ Bullet Studio | Cooking pot model |
| | Toony Kitchen & Ingredients | Sigun Studio | Meat model |
| | Match 3D Object Pack: Fruits and Vegetables | ThreeBox | Apple model |
| | Interiors FREE | Mnostva Art | Water bottle model |
| Effects | VFX URP - Fire Package | Cartoon VFX by Wallcoeur | Fire and smoke effects for the fire pit |
| Interface | Free 2D Mega Pack | Brackeys | Health, food and water icons on the status bars |
| | Inventory Framework FREE | Game Dev Simplified | Interface frames and icons |
| Scenery | Wood Boat | E6 Model | Decorative boat model |

---

## 3.7 Development Environment and Supporting Tools

### 3.7.1 Programming and Debugging Environment

The source code of WildBound was written in Visual Studio, the development environment integrated with Unity through an official extension package. The features used regularly include context-sensitive code completion, syntax error checking at compile time, and rapid navigation between class definitions.

The principal debugging tool employed throughout development was Unity's Console window in combination with the `Debug.Log` and `Debug.LogError` logging statements. The method applied was to place logging points at locations under suspicion in order to establish the program's actual execution flow precisely, and subsequently to remove these statements once the fault had been corrected, so as to keep the source code clean. This approach proved particularly effective for faults whose external symptoms did not reflect their underlying cause - for instance the unresponsive interface button discussed in Section 3.1.3, in which confirming that a particular log line was never printed established that the method containing it had never been executed at all.

In addition, Blender was used during the early stage of the project to modify a number of three-dimensional models to suit the requirements of the game.

### 3.7.2 AI-assisted Tools

During the course of this project, an artificial intelligence tool (Claude) was used in two capacities: **assisting with the writing of source code** during the product development stage, and **assisting with the drafting and translation of report content** during the documentation stage.

The scope of this assistance should be stated clearly. All decisions concerning system design, software architecture, game parameter balancing, and the diagnosis and correction of faults were taken independently within this project, and the operating principles of all source code in the product are fully understood, including those portions produced with the assistance of the tool referred to above. Likewise, all technical content, data and argumentation presented in this report derive from the actual development process of the project.

---

## 3.8 Chapter Summary

Overall, the technologies selected for WildBound constitute a coherent combination well suited to the particular circumstances of an individual project operating under a fixed deadline. Unity 6 together with URP provides the rendering and simulation foundation; C# handles the logic; uGUI and TextMeshPro construct the interface; Git and GitHub secure the data and record progress; while third-party asset packages address the visual content so that resources could be concentrated on system design.

It is notable that the majority of the most valuable technical lessons documented in this chapter - the render pipeline compatibility problem, the event-handling mechanism of the interface system, and the execution ordering of the script lifecycle - did not originate from instructional material, but rather from directly encountering and resolving faults during the development process.

---

# REFERENCES - CHAPTER 3

Chacon S and Straub B (2014) *Pro Git*, 2nd edn, Apress, Git website, accessed 29 July 2026. https://git-scm.com/book/en/v2

Gregory J (2018) *Game engine architecture*, 3rd edn, CRC Press, Boca Raton.

Microsoft (2025) *C# documentation*, Microsoft Learn website, accessed 29 July 2026. https://learn.microsoft.com/en-us/dotnet/csharp/

Nystrom R (2014) *Game programming patterns*, Genever Benning, Game Programming Patterns website, accessed 29 July 2026. https://gameprogrammingpatterns.com/

Unity Technologies (2025a) *Unity user manual*, Unity Documentation website, accessed 29 July 2026. https://docs.unity3d.com/Manual/

Unity Technologies (2025b) *Universal Render Pipeline documentation*, Unity Documentation website, accessed 29 July 2026. https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest

Unity Technologies (2025c) *Asset Store terms of service and EULA*, Unity website, accessed 29 July 2026. https://unity.com/legal/as-terms

Video Game Insights (2025) *The big game engines report of 2025*, VG Insights website, accessed 29 July 2026. https://vginsights.com/assets/reports/The_Big_Game_Engines_Report_of_2025.pdf
