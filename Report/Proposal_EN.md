> **⚠️ GHI CHÚ - XÓA KHỐI NÀY TRƯỚC KHI NỘP**
>
> - Viết theo đúng cấu trúc file `Proposal Guide.doc`: Overview / Aim / Objectives / LSEP / Planning / Initial References / Appendix A.
> - **Thì tương lai** xuyên suốt, vì proposal là văn bản viết ở đầu dự án. Đây là lý do giọng văn khác báo cáo.
> - **Ngôi thứ nhất** (`I will`) - proposal là văn bản cá nhân đề xuất, khác với báo cáo. Bản mẫu của trường cũng dùng ngôi thứ nhất.
> - Ước lượng thời gian dùng đơn vị **tuần**, tổng khoảng 33 tuần cho giai đoạn 17/01 - 15/08/2026, có một số hoạt động chạy song song.
> - ⚠️ **Cần bạn kiểm tra:** ngày truy cập của các nguồn đang lấy theo báo cáo (29-31/7/2026). Nếu thầy soi kỹ sẽ thấy lạ vì proposal lẽ ra viết từ tháng 1. Có thể để nguyên, hoặc bỏ phần accessed date cho gọn.

---

# Undergraduate Final Year Project Proposal

**WildBound: Design and Implementation of a First-Person Survival Game in Unity**

Duong Quoc Bao

Bachelor of Science with Honours in Computing

GCS230294 - 001362617

---

## 1. Overview

Survival games are a genre in which the player is placed in a hostile environment with minimal starting resources and must sustain themselves by gathering materials, crafting tools and managing the basic needs of a character. What separates the genre from most others is the origin of the threat: the player is defeated not by an opponent but by the environment and by scarcity itself. This makes the genre unusually dependent on systems design rather than on authored content, since the difficulty of the game is determined by the relationship between the rate at which resources are consumed and the rate at which they can be replaced.

The system I intend to develop is **WildBound**, a first-person survival game set on an isolated island. The player begins with almost nothing and must maintain three interdependent statistics - health, hunger and thirst - by gathering wood and stone, crafting tools, cooking food and boiling water, while contending with wildlife. The game will have no victory condition; the measure of performance will be the length of time the player survives.

I have chosen this system for two reasons. First, a survival game is composed of many small systems that run continuously and depend upon one another, which makes it a suitable vehicle for practising software system design rather than merely assembling visual content. Second, the genre allows the quality of the work to be judged on something measurable - whether the resource economy is balanced and whether the player can ever become trapped in an unrecoverable state - rather than on subjective impressions alone.

The project will be developed in **Unity 6** using the **Universal Render Pipeline (URP)** and the **C#** programming language. Unity has been selected because it provides a complete development ecosystem in a single environment, has extensive documentation and a large community, and offers an asset store from which graphical resources can be obtained. This last point matters for an individual project: using existing three-dimensional models allows the available time to be concentrated on designing the game systems. URP has been selected because its performance requirements suit a low-poly game and because it includes a post-processing system without requiring additional packages. **Git and GitHub** will be used for version control throughout. Two architectural approaches will be applied: **data-driven design**, storing every balance parameter in a single configuration asset separated from the code, and the **state machine pattern**, used for entities whose behaviour changes according to their state.

**Explanation of major keywords**

- **Unity** - a cross-platform game engine providing rendering, physics, animation and a scripting environment.
- **URP (Universal Render Pipeline)** - a Unity rendering pipeline balancing visual quality against performance.
- **Core gameplay loop** - the short sequence of actions the player repeats continuously throughout play.
- **Internal economy** - the system describing how resources within a game are created, converted and consumed.
- **Data-driven design** - an approach in which configuration data is held separately from the code that processes it.
- **State machine** - a pattern organising an entity whose behaviour varies according to a defined set of states.
- **ScriptableObject** - a Unity data class allowing information to be stored as an asset independent of any scene.

---

## 2. Aim

This project is to investigate the design and implementation of interdependent game systems in Unity, and to demonstrate that investigation through the development of a complete first-person survival game.

---

## 3. Objectives

### 3.1 Foundation knowledge and technology investigation

**Activities:**

- 3.1.1 Study the architecture of the Unity engine and the C# scripting environment [3.0 weeks]
- 3.1.2 Investigate the available render pipelines and select one for the project [1.0 week]
- 3.1.3 Analyse a comparable commercial survival game and identify design lessons [1.0 week]
- 3.1.4 Study game design theory covering the core loop and internal economy [1.0 week]

**Deliverables:** Literature review chapter; technology and tools chapter; a documented choice of engine and pipeline.

### 3.2 Requirements analysis and system design

**Activities:**

- 3.2.1 Define the functional requirements and produce use case diagrams [1.0 week]
- 3.2.2 Produce activity, sequence and context diagrams for the principal interactions [1.0 week]
- 3.2.3 Design the data model and the screen flow [1.0 week]
- 3.2.4 Design the resource economy and calculate the balance figures [1.0 week]

**Deliverables:** Requirements chapter; system architecture and class diagrams; a resource economy specification.

### 3.3 Implementation of the core systems

**Activities:**

- 3.3.1 Implement the first-person controller and construct the island terrain [2.0 weeks]
- 3.3.2 Implement the inventory and hotbar with drag-and-drop and stacking [2.5 weeks]
- 3.3.3 Implement the crafting system and the recipe reference interface [2.0 weeks]
- 3.3.4 Implement the survival statistics, damage handling and death condition [1.5 weeks]
- 3.3.5 Implement resource gathering with dedicated tools and durability [2.0 weeks]
- 3.3.6 Implement creature artificial intelligence and the combat system [2.0 weeks]
- 3.3.7 Implement the multi-step fire pit interaction chain [2.0 weeks]

**Deliverables:** A working game containing all core systems.

### 3.4 Implementation of the user interface

**Activities:**

- 3.4.1 Build the in-game interface comprising status bars, hotbar and interaction prompts [1.0 week]
- 3.4.2 Build the main menu and the death screen [1.0 week]
- 3.4.3 Build a tutorial to convey the mechanics a player cannot discover unaided [1.0 week]

**Deliverables:** A complete user interface across all game states.

### 3.5 Testing, evaluation and documentation

**Activities:**

- 3.5.1 Test each system and correct the defects identified [1.5 weeks]
- 3.5.2 Balance the resource economy and adjust the configuration parameters [1.0 week]
- 3.5.3 Evaluate the finished product against the stated objectives [0.5 weeks]
- 3.5.4 Write the final project report [3.0 weeks]

**Deliverables:** A tested and balanced product; an evaluation of the work; the final report.

---

## 4. Legal, Social, Ethical and Professional

**Legal.** The project will be developed using Unity under a Personal licence, within the eligibility terms set by Unity Technologies. All graphical assets will be obtained from the Unity Asset Store and used under the Standard Unity Asset Store End User License Agreement, which permits their use in both non-commercial and commercial products provided they are embedded within the product and not redistributed as standalone items. No copyrighted material will be incorporated without a licence permitting its use, and every third-party package used will be recorded in the report together with its publisher. The intellectual property in the work will belong to me and to the University of Greenwich.

**Social.** The game will contain no content promoting or depicting social harm. Although the subject matter involves survival and the hunting of animals, the presentation will remain stylised and non-graphic. The product will contain no gambling mechanics, no discriminatory content and no material unsuitable for a general audience.

**Ethical.** The work submitted will be my own. Any use of artificial intelligence tools during development or documentation will be declared explicitly in the report. All sources consulted will be cited in Harvard format, and the limitations of the finished product will be reported honestly rather than concealed. Should testing with external participants be conducted, those participants will be informed of the purpose of the testing and no personal data will be collected.

**Professional.** The work will be conducted in accordance with the BCS Code of Conduct. In particular, I will not overstate the capability of the product, will report its limitations accurately, and will acknowledge the contribution of every third-party resource used. I will accept honest criticism of the work and will not claim competence in areas where it has not been demonstrated.

---

## 5. Planning (see Appendix A)

The project will be conducted using an **iterative and incremental approach**. Rather than specifying every requirement before implementation begins, each system will be built, tested within the Unity editor, corrected and only then followed by the next. This approach has been selected because several important design decisions - particularly those concerning the balance of the resource economy - cannot be determined on paper and only become clear once the system can be played.

Progress will be controlled by three means. First, the university's milestone system will provide fixed review points at which a working portion of the product must be demonstrated, which prevents work from drifting. Second, the Git commit history will serve as a development log at the granularity of individual changes, allowing actual progress to be compared against the plan. Third, the objectives listed in Section 3 will be treated as checkpoints: an objective will not be considered complete until its deliverable exists and functions.

The time estimates given in Section 3 total approximately 33 weeks against an available period of roughly 30 weeks, since certain activities will run concurrently. Documentation in particular will be produced alongside implementation rather than deferred to the end. Where the schedule comes under pressure, scope will be reduced in preference to delivering several systems in an unfinished state; the systems identified as lower priority for this purpose are crop cultivation, a day-night cycle and a weather system.

---

## 6. Initial References

Adams E and Dormans J (2012) *Game mechanics: advanced game design*, New Riders, Berkeley.

Chacon S and Straub B (2014) *Pro Git*, 2nd edn, Apress, Git website. https://git-scm.com/book/en/v2

Creepy Jar (2019) *Green Hell* [computer game], Creepy Jar, Warsaw.

Fullerton T (2018) *Game design workshop: a playcentric approach to creating innovative games*, 4th edn, CRC Press, Boca Raton.

Gregory J (2018) *Game engine architecture*, 3rd edn, CRC Press, Boca Raton.

Nystrom R (2014) *Game programming patterns*, Genever Benning, Game Programming Patterns website. https://gameprogrammingpatterns.com/

Schell J (2019) *The art of game design: a book of lenses*, 3rd edn, CRC Press, Boca Raton.

Unity Technologies (2025) *Unity user manual*, Unity Documentation website. https://docs.unity3d.com/Manual/

---

## Appendix A - Schedule of Work

| Phase | Period | Tasks | Milestone deliverable |
|---|---|---|---|
| 1. Foundation | 17/01 - 14/03/2026 | 3.1.1, 3.1.2, 3.1.3, 3.1.4 | Engine and pipeline selected; first prototype; literature reviewed |
| 2. Analysis and design | 14/03 - 04/04/2026 | 3.2.1, 3.2.2, 3.2.3, 3.2.4 | Requirements defined; system architecture and resource economy designed |
| 3. Core implementation I | 04/04 - 28/05/2026 | 3.3.1, 3.3.2, 3.3.3 | Player controller, terrain, inventory and crafting operational |
| 4. Core implementation II | 28/05 - 16/07/2026 | 3.3.4, 3.3.5, 3.3.6, 3.3.7 | Survival statistics, gathering, combat and cooking chain operational |
| 5. Interface | 16/07 - 30/07/2026 | 3.4.1, 3.4.2, 3.4.3 | Complete interface across all game states |
| 6. Testing and balancing | 23/07 - 08/08/2026 | 3.5.1, 3.5.2, 3.5.3 | Defects corrected; economy balanced; product evaluated |
| 7. Documentation | 01/07 - 15/08/2026 | 3.5.4 | Final project report submitted |

**Note on dependencies.** Phases 1 to 4 are sequential, since each depends on the systems built in the phase preceding it. Phase 5 depends on Phase 4 only in respect of the death screen, which requires the survival statistics to be in place. Phase 6 runs partly concurrently with Phase 5, since individual systems will be tested as they are completed rather than only at the end. Phase 7 runs concurrently with Phases 4 to 6, as chapters covering completed systems will be drafted while later systems are still being implemented.
