# SƯỜN BÁO CÁO TỐT NGHIỆP - WILDBOUND
### Tài liệu tham khảo nội bộ (tiếng Việt) - bản nộp cuối cùng sẽ viết bằng tiếng Anh

---

## THÔNG TIN CHUNG

| Mục | Nội dung |
|---|---|
| Tên đề tài | **Wildbound** - First-person survival game (Unity 6, URP) |
| Hình thức | Cá nhân (solo) |
| Bắt đầu | 17/01/2026 |
| Tách khỏi tutorial | 14/03/2026 |
| Tách khỏi nhóm 3 người | 04/04/2026 |
| **Hạn nộp Report + Proposal** | **01/08/2026** |
| Hạn nộp game/code | 15/08/2026 |
| Buổi check ở trường | 30/07/2026 |
| Ngôn ngữ bản nộp | Tiếng Anh 100% |
| Template | Greenwich (font, size, cover page theo mẫu trường) |
| Độ dài dự kiến | ~60-90 trang (chưa tính phụ lục) |

**Nguyên tắc xuyên suốt cả bài:**
1. Mỗi khẳng định mang tính lý thuyết đều phải có nguồn trích dẫn (Harvard style).
2. Mỗi phần lý thuyết phải "hạ cánh" xuống Wildbound - tức là nói xong lý thuyết phải có 1 đoạn "áp dụng vào project của tôi như thế nào". Đây là chỗ ăn điểm lớn nhất, cũng là chỗ sinh viên hay mất điểm vì chỉ chép lý thuyết suông.
3. Viết ở ngôi thứ nhất số ít khi nói về quyết định cá nhân ("I decided to...") - vì đây là đồ án cá nhân, người chấm muốn thấy chính kiến của bạn.
4. Thà ít mà sâu còn hơn nhiều mà nông (giảng viên đã xác nhận: 1 engine, 1 game so sánh, nhưng phải phân tích chi tiết).

---

# CHƯƠNG 1 - INTRODUCTION (5-10 trang)

> **Vai trò của chương này:** Người chấm đọc xong chương 1 phải trả lời được: Đề tài này là gì? Tại sao đáng làm? Làm trong bao lâu, theo kế hoạch nào? Kết quả ra sao? Nó là "bản tóm tắt mở rộng" của toàn bộ báo cáo.

## 1.1 Introduction about the project subject (1-2 trang)

**Mục tiêu:** Giới thiệu bối cảnh và bản thân đề tài. Người đọc chưa biết gì về game của bạn đọc xong phải hình dung được game này chơi kiểu gì.

**Nội dung cần có:**
- Bối cảnh chung: thể loại survival game là gì, tại sao nó phổ biến (dẫn số liệu thị trường game nếu tìm được nguồn).
- Giới thiệu Wildbound: game sinh tồn góc nhìn thứ nhất, bối cảnh một hòn đảo biệt lập, người chơi phải quản lý HP/Khát/Đói, thu thập tài nguyên, chế tạo công cụ, nấu ăn, chiến đấu để sống sót càng lâu càng tốt.
- **Giải thích tên game** (chỗ này nên có, nó tạo ấn tượng tốt):
  - *Wild* = thiên nhiên hoang dã, sự sinh tồn giữa tự nhiên.
  - *Bound* = bị giới hạn/trói buộc trong một không gian cố định (hòn đảo) - người chơi không thể rời đi, chỉ có thể thích nghi.
  - Ghi nhẹ 1 câu: tên gọi cũng có sự tương đồng ngữ âm với tên tác giả (Quốc Bảo). **Chỉ 1 câu duy nhất, không nhắc lại ở bất kỳ đâu khác trong bài.**
- Lý do chọn đề tài: kết hợp giữa sở thích cá nhân với thể loại game và mong muốn học sâu về thiết kế hệ thống game (game systems design) chứ không chỉ là dựng hình ảnh.

**Bẫy cần tránh:** Đừng viết phần này như quảng cáo game. Đây là văn phong học thuật - mô tả khách quan, có dẫn nguồn cho các nhận định về thị trường/thể loại.

---

## 1.2 Project objectives (1-2 trang)

**Mục tiêu:** Liệt kê rõ ràng các mục tiêu đo lường được. Chương 7 sẽ quay lại đối chiếu xem đạt hay không đạt, nên phần này phải viết sao cho **có thể kiểm chứng được**.

**Nội dung cần có:**

*Mục tiêu tổng quát (1 đoạn):* Xây dựng một game sinh tồn góc nhìn thứ nhất hoàn chỉnh về mặt vòng lặp cốt lõi (core gameplay loop), tự thiết kế hệ thống thay vì làm theo hướng dẫn có sẵn.

*Mục tiêu cụ thể (nên đánh số, dạng danh sách):*
1. Xây dựng hệ thống Inventory + Hotbar có kéo thả và xếp chồng vật phẩm.
2. Xây dựng hệ thống chế tạo (Crafting) dựa trên công thức có định lượng nguyên liệu.
3. Xây dựng hệ thống chỉ số sinh tồn (HP / Thirst / Hunger) có tương tác lẫn nhau.
4. Xây dựng chuỗi tương tác nhiều bước với môi trường (lấy nước → đun sôi → múc nước / nấu thịt).
5. Xây dựng hệ thống thu thập tài nguyên có công cụ chuyên biệt (rìu, cuốc) kèm độ bền.
6. Xây dựng AI sinh vật (thỏ) có hành vi đi lang thang và phản công.
7. Thiết kế nền kinh tế tài nguyên khép kín (cung - cầu) đảm bảo người chơi không rơi vào trạng thái bế tắc không thể thắng.
8. Xây dựng giao diện đầy đủ: Main Menu, HUD, Dead Screen.

*Mục tiêu phi chức năng (nếu muốn ăn thêm điểm):* code có cấu trúc rõ ràng, dễ mở rộng, mọi thông số cân bằng game tập trung tại một nơi duy nhất (GameConfig) thay vì rải rác trong code.

**Mẹo:** Có thể suy ngược danh sách này từ chính các milestone bạn đã nộp trên hệ thống của trường (Create an Interactive Dot → Adjust new terrain → Hotbar Selection & Item Interaction → Player Combat & Meat Consumption → Rabbit AI Combat & Drop → Core World Interaction → Onboarding Tutorial). Cách này vừa nhanh vừa khớp với hồ sơ nhà trường đang lưu.

---

## 1.3 Project plan (1-2 trang)

**Mục tiêu:** Chứng minh bạn có kế hoạch và bám theo được kế hoạch (kể cả khi kế hoạch phải thay đổi).

**Nội dung cần có:**
- **Bảng timeline / biểu đồ Gantt** theo các mốc thật:

| Giai đoạn | Thời gian | Nội dung |
|---|---|---|
| Khởi động | 17/01 - 14/03/2026 | Học Unity qua tutorial YouTube, dựng nền tảng ban đầu |
| Chuyển giao | 14/03/2026 | Tách khỏi tutorial, bắt đầu tự thiết kế hệ thống |
| Tái cấu trúc | 04/04/2026 | Tách khỏi nhóm 3 người, chuyển sang làm cá nhân |
| Phát triển lõi | 04/04 - 25/06/2026 | Inventory, Hotbar, Crafting, Combat, Rabbit AI |
| Mở rộng hệ thống | 25/06 - 23/07/2026 | Cooking chain, độ bền công cụ, cân bằng tài nguyên |
| Hoàn thiện | 23/07 - 15/08/2026 | Main Menu, đánh bóng, viết báo cáo |

- **Giải thích 2 quyết định quan trọng** (đây là phần "có giá trị kể chuyện" nhất của bài):
  1. *Tại sao tách khỏi tutorial (14/03):* tutorial chỉ dạy làm theo, không dạy thiết kế; muốn hệ thống theo ý đồ riêng nên buộc phải viết lại từ nền tảng.
  2. *Tại sao tách khỏi nhóm (04/04):* làm nhóm 3 người không hiệu quả, bản thân chủ động đề xuất tách để kiểm soát được tiến độ và chất lượng. **Viết trung lập, chuyên nghiệp - nói về hiệu quả công việc, tuyệt đối không chê bai cá nhân nào.**
- Công cụ quản lý tiến độ: hệ thống milestone của trường + Git commit history (có thể chụp màn hình biểu đồ contribution GitHub làm bằng chứng).

---

## 1.4 Project outcomes (1-2 trang)

**Mục tiêu:** Nói rõ cuối cùng đã giao được cái gì.

**Nội dung cần có:**
- Sản phẩm bàn giao: game Wildbound chạy được, hoàn chỉnh vòng lặp cốt lõi (vào Main Menu → chơi → sinh tồn → chết → xem thời gian sống sót → chơi lại).
- Danh sách hệ thống đã hoàn thành (đối chiếu 1-1 với mục 1.2).
- Số liệu định lượng nghe rất thuyết phục, nên có:
  - Số lượng script C# tự viết (~24 file).
  - Số lượng commit trên GitHub.
  - Số hệ thống game hoàn chỉnh.
  - Số vật phẩm/công thức chế tạo.
- Kèm 1-2 ảnh chụp màn hình tiêu biểu (để người đọc thấy ngay sản phẩm, không phải đợi tới chương 6).

---

## 1.5 Project evaluation (1-2 trang) - **ĐÁNH GIÁ NGẮN**

**Mục tiêu:** Tự đánh giá sơ bộ. **Lưu ý: chỉ tóm tắt, đánh giá đầy đủ nằm ở chương 7 - không viết dài ở đây, tránh trùng lặp bị trừ điểm.**

**Nội dung cần có:**
- Điểm mạnh (2-3 ý): vòng lặp cốt lõi hoàn chỉnh; kiến trúc code có tính tái sử dụng cao; hệ thống cân bằng tài nguyên được tính toán kỹ.
- Điểm hạn chế (2-3 ý, phải thành thật): chưa có chu kỳ ngày/đêm, thời tiết, hệ thống trồng trọt; đồ họa dùng asset có sẵn; chưa có lưu game.
- 1 câu kết: mức độ hoàn thành so với mục tiêu ban đầu (ước lượng %).

---

# CHƯƠNG 2 - LITERATURE REVIEW

> **Vai trò của chương này:** Chứng minh bạn đã nghiên cứu nền tảng lý thuyết trước khi làm, chứ không phải "vọc" ngẫu nhiên. Đây là chương học thuật nhất, **bắt buộc dày trích dẫn**. Tối thiểu 5 nguồn cho phần lý thuyết (giảng viên không ép con số nhưng chắc chắn bắt buộc phải có).

**Cấu trúc chuẩn cho MỖI mục 2.x:** (1) Định nghĩa/lý thuyết có trích dẫn → (2) Phân tích, so sánh các quan điểm → (3) **Liên hệ về Wildbound**.

## 2.1 Survival games - định nghĩa và lịch sử thể loại
- Định nghĩa thể loại, các đặc trưng bắt buộc (quản lý tài nguyên, chỉ số sinh tồn, chế tạo, nguy hiểm thường trực).
- Lịch sử hình thành và phát triển (Minecraft → DayZ → Don't Starve → Rust/The Forest/Green Hell).
- Phân biệt survival game với survival horror, roguelike, sandbox.
- → Wildbound thuộc nhánh nào.

## 2.2 Core gameplay loop và cơ chế giữ chân người chơi
- Khái niệm gameplay loop (vòng lặp cốt lõi), loop ngắn/loop dài.
- Lý thuyết về động lực chơi game: cơ chế phần thưởng, sự tiến triển (progression), lý thuyết dòng chảy (Flow theory - Csikszentmihalyi).
- Khái niệm áp lực tài nguyên (resource scarcity/pressure) tạo căng thẳng có chủ đích.
- → Vòng lặp của Wildbound: thu thập → chế tạo → sinh tồn lâu hơn → thu thập hiệu quả hơn.

## 2.3 Thiết kế nền kinh tế tài nguyên trong game (Game Economy Design)
- Lý thuyết nguồn (source) - bể chứa (sink) - dòng chảy (flow) tài nguyên.
- Vấn đề "dead-end state" (trạng thái bế tắc): người chơi tiêu hết tài nguyên và không thể tiếp tục.
- → **Đây là mục nên viết kỹ nhất chương 2, vì Wildbound có ví dụ thực tế cực tốt:** hệ thống cung-cầu đã thiết kế (Stick từ bụi cây + chặt cây; Rock từ nhặt + đào; cơ chế BigRock tự sinh đá dự phòng mỗi 3 phút chính là giải pháp chống bế tắc; Stick/Rock dư được dùng làm nhiên liệu tiếp cho bếp chính là "sink").

## 2.4 Game engine - tổng quan và Unity
- Game engine là gì, gồm những thành phần nào (rendering, physics, audio, scripting, asset pipeline).
- So sánh nhanh Unity / Unreal / Godot (chỉ 1 đoạn ngắn cho có bối cảnh).
- **Phân tích sâu Unity**: kiến trúc GameObject-Component, vòng đời script (Awake/Start/Update), Prefab system, ScriptableObject, hệ thống Render Pipeline (Built-in / URP / HDRP).
- → Lý do chọn Unity + URP cho Wildbound.

## 2.5 Kiến trúc phần mềm trong phát triển game
- Các mẫu thiết kế (design pattern) phổ biến trong game: Singleton, State Machine, Component-based architecture, Observer.
- Vấn đề data-driven design: tách dữ liệu cân bằng ra khỏi logic.
- → Wildbound dùng Singleton (InventorySystem, CraftingSystem...), State Machine (FirePitManager, Bush), data-driven (GameConfig ScriptableObject).

## 2.6 First-person controller và tương tác trong không gian 3D
- Cơ chế điều khiển góc nhìn thứ nhất, camera, chuột.
- Kỹ thuật phát hiện tương tác: raycast vs collider trigger vs khoảng cách - ưu nhược điểm từng cái.
- → Wildbound chọn raycast từ camera; giải thích lý do đã thử và loại bỏ phương án Sphere Collider quanh người chơi.

## 2.7 (Tùy chọn) Onboarding và trải nghiệm người chơi mới
- Nếu bạn có làm màn hình hướng dẫn (milestone 23/07 có nhắc "Player Onboarding Tutorial Screen") thì thêm mục này: lý thuyết về việc dạy người chơi mà không cần đọc hướng dẫn.

---

# CHƯƠNG 3 - TECHNOLOGY AND TOOLS

> **Vai trò:** Nói về **công cụ**, không nói về **lý thuyết** (lý thuyết đã ở chương 2). Mỗi mục nên có: công cụ đó là gì → tại sao chọn nó → dùng nó làm gì cụ thể trong Wildbound.

## 3.1 Unity 6 Game Engine
- Phiên bản cụ thể (6000.3.10f1 - lấy đúng số trong project).
- Các thành phần đã sử dụng: Scene system, Prefab, Physics (Rigidbody/Collider/Raycast), Animator, Terrain.
- Lý do chọn Unity 6 (bản mới nhất, hỗ trợ dài hạn, cộng đồng lớn, tài liệu phong phú).

## 3.2 Universal Render Pipeline (URP)
- URP là gì, khác Built-in và HDRP ở đâu.
- Lý do chọn URP: cân bằng giữa chất lượng hình ảnh và hiệu năng, hỗ trợ tốt post-processing.
- Ứng dụng thực tế: hiệu ứng Depth of Field làm mờ nền ở màn hình Main Menu.
- **Nên nhắc tới sự cố thật:** milestone 28/05 "Resolved Major Render Pipeline Compatibility Issues Between Built-in, URP and HDRP" - đây là kinh nghiệm thực tế rất đáng viết, cho thấy bạn hiểu sự khác biệt giữa các pipeline chứ không chỉ dùng theo mặc định.

## 3.3 C# và .NET
- Ngôn ngữ lập trình chính, các đặc trưng đã dùng: OOP, kế thừa từ MonoBehaviour, property, enum, generic, LINQ (nếu có).

## 3.4 Visual Studio / IDE
- Môi trường viết code, debug, IntelliSense.

## 3.5 Git và GitHub
- Khái niệm version control và lý do bắt buộc phải có với dự án dài hạn.
- Quy trình làm việc thực tế: add → commit → push, quy ước đặt tên commit.
- GitHub làm nơi lưu trữ dự phòng và ghi nhận lịch sử phát triển (có thể chụp ảnh biểu đồ commit).
- Nhắc tới thử thách gặp phải: giới hạn dung lượng 2GB mỗi lần push của GitHub, phải tách commit lớn thành nhiều lần.

## 3.6 Third-party Assets
- Liệt kê rõ ràng, minh bạch:
  - *Low-Poly Forest Survival Starter Pack Lite* (Devtricked) - mô hình môi trường rừng.
  - *Fantasy Environments* - vật liệu và mô hình đá.
  - (Bổ sung các asset khác nếu có.)
- Với mỗi asset: nguồn (Unity Asset Store), giấy phép sử dụng, dùng vào việc gì.
- **1 câu quan trọng:** các asset này chỉ cung cấp phần hình ảnh; toàn bộ logic gameplay là do tác giả tự viết.
- → Để thẳng trong chương này, **không đẩy xuống phụ lục** (nội dung ngắn, nằm trong thân bài hợp lý hơn).

## 3.7 AI-assisted development tools
- Khai báo trung thực nhưng gọn: có sử dụng công cụ AI (Claude) để **hỗ trợ viết code và hiệu đính ngữ pháp/câu chữ**.
- 1-2 câu nhấn mạnh: toàn bộ thiết kế hệ thống, quyết định kiến trúc, cân bằng gameplay và việc gỡ lỗi là do tác giả chủ động; tác giả nắm được cách hoạt động của toàn bộ mã nguồn.
- **Viết ngắn gọn, thái độ chuyên nghiệp, không né tránh cũng không nhấn mạnh quá mức.**

---

# CHƯƠNG 4 - SOFTWARE PRODUCT REQUIREMENTS

> **Vai trò:** Đây là chương "kỹ sư phần mềm" - thể hiện bạn biết phân tích yêu cầu bằng sơ đồ chuẩn UML, không chỉ biết code.

## 4.1 Review/overview of other similar products
- Chọn **1 game** để phân tích chi tiết (giảng viên cho phép ít mà sâu). Gợi ý: **The Forest** hoặc **Green Hell** - vì cả hai đều là survival góc nhìn thứ nhất, có chế tạo công cụ, nấu ăn bằng lửa trại, chỉ số đói/khát → gần Wildbound nhất.
- Nội dung phân tích: vòng lặp cốt lõi, hệ thống chế tạo, cách quản lý chỉ số sinh tồn, giao diện, điểm mạnh, điểm yếu.
- **Kết lại bằng bảng so sánh** giữa game đó và Wildbound: cái gì học theo, cái gì làm khác, cái gì lược bỏ và tại sao.

## 4.2 Use Case Diagram / User Stories
- **Actor:** Player (game 1 người chơi nên chỉ 1 actor chính; có thể thêm "System" cho các tiến trình tự động như hồi sinh tài nguyên, sinh thỏ).
- **Liệt kê ĐẦY ĐỦ mọi hành động** (đã xác nhận yêu cầu này):
  - Di chuyển, chạy nhanh, nhảy, xoay camera
  - Nhặt vật phẩm, thả vật phẩm (Q)
  - Chọn ô hotbar (phím số / cuộn chuột)
  - Mở/đóng túi đồ, kéo thả vật phẩm giữa các ô
  - Chế tạo thủ công, chế tạo qua Tool Library
  - Ăn/uống (F)
  - Chặt cây, hái bụi cây, đào đá lớn
  - Tấn công thỏ, nhận sát thương từ thỏ
  - Lấy nước bẩn, đun nước, múc nước
  - Nấu thịt, tiếp nhiên liệu cho bếp
  - Xem thời gian sống sót, chết, chơi lại, về menu chính
- Viết kèm User Story dạng: *"As a player, I want to ... so that ..."*

## 4.3 Use Case Specifications / Activity Diagrams / Sequence Diagrams
- **Use Case Specification** (bảng chi tiết): chọn 3-5 use case phức tạp nhất viết đầy đủ (tên, actor, tiền điều kiện, luồng chính, luồng thay thế, hậu điều kiện). Gợi ý chọn: *Craft an item*, *Boil water*, *Cook meat*, *Attack a rabbit*.
- **Activity Diagram:** vẽ luồng chuỗi nấu nướng (Pot → nước bẩn → đun → múc nước) - đây là luồng nhiều nhánh nhất, vẽ ra rất đẹp.
- **Context Diagram:** hệ thống Wildbound và các thực thể tương tác.
- **Sequence Diagram:** chọn 1-2 luồng thể hiện sự phối hợp giữa nhiều script, ví dụ: Player click → PlayerAttack → RabbitHealth → InventorySystem (rớt thịt); hoặc luồng chế tạo: Player → CraftingSlot → CraftingSystem → InventorySystem.

## 4.4 ERD → **Data Model Diagram** (đã thống nhất thay thế)
- Game không dùng cơ sở dữ liệu, nên thay ERD bằng **sơ đồ quan hệ dữ liệu trong game**.
- Các thực thể: `ItemData` (itemName, maxStack, currentStack, isConsumable, hungerRestore, thirstRestore, maxDurability, currentDurability), `CraftingRecipe` (input1, count1, input2, count2, output), `ItemSlot`, `PlayerStats`, `GameConfig`.
- Vẽ quan hệ giữa chúng (1-n, n-n) như một ERD bình thường.
- **Nhớ giải thích 1 đoạn tại sao không dùng database:** game offline một người chơi, toàn bộ trạng thái nằm trong bộ nhớ khi chạy, dữ liệu cấu hình được lưu dạng ScriptableObject asset.

## 4.5 Sitemap → **Screen Flow Diagram** (đã thống nhất thay thế)
- Sơ đồ luồng màn hình: `Main Menu → (Play) → Gameplay Scene → (E) Inventory/Crafting → (Tool Library) → ... ` và `Gameplay → (HP = 0) → Dead Screen → (Restart) → Gameplay` / `(Home) → Main Menu`.
- Kèm mô tả ngắn chức năng từng màn hình.

---

# CHƯƠNG 5 - REVIEW OF SOFTWARE DEVELOPMENT METHODOLOGIES (5-10 trang)

> **Vai trò:** Chương lý thuyết thuần túy, viết ngắn gọn theo đúng yêu cầu (1-2 trang/mục). **Giá trị thật nằm ở mục 5.5** - chỗ bạn giải thích lựa chọn của mình.

Mỗi mục 5.1 - 5.4 viết theo cùng một khuôn: định nghĩa → các giai đoạn → sơ đồ minh họa → ưu điểm → nhược điểm → phù hợp với loại dự án nào.

## 5.1 Waterfall (1-2 trang)
Tuần tự, mỗi giai đoạn xong mới sang giai đoạn kế, khó quay lui.

## 5.2 Spiral (1-2 trang)
Lặp theo vòng xoắn, nhấn mạnh phân tích rủi ro ở mỗi vòng.

## 5.3 RAD / Prototyping (1-2 trang)
Làm bản mẫu nhanh, lấy phản hồi, hoàn thiện dần.

## 5.4 Agile (1-2 trang)
Lặp ngắn (sprint), linh hoạt trước thay đổi, ưu tiên sản phẩm chạy được hơn tài liệu.

## 5.5 Lựa chọn và biện luận (1-2 trang) - **MỤC QUAN TRỌNG NHẤT CHƯƠNG 5**
- **Lựa chọn: Agile (kết hợp yếu tố Prototyping), theo hướng iterative & incremental.**
- **Biện luận bằng chính thực tế đã làm** (đây là điểm mạnh của bạn, vì cách làm thực tế đúng là Agile):
  - Làm từng tính năng nhỏ → chạy thử ngay trong Unity → phát hiện lỗi → sửa → làm tiếp. Mỗi vòng lặp ngắn (vài giờ đến vài ngày).
  - Yêu cầu thay đổi liên tục trong quá trình làm (bỏ hẳn hệ thống trồng trọt giữa chừng; đổi cơ chế bụi cây; cân bằng lại toàn bộ số liệu nhiều lần) - Waterfall sẽ không kham nổi những thay đổi này.
  - Tự mình vừa là người phát triển vừa là người kiểm thử, phản hồi tức thì.
  - Hệ thống milestone 2 tuần/lần của nhà trường về bản chất hoạt động như các sprint.
- **Nêu rõ hạn chế khi áp dụng Agile cho dự án cá nhân:** không có daily standup, không có khách hàng thật để lấy phản hồi, không có review chéo - nêu được điều này chứng tỏ hiểu bản chất chứ không áp dụng máy móc.

---

# CHƯƠNG 6 - DESIGN AND IMPLEMENTATION

> **Vai trò:** Chương dày nhất, quan trọng nhất, chứng minh năng lực kỹ thuật thật sự.

## 6.1 Product Analysis and Design

### a) GUI Design
- Mô tả thiết kế giao diện: HUD (3 thanh chỉ số, hotbar, thời gian sống sót, tên vật phẩm, dòng chữ gợi ý tương tác), Inventory + Crafting, Tool Library, Dead Screen, Main Menu.
- **Trung thực về quy trình:** thiết kế trực tiếp trong Unity Editor theo hướng lặp, không vẽ wireframe trước - và giải thích tại sao lựa chọn này hợp lý (dự án cá nhân, phản hồi hình ảnh tức thì, tiết kiệm thời gian). Không cần bịa ra wireframe.
- Nguyên tắc thiết kế đã áp dụng: thông tin quan trọng đặt ở rìa màn hình để không che tầm nhìn, dùng màu sắc phân biệt trạng thái (thanh máu, ô hotbar được chọn sáng vàng), phản hồi tức thì khi trúng đòn (màn hình lóa đỏ).

### b) Analysis
- Phân rã hệ thống: liệt kê các hệ thống con và trách nhiệm của từng cái.
- Sơ đồ kiến trúc tổng thể: cách các hệ thống giao tiếp với nhau (qua Singleton).
- Sơ đồ lớp (Class Diagram) rút gọn cho các lớp cốt lõi.

### c) Design - Basic
- Kiến trúc tổng: Unity component-based + các Singleton quản lý.
- Quy ước tổ chức project: cấu trúc thư mục, quy ước đặt tên prefab, quy ước Resources.Load theo tên, quy ước Tag/Layer.

### d) Design - Detailed
Chọn 3-4 hệ thống mô tả chi tiết kèm sơ đồ:
1. **Hệ thống Inventory/Hotbar**: cấu trúc slot, cách nhận diện vật phẩm trong ô, cơ chế xếp chồng.
2. **Hệ thống Crafting**: cấu trúc CraftingRecipe, thuật toán so khớp công thức, cách xử lý khi 2 công thức trùng nguyên liệu.
3. **State machine của FirePit**: sơ đồ trạng thái Normal → Boiling → BoiledWater, cơ chế đổi prefab kèm chuyển giao trạng thái, hệ thống độ bền.
4. **Vòng đời tài nguyên**: sơ đồ nguồn - bể chứa của toàn bộ tài nguyên trong game.

## 6.2 Features với screenshot (5-7 ảnh)

Mỗi ảnh kèm 1 đoạn giải thích ngắn (3-5 câu): đang thể hiện gì, hoạt động ra sao, thiết kế như vậy vì lý do gì.

**Danh sách ảnh đề xuất:**
1. **Main Menu** - tên game Wildbound, nền camera quay chậm được làm mờ bằng Depth of Field, nút Play.
2. **Gameplay tổng quan + HUD** - 3 thanh chỉ số, hotbar, thời gian sống sót, chữ gợi ý khi nhìn vào vật thể.
3. **Inventory + Crafting** - có vật phẩm xếp chồng hiện số lượng, đang kéo thả.
4. **Tool Library** - danh sách công thức phân trang, nút Choose.
5. **Chuỗi nấu nướng ở bếp** - chụp lúc hiện chữ "Cooking... 60%" hoặc "Boil Water" (thể hiện chuỗi tương tác nhiều bước).
6. **Chiến đấu với thỏ** - thỏ đang đuổi theo, kèm hiệu ứng màn hình lóa đỏ khi trúng đòn (nếu bắt kịp khoảnh khắc).
7. **Dead Screen** - nền đỏ mờ dần, chữ Game Over, thời gian sống sót, nút Restart/Home.

*Mẹo chụp:* chụp ở độ phân giải 16:9 cố định, tắt Gizmos trong Game view, chụp ở chế độ Maximize On Play để ảnh sạch.

## 6.3 Product Implementation - Major pieces of code (5-7 đoạn)

Mỗi đoạn code kèm: bối cảnh (giải quyết vấn đề gì) → code (có đánh số dòng) → giải thích kỹ thuật → **tại sao làm cách này mà không phải cách khác**.

**7 đoạn code đề xuất (đã chọn lọc để mỗi đoạn thể hiện một kỹ thuật khác nhau):**

1. **`SpawnReplacement()` - mẫu thay thế đối tượng có bảo toàn trạng thái**
   *(FirePitManager.cs / Bush.cs)* - Kỹ thuật: state machine + prefab swap, chuyển giao dữ liệu sang thực thể mới. Điểm nhấn: cùng một mẫu thiết kế được tái sử dụng cho 2 hệ thống hoàn toàn khác nhau (bếp lửa và bụi cây) → chứng minh khả năng trừu tượng hóa.

2. **`CraftingSystem.CheckRecipe()` - thuật toán so khớp công thức theo độ ưu tiên**
   Kỹ thuật: duyệt danh sách, so khớp không phụ thuộc thứ tự đầu vào, chọn công thức có tổng nguyên liệu cao nhất khi nhiều công thức cùng khớp. Điểm nhấn: đây là lời giải cho một lỗi thật (Rìu và Bếp lửa cùng dùng Đá + Gậy, hệ thống chọn nhầm công thức).

3. **`PlayerStats.Update()` - vòng lặp mô phỏng sinh tồn**
   Kỹ thuật: các chỉ số phụ thuộc lẫn nhau, hồi máu có điều kiện và có đánh đổi (hồi máu làm đói/khát nhanh hơn). Điểm nhấn: thiết kế cân bằng game thể hiện bằng code.

4. **Mẫu Singleton + GameConfig ScriptableObject - kiến trúc data-driven**
   Kỹ thuật: truy cập toàn cục có kiểm soát; tách toàn bộ số liệu cân bằng khỏi logic. Điểm nhấn: đổi độ khó game không cần sửa một dòng code nào.

5. **`PotInteraction` - chuỗi tương tác nhiều bước phụ thuộc ngữ cảnh**
   Kỹ thuật: raycast + so khớp (vật phẩm đang cầm × mục tiêu đang nhìn), xử lý phím giữ có tiến trình (%). Điểm nhấn: một script điều phối nhiều luồng tương tác khác nhau.

6. **`PlayerAttack` - tái sử dụng logic cho nhiều công cụ**
   Kỹ thuật: gộp Rìu và Cuốc chung một đường xử lý sát thương và độ bền, chỉ khác ở đối tượng được phép tác động. Điểm nhấn: nguyên tắc DRY (không lặp lại code) - thêm công cụ mới gần như không phải viết thêm logic.

7. **`MenuCameraRig` - điều khiển chuyển cảnh bằng máy trạng thái và bộ đếm thời gian**
   Kỹ thuật: state machine (Panning → FadingOut → FadingIn), nội suy góc xoay Slerp có làm mượt SmoothStep. Điểm nhấn: chọn cách dùng timer trong Update thay vì Coroutine để nhất quán với phần còn lại của project.

## 6.4 Evaluation of your product (1-2 trang)

**Điểm tốt:**
- Vòng lặp cốt lõi hoàn chỉnh và chơi được trọn vẹn.
- Nền kinh tế tài nguyên được tính toán cẩn thận, không rơi vào bế tắc (dẫn chứng bằng bài toán cung-cầu đã tính: 2 gậy + 3 đá cho công cụ đầu, cơ chế đá tự sinh dự phòng...).
- Kiến trúc code dễ mở rộng: thêm vật phẩm/công thức/công cụ mới rất nhanh.
- Mọi thông số cân bằng tập trung một chỗ.

**Điểm chưa tốt (phải thành thật, người chấm đánh giá cao sự trung thực):**
- Chưa có hệ thống lưu game.
- Chưa có chu kỳ ngày/đêm, thời tiết.
- AI của thỏ còn đơn giản (đi theo 4 hướng cố định, không tìm đường).
- Không có âm thanh / âm nhạc (nếu đúng vậy).
- Chưa kiểm thử với người chơi thật (chỉ tự kiểm thử).
- Đồ họa phụ thuộc asset có sẵn.

---

# CHƯƠNG 7 - CONCLUSIONS (3-6 trang)

## 7.1 What you have learned (1-2 trang)

Chia theo nhóm cho dễ đọc:

**Kỹ năng kỹ thuật:**
- Unity chuyên sâu: vòng đời script và tại sao thứ tự Awake/Start lại quan trọng, hệ thống prefab, ScriptableObject, hệ thống sự kiện UI, raycast, post-processing.
- Kiến trúc phần mềm: khi nào nên dùng Singleton, cách thiết kế state machine, nguyên tắc "một đối tượng chỉ có một chủ sở hữu" khi nhiều script cùng tác động lên một UI.

**Kỹ năng gỡ lỗi (nên viết kỹ, đây là thứ trưởng thành nhất qua dự án này):**
- Học cách truy ngược nguyên nhân gốc thay vì chữa triệu chứng. Nên kể **1 ví dụ cụ thể** để minh họa, ví dụ lỗi "Tool Library phải mở một lần mới hoạt động" - thoạt nhìn là lỗi nút bấm, thực chất là do script tự vô hiệu hóa chính GameObject chứa nó nên `Awake()`/`Start()` không bao giờ chạy lại, cộng thêm một lỗi thứ hai độc lập về thứ tự `Awake()` giữa các script.
- Học cách đọc log, đặt log chẩn đoán đúng chỗ, và dọn log sau khi sửa xong.

**Kỹ năng thiết kế game:**
- Hiểu rằng cân bằng game là bài toán toán học (cung - cầu), không phải cảm tính.
- Hiểu tầm quan trọng của việc chống trạng thái bế tắc cho người chơi.

**Kỹ năng quản lý dự án:**
- Biết cắt giảm phạm vi đúng lúc (quyết định bỏ hẳn hệ thống trồng trọt để dồn sức hoàn thiện vòng lặp sinh tồn - đây là quyết định đúng và nên nói rõ).
- Biết nhận ra khi mô hình làm việc không hiệu quả và chủ động thay đổi (tách nhóm).
- Sử dụng version control một cách kỷ luật.

## 7.2 What is the result of this project (1-2 trang)
- Đối chiếu từng mục tiêu ở 1.2 với kết quả thực tế - **nên làm dạng bảng: Mục tiêu | Trạng thái (Đạt / Đạt một phần / Không đạt) | Ghi chú.**
- Tổng kết mức độ hoàn thành.
- Nêu rõ những gì đã hứa mà không làm được và lý do trung thực (hết thời gian, thay đổi ưu tiên).

## 7.3 Further development (1-2 trang)

Nên chia theo mức độ ưu tiên/khả thi:

**Ngắn hạn (hoàn thiện sản phẩm hiện có):**
- Hệ thống lưu/tải game.
- Âm thanh và nhạc nền.
- Cải thiện AI thỏ (tìm đường bằng NavMesh).
- Kiểm thử với người chơi thật và điều chỉnh cân bằng theo phản hồi.

**Trung hạn (mở rộng nội dung - chính là những thứ đã lược bỏ):**
- Chu kỳ ngày/đêm và hệ thống nhiệt độ.
- Hệ thống thời tiết (mưa ảnh hưởng lửa, ảnh hưởng khát).
- Hệ thống trồng trọt (nguồn thức ăn bền vững).
- Thêm sinh vật, thêm khu vực địa hình mới.
- Hệ thống xây dựng (nơi trú ẩn).

**Dài hạn:**
- Chế độ nhiều người chơi (co-op) - nêu rõ thách thức: phải thiết kế lại kiến trúc theo hướng đồng bộ mạng, Singleton hiện tại sẽ không còn phù hợp.
- Phát hành trên Steam: những việc cần làm (đóng gói, đồ họa hoàn thiện, thử nghiệm rộng, tối ưu hiệu năng, tài liệu pháp lý về giấy phép asset).

---

# REFERENCES (Harvard style)

- Cần chuẩn bị **tối thiểu 5 nguồn cho phần lý thuyết**, thực tế nên có 12-20 nguồn cho toàn bài.
- Các nhóm nguồn cần tìm:
  1. Sách/bài báo về thiết kế game (game design fundamentals, game economy).
  2. Bài báo/nghiên cứu về thể loại survival game hoặc động lực người chơi.
  3. Tài liệu chính thức của Unity (Unity Documentation, URP docs).
  4. Tài liệu về mẫu thiết kế phần mềm trong game.
  5. Nguồn về các phương pháp phát triển phần mềm (Agile Manifesto, sách về SDLC).
  6. Nguồn về các game tham chiếu (The Forest / Green Hell) - bài phân tích, review chuyên môn.
- **Lưu ý về Harvard:** trích dẫn trong bài dạng `(Tác giả, năm)`, danh sách cuối bài xếp theo bảng chữ cái tên tác giả. Nguồn web phải có ngày truy cập.

# PROJECT PROPOSAL
Chèn nguyên bản proposal đã được duyệt vào đây.

# APPENDIX 1 - Survey and results (tùy chọn)
Nếu không làm khảo sát thì bỏ qua, hoặc thay bằng kết quả tự kiểm thử.

# APPENDIX 2 (tùy chọn)
Gợi ý nội dung phù hợp: bảng đầy đủ toàn bộ thông số GameConfig, danh sách toàn bộ script kèm mô tả, lịch sử commit, bảng tính cân bằng tài nguyên chi tiết.

---

# THỨ TỰ VIẾT ĐỀ XUẤT (deadline 01/08)

Không viết theo thứ tự chương. Viết theo thứ tự dễ → khó, tận dụng tối đa những gì đã có sẵn:

| Ưu tiên | Phần | Lý do |
|---|---|---|
| 1 | Chương 3 (Technology & Tools) | Dễ nhất, chỉ mô tả công cụ đã dùng |
| 2 | Chương 6.2 + 6.3 (Screenshot + Code) | Nội dung đã có sẵn 100%, chỉ cần viết giải thích |
| 3 | Chương 1 | Đã có đủ dữ liệu (timeline, mục tiêu, kết quả) |
| 4 | Chương 4 | Cần thời gian vẽ sơ đồ - nên bắt đầu sớm, vẽ song song |
| 5 | Chương 6.1 + 6.4 | Phân tích thiết kế, cần suy nghĩ |
| 6 | Chương 7 | Viết sau cùng vì phải nhìn lại toàn bộ |
| 7 | Chương 2 + Chương 5 | Lý thuyết thuần, cần tra cứu nguồn - có thể làm song song bất cứ lúc nào |
| 8 | References, cover page, mục lục, rà soát định dạng | Bước cuối |

**Công cụ vẽ sơ đồ gợi ý:** draw.io (miễn phí), Lucidchart, hoặc PlantUML nếu muốn vẽ bằng code.
