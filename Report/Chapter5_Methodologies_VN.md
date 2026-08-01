# CHƯƠNG 5 - REVIEW OF SOFTWARE DEVELOPMENT METHODOLOGIES
### Bản tiếng Việt (bản duyệt nội dung)

> **Ghi chú:**
> - Ngôi thứ ba, giọng bị động. Không dùng "tôi", không dùng "tác giả".
> - Chỉ dùng dấu gạch thường `-`.
> - Trích dẫn RMIT Harvard. Chương này dùng **6 nguồn mới**, đều là tài liệu gốc kinh điển của từng phương pháp.
> - Bốn mục 5.1-5.4 cố ý viết ngắn theo đúng yêu cầu đề bài; giá trị chính nằm ở mục 5.5.

---

## 5.0 Giới thiệu chương

Chương này khảo sát bốn phương pháp phát triển phần mềm được sử dụng phổ biến, phân tích ưu điểm và hạn chế của từng phương pháp, sau đó trình bày phương pháp đã được lựa chọn cho đồ án WildBound cùng lập luận cho lựa chọn đó. Mỗi phương pháp được trình bày theo cùng một khuôn: định nghĩa, các giai đoạn, ưu điểm, hạn chế và loại dự án phù hợp.

---

## 5.1 Mô hình thác nước (Waterfall)

Mô hình thác nước là mô hình phát triển phần mềm tuần tự, trong đó dự án được chia thành các giai đoạn nối tiếp nhau và mỗi giai đoạn phải hoàn tất trước khi giai đoạn kế tiếp bắt đầu. Mô hình này thường được quy cho bài viết của Royce (1970), mặc dù trên thực tế bài viết đó trình bày mô hình tuần tự thuần túy như một cách làm tiềm ẩn rủi ro chứ không phải một khuyến nghị.

Các giai đoạn điển hình gồm: xác định yêu cầu, thiết kế hệ thống, hiện thực, kiểm thử, triển khai và bảo trì. Kết quả của mỗi giai đoạn là một tài liệu hoặc sản phẩm bàn giao làm đầu vào cho giai đoạn sau.

**Ưu điểm.** Cấu trúc rõ ràng và dễ quản lý, do mỗi giai đoạn có đầu ra xác định và có thể kiểm tra được. Tài liệu được sinh ra đầy đủ trong suốt quá trình, thuận lợi cho việc bàn giao và bảo trì lâu dài. Tiến độ dễ đo lường vì các mốc hoàn thành là rạch ròi.

**Hạn chế.** Mô hình giả định rằng toàn bộ yêu cầu có thể được xác định chính xác ngay từ đầu, điều hiếm khi đúng trong thực tế (Sommerville 2011). Việc quay lui để sửa đổi yêu cầu ở giai đoạn muộn có chi phí rất cao. Ngoài ra, sản phẩm chạy được chỉ xuất hiện ở giai đoạn cuối, nên các sai lệch về yêu cầu chỉ bị phát hiện khi phần lớn công sức đã bỏ ra.

**Phù hợp với.** Các dự án có yêu cầu ổn định, được hiểu rõ ngay từ đầu và ít khả năng thay đổi, đặc biệt là những dự án chịu ràng buộc pháp lý hoặc an toàn đòi hỏi tài liệu đầy đủ.

---

## 5.2 Mô hình xoắn ốc (Spiral)

Mô hình xoắn ốc do Boehm (1988) đề xuất, kết hợp tính lặp của phát triển theo nguyên mẫu với tính có kiểm soát của mô hình thác nước, và đặt **phân tích rủi ro** làm trung tâm.

Dự án được thực hiện qua nhiều vòng lặp, mỗi vòng gồm bốn hoạt động: xác định mục tiêu và ràng buộc, đánh giá phương án và phân tích rủi ro, phát triển và kiểm chứng sản phẩm của vòng đó, và lập kế hoạch cho vòng tiếp theo. Sau mỗi vòng, phạm vi và mức độ hoàn thiện của sản phẩm tăng dần.

**Ưu điểm.** Rủi ro được nhận diện và xử lý sớm thay vì tích tụ tới cuối dự án. Mô hình cho phép điều chỉnh hướng đi sau mỗi vòng, đồng thời vẫn duy trì được kỷ luật quản lý. Phù hợp với các hệ thống lớn và phức tạp, nơi hậu quả của một quyết định sai là đáng kể.

**Hạn chế.** Chi phí quản lý cao do mỗi vòng đều đòi hỏi hoạt động phân tích rủi ro. Việc áp dụng hiệu quả phụ thuộc nhiều vào năng lực đánh giá rủi ro của người quản lý dự án. Với các dự án nhỏ, chi phí này thường vượt quá lợi ích thu được.

**Phù hợp với.** Các dự án quy mô lớn, có mức độ rủi ro kỹ thuật hoặc rủi ro tài chính cao, và có đủ nguồn lực cho hoạt động quản lý.

---

## 5.3 Phát triển ứng dụng nhanh (RAD) và làm nguyên mẫu

Phát triển ứng dụng nhanh là phương pháp nhấn mạnh tốc độ bàn giao thông qua việc xây dựng nguyên mẫu liên tục và thu thập phản hồi từ người dùng, thay vì lập kế hoạch chi tiết từ đầu (Martin 1991).

Đặc trưng của phương pháp gồm: xây dựng nguyên mẫu để người dùng trải nghiệm và góp ý sớm, giới hạn thời gian cho từng vòng phát triển, và sử dụng các công cụ hỗ trợ nhằm rút ngắn thời gian hiện thực.

**Ưu điểm.** Sản phẩm chạy được xuất hiện rất sớm, giúp phát hiện sai lệch về yêu cầu ngay khi chi phí sửa chữa còn thấp. Người dùng tham gia trực tiếp vào quá trình, nhờ đó sản phẩm bám sát nhu cầu thực tế. Thời gian đưa sản phẩm ra thị trường được rút ngắn.

**Hạn chế.** Tài liệu thường thiếu hoặc không nhất quán do trọng tâm đặt vào sản phẩm chạy được. Chất lượng kiến trúc có thể bị hy sinh cho tốc độ, dẫn tới khó bảo trì về sau. Phương pháp cũng đòi hỏi người dùng sẵn sàng tham gia liên tục, điều không phải lúc nào cũng khả thi.

**Phù hợp với.** Các dự án có yêu cầu chưa rõ ràng, quy mô vừa và nhỏ, và có thời hạn gấp.

---

## 5.4 Agile

Agile không phải một quy trình cụ thể mà là một tập hợp các giá trị và nguyên tắc, được công bố trong Tuyên ngôn Phát triển Phần mềm Linh hoạt (Beck et al. 2001). Bốn giá trị cốt lõi ưu tiên cá nhân và tương tác hơn quy trình và công cụ, phần mềm chạy được hơn tài liệu đầy đủ, hợp tác với khách hàng hơn đàm phán hợp đồng, và phản hồi với thay đổi hơn bám theo kế hoạch.

Về mặt thực hành, Agile được triển khai qua các chu kỳ phát triển ngắn, mỗi chu kỳ tạo ra một phần sản phẩm hoạt động được và có thể đánh giá. Yêu cầu được làm rõ dần qua từng chu kỳ thay vì được cố định ngay từ đầu (Highsmith 2002).

**Ưu điểm.** Khả năng thích ứng cao với thay đổi, vốn là điều gần như không tránh khỏi trong phát triển phần mềm. Phản hồi đến sớm và liên tục, giúp giảm rủi ro xây dựng sai sản phẩm. Sản phẩm chạy được luôn tồn tại ở mọi thời điểm của dự án.

**Hạn chế.** Tài liệu thường mỏng, gây khó khăn cho việc bàn giao và bảo trì dài hạn. Tính linh hoạt nếu không được kiểm soát có thể dẫn tới phình phạm vi, khi các yêu cầu mới liên tục được bổ sung mà không có cơ chế ưu tiên. Phương pháp cũng đòi hỏi mức độ tự chủ và kỷ luật cao ở người thực hiện.

**Phù hợp với.** Các dự án có yêu cầu biến động, cần bàn giao sớm, và có người thực hiện đủ năng lực tự quản lý.

---

## 5.5 Phương pháp được lựa chọn và lập luận

### Lựa chọn

Đồ án WildBound áp dụng phương pháp **Agile theo hướng lặp và tăng dần** (iterative and incremental), có kết hợp yếu tố làm nguyên mẫu của RAD ở giai đoạn đầu.

Cần nêu rõ rằng lựa chọn này không phải là một quyết định được đưa ra trên giấy trước khi bắt đầu, mà là sự ghi nhận chính xác cách thức mà đồ án đã thực sự được thực hiện.

### Lập luận dựa trên thực tế triển khai

**Chu kỳ phát triển thực tế rất ngắn.** Mỗi tính năng được xây dựng, chạy thử ngay trong trình soạn thảo, phát hiện lỗi, sửa, rồi mới chuyển sang tính năng kế tiếp. Một chu kỳ như vậy kéo dài từ vài giờ tới vài ngày. Đây chính là mô hình lặp và tăng dần ở quy mô nhỏ nhất.

**Yêu cầu thay đổi liên tục trong quá trình thực hiện.** Ba hệ thống dự kiến ban đầu bị loại bỏ giữa chừng, cơ chế thu hoạch của bụi cây được thiết kế lại, và toàn bộ hệ thống thông số cân bằng được điều chỉnh nhiều lần. Với mô hình thác nước, mỗi thay đổi như vậy đòi hỏi quay lui về giai đoạn xác định yêu cầu, và chi phí sẽ vượt quá khả năng chịu đựng của một đồ án bảy tháng.

**Ba tính năng được bổ sung ở giai đoạn muộn.** Hệ thống khai thác đá, biến thể sinh vật cấp cao và lớp phủ hướng dẫn đều nảy sinh sau khi kiến trúc đã hình thành, và đều được tích hợp mà không phải thiết kế lại phần đã có. Khả năng tiếp nhận bổ sung muộn này là đặc trưng của phát triển tăng dần.

**Vòng phản hồi khép kín trong cùng một người.** Do đồ án được thực hiện độc lập, vai trò phát triển và vai trò kiểm thử nằm ở cùng một chỗ, nên phản hồi là tức thì và không có độ trễ giao tiếp.

**Yếu tố nguyên mẫu ở giai đoạn đầu.** Giai đoạn từ tháng 01 đến tháng 03 năm 2026 về bản chất là quá trình xây dựng nguyên mẫu: hệ thống được dựng nhanh theo hướng dẫn để hiểu công cụ, sau đó bị loại bỏ và viết lại từ nền tảng khi yêu cầu thực sự đã rõ ràng. Đây là đặc trưng của RAD, trong đó nguyên mẫu tồn tại để học hỏi chứ không nhất thiết để giữ lại.

**Hệ thống mốc kiểm tra của nhà trường vận hành như các chu kỳ Agile.** Mỗi mốc yêu cầu một phần sản phẩm hoạt động được để trình bày, tương ứng với khái niệm sản phẩm bàn giao gia tăng sau mỗi chu kỳ.

### Vì sao không chọn các phương pháp còn lại

**Mô hình thác nước** bị loại vì tiền đề của nó không thỏa mãn: yêu cầu của đồ án không thể xác định đầy đủ ngay từ đầu, bởi phần lớn quyết định thiết kế chỉ trở nên rõ ràng sau khi hệ thống đã chạy được và được chơi thử. Cân bằng trò chơi là ví dụ điển hình - không có cách nào xác định đúng tốc độ tiêu hao chỉ số trên giấy mà không thông qua thử nghiệm.

**Mô hình xoắn ốc** bị loại vì chi phí quản lý không tương xứng với quy mô. Hoạt động phân tích rủi ro chính thức ở mỗi vòng lặp là hợp lý với dự án lớn nhiều bên liên quan, nhưng với một đồ án cá nhân thì phần lớn công sức đó sẽ dành cho việc tạo ra tài liệu không có người đọc.

**RAD thuần túy** không được áp dụng toàn phần vì thiếu điều kiện tiên quyết quan trọng nhất của nó: sự tham gia liên tục của người dùng. Đồ án không có nhóm người chơi thử để lấy phản hồi định kỳ, nên chỉ có yếu tố làm nguyên mẫu của RAD được sử dụng, còn phần lấy phản hồi người dùng thì không.

### Hạn chế khi áp dụng Agile cho một đồ án cá nhân

Việc áp dụng Agile trong bối cảnh này có ba giới hạn cần được thừa nhận.

Thứ nhất, **phần lớn thực hành Agile được thiết kế cho làm việc nhóm**. Họp đồng bộ hằng ngày, lập trình cặp, xem xét mã chéo và họp tổng kết chu kỳ đều không có ý nghĩa với một người. Những gì còn lại chỉ là phần lõi lặp và tăng dần.

Thứ hai, **không có khách hàng hoặc người dùng thực để lấy phản hồi**. Trong khi Agile đặt sự hợp tác với khách hàng làm một trong bốn giá trị cốt lõi, đồ án này không có người chơi bên ngoài tham gia. Hệ quả là vòng phản hồi tuy nhanh nhưng khép kín, và các quyết định về cân bằng chỉ phản ánh góc nhìn của một người vốn đã biết trước mọi cơ chế. Hạn chế này được phân tích thêm tại mục 6.4.1.

Thứ ba, **việc xem nhẹ tài liệu để lại hệ quả thực tế**. Do trọng tâm được đặt vào sản phẩm chạy được, phần lớn các quyết định thiết kế trong quá trình phát triển không được ghi lại tại thời điểm chúng được đưa ra, và phải được dựng lại về sau từ mã nguồn cùng lịch sử thay đổi khi viết báo cáo này. Đây đúng là hạn chế mà Martin (1991) và Highsmith (2002) đều cảnh báo, và nó đã xuất hiện đúng như dự đoán.

### Kết luận

Nhìn chung, Agile theo hướng lặp và tăng dần là phương pháp phù hợp nhất với đồ án WildBound, bởi nó tương thích với ba đặc điểm nổi bật của đồ án: yêu cầu chưa ổn định ngay từ đầu, thời hạn cố định, và người thực hiện đồng thời giữ vai trò phát triển lẫn kiểm thử. Các hạn chế nêu trên không làm mất giá trị của lựa chọn, nhưng cho thấy phương pháp đã được áp dụng ở dạng rút gọn phù hợp với bối cảnh cá nhân, chứ không phải áp dụng nguyên vẹn một cách máy móc.

---

# TÀI LIỆU THAM KHẢO CHO CHƯƠNG 5

Beck K et al. (2001) *Manifesto for agile software development*, Agile Alliance website, accessed 31 July 2026.
https://agilemanifesto.org/

Boehm B W (1988) 'A spiral model of software development and enhancement', *Computer*, 21(5):61-72.

Highsmith J (2002) *Agile software development ecosystems*, Addison-Wesley, Boston.

Martin J (1991) *Rapid application development*, Macmillan, New York.

Royce W W (1970) 'Managing the development of large software systems', *Proceedings of IEEE WESCON*, 26:1-9.

Sommerville I (2011) *Software engineering*, 9th edn, Pearson, Harlow.
