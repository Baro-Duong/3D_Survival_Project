> **⚠️ GHI CHÚ - XÓA KHỐI NÀY TRƯỚC KHI NỘP**
>
> - Ngôi thứ ba, giọng bị động. Không dùng "I", không dùng "the author".
> - Chỉ dùng dấu gạch thường `-`.
> - Chương này thêm **6 nguồn mới**: Adams (2014), Adams and Dormans (2012), Csikszentmihalyi (1990), Hunicke et al. (2004), Koster (2013), Salen and Zimmerman (2004). Các nguồn còn lại đã dùng ở Chương 3, 6 và 7.
> - ⚠️ **Adams E (2014)** và **Adams E and Dormans J (2012)** là hai đầu sách khác nhau của cùng tác giả - khi gộp danh sách tham khảo tổng, giữ nguyên cả hai, đừng gộp làm một.

---

# CHAPTER 2 - LITERATURE REVIEW

## 2.0 Chapter Introduction

This chapter surveys the theoretical foundations of the fields bearing directly on the project, across six topics: the defining characteristics of the survival game genre, the theory of the core loop and player motivation, the design of a game's internal economy, the architecture of game engines, software design patterns in game development, and interaction techniques in three-dimensional space. For each topic the theory is presented first, followed by its bearing on specific design decisions taken in WildBound.

---

## 2.1 The Survival Game Genre

### Definition and Position Within Genre Taxonomy

The classification of video games into genres rests principally on **the kind of challenge the game sets the player**, rather than on setting or presentation (Adams 2014). Under this classification, the survival game is defined by a distinctive combination of challenges: maintaining the character's biological statistics, gathering scarce resources from the environment, and converting those resources into items of greater utility.

The most significant distinction between the survival genre and its neighbours lies in **the origin of the threat**. In most action genres, the threat comes from a deliberate opponent. In a survival game, the threat comes principally from the environment and from the passage of time itself: the player loses not through defeat in a confrontation, but because the statistics sustaining life run out.

This distinction has direct consequences for design. In an action game, difficulty is adjusted chiefly through the strength of opponents. In a survival game, difficulty resides in **the relationship between the rate of consumption and the rate of resource replenishment** - that is, in the figures of the game's economy rather than in its adversaries.

### Bearing on WildBound

WildBound belongs to the survival-crafting branch of the genre, in which crafting tools is the precondition for improving the efficiency of gathering. The game has no intelligent adversary and no victory condition, and all pressure placed upon the player derives from three statistics that decline over time. This is why the majority of the project's design effort was directed at balancing figures rather than at designing opponents, and equally why Section 2.3 is treated at greater length than the remaining sections.

---

## 2.2 The Core Loop and Player Motivation

### The MDA Framework

Among the most widely used analytical frameworks in game design research is the MDA model, which separates a game into three layers: **mechanics**, the rules and data at the level of the code; **dynamics**, the behaviour that emerges as the player operates those mechanics; and **aesthetics**, the emotional experience that results (Hunicke et al. 2004).

The significance of this model is that a designer can act directly only upon the mechanics layer, whereas the player receives the game at the aesthetics layer. The desired experience therefore cannot be programmed directly, but can only be produced indirectly through the design of rules.

### The Core Loop

The core loop is the short sequence of actions a player repeats continuously throughout play. The quality of a game depends substantially on whether that loop remains satisfying under repetition (Salen and Zimmerman 2004).

That satisfaction is bound up with the process of learning. Players find engagement while they are still coming to grasp a pattern, and lose it once the pattern has been fully grasped and nothing remains to discover (Koster 2013). This explains why an overly simple loop will become tedious rapidly, however well executed it may be.

### Flow Theory

The concept of flow describes the state of deep concentration a person reaches when the challenge of an activity matches their capability; challenge exceeding capability produces anxiety, while challenge below it produces boredom (Csikszentmihalyi 1990). In game design, this concept is commonly invoked to argue that difficulty should rise in step with the player's developing competence.

### Bearing on WildBound

The core loop of WildBound is the sequence: gather resources, craft tools, use those tools to gather more efficiently, and expend the resources obtained on maintaining the survival statistics. This loop is self-reinforcing, since each completion improves the capacity to perform the next.

Applying the MDA model to this game, the mechanics layer comprises the rules governing statistic depletion, crafting recipes and tool durability; the dynamics layer comprises the resulting behaviour, such as the player having to decide between sprinting to save time and walking to conserve water; and the aesthetics layer is the sense of pressure as resources dwindle. It should be emphasised that this sense of pressure was not programmed directly but is an indirect consequence of the configuration figures.

With regard to flow theory, one limitation must be acknowledged: since WildBound has not been tested with external players, the degree to which its difficulty matches the capability of a new player remains unconfirmed. This point is examined further in Section 6.4.1.

---

## 2.3 Designing a Game's Internal Economy

### The Concept of an Internal Economy

The internal economy is the system describing how resources within a game are created, converted and consumed. Adams and Dormans (2012) propose a vocabulary for analysing this system, comprising four principal elements:

- **Sources**: the points at which resources are generated and enter the system.
- **Drains**: the points at which resources are consumed and leave the system permanently.
- **Converters**: mechanisms transforming one type of resource into another.
- **Flows**: the movement of resources between the elements above.

The value of this vocabulary lies in permitting a game economy to be analysed systematically, rather than having its figures adjusted by intuition until the result feels acceptable.

### Two Symmetrical Risks

An unbalanced game economy can fail in either of two opposing directions.

The first is the **dead-end state**: the player consumes finite resources and has no means of recovery, so that the game, while not formally over, can no longer be continued meaningfully. This is a serious design failure, since it deprives the player of agency.

The second is **surplus accumulation**: resources are generated faster than they are consumed, so that decisions about allocating them lose all meaning. When everything is abundant, choosing is no longer choosing. Schell (2019) describes this as a collapse of the game's challenge.

A well-designed economy therefore requires both a mechanism ensuring that supply never runs out absolutely, and drains sufficient to prevent resources accumulating without limit.

### Bearing on WildBound

The entire economy of WildBound was designed according to the vocabulary set out above:

| Element | Implementation in WildBound |
|---|---|
| Sources | Bushes, trees, the boulder and rabbit burrows, all of which regenerate on a cycle |
| Converters | The crafting panel (sticks and rocks into tools) and the fire pit (dirty water into clean, raw meat into cooked) |
| Drains | Crafting costs, tool durability consumed through use, fire pit durability consumed by boiling and cooking |
| Flows | The chain running from gathering through crafting to use and finally to wear |

Both risks identified above were addressed deliberately. Against the dead-end risk, the boulder produces one unit of rock on a fixed cycle entirely independently of the player's actions, and bushes regrow after a defined interval. As a result, even where the player has expended all resources on poor choices, supply still recovers.

Against the surplus risk, the mechanism permitting sticks and rocks to be fed into the fire pit to restore its durability serves as an additional drain, consuming resources beyond what crafting requires. It is worth noting that this drain was not an arbitrary addition but a response to precisely the problem the theory predicts: once sufficient tools have been crafted, further sticks and rocks would lose all purpose in the absence of somewhere to spend them.

A detailed analysis of the soundness of this economy, together with the specific figures, is presented in Section 6.4.3.

---

## 2.4 Game Engines and the Unity Platform

### The Role of a Game Engine

A game engine is an integrated software framework providing the foundational systems required for game development, including graphics rendering, physics simulation, audio processing, asset management and a scripting environment (Gregory 2018). Before commercial engines became widespread, studios typically built their own technology, which made the cost of entering the industry very high.

Gregory (2018) analyses modern engine architecture as a layered model, in which lower layers handle direct interaction with the hardware while higher layers present a programming interface to the game developer. This layering allows developers to work at a high level of abstraction without needing to understand how the hardware operates in detail.

### Unity

Unity is currently among the most widely used engines: according to a report analysing more than 13,000 titles on the Steam platform, 51% of games released during 2024 were developed with Unity (Video Game Insights 2025).

Unity's most significant architectural characteristic is its component-based model, in which every entity is an empty container and all functionality is added by attaching independent components to it (Unity Technologies 2025a). Unity additionally provides a mechanism for storing data independently of any scene, which creates the conditions for separating configuration data from the code that processes logic.

### Bearing on WildBound

A detailed analysis of the Unity features used in the project, together with the reasoning behind the choice of engine and render pipeline, is presented in Chapter 3. It suffices here to note that Unity's component-based model is the key factor allowing a single component to serve many different categories of object within the game, and that its scene-independent data mechanism is the foundation on which all balance parameters were centralised in one place.

---

## 2.5 Software Design Patterns in Game Development

### Design Patterns in General

Design patterns are proven solutions to design problems that recur in object-oriented software development (Gamma et al. 1994). Their value lies in supplying a shared vocabulary for discussing code structure, and in sparing developers from rediscovering solutions to problems already solved.

Patterns are not, however, formulas to be applied unconditionally. Each carries a cost, and applying one mechanically can make code more complex than it needs to be.

### Particularities of the Games Domain

Nystrom (2014) examines design patterns within the specific context of game development, where two constraints uncommon in ordinary software apply: real-time performance requirements, and the prevalence of entities possessing multiple states that change continuously.

The two patterns most discussed in this context are the **state machine**, used to organise entities whose behaviour varies by state, and the **Singleton**, used to provide a global access point to management systems. It is notable that Nystrom (2014) treats the Singleton chiefly in terms of the problems it creates: global state, concealed dependency relationships, and difficulty in automated testing.

A further approach emphasised is **data-driven design**, in which configuration data is separated entirely from the logic-processing code, allowing game behaviour to be adjusted without recompilation.

### Bearing on WildBound

All three approaches were applied in the project. State machines govern the fire pit (three states), the bush (two states) and the main menu camera (three states). The Singleton pattern is used for the management systems, accompanied by explicit acknowledgement of the limitations Nystrom identifies. Data-driven design is realised through a single configuration asset holding more than fifty parameters.

A detailed analysis of how each pattern was applied, along with the reasoning for retaining the Singleton despite its known limitations, is presented in Sections 6.3.1 and 6.3.4.

---

## 2.6 Interaction in Three-Dimensional Space

### Techniques for Detecting Interaction

In a three-dimensional game, the system must determine which object the player intends to interact with. Three techniques are commonly used to address this.

The first is **raycasting**: a ray is projected from the position and orientation of the camera, and the first object it intersects is taken as the target. The technique rests on the geometric intersection tests the engine provides (Gregory 2018).

The second is the use of **trigger volumes** surrounding the character or the objects, with interaction registered when two volumes overlap.

The third is a simple **distance check** between the character and nearby objects.

Each has different characteristics. Raycasting reflects the player's intention accurately because it follows their line of sight, but requires them to aim precisely. Trigger volumes and distance checks are more forgiving but cannot distinguish intent when several objects lie nearby at once.

### Bearing on WildBound

WildBound uses raycasting as its principal interaction mechanism, for displaying object names, for attacking and for the whole of the fire pit interaction chain. This choice was made after an approach based on a trigger volume surrounding the character had been trialled and discarded, since it could not determine which object the player intended to act upon when several lay within the volume.

One technical consequence of the choice is that every interactable object must possess a collision component, including objects that are otherwise purely visual, such as the water surface. This is a detail that emerges only during implementation, and it was recorded during development.

---

## 2.7 Onboarding New Players

### The Problem

A game composed of interlocking mechanisms presents a difficulty: a new player must grasp the basic rules before they can make reasonable decisions, yet presenting every rule at once produces cognitive overload.

Fullerton (2018) emphasises that developers lose the ability to judge how intelligible their own game is, precisely because they already know every mechanism within it. This makes testing with genuine players an irreplaceable stage in designing any tutorial.

Schell (2019) adds that mechanisms a player cannot discover through natural experimentation are exactly those that must be communicated explicitly.

### Bearing on WildBound

WildBound has no tutorial integrated into play. Instead, a paginated tutorial overlay is presented on the main menu, with the Play button locked until the player has read through it on a first launch.

The specific mechanism prompting this decision is the health regeneration rule: health recovers only while both hunger and thirst remain above half. This is exactly the category Schell (2019) describes, since a player is most unlikely to deduce it through random experimentation. Locking the Play button only on a first launch ensures that the information reaches new players without inconveniencing those returning.

---

## 2.8 Chapter Summary

Overall, the theoretical foundations surveyed in this chapter shaped the design decisions taken in WildBound in three respects. First, the defining characteristics of the survival genre established that design effort had to be directed at balancing figures rather than at adversaries. Second, the theory of internal economies supplied a vocabulary for analysing and verifying the resource system systematically, in place of intuitive adjustment. Third, software design patterns offered established solutions to problems of code organisation while also giving warning of the cost attached to each choice.

One gap in the literature should be recorded: although the survival genre is commercially prominent, comparatively little dedicated academic research addresses it, so most of the analysis in this chapter rests on general game design literature rather than on sources specific to the genre.

---

# REFERENCES - CHAPTER 2

Adams E (2014) *Fundamentals of game design*, 3rd edn, New Riders, Berkeley.

Adams E and Dormans J (2012) *Game mechanics: advanced game design*, New Riders, Berkeley.

Csikszentmihalyi M (1990) *Flow: the psychology of optimal experience*, Harper & Row, New York.

Fullerton T (2018) *Game design workshop: a playcentric approach to creating innovative games*, 4th edn, CRC Press, Boca Raton.

Gamma E, Helm R, Johnson R and Vlissides J (1994) *Design patterns: elements of reusable object-oriented software*, Addison-Wesley, Boston.

Gregory J (2018) *Game engine architecture*, 3rd edn, CRC Press, Boca Raton.

Hunicke R, LeBlanc M and Zubek R (2004) 'MDA: a formal approach to game design and game research', *Proceedings of the AAAI Workshop on Challenges in Game AI*, 4(1):1-5.

Koster R (2013) *A theory of fun for game design*, 2nd edn, O'Reilly Media, Sebastopol.

Nystrom R (2014) *Game programming patterns*, Genever Benning, Game Programming Patterns website, accessed 31 July 2026.
https://gameprogrammingpatterns.com/

Salen K and Zimmerman E (2004) *Rules of play: game design fundamentals*, MIT Press, Cambridge.

Schell J (2019) *The art of game design: a book of lenses*, 3rd edn, CRC Press, Boca Raton.

Unity Technologies (2025a) *Unity user manual*, Unity Documentation website, accessed 29 July 2026.
https://docs.unity3d.com/Manual/

Video Game Insights (2025) *The big game engines report of 2025*, VG Insights website, accessed 29 July 2026.
https://vginsights.com/assets/reports/The_Big_Game_Engines_Report_of_2025.pdf
