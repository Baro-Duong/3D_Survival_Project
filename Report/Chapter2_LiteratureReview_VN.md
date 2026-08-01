# CHƯƠNG 2 - LITERATURE REVIEW
### Bản tiếng Việt (bản duyệt nội dung)

> **Ghi chú:**
> - Ngôi thứ ba, giọng bị động. Không dùng "tôi", không dùng "tác giả".
> - Chỉ dùng dấu gạch thường `-`.
> - Mỗi mục theo khuôn: **lý thuyết có trích dẫn → phân tích → liên hệ WildBound**. Phần liên hệ là chỗ ăn điểm.
> - Chương này dùng **6 nguồn mới**: Adams (2014), Adams và Dormans (2012), Csikszentmihalyi (1990), Hunicke et al. (2004), Koster (2013), Salen và Zimmerman (2004).

---

## 2.0 Giới thiệu chương

Chương này khảo sát nền tảng lý thuyết của các lĩnh vực liên quan trực tiếp tới đồ án, gồm sáu chủ đề: đặc trưng của thể loại trò chơi sinh tồn, lý thuyết về vòng lặp cốt lõi và động lực người chơi, thiết kế nền kinh tế nội tại của trò chơi, kiến trúc của game engine, các mẫu thiết kế phần mềm trong phát triển trò chơi, và kỹ thuật tương tác trong không gian ba chiều. Với mỗi chủ đề, phần lý thuyết được trình bày trước, sau đó là phần liên hệ tới các quyết định thiết kế cụ thể trong WildBound.

---

## 2.1 Thể loại trò chơi sinh tồn

### Định nghĩa và vị trí trong hệ thống phân loại

Việc phân loại trò chơi điện tử theo thể loại chủ yếu dựa trên **kiểu thử thách mà trò chơi đặt ra cho người chơi**, chứ không dựa trên bối cảnh hay hình thức thể hiện (Adams 2014). Theo cách phân loại này, trò chơi sinh tồn được xác định bởi một tổ hợp thử thách đặc thù: duy trì các chỉ số sinh học của nhân vật, thu thập tài nguyên khan hiếm từ môi trường, và chế biến các tài nguyên đó thành vật phẩm có giá trị sử dụng cao hơn.

Điểm phân biệt quan trọng nhất giữa thể loại sinh tồn và các thể loại lân cận nằm ở **nguồn gốc của mối đe dọa**. Trong phần lớn các thể loại hành động, mối đe dọa đến từ một đối thủ có chủ đích. Trong trò chơi sinh tồn, mối đe dọa chủ yếu đến từ chính môi trường và từ sự trôi qua của thời gian: người chơi thua không phải vì bị đánh bại trong một cuộc đối đầu, mà vì các chỉ số duy trì sự sống cạn kiệt.

Sự phân biệt này có hệ quả trực tiếp lên thiết kế. Ở một trò chơi hành động, độ khó được điều chỉnh chủ yếu qua sức mạnh của đối thủ. Ở một trò chơi sinh tồn, độ khó nằm ở **tương quan giữa tốc độ tiêu hao và tốc độ bổ sung tài nguyên**, tức là ở các con số của nền kinh tế trong trò chơi chứ không ở đối thủ.

### Liên hệ với WildBound

WildBound thuộc nhánh sinh tồn - chế tạo (survival-crafting), trong đó việc chế tạo công cụ là điều kiện để nâng cao hiệu quả thu thập. Trò chơi không có đối thủ có trí tuệ, không có mục tiêu chiến thắng, và toàn bộ áp lực đặt lên người chơi đến từ ba chỉ số suy giảm theo thời gian. Đây là lý do khiến phần lớn công sức thiết kế của đồ án tập trung vào cân bằng số liệu thay vì vào thiết kế đối thủ, và cũng là lý do mục 2.3 được trình bày kỹ hơn các mục còn lại.

---

## 2.2 Vòng lặp cốt lõi và động lực người chơi

### Khung phân tích MDA

Một trong những khung phân tích được sử dụng rộng rãi nhất trong nghiên cứu thiết kế trò chơi là mô hình MDA, phân tách trò chơi thành ba tầng: **cơ chế** (mechanics) là các quy tắc và dữ liệu ở tầng mã nguồn, **động lực học** (dynamics) là hành vi phát sinh khi người chơi vận hành các cơ chế đó, và **thẩm mỹ** (aesthetics) là trải nghiệm cảm xúc thu được (Hunicke et al. 2004).

Điểm quan trọng của mô hình này là nhà thiết kế chỉ có thể tác động trực tiếp lên tầng cơ chế, trong khi người chơi lại tiếp nhận từ tầng thẩm mỹ. Trải nghiệm mong muốn vì vậy không thể được lập trình trực tiếp mà chỉ có thể được tạo ra một cách gián tiếp thông qua việc thiết kế các quy tắc.

### Vòng lặp cốt lõi

Vòng lặp cốt lõi là chuỗi hành động ngắn mà người chơi lặp lại liên tục trong suốt thời gian chơi. Chất lượng của một trò chơi phụ thuộc rất lớn vào việc vòng lặp này có tạo ra sự thỏa mãn khi lặp lại hay không (Salen và Zimmerman 2004).

Sự thỏa mãn đó gắn liền với quá trình học hỏi. Người chơi cảm thấy hứng thú khi họ đang dần nắm bắt được một khuôn mẫu, và mất hứng thú khi khuôn mẫu đã được nắm bắt hoàn toàn và không còn gì để khám phá (Koster 2013). Điều này giải thích vì sao một vòng lặp quá đơn giản sẽ nhanh chóng gây nhàm chán bất kể được thực hiện tốt tới đâu.

### Lý thuyết dòng chảy

Khái niệm dòng chảy (flow) mô tả trạng thái tập trung cao độ mà một người đạt được khi mức độ thử thách của hoạt động tương xứng với năng lực của họ; thử thách vượt quá năng lực gây lo âu, còn thử thách dưới năng lực gây chán (Csikszentmihalyi 1990). Trong thiết kế trò chơi, khái niệm này thường được dùng để lập luận rằng độ khó cần tăng dần song song với sự tiến bộ của người chơi.

### Liên hệ với WildBound

Vòng lặp cốt lõi của WildBound là chuỗi: thu thập tài nguyên, chế tạo công cụ, sử dụng công cụ để thu thập hiệu quả hơn, và dùng tài nguyên thu được để duy trì các chỉ số sinh tồn. Vòng lặp này có tính tự củng cố, bởi mỗi lần hoàn thành lại nâng cao khả năng thực hiện lần tiếp theo.

Áp dụng mô hình MDA cho trò chơi này, tầng cơ chế là các quy tắc về tiêu hao chỉ số, công thức chế tạo và độ bền công cụ; tầng động lực học là hành vi phát sinh, chẳng hạn việc người chơi phải quyết định giữa chạy nhanh để tiết kiệm thời gian hay đi bộ để tiết kiệm nước; và tầng thẩm mỹ là cảm giác căng thẳng khi tài nguyên cạn dần. Cần nhấn mạnh rằng cảm giác căng thẳng đó không được lập trình trực tiếp mà là hệ quả gián tiếp của các con số cấu hình.

Về lý thuyết dòng chảy, cần thừa nhận một giới hạn: do WildBound chưa được kiểm thử với người chơi bên ngoài, mức độ tương xứng giữa độ khó và năng lực người chơi mới chưa được xác nhận. Nhận định này được phân tích thêm tại mục 6.4.1.

---

## 2.3 Thiết kế nền kinh tế nội tại của trò chơi

### Khái niệm nền kinh tế nội tại

Nền kinh tế nội tại (internal economy) là hệ thống mô tả cách các tài nguyên trong trò chơi được tạo ra, chuyển đổi và tiêu thụ. Adams và Dormans (2012) đề xuất một bộ khái niệm để phân tích hệ thống này, gồm bốn thành phần chính:

- **Nguồn (source)**: nơi tài nguyên được sinh ra và đưa vào hệ thống.
- **Bể chứa (drain)**: nơi tài nguyên bị tiêu thụ và rời khỏi hệ thống vĩnh viễn.
- **Bộ chuyển đổi (converter)**: cơ chế biến tài nguyên loại này thành tài nguyên loại khác.
- **Dòng chảy (flow)**: sự dịch chuyển của tài nguyên giữa các thành phần trên.

Giá trị của bộ khái niệm này nằm ở chỗ nó cho phép phân tích một nền kinh tế trò chơi một cách có hệ thống, thay vì điều chỉnh các con số theo cảm tính cho tới khi cảm thấy vừa.

### Hai rủi ro đối xứng

Một nền kinh tế trò chơi mất cân đối có thể hỏng theo hai hướng ngược nhau.

Hướng thứ nhất là **trạng thái bế tắc**: người chơi tiêu hết tài nguyên hữu hạn và không còn cách nào phục hồi, dẫn tới trò chơi tuy chưa kết thúc nhưng đã không thể tiếp tục một cách có ý nghĩa. Đây là lỗi thiết kế nghiêm trọng, bởi nó tước đi quyền hành động của người chơi.

Hướng thứ hai là **tích lũy dư thừa**: tài nguyên được sinh ra nhanh hơn mức tiêu thụ, khiến các quyết định về phân bổ tài nguyên mất hết ý nghĩa. Khi mọi thứ đều dư dả, việc lựa chọn không còn là lựa chọn. Schell (2019) mô tả đây là hiện tượng làm sụp đổ tính thử thách của trò chơi.

Do đó, một nền kinh tế được thiết kế tốt cần vừa có cơ chế bảo đảm nguồn cung không bao giờ cạn tuyệt đối, vừa có bể chứa đủ lớn để tài nguyên không tích tụ vô hạn.

### Liên hệ với WildBound

Toàn bộ nền kinh tế của WildBound được thiết kế theo bộ khái niệm nêu trên:

| Thành phần | Hiện thực trong WildBound |
|---|---|
| Nguồn | Bụi cây, cây, tảng đá lớn, hang thỏ - tất cả đều tự tái tạo theo chu kỳ |
| Bộ chuyển đổi | Bàn chế tạo (gậy và đá thành công cụ), bếp lửa (nước bẩn thành nước sạch, thịt sống thành thịt chín) |
| Bể chứa | Chế tạo công cụ, độ bền công cụ hao mòn khi sử dụng, độ bền bếp lửa tiêu hao khi đun và nấu |
| Dòng chảy | Chuỗi từ thu thập tới chế tạo tới sử dụng tới hao mòn |

Hai rủi ro nêu trên đều được xử lý một cách có chủ đích. Đối với rủi ro bế tắc, tảng đá lớn sinh ra một đơn vị đá theo chu kỳ cố định hoàn toàn độc lập với hành động của người chơi, và bụi cây tự hồi sinh sau một khoảng thời gian xác định. Nhờ vậy, ngay cả khi người chơi tiêu hết toàn bộ tài nguyên vào những lựa chọn sai lầm, nguồn cung vẫn tự phục hồi.

Đối với rủi ro tích lũy dư thừa, cơ chế nạp gậy và đá vào bếp lửa để phục hồi độ bền đóng vai trò một bể chứa bổ sung, tiêu thụ lượng tài nguyên vượt quá nhu cầu chế tạo. Điều đáng chú ý là bể chứa này không phải là một cơ chế được thêm vào một cách tùy tiện, mà giải quyết đúng vấn đề mà lý thuyết dự báo: sau khi đã chế tạo đủ công cụ, gậy và đá thu được về sau sẽ mất hết công dụng nếu không có nơi tiêu thụ.

Phân tích chi tiết về tính đúng đắn của nền kinh tế này, kèm số liệu cụ thể, được trình bày tại mục 6.4.3.

---

## 2.4 Game engine và nền tảng Unity

### Vai trò của game engine

Game engine là khung phần mềm tích hợp cung cấp sẵn các hệ thống nền tảng cần thiết cho phát triển trò chơi, bao gồm kết xuất đồ họa, mô phỏng vật lý, xử lý âm thanh, quản lý tài nguyên và môi trường lập trình (Gregory 2018). Trước khi các engine thương mại trở nên phổ biến, mỗi studio thường tự xây dựng công nghệ riêng, khiến chi phí gia nhập ngành rất cao.

Gregory (2018) phân tích kiến trúc engine hiện đại theo mô hình phân tầng, trong đó các tầng thấp xử lý tương tác trực tiếp với phần cứng, còn các tầng cao cung cấp giao diện lập trình cho nhà phát triển trò chơi. Mô hình phân tầng này cho phép nhà phát triển làm việc ở mức trừu tượng cao mà không cần hiểu chi tiết cách phần cứng vận hành.

### Unity

Unity hiện là một trong những engine được sử dụng rộng rãi nhất: theo báo cáo phân tích hơn 13.000 tựa game trên nền tảng Steam, 51% số trò chơi phát hành trong năm 2024 được phát triển bằng Unity (Video Game Insights 2025).

Đặc trưng kiến trúc quan trọng nhất của Unity là mô hình hướng thành phần, trong đó mọi thực thể là một vật chứa rỗng và toàn bộ chức năng được bổ sung bằng cách gắn các thành phần độc lập vào đó (Unity Technologies 2025a). Bên cạnh đó, Unity cung cấp cơ chế lưu trữ dữ liệu độc lập với cảnh chơi, tạo điều kiện cho việc tách dữ liệu cấu hình khỏi mã nguồn xử lý logic.

### Liên hệ với WildBound

Việc phân tích chi tiết các thành phần Unity được sử dụng trong đồ án, cùng với lý do lựa chọn engine và pipeline kết xuất, được trình bày tại Chương 3. Ở đây chỉ cần ghi nhận rằng mô hình hướng thành phần của Unity là yếu tố then chốt cho phép một thành phần duy nhất phục vụ nhiều loại đối tượng khác nhau trong trò chơi, và cơ chế dữ liệu độc lập với cảnh là nền tảng để tập trung toàn bộ thông số cân bằng vào một nơi.

---

## 2.5 Mẫu thiết kế phần mềm trong phát triển trò chơi

### Mẫu thiết kế nói chung

Mẫu thiết kế là các giải pháp đã được kiểm chứng cho những vấn đề thiết kế lặp đi lặp lại trong phát triển phần mềm hướng đối tượng (Gamma et al. 1994). Giá trị của chúng nằm ở chỗ chúng cung cấp một vốn từ chung để trao đổi về cấu trúc mã nguồn, đồng thời tránh việc phải tìm lại lời giải cho những vấn đề đã có lời giải.

Tuy nhiên, các mẫu thiết kế không phải công thức áp dụng vô điều kiện. Mỗi mẫu đều đi kèm chi phí, và việc áp dụng máy móc có thể làm mã nguồn phức tạp hơn mức cần thiết.

### Đặc thù của lĩnh vực trò chơi

Nystrom (2014) phân tích các mẫu thiết kế trong bối cảnh riêng của phát triển trò chơi, nơi tồn tại hai ràng buộc không phổ biến ở phần mềm thông thường: yêu cầu về hiệu năng theo thời gian thực, và sự phổ biến của các thực thể có nhiều trạng thái thay đổi liên tục.

Hai mẫu được bàn tới nhiều nhất trong bối cảnh này là **máy trạng thái** (state machine), dùng để tổ chức các thực thể có hành vi thay đổi theo trạng thái, và **Singleton**, dùng để cung cấp điểm truy cập toàn cục tới các hệ thống quản lý. Đáng chú ý là Nystrom (2014) phân tích Singleton chủ yếu ở khía cạnh những vấn đề mà nó gây ra: trạng thái toàn cục, quan hệ phụ thuộc bị che giấu, và khó khăn trong kiểm thử tự động.

Một hướng tiếp cận khác được nhấn mạnh là **thiết kế hướng dữ liệu**, trong đó dữ liệu cấu hình được tách hoàn toàn khỏi mã nguồn xử lý logic, cho phép điều chỉnh hành vi trò chơi mà không cần biên dịch lại.

### Liên hệ với WildBound

Cả ba hướng nêu trên đều được áp dụng trong đồ án. Máy trạng thái được dùng cho bếp lửa (ba trạng thái), bụi cây (hai trạng thái) và camera màn hình chính (ba trạng thái). Singleton được dùng cho các hệ thống quản lý, kèm theo việc thừa nhận rõ ràng những hạn chế mà Nystrom nêu ra. Thiết kế hướng dữ liệu được hiện thực qua một tài nguyên cấu hình duy nhất chứa hơn năm mươi thông số.

Phân tích chi tiết cách áp dụng từng mẫu, cùng lập luận về việc vì sao Singleton vẫn được lựa chọn dù có những hạn chế đã biết, được trình bày tại các mục 6.3.1 và 6.3.4.

---

## 2.6 Tương tác trong không gian ba chiều

### Các kỹ thuật phát hiện tương tác

Trong một trò chơi ba chiều, hệ thống phải xác định được người chơi đang có ý định tương tác với vật thể nào. Có ba kỹ thuật phổ biến để giải quyết vấn đề này.

Kỹ thuật thứ nhất là **phát tia** (raycasting): một tia được phóng từ vị trí và hướng nhìn của camera, và vật thể đầu tiên bị tia cắt qua được xem là mục tiêu. Kỹ thuật này dựa trên các phép kiểm tra giao cắt hình học được engine cung cấp sẵn (Gregory 2018).

Kỹ thuật thứ hai là sử dụng **vùng kích hoạt** (trigger volume) bao quanh nhân vật hoặc vật thể, và ghi nhận tương tác khi hai vùng chồng lấn.

Kỹ thuật thứ ba là **kiểm tra khoảng cách** đơn thuần giữa nhân vật và các vật thể lân cận.

Mỗi kỹ thuật có đặc điểm khác nhau. Phát tia phản ánh chính xác ý định của người chơi vì nó dựa trên hướng nhìn, nhưng đòi hỏi người chơi phải nhắm chính xác. Vùng kích hoạt và kiểm tra khoảng cách thì dễ sử dụng hơn nhưng không phân biệt được ý định khi có nhiều vật thể cùng ở gần.

### Liên hệ với WildBound

WildBound sử dụng phát tia làm cơ chế tương tác chính, cho cả việc hiển thị tên vật thể, việc tấn công và toàn bộ chuỗi tương tác với bếp lửa. Lựa chọn này được đưa ra sau khi phương án dùng vùng kích hoạt bao quanh nhân vật đã được thử nghiệm và loại bỏ, do nó không xác định được người chơi muốn tương tác với vật thể nào khi nhiều vật thể cùng nằm trong vùng.

Một hệ quả kỹ thuật của lựa chọn này là mọi vật thể có thể tương tác đều bắt buộc phải có thành phần va chạm, kể cả những vật thể chỉ mang tính hiển thị như mặt nước. Đây là chi tiết chỉ bộc lộ khi hiện thực, và được ghi nhận trong quá trình phát triển.

---

## 2.7 Hướng dẫn người chơi mới

### Vấn đề

Một trò chơi có nhiều cơ chế liên kết với nhau đặt ra vấn đề: người chơi mới cần nắm được các quy tắc cơ bản trước khi có thể ra quyết định hợp lý, nhưng việc trình bày toàn bộ quy tắc ngay từ đầu lại gây quá tải nhận thức.

Fullerton (2018) nhấn mạnh rằng nhà phát triển không còn khả năng đánh giá mức độ dễ hiểu của trò chơi mình tạo ra, bởi họ đã nắm rõ mọi cơ chế bên trong. Điều này khiến việc kiểm thử với người chơi thật trở thành công đoạn không thể thay thế khi thiết kế phần hướng dẫn.

Schell (2019) bổ sung rằng những cơ chế mà người chơi không thể phát hiện thông qua thử nghiệm tự nhiên là những cơ chế bắt buộc phải được truyền đạt tường minh.

### Liên hệ với WildBound

WildBound không có hệ thống hướng dẫn tích hợp trong quá trình chơi. Thay vào đó, một lớp phủ hướng dẫn phân trang được đặt tại màn hình chính, và nút bắt đầu trò chơi bị khóa cho tới khi người chơi đọc hết ở lần chạy đầu tiên.

Cơ chế cụ thể thúc đẩy quyết định này là quy tắc hồi máu: máu chỉ tự hồi khi cả chỉ số đói lẫn chỉ số khát đều trên một nửa. Đây đúng là loại cơ chế mà Schell (2019) mô tả, bởi người chơi gần như không thể suy ra được nó thông qua thử nghiệm ngẫu nhiên. Việc khóa nút bắt đầu chỉ áp dụng ở lần chạy đầu tiên nhằm bảo đảm thông tin đến được với người chơi mới mà không gây phiền cho người chơi quay lại.

---

## 2.8 Tổng kết chương

Nhìn chung, nền tảng lý thuyết được khảo sát trong chương này định hình các quyết định thiết kế của WildBound theo ba hướng. Thứ nhất, đặc trưng của thể loại sinh tồn xác định rằng trọng tâm thiết kế phải nằm ở cân bằng số liệu chứ không ở đối thủ. Thứ hai, lý thuyết về nền kinh tế nội tại cung cấp bộ khái niệm để phân tích và kiểm chứng hệ thống tài nguyên một cách có hệ thống, thay vì điều chỉnh theo cảm tính. Thứ ba, các mẫu thiết kế phần mềm cung cấp giải pháp có sẵn cho những vấn đề tổ chức mã nguồn, đồng thời cũng cảnh báo về chi phí đi kèm của từng lựa chọn.

Cần ghi nhận một khoảng trống trong tài liệu: thể loại sinh tồn tuy phổ biến về mặt thương mại nhưng chưa có nhiều nghiên cứu học thuật chuyên biệt, nên phần lớn phân tích trong chương này dựa trên các tài liệu về thiết kế trò chơi nói chung thay vì tài liệu riêng cho thể loại.

---

# TÀI LIỆU THAM KHẢO CHO CHƯƠNG 2

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
