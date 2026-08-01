> **⚠️ GHI CHÚ - XÓA KHỐI NÀY TRƯỚC KHI NỘP**
>
> - Ngôi thứ ba, giọng bị động. Không dùng "I" và không dùng "the author".
> - Chỉ dùng dấu gạch thường `-`, không dùng gạch dài.
> - Trích dẫn RMIT Harvard. Chương này dùng lại nguồn **Fullerton (2018)** đã có ở mục 6.4.
> - §7.2 cố ý **không lặp lại** bảng đối chiếu chi tiết ở §6.4.2 mà chỉ trỏ sang, tránh bị trừ điểm vì trùng nội dung.

---

# CHAPTER 7 - CONCLUSIONS

## 7.1 What Was Learned From This Project

The seven months spent on this project yielded knowledge falling into five distinct groups. It is worth noting that most of the more valuable lessons came not from instructional material but from encountering and resolving problems directly.

### Technical Knowledge of Unity

The first group concerns how the engine actually behaves, at a level of depth beyond what introductory tutorials present.

The most significant of these is the **script lifecycle**. Unity guarantees that every `Awake()` method completes before any `Start()` begins, yet it makes no guarantee about the order in which `Awake()` runs across different scripts. This distinction may appear to be a matter of detail, but it was the direct cause of a fault in which the tool library interface failed to respond on its first click, and it was equally the instrument by which that fault was resolved conclusively rather than patched over.

The second lesson concerns **Prefab references**: a reference dragged from the Hierarchy window points to an instance within the scene rather than to the original asset, and this becomes apparent only once that instance is destroyed at runtime. It is the kind of fault that produces no compilation error and no warning, manifesting instead as incorrect behaviour at a considerably later point.

The third lies within the **interface system**: an element's capacity to receive pointer events and its display role are entirely independent properties. A text element intended only to show a quantity will, if left with its default event-handling property, silently block drag operations on the element beneath it.

Alongside these came knowledge of the differences between render pipelines and the shader compatibility consequences that follow from them, of the post-processing system, and of the `Time.deltaTime` mechanism by which simulation speed is made independent of hardware performance.

### Knowledge of Software Architecture

The second group concerns organising code at a scale beyond a handful of files.

The project demonstrated that **the Singleton pattern is neither good nor bad in absolute terms**, but rather a conditional trade-off. For a single-player game in which each system genuinely exists only once, and where the codebase remains within one person's comprehension, the pattern saves considerable effort. Yet those same conditions also identify precisely when it ceases to be appropriate, a matter taken up in Section 7.3.

The second lesson is the **principle of single ownership**. When two classes write directly to the same interface object and no ordering is guaranteed between them, the result is that they continually overwrite one another, producing a fault that cannot be reproduced reliably. The solution lay not in correcting individual cases but in re-establishing ownership: one class alone may alter the object's state, while others merely set a flag for that owner to read.

The third is **creating variants through flags and multipliers rather than through inheritance**. The higher-tier creature in the game possesses different statistics and behaviour, yet was implemented using only a control flag and a multiplier applied to the existing class. This keeps the whole of the logic in one place and avoids duplicating code.

### Debugging Methodology

The third group, and probably the one that altered working practice most, is the **habit of tracing root causes rather than treating symptoms**.

The clearest illustration is the tool library fault described above. The external symptom was that "the button does not work", and the natural response would have been to examine the button's event registration. The step that identified the real problem, however, came from a different observation: a log line placed within the initialisation method **was never printed at all**. That absence proved the method had never executed, and redirected the entire diagnosis from "the button is faulty" to "the object never initialised".

A further category of fault also proved instructive: **failures that occur silently**. Where a call retrieving a component on a newly created object returns null and no branch handles that case, the program continues and the original object is destroyed regardless, leaving behind a replacement stripped of its functionality. The lesson is that every "not found" branch requires explicit reporting rather than being passed over.

Last came the lesson concerning **static state persisting across scene loads**. A static variable counting creature spawn cycles was not cleared automatically when the player began a new session, allowing the state of one session to influence the next. This is a category of fault that appears only when the restart scenario is specifically tested, and is readily overlooked when features are tested in isolation.

### Knowledge of Game Design

The fourth group is the recognition that **game balance is a quantitative problem rather than a matter of intuition**. Determining crafting costs, statistic depletion rates and resource regeneration intervals must proceed from an explicit calculation of supply against demand; without it, the outcome tends towards one of two extremes, in which the player is either stranded for want of resources or loses motivation through having too many.

Two specific principles accompany this. First, **any system involving consumption requires a dead-end prevention mechanism** that operates independently of the player's actions. Second, **a beneficial mechanism should carry a cost** if the game is to retain its pressure; the automatic health regeneration in WildBound accelerates hunger and thirst depletion for precisely this reason.

### Project Management Skills

The final group comprises lessons that are not technical in nature.

Foremost among them is **knowing when to narrow scope**. Removing three planned systems - crop cultivation, the day-night cycle and weather - allowed the core systems to be finished properly, rather than leaving several systems simultaneously incomplete at the deadline. Alongside this came the lesson of recognising an ineffective working arrangement and changing it deliberately, reflected in the decision to move from group work to working alone.

In terms of tooling, the project also represented a first disciplined application of version control, together with the particularities of applying it to a game project, where graphical assets rather than source code dominate the size of the repository.

---

## 7.2 The Result of This Project

### Degree of Completion

The project completed all eight specific objectives set out in Section 1.2: seven in full, and one - creature artificial intelligence - at a basic level, since pathfinding was not implemented. A detailed point-by-point comparison is presented in Section 6.4.2.

Beyond the original scope, three further items were completed: the rock-mining system operated with the pickaxe, the higher-tier creature variant capable of detecting the player independently, and the paginated tutorial overlay on the main menu. That these three were added at a late stage without requiring existing work to be redesigned constitutes practical evidence for the non-functional objective concerning extensibility stated in Section 1.2.

Conversely, three systems originally envisaged were not built: crop cultivation, the day-night cycle and weather. It should be stated clearly that these do not represent failures but items **deliberately removed** when the scope was narrowed, for the reasons set out in Sections 1.2 and 1.3.

### The Delivered Product

The final result is a working game with a complete core loop. The player launches from the main menu, reads the tutorial, enters the game, gathers resources, crafts tools, prepares food and water, contends with creatures, and concludes the session with their survival time recorded. The product comprises 32 source files written for the project, amounting to approximately 3,200 lines of code, more than fifty balance parameters centralised in a single configuration asset, and two complete scenes.

### The Significance of the Result

In academic terms, the value of this project lies not in the volume of game content but in the fact that its systems were designed and implemented from first principles, connected to one another, and grounded in calculation. This is most evident in the resource economy: every figure governing crafting costs and resource regeneration intervals derives from balancing supply against demand, rather than from arbitrary adjustment until the result felt acceptable.

It should be acknowledged honestly that the above conclusion holds only within the scope of what can be verified objectively. Since no external players took part in testing, the actual suitability of the balance parameters for a new player remains unconfirmed (Fullerton 2018). This is a limitation of the evaluation process rather than of the design itself, and it is the item that would require attention first were the project to continue.

---

## 7.3 Further Development of This Project

The directions for further development are arranged into three tiers of priority, on the principle that the quality of what already exists should be improved before its volume is expanded.

### Short Term: Completing the Existing Product

**Testing with external players.** This is the highest priority, since it determines the reliability of every claim about balance made in this report. Centralising all parameters in a single configuration asset has already established the technical conditions for such work: parameters can be adjusted between testing sessions without recompiling the program.

**Save and load functionality.** This is the most substantial limitation in feature terms. Adding it would permit longer sessions and thereby give the accumulation of resources greater meaning.

**An audio system.** Background music, sound effects for actions and audible warning cues. In the survival genre, audio matters particularly because it is the only channel capable of warning of a threat outside the field of view.

**Improved creature artificial intelligence** through Unity's existing pathfinding system, so that creatures no longer become stuck against obstacles. The navigation package is already present in the project but remains unused.

### Medium Term: Expanding Content

This tier comprises the three systems removed during the narrowing of scope, together with several natural additions:

- **A day-night cycle**, establishing a rhythm for the game and opening scope for time-dependent mechanics such as restricted visibility at night or creatures active only during certain hours.
- **A weather system**, with effects that interact meaningfully with existing mechanisms: rain might extinguish a fire pit or serve as a supplementary water source.
- **Crop cultivation**, providing a sustainable food source as an alternative to hunting.
- **A shelter construction system**, giving the player a medium-term objective beyond survival alone.
- **Additional creatures and terrain regions**, extending the scope for exploration.

What these share is that all could be built upon the existing architecture without redesigning it: the state machine, the object replacement mechanism and the centralised configuration are already prepared to accommodate new content.

### Long Term

**A multiplayer mode.** This is the direction demanding the most extensive change, and it is worth noting that the obstacle lies not in the volume of content but in the architecture. All current management systems are built upon the Singleton pattern, which rests on the assumption that each system exists only once. That assumption no longer holds with multiple players, and much of the architecture would therefore require redesign around network synchronisation. This is the direct consequence of a technical decision weighed and accepted at the outset, as analysed in Section 6.3.4.

**Commercial release.** Were the product to be brought to a distribution platform, considerable work beyond the technical scope would be required: completing the artwork in a single coherent style rather than drawing on packages by different authors, optimising performance, testing across a range of hardware configurations, and reviewing the licensing terms of every third-party asset in use.

Taken as a whole, WildBound in its present state is both a product complete at the level of its core loop and a foundation capable of extension. Most of the directions outlined above represent additions to what already exists rather than work that would need to be redone, the multiplayer mode being the one exception.

---

# REFERENCES - CHAPTER 7

> The source below already appears in the list for Section 6.4 and is repeated here only for ease of reference.

Fullerton T (2018) *Game design workshop: a playcentric approach to creating innovative games*, 4th edn, CRC Press, Boca Raton.
