# CHƯƠNG 7 - CONCLUSIONS
### Bản tiếng Việt (bản duyệt nội dung)

> **Ghi chú:**
> - Ngôi thứ ba, giọng bị động. Không dùng "tôi" và không dùng "tác giả".
> - Chỉ dùng dấu gạch thường `-`.
> - Trích dẫn chuẩn RMIT Harvard.
> - Mục 7.2 cố ý **không lặp lại** bảng đối chiếu chi tiết đã có ở 6.4.2, mà đưa ra kết luận ở cấp độ tổng thể.

---

## 7.1 Những gì đã học được từ đồ án

Đồ án kéo dài bảy tháng này mang lại kiến thức thuộc năm nhóm khác nhau. Đáng chú ý là phần lớn những bài học có giá trị nhất không đến từ tài liệu hướng dẫn, mà đến từ việc trực tiếp gặp phải và xử lý sự cố.

### Kiến thức kỹ thuật về Unity

Nhóm kiến thức đầu tiên là cách vận hành thực sự của engine, ở mức sâu hơn những gì các bài hướng dẫn nhập môn trình bày.

Quan trọng nhất là **vòng đời script**. Unity bảo đảm mọi phương thức `Awake()` chạy xong trước khi bất kỳ `Start()` nào bắt đầu, nhưng không bảo đảm thứ tự `Awake()` giữa các script khác nhau. Sự phân biệt này thoạt nhìn có vẻ mang tính chi tiết, nhưng chính nó là nguyên nhân của một lỗi khiến giao diện thư viện công cụ không phản hồi ở lần bấm đầu tiên, và cũng chính nó là công cụ để khắc phục lỗi đó một cách dứt điểm thay vì chắp vá.

Bài học thứ hai là về **tham chiếu Prefab**: một tham chiếu kéo từ cửa sổ Hierarchy trỏ tới thực thể trong scene chứ không trỏ tới tài nguyên gốc, và điều này chỉ bộc lộ khi thực thể đó bị hủy lúc chạy. Đây là loại lỗi không gây báo lỗi biên dịch, không gây cảnh báo, mà chỉ biểu hiện thành hành vi sai ở thời điểm muộn hơn nhiều.

Bài học thứ ba nằm ở **hệ thống giao diện**: khả năng nhận sự kiện chuột và vai trò hiển thị của một phần tử là hai thuộc tính hoàn toàn độc lập. Một phần tử văn bản chỉ để hiển thị số lượng, nếu để nguyên thuộc tính nhận sự kiện mặc định, sẽ âm thầm chặn thao tác kéo thả của phần tử nằm dưới nó.

Ngoài ra còn có kiến thức về sự khác biệt giữa các render pipeline và hệ quả tương thích shader kèm theo, về hệ thống hậu xử lý hình ảnh, và về cơ chế `Time.deltaTime` để bảo đảm tốc độ mô phỏng độc lập với cấu hình phần cứng.

### Kiến thức về kiến trúc phần mềm

Nhóm thứ hai liên quan tới việc tổ chức mã nguồn ở quy mô vượt quá vài tệp.

Đồ án cho thấy **mẫu Singleton không phải là lựa chọn tốt hay xấu một cách tuyệt đối**, mà là một sự đánh đổi có điều kiện. Với một trò chơi một người chơi, mỗi hệ thống chỉ tồn tại một thể hiện, và quy mô mã nguồn nằm trong tầm nắm bắt của một cá nhân, thì mẫu này tiết kiệm được đáng kể công sức. Nhưng chính những điều kiện đó cũng chỉ ra khi nào nó không còn phù hợp, và đây là nội dung được bàn tiếp tại 7.3.

Bài học thứ hai là **nguyên tắc một đối tượng chỉ có một chủ sở hữu**. Khi hai lớp cùng ghi trực tiếp lên một đối tượng giao diện mà thứ tự thực thi giữa chúng không được bảo đảm, kết quả là chúng liên tục ghi đè lên nhau và tạo ra lỗi không thể tái hiện ổn định. Giải pháp không nằm ở việc sửa từng trường hợp, mà nằm ở việc xác lập lại quyền sở hữu: một lớp duy nhất được phép thay đổi trạng thái đối tượng, các lớp khác chỉ bật cờ để lớp đó đọc.

Bài học thứ ba là **tạo biến thể bằng cờ và hệ số thay vì bằng kế thừa**. Sinh vật cấp cao trong trò chơi có chỉ số và hành vi khác biệt, nhưng được hiện thực chỉ bằng một cờ điều khiển và một hệ số nhân trên lớp sẵn có. Cách này giữ toàn bộ logic ở một nơi và tránh việc nhân bản mã nguồn.

### Phương pháp gỡ lỗi

Nhóm thứ ba, và có lẽ là nhóm thay đổi cách làm việc nhiều nhất, là **thói quen truy tìm nguyên nhân gốc thay vì chữa triệu chứng**.

Trường hợp minh họa rõ nhất là lỗi thư viện công cụ nêu trên. Biểu hiện bên ngoài là "nút bấm không hoạt động", và phản ứng tự nhiên sẽ là kiểm tra phần đăng ký sự kiện của nút. Nhưng bước xác định đúng vấn đề lại đến từ một quan sát khác: một dòng nhật ký đặt trong phương thức khởi tạo **không hề được in ra**. Chính sự vắng mặt đó chứng minh phương thức chưa từng được thực thi, và chuyển hướng toàn bộ quá trình chẩn đoán từ "nút bị lỗi" sang "đối tượng chưa từng khởi tạo".

Một dạng lỗi khác cũng được rút kinh nghiệm là **lỗi thất bại trong im lặng**. Khi một lời gọi lấy thành phần trên đối tượng vừa tạo trả về giá trị rỗng mà không có nhánh xử lý, chương trình vẫn chạy tiếp và đối tượng cũ vẫn bị hủy, để lại một đối tượng thay thế mất chức năng. Bài học là mọi nhánh "không tìm thấy" đều cần được ghi nhận rõ ràng, thay vì bỏ qua.

Cuối cùng là bài học về **trạng thái tĩnh tồn tại xuyên qua việc tải lại cảnh**. Một biến tĩnh dùng để đếm chu kỳ sinh sinh vật không tự động được xóa khi người chơi bắt đầu lượt mới, dẫn tới trạng thái của lượt cũ ảnh hưởng sang lượt sau. Đây là loại lỗi chỉ xuất hiện khi kiểm thử đúng kịch bản chơi lại, và dễ bị bỏ sót nếu chỉ kiểm thử từng tính năng riêng lẻ.

### Kiến thức về thiết kế trò chơi

Nhóm thứ tư là nhận thức rằng **cân bằng trò chơi là một bài toán định lượng chứ không phải cảm tính**. Việc xác định chi phí chế tạo, tốc độ tiêu hao chỉ số và tần suất hồi sinh tài nguyên đều phải xuất phát từ tính toán quan hệ cung - cầu, nếu không sẽ dẫn tới một trong hai thái cực: người chơi bế tắc vì thiếu tài nguyên, hoặc mất động lực vì tài nguyên quá dư thừa.

Đi kèm với đó là hai nguyên tắc cụ thể. Thứ nhất, **mọi hệ thống có tiêu hao đều cần cơ chế chống bế tắc** hoạt động độc lập với hành động của người chơi. Thứ hai, **một cơ chế có lợi nên đi kèm chi phí** để giữ được sức ép của trò chơi; cơ chế hồi máu tự động trong WildBound làm tăng tốc độ tiêu hao đói và khát chính vì lý do này.

### Kỹ năng quản lý dự án

Nhóm cuối cùng là các bài học không thuộc về kỹ thuật.

Quan trọng nhất là **biết thu hẹp phạm vi đúng thời điểm**. Việc loại bỏ ba hệ thống đã dự kiến - trồng trọt, chu kỳ ngày đêm và thời tiết - cho phép các hệ thống cốt lõi được hoàn thiện trọn vẹn, thay vì để nhiều hệ thống cùng ở trạng thái dở dang khi tới hạn. Bên cạnh đó là bài học về việc nhận ra một mô hình làm việc không hiệu quả và chủ động thay đổi, thể hiện qua quyết định chuyển từ làm nhóm sang làm cá nhân.

Về mặt công cụ, đồ án cũng là lần đầu áp dụng hệ thống quản lý phiên bản một cách có kỷ luật, cùng với những đặc thù của việc áp dụng nó cho dự án trò chơi, nơi tài nguyên đồ họa chứ không phải mã nguồn mới là thành phần chi phối dung lượng.

---

## 7.2 Kết quả của đồ án

### Mức độ hoàn thành

Đồ án hoàn thành toàn bộ tám mục tiêu cụ thể đặt ra tại mục 1.2, trong đó bảy mục đạt đầy đủ và một mục - trí tuệ nhân tạo của sinh vật - đạt ở mức cơ bản do chưa hiện thực khả năng tìm đường. Bảng đối chiếu chi tiết từng mục tiêu được trình bày tại mục 6.4.2.

Ngoài phạm vi ban đầu, ba hạng mục bổ sung cũng đã hoàn thành: hệ thống khai thác đá bằng cuốc chim, biến thể sinh vật cấp cao có khả năng tự phát hiện người chơi, và lớp phủ hướng dẫn phân trang trên màn hình chính. Việc ba hạng mục này được bổ sung ở giai đoạn muộn mà không phải thiết kế lại phần đã có chính là bằng chứng thực tế cho mục tiêu phi chức năng về khả năng mở rộng nêu tại 1.2.

Ngược lại, ba hệ thống từng nằm trong ý tưởng ban đầu đã không được thực hiện: trồng trọt, chu kỳ ngày đêm và thời tiết. Cần nói rõ rằng đây không phải là các hạng mục thất bại mà là các hạng mục **bị loại bỏ có chủ đích** khi phạm vi được thu hẹp, và lý do của quyết định đã được trình bày tại 1.2 và 1.3.

### Sản phẩm bàn giao

Kết quả cuối cùng là một trò chơi chạy được với vòng lặp cốt lõi hoàn chỉnh. Người chơi khởi động từ màn hình chính, đọc hướng dẫn, vào trò chơi, thu thập tài nguyên, chế tạo công cụ, chế biến thức ăn và nước uống, đối đầu với sinh vật, và kết thúc lượt chơi với thời gian sống sót được ghi nhận. Sản phẩm bao gồm 32 tệp mã nguồn tự viết với khoảng 3.200 dòng lệnh, hơn năm mươi thông số cân bằng tập trung trong một tài nguyên cấu hình duy nhất, và hai cảnh hoàn chỉnh.

### Ý nghĩa của kết quả

Xét về mặt học thuật, giá trị của đồ án không nằm ở khối lượng nội dung trò chơi mà nằm ở việc các hệ thống được thiết kế và hiện thực từ đầu, có liên kết với nhau, và có cơ sở tính toán. Điều này thể hiện rõ nhất qua nền kinh tế tài nguyên: mỗi con số về chi phí chế tạo và tốc độ hồi sinh tài nguyên đều xuất phát từ việc cân đối cung - cầu chứ không phải điều chỉnh ngẫu nhiên cho tới khi cảm thấy vừa.

Cần ghi nhận một cách trung thực rằng kết luận trên chỉ đúng ở phạm vi những gì có thể kiểm chứng khách quan. Do không có người chơi bên ngoài tham gia kiểm thử, mức độ phù hợp thực tế của các thông số cân bằng đối với người chơi mới vẫn chưa được xác nhận (Fullerton 2018). Đây là giới hạn của quá trình đánh giá, không phải của bản thân thiết kế, và là hạng mục cần được xử lý trước tiên nếu đồ án được tiếp tục.

---

## 7.3 Hướng phát triển tiếp theo

Các hướng phát triển được sắp xếp theo ba mức độ ưu tiên, dựa trên nguyên tắc hoàn thiện chất lượng của phần đã có trước khi mở rộng khối lượng.

### Ngắn hạn: hoàn thiện sản phẩm hiện có

**Kiểm thử với người chơi bên ngoài.** Đây là hạng mục ưu tiên cao nhất, bởi nó quyết định độ tin cậy của mọi nhận định về cân bằng trong báo cáo này. Việc tập trung toàn bộ thông số vào một tài nguyên cấu hình duy nhất đã tạo sẵn điều kiện kỹ thuật cho công việc này: các thông số có thể được điều chỉnh giữa các phiên kiểm thử mà không cần biên dịch lại chương trình.

**Chức năng lưu và tải trò chơi.** Đây là hạn chế đáng kể nhất về mặt tính năng. Việc bổ sung sẽ cho phép các phiên chơi dài, qua đó làm cho việc tích lũy tài nguyên có ý nghĩa hơn.

**Hệ thống âm thanh.** Nhạc nền, hiệu ứng âm thanh cho hành động và tín hiệu cảnh báo bằng âm thanh. Trong thể loại sinh tồn, âm thanh đặc biệt quan trọng vì nó là kênh duy nhất có thể cảnh báo về mối đe dọa nằm ngoài tầm nhìn.

**Cải thiện trí tuệ nhân tạo của sinh vật** bằng hệ thống tìm đường có sẵn của Unity, để sinh vật không còn bị kẹt khi gặp vật cản. Gói điều hướng đã có trong dự án nhưng chưa được sử dụng.

### Trung hạn: mở rộng nội dung

Nhóm này gồm chính ba hệ thống đã bị loại bỏ khi thu hẹp phạm vi, cùng một số bổ sung tự nhiên:

- **Chu kỳ ngày đêm**, tạo nhịp điệu cho trò chơi và mở ra không gian cho các cơ chế phụ thuộc thời gian, chẳng hạn tầm nhìn hạn chế vào ban đêm hoặc sinh vật hoạt động theo khung giờ.
- **Hệ thống thời tiết**, với các tác động có ý nghĩa tới cơ chế hiện có: mưa có thể dập tắt bếp lửa hoặc trở thành nguồn nước bổ sung.
- **Hệ thống trồng trọt**, cung cấp nguồn thức ăn bền vững thay thế cho việc săn bắt.
- **Hệ thống xây dựng nơi trú ẩn**, tạo mục tiêu trung hạn cho người chơi ngoài việc sinh tồn thuần túy.
- **Bổ sung sinh vật và khu vực địa hình mới**, mở rộng phạm vi khám phá.

Điểm chung của nhóm này là chúng đều có thể được xây dựng trên kiến trúc hiện có mà không cần thiết kế lại: hệ thống trạng thái, cơ chế thay thế đối tượng và cấu hình tập trung đều đã sẵn sàng tiếp nhận nội dung mới.

### Dài hạn

**Chế độ nhiều người chơi.** Đây là hướng phát triển đòi hỏi nhiều thay đổi nhất, và đáng lưu ý là rào cản không nằm ở khối lượng nội dung mà nằm ở kiến trúc. Toàn bộ các hệ thống quản lý hiện tại được xây dựng trên mẫu Singleton, vốn dựa trên giả định mỗi hệ thống chỉ tồn tại một thể hiện duy nhất. Giả định này không còn đúng khi có nhiều người chơi, do đó phần lớn kiến trúc sẽ phải được thiết kế lại theo hướng đồng bộ qua mạng. Đây là hệ quả trực tiếp của một quyết định kỹ thuật đã được cân nhắc và chấp nhận từ đầu, như đã phân tích tại 6.3.4.

**Phát hành thương mại.** Nếu hướng tới việc đưa sản phẩm lên nền tảng phân phối, cần bổ sung nhiều công việc ngoài phạm vi kỹ thuật: hoàn thiện đồ họa theo một phong cách thống nhất thay vì sử dụng nhiều gói tài nguyên từ các tác giả khác nhau, tối ưu hiệu năng, kiểm thử trên nhiều cấu hình phần cứng, và rà soát điều kiện bản quyền của toàn bộ tài nguyên bên thứ ba đang sử dụng.

Nhìn chung, WildBound ở trạng thái hiện tại là một sản phẩm hoàn chỉnh ở mức vòng lặp cốt lõi, đồng thời là một nền tảng có thể mở rộng. Phần lớn các hướng phát triển nêu trên là bổ sung trên nền có sẵn chứ không đòi hỏi làm lại, ngoại trừ trường hợp chế độ nhiều người chơi.

---

# TÀI LIỆU THAM KHẢO CHO CHƯƠNG 7

> Nguồn dưới đây đã có trong danh sách của mục 6.4, nhắc lại để tiện đối chiếu.

Fullerton T (2018) *Game design workshop: a playcentric approach to creating innovative games*, 4th edn, CRC Press, Boca Raton.
