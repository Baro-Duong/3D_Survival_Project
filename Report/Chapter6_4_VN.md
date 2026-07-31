# MỤC 6.4 — ĐÁNH GIÁ SẢN PHẨM
### Bản tiếng Việt (bản duyệt nội dung)

> **Ghi chú:**
> - Ngôi thứ ba, giọng bị động — thống nhất với 6.2 và 6.3.
> - Trích dẫn chuẩn RMIT Harvard.
> - Số liệu công thức chế tạo trong mục 6.4.3 được đọc trực tiếp từ ba tài nguyên công thức của dự án, đã đối chiếu bằng hai phương pháp độc lập.
> - ⚠️ Bảng ở 6.4.3 có ba ô đánh dấu `[XÁC NHẬN]` — cần mở GameConfig trong Unity đọc giá trị thật rồi điền, vì tệp cấu hình lưu ở dạng nhị phân nên không đọc được từ bên ngoài.

---

## 6.4 Đánh giá sản phẩm

### 6.4.1 Phương pháp và tiêu chí đánh giá

Trước khi trình bày kết quả, cần nêu rõ phương pháp đã được sử dụng cùng giới hạn của nó. Toàn bộ quá trình kiểm thử WildBound được thực hiện bởi chính người phát triển, không có sự tham gia của người chơi bên ngoài. Điều này đồng nghĩa với việc mọi nhận định về độ khó, nhịp độ và mức độ dễ hiểu của trò chơi đều xuất phát từ góc nhìn của người đã nắm rõ mọi cơ chế bên trong — một góc nhìn khác biệt căn bản so với người chơi lần đầu. Trong tài liệu về thiết kế trò chơi, việc kiểm thử với người chơi thật được xem là công đoạn không thể thay thế, bởi nhà phát triển không còn khả năng trải nghiệm sản phẩm của mình như một người mới (Fullerton 2018).

Do hạn chế này, các nội dung đánh giá dưới đây được phân thành hai nhóm có mức độ tin cậy khác nhau:

- **Nhóm kiểm chứng khách quan được**: những nhận định có thể xác minh bằng số liệu đo đếm, bằng cấu trúc mã nguồn, hoặc bằng chính quá trình phát triển đã diễn ra. Nhóm này bao gồm tính đầy đủ chức năng, tính cân đối của nền kinh tế tài nguyên và khả năng mở rộng của kiến trúc.
- **Nhóm mang tính chủ quan**: những nhận định về trải nghiệm người chơi, độ khó phù hợp và tính hấp dẫn. Nhóm này chỉ có giá trị tham khảo và cần được kiểm chứng bằng thử nghiệm với người chơi thật.

Bốn tiêu chí được sử dụng để đánh giá sản phẩm: mức độ hoàn thành chức năng so với mục tiêu đề ra, tính đúng đắn của thiết kế nền kinh tế tài nguyên, chất lượng kiến trúc mã nguồn xét theo khả năng mở rộng, và chất lượng phản hồi cho người chơi.

---

### 6.4.2 Mức độ hoàn thành chức năng

Đối chiếu với các mục tiêu cụ thể đặt ra tại Chương 1, kết quả đạt được như sau:

| Mục tiêu đề ra | Kết quả | Ghi chú |
|---|---|---|
| Hệ thống túi đồ và thanh công cụ nhanh có kéo thả, xếp chồng | Đạt | 8 ô thanh công cụ nhanh + 24 ô túi đồ |
| Hệ thống chế tạo theo công thức có định lượng | Đạt | Ba công thức, thuật toán so khớp theo độ đặc hiệu |
| Hệ thống chỉ số sinh tồn tương tác lẫn nhau | Đạt | Máu, khát, đói cùng cơ chế hồi phục có điều kiện |
| Chuỗi tương tác nhiều bước với môi trường | Đạt | Sáu tương tác khác nhau với bếp lửa |
| Thu thập tài nguyên có công cụ chuyên biệt kèm độ bền | Đạt | Rìu và cuốc chim, mỗi loại có đối tượng riêng |
| Trí tuệ nhân tạo của sinh vật | Đạt một phần | Có hành vi lang thang và truy đuổi, chưa có tìm đường |
| Nền kinh tế tài nguyên khép kín, chống bế tắc | Đạt | Xem phân tích tại 6.4.3 |
| Giao diện đầy đủ: màn hình chính, HUD, màn hình kết thúc | Đạt | Bổ sung thêm biến thể sinh vật cấp cao ngoài dự kiến |

Vòng lặp cốt lõi của trò chơi hoàn chỉnh và chơi được trọn vẹn: người chơi khởi động từ màn hình chính, thu thập tài nguyên, chế tạo công cụ, chế biến thức ăn và nước uống, chiến đấu với sinh vật, rồi kết thúc lượt chơi với một chỉ số thành tích rõ ràng. Không có mắt xích nào trong chuỗi này bị bỏ dở hay phải giả lập bằng dữ liệu tạm.

Cần lưu ý rằng ba hạng mục vượt ngoài phạm vi dự kiến ban đầu cũng đã được hoàn thành: hệ thống khai thác đá bằng cuốc chim, biến thể sinh vật cấp cao với hành vi chủ động phát hiện người chơi, và lớp phủ hướng dẫn phân trang trên màn hình chính.

---

### 6.4.3 Tính đúng đắn của nền kinh tế tài nguyên

Đây là hạng mục có thể kiểm chứng khách quan nhất, bởi nó được quyết định hoàn toàn bởi các con số cấu hình chứ không phụ thuộc cảm nhận.

**Cấu trúc nguồn cung.** Mỗi loại tài nguyên trong WildBound đều có ít nhất hai nguồn cung độc lập, nhằm tránh trường hợp mất một nguồn dẫn tới bế tắc toàn bộ:

| Tài nguyên | Nguồn cung 1 | Nguồn cung 2 | Cơ chế bổ sung |
|---|---|---|---|
| Gậy | Hái bụi cây (kèm quả) | Chặt cây bằng rìu | Bụi cây tự hồi sinh theo chu kỳ |
| Đá | Nhặt trên bản đồ | Khai thác tảng đá lớn bằng cuốc | Tảng đá tự sinh đá theo chu kỳ |
| Thịt | Săn thỏ thường | Săn thỏ đầu đàn (rơi gấp đôi) | Hang thỏ sinh thỏ định kỳ |
| Quả | Hái bụi cây | — | Bụi cây tự hồi sinh theo chu kỳ |
| Nước sạch | Đun nước bẩn tại bếp lửa | — | Phụ thuộc độ bền bếp |

**Bài toán chi phí công cụ ban đầu.** Ba công thức chế tạo hiện có yêu cầu:

| Công thức | Gậy | Đá | Tổng nguyên liệu |
|---|---|---|---|
| Rìu | 1 | 1 | 2 |
| Cuốc chim | 1 | 2 | 3 |
| Bếp lửa | 5 | 4 | 9 |

Người chơi khởi đầu không có công cụ nào, do đó phải dựa vào hai nguồn không cần công cụ: hái bụi cây và nhặt đá trên bản đồ. Với chi phí hai nguyên liệu cho rìu và ba nguyên liệu cho cuốc, ngưỡng để có được bộ công cụ đầu tiên nằm trong tầm với ngay từ những phút đầu, tránh được tình trạng người chơi bị mắc kẹt ở giai đoạn khởi đầu. Ngược lại, bếp lửa với tổng chín nguyên liệu chỉ khả thi sau khi đã có công cụ, tạo thành một trình tự tiến triển tự nhiên: hái lượm thủ công → chế tạo công cụ → khai thác quy mô lớn → xây dựng bếp lửa → tiếp cận nguồn nước sạch và thức ăn nấu chín.

**Cơ chế chống bế tắc.** Rủi ro nghiêm trọng nhất của một hệ thống chế tạo có tiêu hao là người chơi dùng hết tài nguyên hữu hạn vào những lựa chọn sai và rơi vào trạng thái không thể phục hồi. WildBound xử lý rủi ro này bằng hai lưới an toàn hoạt động độc lập với hành động của người chơi:

- **Tảng đá lớn tự sinh ra một đơn vị đá theo chu kỳ cố định**, không phụ thuộc việc người chơi có khai thác hay không. Nghĩa là ngay cả khi toàn bộ đá đã bị tiêu hết và người chơi không còn cuốc để đào, nguồn đá vẫn tự phục hồi.
- **Bụi cây tự hồi sinh sau một khoảng thời gian cố định**, bảo đảm nguồn gậy và quả không bao giờ cạn vĩnh viễn.

Hai cơ chế này được thiết kế từ trước khi hiện thực, xuất phát từ việc tính toán cân đối cung–cầu, chứ không phải là bản vá được thêm vào sau khi phát sinh sự cố.

**Cơ chế tiêu thụ tài nguyên dư thừa.** Một vấn đề đối xứng với bế tắc là hiện tượng tài nguyên tích lũy vô ích: khi người chơi đã chế tạo đủ công cụ, lượng gậy và đá thu được về sau không còn công dụng. WildBound giải quyết bằng cách cho phép nạp gậy và đá vào bếp lửa để phục hồi độ bền, biến tài nguyên dư thành nguồn kéo dài tuổi thọ công trình.

| Vật liệu nạp | Độ bền phục hồi | Ghi chú |
|---|---|---|
| Gậy | [XÁC NHẬN] điểm | Nguồn dồi dào, giá trị phục hồi thấp |
| Đá | [XÁC NHẬN] điểm | Khan hiếm hơn, giá trị phục hồi cao hơn |
| Độ bền tối đa của bếp | [XÁC NHẬN] điểm | Giới hạn trần khi nạp |

Chênh lệch giá trị phục hồi giữa hai loại vật liệu phản ánh đúng mức độ khan hiếm tương đối của chúng, tạo cho người chơi một lựa chọn có ý nghĩa thay vì một thao tác máy móc.

---

### 6.4.4 Chất lượng kiến trúc mã nguồn

Khả năng mở rộng của kiến trúc không chỉ được khẳng định về mặt lý thuyết mà đã được kiểm chứng bằng chính quá trình phát triển. Hai tính năng được bổ sung ở giai đoạn muộn của dự án đóng vai trò như phép thử tự nhiên:

**Cuốc chim.** Đây là một công cụ hoàn chỉnh với mô hình ba chiều riêng, độ bền riêng, công thức chế tạo riêng và đối tượng khai thác riêng. Việc tích hợp phần logic chiến đấu và hao mòn chỉ đòi hỏi thêm một biến luận lý cùng một nhánh điều kiện trong lớp xử lý tấn công, như đã trình bày tại mục 6.3.6. Toàn bộ cơ chế sát thương và trừ độ bền hoạt động ngay mà không cần sửa đổi.

**Biến thể sinh vật cấp cao.** Thỏ đầu đàn có lượng máu, sát thương và tốc độ truy đuổi khác biệt, đồng thời có hành vi chủ động phát hiện người chơi mà thỏ thường không có. Biến thể này được xây dựng hoàn toàn bằng cách bổ sung một cờ điều khiển và một hệ số nhân vào lớp sẵn có, không cần tạo lớp kế thừa mới, không cần nhân bản mã nguồn.

**Tập trung hóa thông số cân bằng.** Hơn năm mươi thông số điều khiển hành vi trò chơi được lưu trong một tài nguyên cấu hình duy nhất, tách hoàn toàn khỏi mã nguồn xử lý logic. Việc điều chỉnh độ khó, nhịp độ tiêu hao tài nguyên hay sức mạnh sinh vật có thể thực hiện qua giao diện Unity mà không cần biên dịch lại. Đây là điều kiện tiên quyết cho bất kỳ quá trình cân bằng nghiêm túc nào về sau, và cũng là điều kiện cần nếu muốn tiến hành kiểm thử với người chơi thật.

**Tái sử dụng mẫu thiết kế.** Mẫu thay thế đối tượng có bảo toàn trạng thái, trình bày tại mục 6.3.1, được áp dụng cho hai hệ thống có bản chất khác nhau: bếp lửa với ba trạng thái và bụi cây với hai trạng thái. Việc một mẫu thiết kế phục vụ được hai hệ thống không liên quan cho thấy mức độ trừu tượng hóa đạt được là thực chất.

---

### 6.4.5 Chất lượng phản hồi cho người chơi

Mọi hành động quan trọng trong WildBound đều có phản hồi thị giác tức thì:

| Tình huống | Phản hồi | Mục đích |
|---|---|---|
| Nhận sát thương | Màn hình lóa đỏ rồi mờ dần | Cảnh báo mất máu khi người chơi đang tập trung quan sát môi trường |
| Đang nấu thịt | Hiển thị tiến trình theo phần trăm | Cho biết thao tác đang diễn ra và còn bao lâu |
| Cầm công cụ | Số độ bền hiển thị trên biểu tượng | Cảnh báo công cụ sắp hỏng |
| Nhìn vào sinh vật | Tên và lượng máu hiện tại | Phân biệt thỏ thường với thỏ đầu đàn trước khi giao chiến |
| Nhìn vào vật thể tương tác được | Dòng chữ gợi ý thao tác | Loại bỏ nhu cầu ghi nhớ phím bấm |
| Nhìn vào bếp lửa | Số độ bền còn lại | Quyết định có nên nạp thêm nhiên liệu |
| Trước lượt chơi đầu tiên | Lớp phủ hướng dẫn trên màn hình chính | Truyền đạt các quy tắc không thể tự suy ra qua thử nghiệm |

Nguyên tắc chung được áp dụng là người chơi phải hiểu được trạng thái trò chơi mà không cần đọc tài liệu hướng dẫn bên ngoài. Lớp phủ hướng dẫn trình bày tại mục 6.2.2 phục vụ đúng nguyên tắc này ngay tại điểm khởi đầu: thay vì cung cấp một tài liệu riêng biệt, những quy tắc khó tự khám phá nhất trong lúc chơi — trước hết là điều kiện để máu tự hồi phục — được trình bày ngay bên trong trò chơi trước khi lượt chơi đầu tiên bắt đầu. Cơ chế phản hồi bằng màn hình lóa đỏ là ví dụ điển hình: nó được bổ sung sau khi quan sát thấy rằng nếu chỉ dựa vào thanh máu, người chơi rất dễ bỏ sót việc mình đang mất máu trong lúc tập trung quan sát xung quanh.

Tuy nhiên, cần nhấn mạnh rằng toàn bộ đánh giá trong mục này thuộc nhóm chủ quan theo phân loại tại 6.4.1. Việc các cơ chế phản hồi này có thực sự dễ hiểu với người chơi mới hay không chưa được kiểm chứng.

---

### 6.4.6 Những hạn chế còn tồn tại

Các hạn chế được phân thành ba nhóm theo bản chất, bởi mức độ nghiêm trọng và hướng khắc phục của chúng khác nhau.

**Nhóm 1 — Tính năng chưa được xây dựng**

*Không có chức năng lưu và tải trò chơi.* Đây là hạn chế đáng kể nhất về mặt tính năng. Toàn bộ tiến trình của người chơi tồn tại trong bộ nhớ và bị xóa hoàn toàn khi thoát. Hệ quả là mỗi lượt chơi buộc phải bắt đầu lại từ đầu, khiến trò chơi chỉ phù hợp với phiên chơi ngắn và làm giảm giá trị của việc tích lũy tài nguyên dài hạn.

*Chưa có chu kỳ ngày đêm và hệ thống thời tiết.* Hai hệ thống này nằm trong dự kiến ban đầu nhưng đã bị lược bỏ khi phạm vi dự án được thu hẹp. Sự vắng mặt của chúng khiến môi trường tương đối tĩnh: điều kiện chơi ở phút thứ nhất và phút thứ ba mươi là như nhau, ngoại trừ mức tiêu hao chỉ số của người chơi.

*Trò chơi không có âm thanh.* Không có nhạc nền, hiệu ứng âm thanh cho hành động, hay tín hiệu cảnh báo bằng âm thanh. Đây là thiếu sót đáng kể xét trên phương diện trải nghiệm, bởi âm thanh là kênh phản hồi quan trọng thứ hai sau hình ảnh, đặc biệt trong thể loại sinh tồn nơi việc cảnh báo mối đe dọa nằm ngoài tầm nhìn có ý nghĩa lớn (Schell 2019).

*Nội dung giới hạn trong một bản đồ duy nhất.* Không có khu vực mới để khám phá, không có mục tiêu dài hạn ngoài việc kéo dài thời gian sống sót. Điều này giới hạn động lực chơi lại sau khi người chơi đã nắm được toàn bộ cơ chế.

**Nhóm 2 — Hệ thống hiện có ở mức đơn giản**

*Trí tuệ nhân tạo của sinh vật.* Thỏ di chuyển theo bốn hướng cố định và truy đuổi người chơi theo đường thẳng, không có khả năng tìm đường. Hệ quả là sinh vật có thể bị kẹt khi gặp vật cản nằm giữa nó và người chơi. Gói điều hướng của Unity đã có sẵn trong dự án nhưng chưa được sử dụng do hạn chế thời gian.

*Hệ thống chế tạo bị giới hạn ở hai nguyên liệu.* Cấu trúc công thức hiện tại chỉ cho phép kết hợp đúng hai loại nguyên liệu, khiến không gian thiết kế công thức bị bó hẹp và khó mở rộng sang các công thức phức tạp hơn.

*Tính đồng nhất của phong cách hình ảnh chưa cao.* Do sử dụng nhiều gói tài nguyên từ các tác giả khác nhau, các mô hình trong trò chơi có mức độ chi tiết và phong cách tạo hình không hoàn toàn thống nhất. Đây là hệ quả trực tiếp của quyết định ưu tiên thời gian cho thiết kế hệ thống — một sự đánh đổi có ý thức, nhưng vẫn là hạn chế của sản phẩm cuối cùng.

*Kiến trúc dựa trên mẫu Singleton.* Như đã phân tích tại mục 6.3.4, mẫu này phù hợp với quy mô hiện tại nhưng sẽ trở thành rào cản nếu dự án mở rộng sang chế độ nhiều người chơi.

**Nhóm 3 — Hạn chế của quá trình đánh giá**

*Không có kiểm thử với người chơi bên ngoài.* Đây là hạn chế nghiêm trọng nhất xét về phương pháp, và khác biệt về bản chất so với hai nhóm trên: hai nhóm kia là những thứ chưa làm, còn nhóm này khiến toàn bộ các thông số cân bằng hiện tại chỉ phản ánh cảm nhận của một người duy nhất — người vốn đã biết trước mọi cơ chế. Các giá trị như tốc độ tiêu hao chỉ số, độ bền công cụ hay lượng máu của sinh vật có thể quá dễ hoặc quá khó đối với người chơi mới mà hiện chưa có cách nào xác định.

---

### 6.4.7 Nhận định tổng thể

Xét trên phạm vi đã đặt ra ban đầu, WildBound đạt được mục tiêu cốt lõi: một trò chơi sinh tồn có vòng lặp hoàn chỉnh, các hệ thống hoạt động liên kết với nhau, nền kinh tế tài nguyên được tính toán có cơ sở, và nền tảng mã nguồn đủ vững để tiếp tục phát triển.

Điểm đáng chú ý khi phân loại các hạn chế là phần lớn chúng thuộc nhóm **tính năng chưa được bổ sung** chứ không phải nhóm **hệ thống hiện có hoạt động sai**. Sự phân biệt này có ý nghĩa thực tiễn: những gì đã xây dựng đều vận hành đúng như thiết kế, và phần lớn hạn chế có thể được khắc phục bằng cách xây thêm trên kiến trúc sẵn có mà không cần thiết kế lại. Ngoại lệ duy nhất là kiến trúc Singleton, vốn sẽ phải điều chỉnh nếu chuyển sang chế độ nhiều người chơi.

Nếu dự án được tiếp tục, thứ tự ưu tiên hợp lý sẽ là: kiểm thử với người chơi thật để có cơ sở điều chỉnh cân bằng, bổ sung chức năng lưu trò chơi, rồi mới đến việc mở rộng nội dung. Lý do của thứ tự này là hai hạng mục đầu quyết định chất lượng của những gì đã có, trong khi việc mở rộng nội dung chỉ làm tăng khối lượng chứ không cải thiện phần lõi.

---

# TÀI LIỆU THAM KHẢO BỔ SUNG CHO MỤC 6.4

> Các nguồn dưới đây bổ sung cho danh sách đã có ở Chương 3 và mục 6.3.

Fullerton T (2018) *Game design workshop: a playcentric approach to creating innovative games*, 4th edn, CRC Press, Boca Raton.

Schell J (2019) *The art of game design: a book of lenses*, 3rd edn, CRC Press, Boca Raton.
