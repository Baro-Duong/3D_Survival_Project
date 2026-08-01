> **⚠️ GHI CHÚ - XÓA KHỐI NÀY TRƯỚC KHI NỘP**
>
> - Ngôi thứ ba, giọng bị động. Không dùng "I", không dùng "the author".
> - Chỉ dùng dấu gạch thường `-`.
> - Chương này thêm **6 nguồn mới** vào danh sách tham khảo tổng: Beck et al. (2001), Boehm (1988), Highsmith (2002), Martin (1991), Royce (1970), Sommerville (2011).
> - ⚠️ Lưu ý phân biệt: **Martin J (1991)** ở đây là James Martin (RAD), **không phải** Robert C. Martin của sách Agile - dễ nhầm khi gộp danh sách tham khảo.
> - §5.1-5.4 cố ý viết ngắn theo đúng yêu cầu đề bài; trọng tâm nằm ở §5.5.

---

# CHAPTER 5 - REVIEW OF SOFTWARE DEVELOPMENT METHODOLOGIES

## 5.0 Chapter Introduction

This chapter surveys four widely used software development methodologies, examines the strengths and limitations of each, and then sets out the methodology adopted for the WildBound project together with the reasoning behind that choice. Each methodology is presented in the same structure: definition, phases, advantages, limitations, and the kinds of project to which it is suited.

---

## 5.1 The Waterfall Model

The waterfall model is a sequential development model in which a project is divided into consecutive phases, each of which must be completed before the next begins. The model is commonly attributed to Royce (1970), although that paper in fact presented the purely sequential form as a risky practice rather than as a recommendation.

The typical phases are requirements specification, system design, implementation, testing, deployment and maintenance. Each phase produces a document or deliverable that serves as the input to the phase following it.

**Advantages.** The structure is clear and readily managed, since each phase has a defined and verifiable output. Documentation is produced throughout, which assists handover and long-term maintenance. Progress is straightforward to measure because completion milestones are unambiguous.

**Limitations.** The model assumes that all requirements can be specified accurately at the outset, which is rarely the case in practice (Sommerville 2011). Returning to revise requirements at a late stage carries a very high cost. Furthermore, working software appears only in the final phases, so any divergence from what was actually needed is discovered only after most of the effort has been expended.

**Suited to.** Projects whose requirements are stable, well understood from the beginning and unlikely to change, particularly those subject to legal or safety constraints that demand comprehensive documentation.

---

## 5.2 The Spiral Model

The spiral model proposed by Boehm (1988) combines the iterative character of prototyping with the managerial control of the waterfall model, placing **risk analysis** at its centre.

A project proceeds through repeated cycles, each comprising four activities: determining objectives and constraints, evaluating alternatives and analysing risks, developing and verifying the deliverable for that cycle, and planning the cycle to follow. With each cycle, the scope and maturity of the product increase.

**Advantages.** Risks are identified and addressed early rather than accumulating towards the end of the project. The model permits the direction of work to be adjusted after each cycle while retaining managerial discipline. It suits large and complex systems, where the consequences of a wrong decision are considerable.

**Limitations.** Management overhead is high, since every cycle requires a formal risk analysis activity. Effective application depends substantially on the risk assessment capability of the project manager. For small projects, this overhead typically exceeds the benefit obtained.

**Suited to.** Large-scale projects carrying significant technical or financial risk, and possessing sufficient resources for managerial activity.

---

## 5.3 Rapid Application Development and Prototyping

Rapid Application Development is a methodology emphasising speed of delivery through the continuous construction of prototypes and the gathering of user feedback, in preference to detailed planning at the outset (Martin 1991).

Its characteristic features are the construction of prototypes for users to experience and comment upon early, the imposition of time limits on each development cycle, and the use of supporting tools to shorten implementation time.

**Advantages.** Working software appears very early, allowing divergences from actual requirements to be detected while the cost of correction remains low. Users participate directly in the process, so the product remains close to genuine needs. Time to market is reduced.

**Limitations.** Documentation is frequently sparse or inconsistent, given the emphasis placed on working software. Architectural quality may be sacrificed for speed, leading to difficulties in later maintenance. The methodology also requires users willing to participate continuously, which is not always feasible.

**Suited to.** Projects with unclear requirements, of small to medium scale, and operating under pressing deadlines.

---

## 5.4 Agile

Agile is not a specific process but a set of values and principles, published in the Manifesto for Agile Software Development (Beck et al. 2001). Its four core values place individuals and interactions above processes and tools, working software above comprehensive documentation, customer collaboration above contract negotiation, and responding to change above following a plan.

In practical terms, Agile is implemented through short development cycles, each producing a portion of working, assessable software. Requirements are clarified progressively across cycles rather than being fixed at the beginning (Highsmith 2002).

**Advantages.** A high capacity to adapt to change, which is all but unavoidable in software development. Feedback arrives early and continuously, reducing the risk of building the wrong product. Working software exists at every point in the project.

**Limitations.** Documentation tends to be thin, creating difficulties for handover and long-term maintenance. Flexibility, if left uncontrolled, can produce scope creep as new requirements are added continuously without any mechanism of prioritisation. The methodology also demands a high degree of autonomy and discipline from those carrying it out.

**Suited to.** Projects with volatile requirements, requiring early delivery, and staffed by people capable of managing themselves.

---

## 5.5 The Selected Methodology and Its Justification

### The Selection

The WildBound project applies **Agile in an iterative and incremental form**, incorporating the prototyping element of RAD during its opening phase.

It should be stated clearly that this was not a decision taken on paper before work began, but an accurate account of how the project was in fact carried out.

### Justification Drawn From Actual Practice

**The real development cycles were very short.** Each feature was built, run immediately within the editor, tested for faults, corrected, and only then followed by the next. Such a cycle lasted from a few hours to a few days. This is the iterative and incremental model operating at its smallest scale.

**Requirements changed continuously during development.** Three systems originally planned were removed midway, the bush harvesting mechanism was redesigned, and the entire set of balance parameters was revised on several occasions. Under a waterfall model each such change would require returning to the requirements phase, at a cost exceeding what a seven-month project could absorb.

**Three features were added at a late stage.** The rock-mining system, the higher-tier creature variant and the tutorial overlay all arose after the architecture had taken shape, and all were integrated without redesigning existing work. This capacity to accommodate late additions is characteristic of incremental development.

**The feedback loop was closed within a single person.** Since the project was carried out independently, the development and testing roles occupied the same position, so feedback was immediate and free of communication delay.

**A prototyping element in the opening phase.** The period from January to March 2026 was in essence a prototyping exercise: systems were assembled quickly by following tutorials in order to understand the tools, then discarded and rewritten from their foundations once the real requirements had become clear. This is characteristic of RAD, in which a prototype exists in order to be learned from rather than necessarily to be kept.

**The university's milestone system functioned as Agile cycles.** Each milestone required a working portion of the product to be presented, corresponding to the notion of an incremental deliverable produced at the end of each cycle.

### Why the Other Methodologies Were Not Selected

**The waterfall model** was rejected because its premise was not satisfied: the requirements of this project could not be specified fully at the outset, since most design decisions became clear only once the system was running and could be played. Game balance is the clearest example - there is no means of determining the correct statistic depletion rate on paper without experimentation.

**The spiral model** was rejected because its management overhead is disproportionate to the scale. Formal risk analysis at each cycle is reasonable for a large project involving several stakeholders, but for an individual project most of that effort would be spent producing documents with no reader.

**RAD in its pure form** was not adopted in full because its single most important precondition was absent: continuous user participation. The project had no group of testers from whom to gather periodic feedback, so only the prototyping element of RAD was employed, while the user feedback element was not.

### Limitations of Applying Agile to an Individual Project

Applying Agile in this context carries three limitations that should be acknowledged.

First, **most Agile practices are designed for teamwork**. Daily synchronisation meetings, pair programming, peer code review and cycle retrospectives are all meaningless for one person. What remains is only the iterative and incremental core.

Second, **there was no customer or genuine user from whom to obtain feedback**. Whereas Agile places customer collaboration among its four core values, this project involved no external players. The consequence is that the feedback loop, though rapid, was closed, and that balance decisions reflect only the perspective of someone who already knew every mechanism in advance. This limitation is examined further in Section 6.4.1.

Third, **de-emphasising documentation produced a real consequence**. Because the focus rested on working software, most design decisions taken during development were not recorded at the moment they were made, and had to be reconstructed afterwards from the source code and the change history while writing this report. This is precisely the limitation against which both Martin (1991) and Highsmith (2002) caution, and it materialised exactly as predicted.

### Conclusion

Overall, Agile in an iterative and incremental form was the methodology best suited to the WildBound project, since it accommodates three of the project's defining characteristics: requirements that were not stable at the outset, a fixed deadline, and a single person occupying both the development and the testing role. The limitations noted above do not invalidate the choice, but they do show that the methodology was applied in a reduced form appropriate to an individual context, rather than adopted wholesale and mechanically.

---

# REFERENCES - CHAPTER 5

Beck K et al. (2001) *Manifesto for agile software development*, Agile Alliance website, accessed 31 July 2026.
https://agilemanifesto.org/

Boehm B W (1988) 'A spiral model of software development and enhancement', *Computer*, 21(5):61-72.

Highsmith J (2002) *Agile software development ecosystems*, Addison-Wesley, Boston.

Martin J (1991) *Rapid application development*, Macmillan, New York.

Royce W W (1970) 'Managing the development of large software systems', *Proceedings of IEEE WESCON*, 26:1-9.

Sommerville I (2011) *Software engineering*, 9th edn, Pearson, Harlow.
