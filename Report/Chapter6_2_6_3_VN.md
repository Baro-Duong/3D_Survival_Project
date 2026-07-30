# CHƯƠNG 6 — MỤC 6.2 VÀ 6.3
### Bản tiếng Việt (bản duyệt nội dung)

> **Ghi chú:**
> - Ngôi thứ ba, giọng bị động — thống nhất với Chương 3.
> - Trích dẫn chuẩn RMIT Harvard.
> - `📌 [ẢNH n]` là vị trí chèn ảnh chụp màn hình.
> - Các khối mã được rút gọn, chỉ giữ phần cốt lõi; phần lược bỏ đánh dấu bằng `// ...`.

---

# 6.2 Các tính năng của sản phẩm

Mục này trình bày các tính năng chính của WildBound thông qua hình ảnh chụp từ sản phẩm đang vận hành. Bảy tính năng được lựa chọn nhằm bao quát toàn bộ vòng lặp cốt lõi của trò chơi, từ màn hình khởi đầu, quá trình thu thập và chế tạo, cho tới các tình huống chiến đấu và kết thúc lượt chơi.

---

## 6.2.1 Màn hình chính

📌 **[ẢNH 1]** *Màn hình chính với tên trò chơi, nút Play và khung cảnh nền được làm mờ.*

Màn hình chính là điểm khởi đầu của trò chơi, gồm tên sản phẩm đặt ở trung tâm và nút Play phía dưới. Phần nền là khung cảnh thật của hòn đảo, được quan sát bằng một camera tự động xoay ngang quanh một điểm cố định, sau đó chuyển sang vị trí quan sát khác thông qua hiệu ứng chuyển cảnh mờ dần.

Toàn bộ khung cảnh nền được làm mờ bằng hiệu ứng Depth of Field. Mục đích của lựa chọn thiết kế này là tạo chiều sâu thị giác cho màn hình chính mà không làm phân tán sự chú ý khỏi các thành phần điều khiển. Do lớp giao diện được kết xuất ở chế độ Screen Space Overlay, nó nằm ngoài phạm vi tác động của các hiệu ứng hậu xử lý áp dụng lên camera, nhờ đó tên trò chơi và nút điều khiển vẫn giữ được độ sắc nét tuyệt đối trên nền mờ.

---

## 6.2.2 Giao diện trong lúc chơi

📌 **[ẢNH 2]** *Khung cảnh trong lúc chơi, hiển thị đầy đủ các thành phần giao diện và dòng chữ gợi ý tương tác.*

Giao diện trong lúc chơi được tổ chức theo nguyên tắc đặt toàn bộ thông tin ở vùng rìa màn hình, giữ cho phần trung tâm — nơi người chơi quan sát và thao tác — luôn thông thoáng.

Các thành phần bao gồm: ba thanh chỉ số sinh tồn (máu, khát, đói) kèm giá trị số cụ thể; thanh công cụ nhanh gồm tám ô ở cạnh dưới, trong đó ô đang được chọn được đánh dấu bằng viền sáng màu vàng; bộ đếm thời gian sống sót ở góc màn hình; và dòng chữ gợi ý tương tác xuất hiện ở trung tâm khi người chơi hướng tầm nhìn vào một vật thể có thể tương tác.

Việc hiển thị đồng thời cả thanh trạng thái lẫn giá trị số là một quyết định có chủ đích: thanh trạng thái cho phép nhận biết nhanh mức độ nguy hiểm bằng thị giác ngoại vi, trong khi giá trị số phục vụ việc ra quyết định chính xác, chẳng hạn khi cân nhắc có nên chạy nhanh hay không dựa trên lượng nước còn lại.

---

## 6.2.3 Túi đồ và bảng chế tạo

📌 **[ẢNH 3]** *Giao diện túi đồ đang mở, có vật phẩm xếp chồng hiển thị số lượng và một vật phẩm đang được kéo.*

Giao diện túi đồ được mở bằng phím E, gồm hai vùng chức năng. Vùng chứa đồ có tổng cộng 26 ô, trong đó tám ô đầu tiên đồng thời là các ô của thanh công cụ nhanh và luôn được ưu tiên lấp đầy trước. Vùng chế tạo gồm hai ô nguyên liệu đầu vào, một ô kết quả và nút thực hiện chế tạo.

Các vật phẩm cùng loại được xếp chồng trong một ô duy nhất, với số lượng hiển thị ở góc ô. Cùng vị trí hiển thị này được dùng lại để thể hiện độ bền còn lại đối với các công cụ, trong đó giá trị độ bền được ưu tiên hiển thị khi vật phẩm là công cụ.

Việc di chuyển vật phẩm giữa các ô được thực hiện bằng thao tác kéo thả. Nút chế tạo chỉ chuyển sang trạng thái khả dụng khi hai ô nguyên liệu chứa đúng chủng loại và đủ số lượng theo một công thức đã được định nghĩa.

---

## 6.2.4 Thư viện công thức

📌 **[ẢNH 4]** *Bảng thư viện công thức hiển thị các công thức kèm nguyên liệu và nút Choose.*

Thư viện công thức là bảng tra cứu liệt kê toàn bộ công thức chế tạo hiện có trong trò chơi, được phân trang với bốn công thức trên mỗi trang. Mỗi mục hiển thị biểu tượng của hai loại nguyên liệu kèm số lượng yêu cầu và biểu tượng của sản phẩm tạo thành.

Tính năng này được bổ sung nhằm giải quyết một vấn đề về trải nghiệm người dùng: nếu công thức chế tạo không được trình bày ở bất kỳ đâu trong trò chơi, người chơi buộc phải ghi nhớ hoặc thử nghiệm ngẫu nhiên, dẫn đến cảm giác bế tắc.

Mỗi công thức đi kèm nút Choose. Khi được kích hoạt, hệ thống tự động tìm đủ số lượng nguyên liệu cần thiết trong túi đồ, chuyển chúng vào hai ô nguyên liệu đầu vào và đóng bảng tra cứu. Điểm cần nhấn mạnh là thao tác này di chuyển chính các vật phẩm đang có trong túi chứ không sinh thêm vật phẩm mới; nếu không đủ nguyên liệu, thao tác sẽ không được thực hiện.

---

## 6.2.5 Chuỗi tương tác nấu nướng

📌 **[ẢNH 5]** *Người chơi đang cầm thịt sống và giữ phím F trước bếp lửa, hiển thị tiến trình nấu theo phần trăm.*

Chuỗi tương tác nấu nướng là hệ thống tương tác phức tạp nhất trong WildBound, đồng thời là hệ thống kết nối nhiều nguồn tài nguyên khác nhau của trò chơi.

Hệ thống hoạt động dựa trên tổ hợp giữa vật phẩm người chơi đang cầm và vật thể người chơi đang hướng tầm nhìn tới. Cùng một bếp lửa sẽ cho ra các tùy chọn tương tác khác nhau tùy theo vật phẩm trên tay: cầm nồi nước bẩn sẽ cho phép đun nước, cầm chai rỗng trước bếp đã đun xong sẽ cho phép múc nước, cầm thịt sống sẽ cho phép nấu, còn cầm gậy hoặc đá sẽ cho phép bổ sung độ bền cho bếp.

Riêng thao tác nấu thịt yêu cầu giữ phím F liên tục trong mười giây. Trong suốt thời gian đó, tiến trình được hiển thị theo phần trăm và sẽ bị đặt lại nếu người chơi thả phím hoặc quay đi. Cơ chế giữ phím kèm phản hồi tiến trình được lựa chọn thay cho thao tác bấm một lần nhằm tạo cảm giác nấu nướng cần thời gian, đồng thời buộc người chơi phải đứng yên tại chỗ — một khoảng thời gian dễ bị tổn thương có chủ đích trong thiết kế.

---

## 6.2.6 Chiến đấu và phản hồi khi nhận sát thương

📌 **[ẢNH 6]** *Thỏ đang truy đuổi người chơi, kèm hiệu ứng lóa đỏ trên toàn màn hình tại thời điểm người chơi nhận sát thương. Nếu bắt được khoảnh khắc thỏ đầu đàn tấn công thì nên dùng ảnh đó, vì thể hiện được cả hai loại sinh vật.*

Thỏ là sinh vật duy nhất trong WildBound, đồng thời vừa là nguồn thức ăn vừa là mối đe dọa. Trò chơi có hai biến thể của sinh vật này.

**Thỏ thường** di chuyển ngẫu nhiên theo bốn hướng và không chủ động tấn công. Tuy nhiên, ngay khi bị tấn công lần đầu, nó chuyển sang trạng thái hung dữ vĩnh viễn: quay về phía người chơi, truy đuổi với tốc độ cao hơn tốc độ di chuyển thông thường, và tấn công theo chu kỳ khi ở trong tầm. Thiết kế này biến việc săn bắt thành một quyết định có rủi ro thay vì một thao tác an toàn tuyệt đối: người chơi phải cân nhắc giữa lượng máu có thể mất và lượng thức ăn thu được.

**Thỏ đầu đàn** là biến thể hiếm, được phân biệt bằng kích thước lớn hơn và màu sắc đỏ hồng nổi bật. Nó có lượng máu, sát thương và tốc độ truy đuổi gấp đôi thỏ thường, đồng thời rơi ra gấp đôi lượng thịt khi bị hạ. Khác biệt quan trọng nhất nằm ở hành vi: thỏ đầu đàn **chủ động chuyển sang trạng thái hung dữ ngay khi người chơi tiến vào bán kính phát hiện của nó**, thay vì chờ bị tấn công trước. Bán kính này rộng hơn tầm tấn công thực tế, nghĩa là nó phát hiện người chơi từ xa rồi mới áp sát.

Tại mỗi thời điểm chỉ tồn tại tối đa một thỏ đầu đàn trên bản đồ. Sau khi bị hạ, hệ thống phải sinh ra ba lượt thỏ thường rồi mới sinh lại một con mới.

Sự phân biệt giữa hai biến thể tạo ra một cấp độ rủi ro thứ hai trong trò chơi. Thỏ thường là nguồn thức ăn an toàn nếu người chơi chấp nhận mất một lượng máu nhỏ, trong khi thỏ đầu đàn là mục tiêu có phần thưởng cao nhưng buộc người chơi phải chuẩn bị trước — cả về lượng máu hiện có lẫn công cụ đang cầm. Việc màu sắc và kích thước của nó khác biệt rõ rệt là một quyết định có chủ đích, nhằm giúp người chơi nhận ra mối nguy từ xa và tự quyết định giao chiến hay tránh né.

Khi người chơi nhận sát thương — dù từ vết cắn của thỏ hay do cạn kiệt nước và thức ăn — toàn bộ màn hình sẽ lóa đỏ trong thời gian ngắn rồi mờ dần. Cơ chế phản hồi này được bổ sung do quan sát thấy rằng nếu chỉ dựa vào thanh máu, người chơi rất dễ bỏ sót việc mình đang mất máu trong lúc tập trung quan sát môi trường xung quanh.

---

## 6.2.7 Màn hình kết thúc

📌 **[ẢNH 7]** *Màn hình kết thúc với nền đỏ, dòng chữ Game Over, thời gian sống sót và hai nút điều khiển.*

Khi lượng máu của người chơi giảm về không, màn hình kết thúc được kích hoạt. Một lớp nền màu đỏ hiện dần lên trong khoảng hai giây thay vì xuất hiện tức thì, nhằm tạo cảm giác chuyển tiếp mềm mại hơn.

Đồng thời, toàn bộ các thành phần điều khiển của người chơi — di chuyển, xoay camera, tấn công, chọn ô công cụ và tương tác — đều bị vô hiệu hóa, và con trỏ chuột được giải phóng để người chơi có thể thao tác với các nút trên màn hình.

Thời gian sống sót của lượt chơi được đóng băng và hiển thị ngay dưới dòng chữ kết thúc, trong khi bộ đếm thời gian trên giao diện chính được ẩn đi. Đây là chỉ số thành tích duy nhất của trò chơi: do WildBound không có điều kiện chiến thắng, thời gian sống sót đóng vai trò thước đo hiệu quả của người chơi và là động lực để thực hiện lượt chơi tiếp theo. Hai nút điều khiển cho phép bắt đầu lại lượt chơi mới hoặc quay về màn hình chính.

---

# 6.3 Hiện thực sản phẩm

Mục này phân tích bảy đoạn mã tiêu biểu trong WildBound. Các đoạn mã được lựa chọn theo nguyên tắc mỗi đoạn minh họa một kỹ thuật khác biệt, nhằm phản ánh phạm vi các vấn đề kỹ thuật đã được xử lý trong quá trình phát triển. Toàn bộ mã được rút gọn để giữ lại phần cốt lõi; các phần lược bỏ được đánh dấu bằng `// ...`.

---

## 6.3.1 Mẫu thay thế đối tượng có bảo toàn trạng thái

**Vấn đề cần giải quyết.** Bếp lửa trong WildBound có ba trạng thái với hình dạng khác nhau: bếp thường, bếp đang đun và bếp đã đun xong. Tương tự, bụi cây có hai trạng thái: còn quả và đã bị hái. Yêu cầu đặt ra là khi chuyển trạng thái, mô hình ba chiều phải thay đổi tương ứng, nhưng toàn bộ dữ liệu tiến trình — độ bền còn lại, số lần đã múc nước, thời gian hồi sinh — phải được giữ nguyên.

```csharp
 1  private void SpawnReplacement(GameObject prefab)
 2  {
 3      if (prefab == null) { Debug.LogError("Prefab is null in FirePitManager!"); return; }
 4      GameObject replacement = Instantiate(prefab, transform.position, transform.rotation);
 5
 6      FirePitManager newFP = replacement.GetComponent<FirePitManager>();
 7      if (newFP != null)
 8      {
 9          newFP.state      = state;        // chuyển giao trạng thái hiện tại
10          newFP.scoopCount = scoopCount;   // chuyển giao bộ đếm số lần múc
11          newFP.config     = config;
12          newFP.uses       = uses;         // chuyển giao độ bền còn lại
13      }
14      else
15      {
16          Debug.LogError(prefab.name + " is missing a FirePitManager component");
17      }
18      Destroy(gameObject);
19  }
```

**Giải thích kỹ thuật.** Phương thức này hiện thực mẫu máy trạng thái (state machine) kết hợp với việc hoán đổi prefab (Nystrom 2014). Dòng 4 tạo ra thực thể mới tại đúng vị trí và góc quay của thực thể cũ. Các dòng 9–12 thực hiện việc chuyển giao dữ liệu sang thực thể mới. Dòng 18 hủy thực thể cũ.

**Cơ sở của thiết kế.** Phương án thay thế là giữ nguyên một đối tượng duy nhất và chỉ hoán đổi thành phần hiển thị. Tuy nhiên, các trạng thái của bếp lửa khác nhau không chỉ ở mô hình mà còn ở hiệu ứng hạt, vị trí điểm va chạm và cấu trúc đối tượng con, khiến việc hoán đổi từng phần trở nên phức tạp và dễ phát sinh lỗi hơn so với việc thay thế toàn bộ.

Điểm đáng chú ý nhất là cấu trúc của khối lệnh từ dòng 6 đến 17. Trong phiên bản đầu tiên, nhánh `else` ở dòng 14 không tồn tại, trong khi lệnh `Destroy` ở dòng 18 vẫn luôn được thực thi. Hậu quả là khi prefab đích thiếu thành phần `FirePitManager`, việc chuyển giao dữ liệu bị bỏ qua trong im lặng nhưng đối tượng cũ vẫn bị hủy, để lại một đối tượng thay thế không còn khả năng tương tác. Lỗi này đã xảy ra hai lần trong dự án trước khi thông báo lỗi ở dòng 16 được bổ sung. Bài học rút ra là mọi lời gọi `GetComponent` trên một đối tượng vừa được tạo đều cần có nhánh xử lý khi kết quả trả về rỗng.

Cần lưu ý thêm rằng đoạn mã trên **không** sao chép các tham chiếu prefab sang thực thể mới. Toàn bộ tham chiếu prefab được lưu tập trung tại một đối tượng quản lý duy nhất trong scene. Cách tổ chức này loại bỏ hoàn toàn rủi ro một thực thể vô tình tham chiếu tới chính đối tượng sắp bị hủy — một lỗi đã từng phát sinh và được trình bày tại mục 3.1.4.

Cùng một cấu trúc mã này được áp dụng lại cho hệ thống bụi cây, chỉ khác về kiểu dữ liệu và tập biến được chuyển giao. Việc một mẫu thiết kế phục vụ được hai hệ thống có bản chất khác nhau cho thấy tính khái quát của giải pháp.

---

## 6.3.2 Thuật toán so khớp công thức chế tạo

**Vấn đề cần giải quyết.** Hệ thống chế tạo phải xác định công thức tương ứng với hai nguyên liệu người chơi đặt vào. Vấn đề phát sinh khi nhiều công thức cùng sử dụng một cặp nguyên liệu nhưng khác nhau về số lượng: rìu yêu cầu một gậy và hai đá, trong khi bếp lửa yêu cầu bốn gậy và ba đá. Thuật toán chọn công thức khớp đầu tiên luôn trả về rìu, kể cả khi người chơi đã đặt đủ nguyên liệu cho bếp lửa.

```csharp
 1  public void CheckRecipe()
 2  {
 3      string item1 = input1Slot.ItemName;
 4      string item2 = input2Slot.ItemName;
 5      matchedRecipe   = null;
 6      int bestSpecificity = -1;   // tổng nguyên liệu của công thức khớp tốt nhất
 7
 8      foreach (CraftingRecipe recipe in allRecipes)
 9      {
10          bool straightMatch = recipe.input1Name == item1 && recipe.input2Name == item2
11              && HasEnough(input1Slot, recipe.input1Count)
12              && HasEnough(input2Slot, recipe.input2Count);
13
14          // Thứ tự đặt nguyên liệu không ảnh hưởng tới kết quả
15          bool swappedMatch = !straightMatch
16              && recipe.input1Name == item2 && recipe.input2Name == item1
17              && HasEnough(input1Slot, recipe.input2Count)
18              && HasEnough(input2Slot, recipe.input1Count);
19
20          if (!straightMatch && !swappedMatch) continue;
21
22          int specificity = recipe.input1Count + recipe.input2Count;
23          if (specificity > bestSpecificity)   // ưu tiên công thức đòi hỏi nhiều nguyên liệu hơn
24          {
25              bestSpecificity = specificity;
26              matchedRecipe   = recipe;
27              inputsSwapped   = swappedMatch;
28          }
29      }
30      craftButton.interactable = (matchedRecipe != null);
31  }
```

**Giải thích kỹ thuật.** Thay vì dừng ở công thức khớp đầu tiên, thuật toán duyệt toàn bộ danh sách và ghi nhận công thức tốt nhất theo một tiêu chí định lượng. Tiêu chí này được đặt tên là *độ đặc hiệu* (specificity), tính bằng tổng số nguyên liệu mà công thức yêu cầu (dòng 22). Công thức có độ đặc hiệu cao hơn được ưu tiên (dòng 23).

Các dòng 10–18 xử lý một yêu cầu khác về trải nghiệm: người chơi không cần quan tâm tới thứ tự đặt nguyên liệu. Mỗi công thức được kiểm tra theo cả hai chiều, và biến `inputsSwapped` ghi nhớ chiều nào đã khớp để giai đoạn trừ nguyên liệu sau đó trừ đúng số lượng từ đúng ô.

**Cơ sở của thiết kế.** Một phương án khác là buộc mỗi công thức phải có cặp nguyên liệu duy nhất, tức không cho phép hai công thức dùng chung nguyên liệu. Phương án này giải quyết được xung đột nhưng lại hạn chế nghiêm trọng không gian thiết kế của trò chơi, bởi trong một trò chơi sinh tồn với số loại tài nguyên hữu hạn, việc nhiều công cụ cùng được chế tạo từ gỗ và đá là hoàn toàn tự nhiên. Giải pháp dựa trên độ đặc hiệu giữ được tính linh hoạt đó, đồng thời phù hợp với trực giác của người chơi: khi đã bỏ ra nhiều nguyên liệu hơn, người chơi mong đợi nhận được sản phẩm tương xứng.

---

## 6.3.3 Vòng lặp mô phỏng sinh tồn

**Vấn đề cần giải quyết.** Hệ thống chỉ số sinh tồn phải mô phỏng mối quan hệ giữa ba chỉ số máu, khát và đói theo thời gian thực, đồng thời tạo ra áp lực tài nguyên liên tục lên người chơi.

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
10      if (isRegenerating) thirstDrain += config.thirstDrainRegenBonus;  // hồi máu có chi phí
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

**Giải thích kỹ thuật.** Mọi phép biến đổi chỉ số đều được nhân với `Time.deltaTime` — khoảng thời gian thực đã trôi qua kể từ khung hình trước. Nhờ đó tốc độ tiêu hao chỉ số là như nhau trên mọi cấu hình máy, bất kể số khung hình mỗi giây đạt được. Nếu bỏ qua phép nhân này, người chơi trên máy cấu hình cao sẽ mất nước nhanh hơn nhiều lần so với máy cấu hình thấp.

Các hàm `Mathf.Max` và `Mathf.Min` được dùng để giới hạn chỉ số trong khoảng hợp lệ, ngăn giá trị âm hoặc vượt quá mức tối đa.

**Cơ sở của thiết kế.** Điểm cốt lõi về mặt thiết kế trò chơi nằm ở biến `isRegenerating` và cách nó được sử dụng ở các dòng 10 và 14. Việc hồi máu tự động chỉ diễn ra khi cả nước lẫn thức ăn đều trên ngưỡng năm mươi phần trăm, và quan trọng hơn, quá trình hồi máu làm tăng thêm tốc độ tiêu hao của chính hai chỉ số đó.

Thiết kế này biến việc hồi máu thành một sự đánh đổi thay vì một cơ chế miễn phí. Người chơi bị thương nặng buộc phải lựa chọn giữa việc dừng lại để hồi phục — đồng nghĩa với tiêu tốn nhanh hơn lượng nước và thức ăn dự trữ — hoặc tiếp tục hoạt động với lượng máu thấp. Nếu hồi máu diễn ra vô điều kiện, toàn bộ áp lực tài nguyên của trò chơi sẽ bị vô hiệu hóa, vì người chơi chỉ cần đứng chờ là mọi thiệt hại đều được xóa bỏ.

Các dòng 17 và 18 hiện thực hình phạt khi chỉ số cạn kiệt, với mức độ khác nhau: hết nước gây thiệt hại gấp ba lần hết thức ăn. Sự chênh lệch này phản ánh mức độ cấp thiết khác nhau của hai nhu cầu, đồng thời định hướng thứ tự ưu tiên của người chơi khi phải lựa chọn nguồn tài nguyên để tìm kiếm.

---

## 6.3.4 Kiến trúc Singleton và cấu hình hướng dữ liệu

**Vấn đề cần giải quyết.** Nhiều hệ thống trong trò chơi cần truy cập lẫn nhau: hệ thống tương tác phải thêm vật phẩm vào túi đồ, hệ thống chiến đấu phải trừ máu người chơi. Nếu mỗi lớp đều phải giữ tham chiếu trực tiếp tới các lớp còn lại, số lượng tham chiếu cần gán thủ công trong trình soạn thảo sẽ tăng rất nhanh.

```csharp
 1  public class InventorySystem : MonoBehaviour
 2  {
 3      public static InventorySystem Instance { get; set; }
 4
 5      private void Awake()
 6      {
 7          if (Instance != null && Instance != this)
 8              Destroy(gameObject);   // hủy bản trùng, chỉ giữ một thể hiện duy nhất
 9          else
10              Instance = this;
11      }
12      // ...
13  }
```

Song song với đó, toàn bộ thông số cân bằng được tách khỏi mã nguồn và lưu trong một tài nguyên cấu hình:

```csharp
 1  [CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
 2  public class GameConfig : ScriptableObject
 3  {
 4      [Header("Player Stats - HP Regen")]
 5      public float hpRegenRate      = 5f;
 6      public float hpRegenThreshold = 50f;   // Khát VÀ Đói đều phải trên ngưỡng này
 7
 8      [Header("FirePit Durability")]
 9      public int   firePitMaxUses     = 50;
10      public int   firePitBoilUseCost = 10;
11      public int   stickRepairUses    = 2;
12      public int   rockRepairUses     = 5;
13      // ... khoảng bốn mươi thông số khác
14  }
```

**Giải thích kỹ thuật.** Mẫu Singleton đảm bảo một lớp chỉ tồn tại duy nhất một thể hiện và cung cấp một điểm truy cập toàn cục tới thể hiện đó (Gamma et al. 1994). Thuộc tính tĩnh ở dòng 3 đóng vai trò điểm truy cập, còn khối lệnh trong `Awake()` đảm bảo tính duy nhất bằng cách hủy mọi bản trùng lặp. Nhờ đó, bất kỳ lớp nào trong dự án cũng có thể gọi tới hệ thống túi đồ mà không cần khai báo tham chiếu.

Tài nguyên `GameConfig` hiện thực nguyên tắc thiết kế hướng dữ liệu. Các thuộc tính công khai của nó xuất hiện trực tiếp trong trình soạn thảo, cho phép điều chỉnh mọi thông số cân bằng mà không cần biên dịch lại mã nguồn.

**Cơ sở của thiết kế và hạn chế.** Mẫu Singleton bị phê phán trong nhiều tài liệu về kiến trúc phần mềm, chủ yếu vì ba lý do: nó tạo ra trạng thái toàn cục, nó che giấu các quan hệ phụ thuộc giữa các lớp khiến việc đọc mã trở nên khó khăn hơn, và nó gây trở ngại cho việc kiểm thử tự động (Nystrom 2014).

Mẫu này vẫn được lựa chọn cho WildBound do đặc thù của dự án: trò chơi chỉ có một người chơi, mỗi hệ thống về bản chất chỉ tồn tại một thể hiện duy nhất, và quy mô mã nguồn ở mức ba mươi mốt tệp vẫn nằm trong khả năng nắm bắt của một cá nhân. Trong bối cảnh đó, chi phí của việc xây dựng một hệ thống tiêm phụ thuộc đầy đủ lớn hơn lợi ích thu được.

Tuy nhiên, cần ghi nhận rằng đây là một hạn chế thực sự nếu dự án được mở rộng. Trong trường hợp phát triển chế độ nhiều người chơi, giả định về tính duy nhất của mỗi hệ thống sẽ không còn đúng, và phần lớn kiến trúc hiện tại sẽ phải được thiết kế lại. Vấn đề này được thảo luận thêm tại mục 7.3.

---

## 6.3.5 Chuỗi tương tác phụ thuộc ngữ cảnh

**Vấn đề cần giải quyết.** Cùng một vật thể trong thế giới trò chơi phải cho ra các hành vi khác nhau tùy theo vật phẩm người chơi đang cầm. Bếp lửa có tới sáu tương tác khác nhau, và tất cả phải được xử lý mà không khiến mã nguồn trở nên rối rắm.

```csharp
 1  private void Update()
 2  {
 3      if (InventorySystem.Instance.isOpen) return;
 4
 5      string heldItem = GetHeldItemName();      // vật phẩm đang cầm
 6      RaycastHit hit;
 7      bool hasHit = Physics.Raycast(playerCamera.position, playerCamera.forward,
 8                                    out hit, interactRange);
 9
10      HandleInteractionText(heldItem, hasHit, hit);   // quyết định HIỂN THỊ gì
11      HandleInput(heldItem, hasHit, hit);             // quyết định XỬ LÝ gì
12  }
13
14  private void HandleInput(string heldItem, bool hasHit, RaycastHit hit)
15  {
16      if (!hasHit) { isCooking = false; cookHoldTime = 0f; return; }
17      string hitTag = hit.collider.tag;
18
19      // Nồi + Nước  ->  Nồi nước bẩn
20      if (heldItem == "Pot" && hitTag == "Water" && Input.GetKeyDown(KeyCode.Mouse0))
21      { ReplaceHeldItem("Pot", "DirtyWaterPot"); return; }
22
23      // Thịt sống + Bếp  ->  giữ F trong 10 giây  ->  Thịt chín
24      if (heldItem == "RawMeat" && hitTag == "FirePit")
25      {
26          FirePitManager fp = hit.collider.GetComponent<FirePitManager>();
27          if (fp == null || fp.uses <= 0)      // bếp đã hỏng thì không nấu được
28          { isCooking = false; cookHoldTime = 0f; return; }
29
30          if (Input.GetKey(KeyCode.F))
31          {
32              isCooking = true;
33              cookHoldTime += Time.deltaTime;
34              if (cookHoldTime >= config.cookRequiredTime)
35              {
36                  ConsumeOneAndAdd("CookedMeat");
37                  fp.ConsumeCookUse();          // trừ độ bền của bếp
38                  isCooking = false; cookHoldTime = 0f;
39              }
40          }
41          else { isCooking = false; cookHoldTime = 0f; }   // thả phím thì đặt lại tiến trình
42          return;
43      }
44      // ... bốn tổ hợp tương tác khác
45  }
```

**Giải thích kỹ thuật.** Mỗi khung hình, một tia được phóng từ camera theo hướng nhìn (dòng 7) để xác định vật thể người chơi đang hướng tới. Hành vi được quyết định bởi tổ hợp giữa tên vật phẩm đang cầm và thẻ (tag) của vật thể bị tia chạm tới.

Điểm đáng chú ý về mặt cấu trúc là việc tách thành hai phương thức riêng biệt ở các dòng 10 và 11. `HandleInteractionText` chỉ quyết định hiển thị dòng chữ gợi ý nào, còn `HandleInput` chỉ xử lý thao tác bấm phím. Việc phân tách này cần thiết vì hai nhiệm vụ có điều kiện kích hoạt khác nhau: dòng chữ gợi ý phải hiển thị liên tục khi người chơi nhìn vào vật thể, trong khi hành động chỉ được thực thi tại thời điểm bấm phím.

Cơ chế giữ phím được hiện thực ở các dòng 30–41: biến tích lũy `cookHoldTime` tăng dần theo thời gian thực và bị đặt lại về không ngay khi người chơi thả phím hoặc quay đi.

**Cơ sở của thiết kế.** Một bài học kiến trúc quan trọng đã được rút ra trong quá trình xây dựng hệ thống này. Ở phiên bản đầu, cả lớp này lẫn lớp quản lý hiển thị tên vật thể đều trực tiếp bật, tắt và ghi nội dung lên **cùng một** đối tượng giao diện, trong khi Unity không đảm bảo thứ tự thực thi giữa các lớp trong cùng một khung hình. Hậu quả là hai lớp liên tục ghi đè lên kết quả của nhau, dẫn đến hiện tượng dòng chữ gợi ý biến mất một cách không thể dự đoán trước.

Vấn đề được xử lý bằng cách xác lập nguyên tắc **một đối tượng giao diện chỉ có một lớp sở hữu**. Lớp sở hữu là lớp duy nhất được phép thay đổi trạng thái hiển thị của đối tượng đó; các lớp khác chỉ được phép bật một biến cờ, và lớp sở hữu sẽ đọc biến cờ này để tự quyết định. Nguyên tắc này loại bỏ hoàn toàn sự phụ thuộc vào thứ tự thực thi.

---

## 6.3.6 Tái sử dụng logic cho nhiều loại công cụ

**Vấn đề cần giải quyết.** Rìu và cuốc chim có cùng sát thương khi tấn công sinh vật và cùng cơ chế hao mòn độ bền, nhưng khác nhau về đối tượng chúng có thể tác động: rìu chặt được cây, cuốc đào được đá. Yêu cầu là xử lý điểm chung mà không viết trùng lặp mã nguồn.

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
13          bool holdingTool    = holdingAxe || holdingPickaxe;   // gộp chung hai công cụ
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
24          if (tree != null && holdingAxe)          // chỉ rìu mới chặt được cây
25          { tree.Chop(); ConsumeToolDurability(); return; }
26
27          BigRock bigRock = hit.collider.GetComponentInParent<BigRock>();
28          if (bigRock != null && holdingPickaxe)   // chỉ cuốc mới đào được đá
29          { bigRock.Mine(); ConsumeToolDurability(); return; }
30
31          Bush bush = hit.collider.GetComponentInParent<Bush>();
32          if (bush != null) { bush.TryHarvest(); return; }   // hái bụi không cần công cụ
33      }
34  }
```

**Giải thích kỹ thuật.** Dòng 13 gộp hai loại công cụ vào một biến luận lý chung. Nhờ đó, phần xử lý sát thương ở dòng 18 và phần hao mòn độ bền ở dòng 19 chỉ được viết một lần và áp dụng cho cả hai công cụ. Sự khác biệt giữa chúng chỉ xuất hiện tại các dòng 24 và 28, nơi từng công cụ được gắn với đối tượng tương ứng.

Các dòng 3 và 4 hiện thực khoảng thời gian chờ giữa hai lần tấn công, ngăn người chơi gây sát thương liên tục bằng cách bấm chuột nhanh.

Phương thức `GetComponentInParent` được dùng thay cho `GetComponent` ở các dòng 15, 23, 27 và 31. Nguyên nhân là các thành phần va chạm của những đối tượng này nằm trên đối tượng con chứ không nằm trên đối tượng gốc mang mã điều khiển; nếu chỉ tìm trên chính đối tượng bị tia chạm tới, kết quả sẽ luôn rỗng.

**Cơ sở của thiết kế.** Cấu trúc này thể hiện nguyên tắc tránh lặp lại mã nguồn. Giá trị của nó được kiểm chứng trực tiếp trong quá trình phát triển: cuốc chim được bổ sung ở giai đoạn cuối dự án, và việc tích hợp chỉ đòi hỏi thêm một biến luận lý cùng một nhánh điều kiện, trong khi toàn bộ phần sát thương và độ bền hoạt động ngay mà không cần sửa đổi.

Thứ tự kiểm tra từ dòng 15 đến dòng 31 cũng là một quyết định có chủ đích. Sinh vật được kiểm tra trước tiên vì đây là đối tượng có thể di chuyển và thường nằm chồng lên các vật thể tĩnh trong tầm nhìn; nếu cây hoặc đá được kiểm tra trước, người chơi có thể vô tình chặt cây trong lúc đang cố tấn công một con thỏ đứng cạnh gốc cây đó.

---

## 6.3.7 Điều khiển chuyển cảnh bằng máy trạng thái

**Vấn đề cần giải quyết.** Màn hình chính cần một camera tự động: đứng yên tại một vị trí và xoay ngang để phô diễn khung cảnh, sau đó chuyển sang vị trí quan sát khác và lặp lại vô hạn. Quá trình chuyển vị trí phải được che giấu để người xem không nhìn thấy hiện tượng nhảy hình đột ngột.

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
19                  MoveToWaypoint(currentIndex);   // đổi vị trí trong lúc màn hình đang tối
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

**Giải thích kỹ thuật.** Toàn bộ hành vi được tổ chức thành ba trạng thái tuần hoàn khai báo ở dòng 1. Một biến đếm thời gian duy nhất điều khiển cả ba trạng thái; mỗi lần chuyển trạng thái, biến này được đặt lại về không.

Dòng 10 sử dụng `Quaternion.Slerp` thay vì `Vector3.Lerp`. Phép nội suy cầu này được thiết kế riêng cho phép quay và đảm bảo camera xoay theo cung ngắn nhất với tốc độ góc đều, tránh hiện tượng biến dạng có thể xảy ra khi nội suy tuyến tính trực tiếp trên các góc Euler.

Dòng 9 áp dụng `Mathf.SmoothStep` lên hệ số nội suy. Hàm này biến đổi tiến trình tuyến tính thành đường cong chữ S, khiến chuyển động camera bắt đầu chậm, nhanh dần ở giữa rồi chậm lại ở cuối. Kết quả là chuyển động mang cảm giác điện ảnh thay vì cảm giác máy móc của tốc độ xoay không đổi.

Dòng 19 là điểm mấu chốt của toàn bộ cơ chế: việc dịch chuyển camera sang vị trí mới được thực hiện đúng vào thời điểm màn hình đã tối hoàn toàn. Nhờ đó, hiện tượng nhảy hình được che giấu hoàn toàn khỏi người xem.

**Cơ sở của thiết kế.** Phương án thay thế là di chuyển camera một cách liên tục giữa các điểm quan sát. Phương án này đã bị loại bỏ vì đường đi thẳng giữa hai điểm bất kỳ có thể xuyên qua địa hình hoặc vật thể, tạo ra hình ảnh lỗi mà việc khắc phục đòi hỏi phải xây dựng thêm hệ thống tìm đường cho camera — một khối lượng công việc không tương xứng với giá trị thu được ở màn hình chính.

Về mặt hiện thực, cơ chế đếm thời gian trong `Update()` được lựa chọn thay cho Coroutine của Unity. Cả hai đều khả thi về mặt kỹ thuật, nhưng phương án đầu giữ được tính nhất quán với toàn bộ phần còn lại của dự án, nơi mọi tiến trình theo thời gian — thời gian đun nước, thời gian hồi sinh của bụi cây, chu kỳ sinh sinh vật — đều được hiện thực bằng cùng một cách. Sự nhất quán này giúp giảm chi phí nhận thức khi đọc lại mã nguồn.

---

# TÀI LIỆU THAM KHẢO BỔ SUNG CHO MỤC 6.3

> Các nguồn dưới đây bổ sung cho danh sách đã có ở Chương 3.

Gamma E, Helm R, Johnson R and Vlissides J (1994) *Design patterns: elements of reusable object-oriented software*, Addison-Wesley, Boston.

Nystrom R (2014) *Game programming patterns*, Genever Benning, Game Programming Patterns website, accessed 29 July 2026. https://gameprogrammingpatterns.com/
