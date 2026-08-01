# CHƯƠNG 1 - INTRODUCTION
### Bản tiếng Việt (bản duyệt nội dung)

> **Ghi chú:**
> - Ngôi thứ ba, giọng bị động - thống nhất với các chương đã viết.
> - Trích dẫn chuẩn RMIT Harvard.
> - ⚠️ Số commit ghi trong 1.4 là **29** tại thời điểm viết. Đếm lại bằng `git rev-list --count HEAD` ngay trước khi nộp.
> - Mục 1.5 **cố ý viết ngắn** - đây là yêu cầu của đề bài, đánh giá đầy đủ nằm ở Chương 7.

---

## 1.1 Giới thiệu về đề tài

Trò chơi sinh tồn (survival game) là thể loại trong đó người chơi được đặt vào một môi trường thù địch với rất ít tài nguyên ban đầu, và phải tự duy trì sự sống bằng cách thu thập vật liệu, chế tạo công cụ và quản lý các nhu cầu cơ bản của nhân vật. Điểm phân biệt thể loại này với các thể loại khác nằm ở chỗ mối đe dọa chính không đến từ đối thủ mà đến từ chính môi trường và sự khan hiếm tài nguyên: người chơi thua không phải vì bị đánh bại, mà vì cạn kiệt.

**WildBound** là một trò chơi sinh tồn góc nhìn thứ nhất được xây dựng trong khuôn khổ đồ án tốt nghiệp này. Người chơi bị mắc kẹt trên một hòn đảo biệt lập, trong tay chỉ có một chiếc nồi và một chiếc chai rỗng, không có nguồn tiếp tế. Ba chỉ số quyết định sự sống còn là máu, cơn đói và cơn khát; chúng suy giảm liên tục theo thời gian và chỉ có thể phục hồi thông qua các hoạt động sinh tồn tương ứng. Để duy trì được ba chỉ số này, người chơi phải thu thập gỗ và đá, chế tạo rìu và cuốc chim, dựng bếp lửa, đun sôi nước và nấu chín thịt, đồng thời đối phó với sinh vật hoang dã trên đảo. Trò chơi không có điều kiện chiến thắng: chỉ số thành tích duy nhất là khoảng thời gian người chơi sống sót được.

Tên gọi **WildBound** được ghép từ hai thành tố phản ánh trực tiếp thiết kế của trò chơi. *Wild* chỉ thiên nhiên hoang dã và trạng thái sinh tồn giữa tự nhiên, trong khi *Bound* mang nghĩa bị giới hạn, bị trói buộc trong một không gian cố định - chính là hòn đảo mà người chơi không thể rời khỏi. Sự kết hợp này diễn đạt luận điểm trung tâm của trò chơi: người chơi không thể trốn thoát, chỉ có thể thích nghi. Ngoài ra, tên gọi cũng có sự tương đồng về mặt ngữ âm với tên của tác giả đồ án.

Lý do lựa chọn đề tài xuất phát từ đặc điểm của thể loại sinh tồn xét trên phương diện kỹ thuật. Khác với những thể loại mà phần lớn công sức nằm ở nội dung được dựng sẵn, một trò chơi sinh tồn được cấu thành từ nhiều hệ thống nhỏ vận hành liên tục và phụ thuộc lẫn nhau: chỉ số nhân vật ảnh hưởng tới khả năng di chuyển, khả năng di chuyển ảnh hưởng tới tốc độ thu thập, tốc độ thu thập lại quyết định việc người chơi có kịp phục hồi chỉ số hay không. Chính đặc điểm này khiến thể loại trở thành một đối tượng phù hợp để thực hành thiết kế và hiện thực hệ thống phần mềm, thay vì chỉ dựng hình ảnh và kịch bản. Đây là năng lực mà đồ án hướng tới.

---

## 1.2 Mục tiêu của đồ án

### Mục tiêu tổng quát

Xây dựng một trò chơi sinh tồn góc nhìn thứ nhất hoàn chỉnh ở mức vòng lặp cốt lõi, trong đó toàn bộ các hệ thống trò chơi được thiết kế và hiện thực trong khuôn khổ đồ án, thay vì tái hiện theo một hướng dẫn có sẵn.

### Mục tiêu cụ thể

1. **Hệ thống túi đồ và thanh công cụ nhanh** cho phép cất giữ, kéo thả và xếp chồng vật phẩm, với thứ tự lấp đầy có ưu tiên.
2. **Hệ thống chế tạo** dựa trên công thức có định lượng nguyên liệu, kèm giao diện tra cứu công thức để người chơi không phải ghi nhớ.
3. **Hệ thống chỉ số sinh tồn** gồm máu, đói và khát, trong đó các chỉ số ảnh hưởng lẫn nhau chứ không vận hành độc lập.
4. **Chuỗi tương tác nhiều bước với môi trường**, mà cụ thể là quy trình xử lý nước và chế biến thức ăn qua bếp lửa.
5. **Hệ thống thu thập tài nguyên có công cụ chuyên biệt**, trong đó mỗi công cụ có độ bền hữu hạn và chỉ tác động được lên đối tượng tương ứng.
6. **Trí tuệ nhân tạo cho sinh vật**, bao gồm hành vi di chuyển tự do khi ở trạng thái bình thường và hành vi truy đuổi khi bị khiêu khích.
7. **Nền kinh tế tài nguyên khép kín**, được tính toán để bảo đảm người chơi không rơi vào trạng thái bế tắc không thể phục hồi.
8. **Hệ thống giao diện đầy đủ**, gồm màn hình chính, giao diện trong lúc chơi và màn hình kết thúc có ghi nhận thành tích.

### Mục tiêu phi chức năng

Bên cạnh các mục tiêu chức năng, đồ án đặt ra hai yêu cầu về chất lượng kỹ thuật. Thứ nhất, toàn bộ thông số cân bằng trò chơi phải được tập trung tại một nơi duy nhất và tách rời khỏi mã nguồn xử lý logic, để việc điều chỉnh độ khó không đòi hỏi sửa đổi chương trình. Thứ hai, kiến trúc mã nguồn phải cho phép bổ sung vật phẩm, công thức và công cụ mới với chi phí thấp, thay vì phải viết lại các phần đã có.

### Phạm vi không bao gồm

Ba hệ thống từng nằm trong ý tưởng ban đầu đã được loại bỏ có chủ đích khi phạm vi dự án được thu hẹp: hệ thống trồng trọt, chu kỳ ngày đêm và hệ thống thời tiết. Quyết định này được đưa ra nhằm bảo đảm các hệ thống cốt lõi được hoàn thiện trọn vẹn, thay vì để nhiều hệ thống cùng ở trạng thái dở dang. Lý do và hệ quả của quyết định được phân tích tại Chương 7.

---

## 1.3 Kế hoạch thực hiện

### Các giai đoạn

Đồ án được thực hiện từ tháng 01 đến tháng 08 năm 2026, chia thành năm giai đoạn:

| Giai đoạn | Thời gian | Nội dung chính |
|---|---|---|
| Học nền tảng | 17/01 - 14/03/2026 | Làm quen Unity thông qua hướng dẫn trực tuyến; dựng nguyên mẫu đầu tiên |
| Chuyển hướng | 14/03 - 04/04/2026 | Tách khỏi hướng dẫn, tự thiết kế lại hệ thống; dựng địa hình theo lối chơi riêng |
| Tái cấu trúc | 04/04 - 28/05/2026 | Chuyển sang thực hiện cá nhân; xử lý sự cố tương thích render pipeline |
| Phát triển hệ thống lõi | 28/05 - 16/07/2026 | Túi đồ, thanh công cụ nhanh, chế tạo, chiến đấu, trí tuệ nhân tạo sinh vật, tương tác môi trường |
| Hoàn thiện | 16/07 - 15/08/2026 | Màn hình chính, hướng dẫn người chơi, cân bằng thông số, viết báo cáo |

Tiến độ được theo dõi thông qua hệ thống mốc kiểm tra của nhà trường, kết hợp với lịch sử commit trên kho lưu trữ Git - vốn đóng vai trò như nhật ký phát triển chi tiết ở cấp độ từng thay đổi.

### Hai quyết định có ảnh hưởng lớn tới kế hoạch

**Tách khỏi hướng dẫn trực tuyến (14/03/2026).** Giai đoạn đầu của đồ án được thực hiện bằng cách đi theo một loạt hướng dẫn phát triển trò chơi trên nền tảng chia sẻ video. Phương pháp này giúp làm quen nhanh với công cụ, nhưng sớm bộc lộ giới hạn: các thao tác được tái hiện mà không đi kèm lý do đằng sau từng lựa chọn thiết kế, và khó khăn xuất hiện ngay khi yêu cầu nằm ngoài phạm vi hướng dẫn, bởi khi đó không còn lời giải nào để tham chiếu. Do đồ án đòi hỏi các hệ thống được thiết kế theo ý đồ riêng, việc tiếp tục đi theo hướng dẫn trở thành rào cản. Từ thời điểm này, các hệ thống được viết lại từ nền tảng.

**Chuyển sang thực hiện cá nhân (04/04/2026).** Đồ án ban đầu được đăng ký thực hiện theo nhóm ba người. Sau giai đoạn đầu, hiệu quả phối hợp không đạt yêu cầu và tiến độ chung bị ảnh hưởng. Đề xuất tách nhóm được đưa ra, nhằm giành lại quyền kiểm soát đối với tiến độ và chất lượng sản phẩm. Quyết định này làm tăng đáng kể khối lượng công việc cá nhân, nhưng đổi lại loại bỏ được sự phụ thuộc vào tiến độ của người khác - yếu tố quan trọng đối với một đồ án có thời hạn cố định.

Cả hai quyết định đều làm tăng khối lượng công việc trong ngắn hạn nhưng được đánh giá là cần thiết, bởi chúng chuyển đồ án từ trạng thái tái hiện sang trạng thái tự thiết kế - vốn là yêu cầu học thuật của một đồ án tốt nghiệp.

---

## 1.4 Kết quả đạt được

### Sản phẩm bàn giao

Sản phẩm của đồ án là một trò chơi chạy được hoàn chỉnh ở mức vòng lặp cốt lõi. Người chơi khởi động từ màn hình chính, đọc phần hướng dẫn, vào trò chơi, thu thập tài nguyên, chế tạo công cụ, chế biến thức ăn và nước uống, đối đầu với sinh vật, và kết thúc lượt chơi tại màn hình ghi nhận thời gian sống sót, từ đó có thể chơi lại hoặc quay về màn hình chính. Không có mắt xích nào trong chuỗi này bị bỏ dở hay phải thay thế bằng dữ liệu giả lập.

### Các hệ thống đã hoàn thành

Toàn bộ tám mục tiêu cụ thể nêu tại 1.2 đều đã được hiện thực, trong đó bảy mục đạt đầy đủ và một mục - trí tuệ nhân tạo của sinh vật - đạt ở mức cơ bản do chưa có khả năng tìm đường. Chi tiết đối chiếu từng mục tiêu được trình bày tại mục 6.4.2.

Ngoài phạm vi dự kiến ban đầu, ba hạng mục bổ sung cũng đã được hoàn thành:

- **Hệ thống khai thác đá bằng cuốc chim**, mở rộng nguồn cung khoáng sản và tạo thêm một nhánh chế tạo.
- **Biến thể sinh vật cấp cao** (thỏ đầu đàn) với chỉ số nhân đôi và hành vi chủ động phát hiện người chơi, tạo ra tầng rủi ro thứ hai trong trò chơi.
- **Lớp phủ hướng dẫn phân trang** trên màn hình chính, giải quyết vấn đề người chơi mới không nắm được các cơ chế khó tự phát hiện.

### Số liệu định lượng

| Hạng mục | Số liệu |
|---|---|
| Tệp mã nguồn tự viết | 32 tệp C# |
| Tổng số dòng lệnh | Khoảng 3.200 dòng |
| Thông số cân bằng tập trung | Hơn 50 thông số trong một tài nguyên cấu hình |
| Công thức chế tạo | 3 công thức |
| Số bản ghi thay đổi trên kho lưu trữ | 29 commit *(cập nhật lại trước khi nộp)* |
| Số cảnh trong sản phẩm | 2 cảnh (màn hình chính và cảnh chơi) |

---

## 1.5 Đánh giá sơ bộ

Mục này nêu nhận định tóm tắt; phần đánh giá đầy đủ được trình bày tại mục 6.4 và Chương 7.

**Về những điểm đạt được**, kết quả nổi bật nhất là vòng lặp cốt lõi hoàn chỉnh và chơi được trọn vẹn - mục tiêu quan trọng nhất của đồ án. Bên cạnh đó, nền kinh tế tài nguyên được thiết kế dựa trên tính toán cân đối cung-cầu chứ không phải điều chỉnh cảm tính, và kiến trúc mã nguồn đã chứng minh được khả năng mở rộng thông qua việc bổ sung thành công ba hạng mục ngoài phạm vi ở giai đoạn muộn của dự án.

**Về những hạn chế**, sản phẩm chưa có chức năng lưu trò chơi, chưa có âm thanh, và chưa có chu kỳ ngày đêm cùng hệ thống thời tiết như dự kiến ban đầu. Trí tuệ nhân tạo của sinh vật cũng mới ở mức cơ bản.

**Hạn chế nghiêm trọng nhất lại không nằm ở sản phẩm mà nằm ở quá trình đánh giá**: toàn bộ việc kiểm thử được thực hiện nội bộ trong khuôn khổ đồ án, không có người chơi bên ngoài tham gia. Điều này có nghĩa là các thông số cân bằng hiện tại phản ánh cảm nhận của một người vốn đã biết trước mọi cơ chế, và mức độ phù hợp của chúng với người chơi mới chưa được kiểm chứng (Fullerton 2018).

Xét tổng thể, đồ án hoàn thành được phạm vi đã đặt ra sau khi thu hẹp, với phần lớn hạn chế thuộc nhóm tính năng chưa được bổ sung chứ không phải hệ thống hiện có vận hành sai.

---

# TÀI LIỆU THAM KHẢO CHO CHƯƠNG 1

> Nguồn dưới đây đã có trong danh sách của mục 6.4, chỉ nhắc lại ở đây để tiện đối chiếu.

Fullerton T (2018) *Game design workshop: a playcentric approach to creating innovative games*, 4th edn, CRC Press, Boca Raton.
