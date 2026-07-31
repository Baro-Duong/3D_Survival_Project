# CHƯƠNG 3 — TECHNOLOGY AND TOOLS
### Bản tiếng Việt (bản duyệt nội dung — sau khi duyệt sẽ chuyển sang bản học thuật tiếng Anh)

> **Ghi chú dành cho người viết:**
>
> **1. Ngôi kể:** toàn bộ chương dùng **ngôi thứ ba, giọng bị động/vô nhân xưng**. Chủ ngữ của câu là *đồ án*, *dự án*, *hệ thống*, *sản phẩm* hoặc chính đối tượng kỹ thuật đang được nói tới — không dùng "tôi", cũng không dùng "tác giả đã làm X". Ví dụ: *"Unity được lựa chọn làm nền tảng phát triển"*, *"Hệ thống giao diện được xây dựng bằng uGUI"*, *"Vấn đề này được khắc phục bằng cách..."*. Khi dịch sang tiếng Anh cũng giữ nguyên nguyên tắc này (passive voice: *"Unity was selected as the development platform"*).
>
> **2. Trích dẫn:** theo chuẩn **RMIT Harvard** (Easy Cite) — trong bài **không có dấu phẩy** giữa tên và năm: `(Gregory 2018)`; hai tác giả dùng `and`: `(Chacon and Straub 2014)`.
>
> **3. `📌 [ẢNH]`** là vị trí nên chèn ảnh chụp màn hình minh họa.

---

## 3.0 Giới thiệu chương

Chương này trình bày các công nghệ và công cụ đã được sử dụng để xây dựng WildBound. Với mỗi công nghệ, nội dung được trình bày theo ba phần: bản chất kỹ thuật của công nghệ đó, lý do lựa chọn trong bối cảnh của đồ án, và cách thức nó đã được áp dụng cụ thể vào sản phẩm. Cách tiếp cận này nhằm chứng minh rằng các lựa chọn công nghệ trong đồ án là có cân nhắc, chứ không phải là kết quả của việc sử dụng ngẫu nhiên các công cụ có sẵn.

---

## 3.1 Unity 6 Game Engine

### 3.1.1 Tổng quan và lý do lựa chọn

Game engine là một khung phần mềm tích hợp cung cấp sẵn các hệ thống nền tảng cần thiết cho việc phát triển trò chơi, bao gồm kết xuất đồ họa, mô phỏng vật lý, xử lý âm thanh, quản lý tài nguyên và môi trường lập trình (Gregory 2018). Việc sử dụng game engine cho phép quá trình phát triển tập trung vào thiết kế cơ chế trò chơi thay vì phải xây dựng lại các hệ thống kỹ thuật cơ bản từ đầu.

Unity hiện là một trong những engine được sử dụng rộng rãi nhất trong ngành công nghiệp trò chơi. Theo báo cáo phân tích hơn 13.000 tựa game trên nền tảng Steam, 51% số trò chơi phát hành trong năm 2024 được phát triển bằng Unity (Video Game Insights 2025). Quy mô sử dụng này có ý nghĩa thực tiễn trực tiếp đối với đồ án: engine càng phổ biến thì khối lượng tài liệu, hướng dẫn và thảo luận kỹ thuật có sẵn càng lớn.

Unity được lựa chọn làm nền tảng phát triển cho WildBound dựa trên ba lý do chính.

Thứ nhất, Unity cung cấp một hệ sinh thái phát triển hoàn chỉnh — bao gồm trình soạn thảo trực quan, hệ thống vật lý, hệ thống hoạt ảnh, công cụ dựng địa hình và hệ thống giao diện người dùng — trong cùng một môi trường thống nhất. Điều này đặc biệt quan trọng đối với một đồ án cá nhân, nơi toàn bộ khối lượng công việc do một người đảm nhiệm và không có điều kiện tự xây dựng các hệ thống kỹ thuật nền tảng.

Thứ hai, quy mô cộng đồng người dùng lớn cùng hệ thống tài liệu chính thức đầy đủ giúp rút ngắn đáng kể thời gian xử lý sự cố. Do đồ án được thực hiện độc lập, không có sự trao đổi trực tiếp với đồng nghiệp khi phát sinh lỗi kỹ thuật, khả năng tự tra cứu tài liệu trở thành yếu tố quyết định tiến độ.

Thứ ba, Unity Asset Store cung cấp nguồn tài nguyên đồ họa phong phú, cho phép sử dụng các mô hình ba chiều có sẵn và dành phần lớn thời gian cho việc thiết kế, hiện thực các hệ thống trò chơi — vốn là trọng tâm học thuật của đồ án này.

Phiên bản được sử dụng là **Unity 6 (6000.3.10f1)**. Việc lựa chọn phiên bản mới nhất tại thời điểm khởi động dự án nhằm tận dụng các cải tiến về hiệu năng và những tính năng mới nhất của engine, qua đó tối ưu hóa sản phẩm. Đánh đổi của lựa chọn này là phần lớn tài liệu hướng dẫn và bài giảng trực tuyến hiện có được xây dựng trên các phiên bản cũ hơn, dẫn đến việc một số thao tác trong giao diện hoặc tên gọi thành phần không trùng khớp hoàn toàn. Trong những trường hợp đó, cách làm tương ứng trên phiên bản đang dùng được xác định bằng việc đối chiếu trực tiếp với tài liệu chính thức của Unity.

### 3.1.2 Kiến trúc GameObject – Component

Unity được xây dựng trên mô hình kiến trúc hướng thành phần (component-based architecture). Trong mô hình này, mọi thực thể trong không gian trò chơi đều là một `GameObject` — bản thân nó chỉ là một vật chứa rỗng không mang chức năng nào. Toàn bộ hành vi và thuộc tính được bổ sung bằng cách gắn các `Component` vào GameObject đó (Unity Technologies 2025a).

Mô hình này khác biệt căn bản so với cách tổ chức bằng kế thừa nhiều tầng trong lập trình hướng đối tượng truyền thống. Thay vì xây dựng một cây kế thừa cứng nhắc, các thành phần độc lập được tổ hợp lại để tạo ra thực thể mong muốn. Ưu điểm của cách tiếp cận này là tính linh hoạt: một hành vi được viết một lần dưới dạng component có thể tái sử dụng cho nhiều loại đối tượng hoàn toàn khác nhau mà không phát sinh quan hệ kế thừa phức tạp (Nystrom 2014).

Trong WildBound, nguyên tắc này thể hiện rõ qua component `InteractableObject`. Component này chịu trách nhiệm hiển thị tên của vật thể khi người chơi hướng tầm nhìn vào nó, và được gắn cho nhiều loại đối tượng có bản chất khác nhau: vật phẩm có thể nhặt (đá, gậy, táo), cây có thể chặt, bụi cây có thể hái, tảng đá lớn có thể khai thác và bếp lửa. Nếu sử dụng mô hình kế thừa, các đối tượng này sẽ khó gom về một lớp cha chung do chức năng của chúng quá khác biệt.

### 3.1.3 Vòng đời của script

Unity điều khiển việc thực thi mã nguồn thông qua một chuỗi các phương thức được gọi tự động theo trình tự xác định, gọi là vòng đời script (script lifecycle). Ba phương thức quan trọng nhất là `Awake()`, `Start()` và `Update()` (Unity Technologies 2025a).

Một đặc điểm then chốt cần lưu ý là Unity đảm bảo **phương thức `Awake()` của tất cả các đối tượng đều được thực thi xong trước khi bất kỳ phương thức `Start()` nào bắt đầu chạy**. Ngược lại, Unity **không** đảm bảo thứ tự thực thi `Awake()` giữa các script khác nhau.

Việc hiểu chính xác sự khác biệt này có ý nghĩa thực tiễn quan trọng. Trong quá trình phát triển WildBound, một lỗi đã phát sinh khi nút mở giao diện Tool Library không hoạt động ở lần bấm đầu tiên. Nguyên nhân được xác định là do việc đăng ký sự kiện cho nút được đặt trong `Start()`, trong khi một script khác lại vô hiệu hóa đối tượng chứa nút đó ngay trong `Awake()`. Do một đối tượng đã bị vô hiệu hóa sẽ không thực thi `Start()`, sự kiện của nút không bao giờ được đăng ký. Lỗi này được khắc phục bằng cách chuyển việc đăng ký sự kiện sang `Awake()` và chuyển thao tác vô hiệu hóa sang `Start()`, qua đó tận dụng đúng cơ chế bảo đảm thứ tự của Unity để loại bỏ hoàn toàn tính bất định.

### 3.1.4 Prefab

Prefab là cơ chế cho phép lưu một GameObject cùng toàn bộ cấu hình của nó thành một tài nguyên độc lập, từ đó có thể tạo ra nhiều bản sao trong quá trình chạy chương trình (Unity Technologies 2025a). Prefab là nền tảng của mọi hệ thống sinh đối tượng động trong WildBound: vật phẩm rơi ra khi chặt cây, thỏ được sinh ra định kỳ từ hang, và các biến thể trạng thái của bếp lửa đều được hiện thực bằng Prefab.

Trong quá trình phát triển, một vấn đề kỹ thuật đáng chú ý đã được phát hiện liên quan đến việc gán tham chiếu Prefab. Khi một tham chiếu được kéo thả từ cửa sổ Hierarchy — tức là trỏ tới một thực thể đang tồn tại trong scene — thay vì từ cửa sổ Project (trỏ tới tài nguyên Prefab gốc), tham chiếu đó vẫn hiển thị hợp lệ trong giao diện cho đến khi thực thể được trỏ tới bị hủy trong lúc chạy. Sau thời điểm đó, toàn bộ chuỗi tham chiếu kế thừa từ nó trở nên vô hiệu. Bài học rút ra là tham chiếu Prefab luôn phải được gán từ cửa sổ Project để đảm bảo tính ổn định.

### 3.1.5 ScriptableObject

ScriptableObject là một dạng lớp dữ liệu cho phép lưu trữ thông tin dưới dạng tài nguyên độc lập với scene, không cần gắn vào bất kỳ GameObject nào (Unity Technologies 2025a). Cơ chế này là nền tảng để hiện thực nguyên tắc thiết kế hướng dữ liệu (data-driven design), trong đó dữ liệu cấu hình được tách rời hoàn toàn khỏi mã nguồn xử lý logic.

WildBound áp dụng ScriptableObject cho hai mục đích. Thứ nhất, tài nguyên `GameConfig` tập trung toàn bộ các thông số cân bằng trò chơi — tốc độ di chuyển, tốc độ tiêu hao thể lực, sát thương, độ bền công cụ, thời gian hồi sinh tài nguyên và các thông số của sinh vật. Nhờ đó, việc điều chỉnh độ khó của trò chơi có thể thực hiện hoàn toàn thông qua giao diện Unity mà không cần sửa đổi hay biên dịch lại mã nguồn. Thứ hai, mỗi công thức chế tạo được lưu dưới dạng một tài nguyên `CraftingRecipe` riêng biệt, cho phép bổ sung công thức mới mà không tác động đến hệ thống chế tạo hiện có.

### 3.1.6 Công cụ dựng địa hình

Địa hình của hòn đảo trong WildBound được xây dựng bằng hệ thống Terrain tích hợp sẵn của Unity, sử dụng hai công cụ cơ bản: nâng và hạ độ cao bề mặt để tạo hình khối tổng thể của đảo, và vẽ vật liệu bề mặt để phân biệt các vùng cỏ, đất và đường mòn.

📌 [ẢNH] *Chèn ảnh chụp địa hình đảo trong Scene view hoặc bảng công cụ Terrain.*

---

## 3.2 Universal Render Pipeline (URP)

### 3.2.1 Khái niệm render pipeline

Render pipeline là chuỗi các bước xử lý mà engine thực hiện để chuyển đổi dữ liệu ba chiều của một cảnh — bao gồm hình học, vật liệu, ánh sáng và vị trí camera — thành hình ảnh hai chiều hiển thị trên màn hình (Gregory 2018). Unity cung cấp ba pipeline khác nhau, mỗi loại hướng tới một nhóm đối tượng sử dụng riêng biệt (Unity Technologies 2025b):

- **Built-in Render Pipeline**: pipeline thế hệ cũ, có khả năng tùy biến hạn chế.
- **Universal Render Pipeline (URP)**: được thiết kế nhằm cân bằng giữa chất lượng hình ảnh và hiệu năng, hoạt động ổn định trên nhiều nền tảng phần cứng khác nhau.
- **High Definition Render Pipeline (HDRP)**: hướng tới chất lượng hình ảnh cao nhất, yêu cầu phần cứng mạnh và chỉ phù hợp với nền tảng máy tính và máy chơi game chuyên dụng.

### 3.2.2 Lý do lựa chọn URP

URP được lựa chọn cho WildBound vì ba lý do. Thứ nhất, mức yêu cầu phần cứng của URP phù hợp với một trò chơi sinh tồn theo phong cách low-poly, vốn không đòi hỏi chất lượng hình ảnh ở mức cao nhất. Thứ hai, URP tích hợp sẵn hệ thống hậu xử lý hình ảnh (post-processing) thông qua cơ chế Volume, cho phép áp dụng các hiệu ứng thị giác mà không cần cài đặt thêm gói mở rộng. Thứ ba, URP là pipeline được Unity khuyến nghị làm lựa chọn mặc định cho các dự án mới, đồng nghĩa với việc tài liệu và tài nguyên hỗ trợ dành cho nó là phong phú nhất.

### 3.2.3 Vấn đề tương thích pipeline trong thực tế

Một trong những vấn đề kỹ thuật đáng kể nhất của giai đoạn giữa dự án là sự không tương thích giữa các render pipeline. Vấn đề phát sinh do các gói tài nguyên tải về từ Asset Store được nhà phát hành của chúng xây dựng cho những pipeline khác nhau: một số dùng Built-in, một số dùng URP, một số khác dùng HDRP. Do mỗi pipeline sử dụng một tập shader riêng và các tập này không tương thích lẫn nhau, các vật thể sử dụng shader không phù hợp với pipeline hiện hành sẽ được hiển thị bằng màu hồng đặc trưng — dấu hiệu cho biết Unity không thể phân giải được shader tương ứng.

Vấn đề này được khắc phục bằng việc thống nhất toàn bộ dự án về URP và chuyển đổi vật liệu của các tài nguyên không tương thích sang shader của URP. Bài học kỹ thuật rút ra là render pipeline cần được xác định ngay từ giai đoạn khởi tạo dự án, và mọi tài nguyên trước khi được đưa vào dự án đều cần được kiểm tra về pipeline mà nó hỗ trợ. Đây là một hạn chế mang tính kiến trúc của Unity mà tài liệu hướng dẫn dành cho người mới thường không đề cập đầy đủ.

### 3.2.4 Ứng dụng post-processing trong sản phẩm

Trong WildBound, hệ thống Volume của URP được sử dụng để tạo hiệu ứng Depth of Field ở chế độ Gaussian cho màn hình chính. Hiệu ứng này làm mờ toàn bộ khung cảnh nền phía sau, nhờ đó tên trò chơi và nút điều khiển ở lớp giao diện vẫn giữ được độ sắc nét và thu hút sự tập trung của người chơi. Do giao diện được kết xuất ở chế độ Screen Space Overlay, nó không chịu tác động của các hiệu ứng hậu xử lý áp dụng lên camera.

📌 [ẢNH] *Chèn ảnh chụp màn hình chính có nền mờ.*

---

## 3.3 C# và .NET

C# là ngôn ngữ lập trình hướng đối tượng do Microsoft phát triển, kết hợp giữa tính an toàn kiểu dữ liệu và cú pháp có tính biểu đạt cao (Microsoft 2025). Unity sử dụng C# làm ngôn ngữ scripting chính thức duy nhất, do đó toàn bộ logic của WildBound được viết bằng ngôn ngữ này.

Tổng khối lượng mã nguồn được phát triển trong khuôn khổ đồ án là **32 tệp script với khoảng 3.200 dòng lệnh**. Các đặc trưng của lập trình hướng đối tượng đã được vận dụng bao gồm:

- **Kế thừa**: mọi script điều khiển hành vi đều kế thừa từ lớp cơ sở `MonoBehaviour` của Unity, qua đó được engine tự động gọi các phương thức trong vòng đời script.
- **Đóng gói dữ liệu**: các thuộc tính cần được đọc từ bên ngoài nhưng chỉ được phép sửa đổi từ bên trong lớp được khai báo dưới dạng property với quyền truy cập bất đối xứng, ví dụ `public int selectedIndex { get; private set; }` trong lớp quản lý thanh công cụ nhanh.
- **Kiểu liệt kê (enum)**: được dùng để biểu diễn tập trạng thái hữu hạn một cách tường minh và an toàn về kiểu, thay cho việc dùng số nguyên hoặc chuỗi ký tự. WildBound định nghĩa các enum cho trạng thái bếp lửa, trạng thái bụi cây, loại ô chế tạo và trạng thái camera màn hình chính.
- **Property có logic truy xuất tùy biến**: lớp `ItemSlot` cung cấp property `Item` thực hiện việc duyệt qua các đối tượng con để tìm ra thành phần mang dữ liệu vật phẩm. Cách làm này giải quyết một vấn đề thực tế: mỗi ô chứa đồ có thể chứa nhiều đối tượng con, trong đó có cả phần tử hiển thị số lượng, nên việc truy xuất theo chỉ số thứ tự sẽ cho kết quả sai.
- **Interface**: các interface xử lý sự kiện của Unity như `IBeginDragHandler`, `IDragHandler`, `IDropHandler` và `IPointerEnterHandler` được hiện thực để xây dựng cơ chế kéo thả vật phẩm trong túi đồ.
- **Thành viên tĩnh (static)**: được sử dụng cho mẫu thiết kế Singleton và cho các trạng thái dùng chung toàn cục, chẳng hạn biến lưu vật phẩm đang được kéo giữa các ô.

---

## 3.4 Unity UI (uGUI) và TextMeshPro

Toàn bộ giao diện người dùng của WildBound được xây dựng bằng hệ thống Unity UI (uGUI), bao gồm thanh chỉ số sinh tồn, thanh công cụ nhanh, túi đồ, bảng chế tạo, thư viện công thức, màn hình kết thúc và màn hình chính.

Nền tảng của hệ thống này là đối tượng `Canvas` cùng thành phần `RectTransform`, cho phép định vị các phần tử giao diện theo cơ chế neo (anchor) để đảm bảo bố cục hiển thị đúng trên nhiều độ phân giải màn hình khác nhau (Unity Technologies 2025a). Tương tác của người dùng được xử lý thông qua `EventSystem`, bộ phận chịu trách nhiệm phát tia (raycast) từ vị trí con trỏ chuột để xác định phần tử giao diện đang được tương tác.

TextMeshPro được sử dụng cho toàn bộ nội dung văn bản trong trò chơi. So với thành phần Text thế hệ cũ, TextMeshPro sử dụng kỹ thuật Signed Distance Field, cho phép chữ giữ được độ sắc nét khi phóng to và hỗ trợ nhiều tùy chọn định dạng nâng cao hơn (Unity Technologies 2025a).

Một vấn đề kỹ thuật có giá trị thực tiễn đã phát sinh trong quá trình xây dựng giao diện túi đồ. Để hiển thị số lượng vật phẩm và độ bền công cụ, một phần tử văn bản được đặt chồng lên trên biểu tượng của vật phẩm. Tuy nhiên, do thuộc tính `Raycast Target` của phần tử văn bản mặc định được bật, phần tử này đã chặn các sự kiện chuột hướng tới biểu tượng nằm bên dưới, khiến thao tác kéo thả chỉ thực hiện được ở những vùng mà văn bản không che phủ. Vấn đề được xử lý bằng cách vô hiệu hóa thuộc tính `Raycast Target` cho mọi phần tử giao diện chỉ đóng vai trò hiển thị. Nguyên tắc tổng quát rút ra là: trong hệ thống giao diện của Unity, khả năng nhận sự kiện chuột và vai trò hiển thị của một phần tử là hai thuộc tính độc lập và cần được cấu hình một cách có chủ đích.

---

## 3.5 Git và GitHub

### 3.5.1 Vai trò của hệ thống quản lý phiên bản

Hệ thống quản lý phiên bản (version control system) là công cụ ghi nhận toàn bộ lịch sử thay đổi của mã nguồn, cho phép đối chiếu các phiên bản, khôi phục trạng thái trước đó và làm việc song song trên nhiều nhánh phát triển (Chacon and Straub 2014). Đối với một đồ án kéo dài từ tháng 01 đến tháng 08 năm 2026, việc áp dụng quản lý phiên bản là điều kiện cần thiết để kiểm soát rủi ro mất mát dữ liệu và để có thể quay lui khi một thay đổi gây ra lỗi ngoài dự kiến.

Cần phân biệt Git và GitHub: Git là hệ thống quản lý phiên bản phân tán hoạt động trên máy cục bộ, trong khi GitHub là dịch vụ lưu trữ trực tuyến cung cấp bản sao từ xa của kho mã nguồn (Chacon and Straub 2014).

### 3.5.2 Quy trình áp dụng trong đồ án

Quy trình làm việc được áp dụng trong WildBound tuân theo chu trình cơ bản: kiểm tra trạng thái thay đổi, đưa các thay đổi vào vùng chờ, tạo bản ghi thay đổi kèm thông điệp mô tả, và đồng bộ lên kho lưu trữ từ xa. Tính đến thời điểm viết báo cáo, dự án đã ghi nhận **28 bản commit** trên kho lưu trữ `Baro-Duong/3D_Survival_Project`.

Nguyên tắc đặt tên bản ghi thay đổi được áp dụng là mô tả chính xác nội dung công việc đã thực hiện thay vì các nhãn chung chung. Nhờ đó, lịch sử commit đồng thời đóng vai trò như một nhật ký phát triển, có thể dùng làm căn cứ đối chiếu tiến độ với kế hoạch dự án.

### 3.5.3 Vấn đề dung lượng tài nguyên

Một khó khăn thực tế đã phát sinh ở lần đồng bộ đầu tiên. Do dự án Unity chứa khối lượng lớn tài nguyên đồ họa nhị phân, tổng dung lượng của bản ghi đầu tiên vượt quá **giới hạn 2GB cho mỗi lần đẩy dữ liệu của GitHub**, khiến thao tác bị từ chối. Tình huống này được xử lý bằng cách tách bản ghi thành hai bản ghi riêng biệt và đồng bộ tuần tự.

Sự việc này cho thấy đặc thù của việc áp dụng quản lý phiên bản cho dự án game: khác với dự án phần mềm thông thường nơi mã nguồn chiếm phần lớn dung lượng, ở dự án game, tài nguyên đồ họa mới là thành phần chi phối. Điều này đòi hỏi phải cấu hình tệp `.gitignore` để loại trừ các thư mục do Unity tự sinh — như `Library/`, `Temp/` và `obj/` — vốn có thể được tái tạo tự động và không cần lưu trữ.

Một quan sát bổ sung có giá trị: khi tiến hành rà soát mức độ sử dụng tài nguyên ở giai đoạn cuối dự án (trình bày tại mục 3.6), gói tài nguyên có dung lượng lớn nhất trong kho lưu trữ được xác định là **hoàn toàn không được sử dụng** trong sản phẩm. Điều này cho thấy việc kiểm soát tài nguyên đưa vào dự án cần được thực hiện thường xuyên ngay từ đầu, thay vì để tích lũy.

---

## 3.6 Third-party Assets

### 3.6.1 Nguyên tắc sử dụng

WildBound sử dụng các gói tài nguyên đồ họa miễn phí từ Unity Asset Store cho phần hình ảnh của trò chơi. Quyết định này xuất phát từ việc phân bổ nguồn lực: đồ án được thực hiện bởi một cá nhân trong khoảng bảy tháng, và trọng tâm học thuật được xác định là **thiết kế và hiện thực các hệ thống trò chơi**, không phải công việc tạo hình ba chiều. Việc sử dụng tài nguyên có sẵn cho phép tập trung thời gian vào phần lõi của đồ án.

Cần nêu rõ phạm vi sử dụng: **các gói tài nguyên bên thứ ba chỉ cung cấp mô hình ba chiều, vật liệu, kết cấu bề mặt và hoạt ảnh. Toàn bộ logic vận hành của trò chơi — bao gồm hệ thống túi đồ, chế tạo, chỉ số sinh tồn, chiến đấu, trí tuệ nhân tạo của sinh vật và chuỗi tương tác nấu nướng — đều được thiết kế và lập trình trong khuôn khổ đồ án.**

Về mặt bản quyền, các gói tài nguyên được sử dụng đều được phân phối theo Giấy phép Tiêu chuẩn của Unity Asset Store (Standard Unity Asset Store End User License Agreement). Giấy phép này cho phép sử dụng tài nguyên trong cả sản phẩm phi thương mại và thương mại trên cơ sở miễn phí bản quyền, với điều kiện tài nguyên được nhúng trong sản phẩm và không được phân phối lại dưới dạng độc lập (Unity Technologies 2025c).

### 3.6.2 Danh mục tài nguyên được sử dụng

Nhằm đảm bảo tính chính xác của báo cáo, danh mục dưới đây được xác lập thông qua việc rà soát đồ thị tham chiếu tài nguyên của dự án, xuất phát từ các scene thực tế và các tài nguyên được nạp động trong lúc chạy, thay vì liệt kê toàn bộ các gói đã từng được tải về. Kết quả rà soát cho thấy trong số các gói đã tải, chỉ một phần thực sự được sử dụng trong sản phẩm cuối cùng.

| Nhóm chức năng | Gói tài nguyên | Nhà phát hành | Nội dung sử dụng |
|---|---|---|---|
| Địa hình | Fantasy Landscape | PXLTIGER | Lớp vật liệu mặt đất (cỏ, đường mòn) |
| | Fantasy Skybox FREE | Render Knight | Kết cấu bề mặt bổ trợ cho địa hình |
| Thảm thực vật | Yughues Free Palm Trees | Nobiax / Yughues | Năm biến thể mô hình cây dừa |
| | Fantasy Landscape | PXLTIGER | Mô hình cây bạch dương |
| | Idyllic Fantasy Nature | Edenity | Mô hình bụi cây thu hoạch được, shader thực vật |
| | Low Poly Trees – Free Nature Pack | Nebula | Mô hình bụi cây trang trí |
| Khoáng sản | Free Pack – Rocks Stylized | PolyOne Studio | Mô hình tảng đá lớn khai thác được |
| | Fantasy Landscape | PXLTIGER | Mô hình đá nhỏ |
| Mặt nước | Simple Water Shader URP | IgniteCoders | Bề mặt nước và hiệu ứng phản chiếu |
| Sinh vật | White Rabbit | Niwashi Games | Mô hình thỏ và ba hoạt ảnh (đứng yên, chạy, chết) |
| Công cụ | Low-Poly Forest Survival Starter Pack | Devtricked | Mô hình bếp lửa và cuốc chim |
| | Low Poly Fantasy Warrior | asoliddev | Mô hình rìu |
| Vật phẩm | Rustic Series: a Pot | NZ Bullet Studio | Mô hình nồi nấu |
| | Toony Kitchen & Ingredients | Sigun Studio | Mô hình thịt |
| | Match 3D Object Pack: Fruits and Vegetables | ThreeBox | Mô hình táo |
| | Interiors FREE | Mnostva Art | Mô hình chai đựng nước |
| Hiệu ứng | VFX URP – Fire Package | Cartoon VFX by Wallcoeur | Hiệu ứng lửa và khói cho bếp |
| Giao diện | Free 2D Mega Pack | Brackeys | Biểu tượng máu, thức ăn, nước uống trên thanh chỉ số |
| | Inventory Framework FREE | Game Dev Simplified | Khung nền và biểu tượng giao diện |
| Bối cảnh | Wood Boat | E6 Model | Mô hình thuyền trang trí |

---

## 3.7 Môi trường phát triển và công cụ hỗ trợ

### 3.7.1 Môi trường lập trình và gỡ lỗi

Mã nguồn của WildBound được viết trên Visual Studio, môi trường phát triển tích hợp sẵn với Unity thông qua gói mở rộng chính thức. Các tính năng được sử dụng thường xuyên bao gồm gợi ý mã nguồn theo ngữ cảnh, kiểm tra lỗi cú pháp tại thời điểm biên dịch và điều hướng nhanh giữa các định nghĩa lớp.

Công cụ gỡ lỗi chính trong quá trình phát triển là cửa sổ Console của Unity kết hợp với các câu lệnh ghi nhật ký `Debug.Log` và `Debug.LogError`. Phương pháp được áp dụng là đặt các điểm ghi nhật ký tại những vị trí nghi vấn nhằm xác định chính xác luồng thực thi thực tế của chương trình, sau đó loại bỏ các câu lệnh này khi lỗi đã được khắc phục để giữ mã nguồn sạch sẽ. Cách tiếp cận này đặc biệt hiệu quả với những lỗi mà biểu hiện bên ngoài không phản ánh đúng nguyên nhân gốc — chẳng hạn trường hợp nút giao diện không phản hồi được trình bày tại mục 3.1.3, trong đó việc xác nhận một dòng nhật ký không hề được in ra đã chỉ ra rằng phương thức chứa nó chưa từng được thực thi.

Ngoài ra, phần mềm Blender được sử dụng ở giai đoạn đầu của dự án để hiệu chỉnh một số mô hình ba chiều cho phù hợp với nhu cầu sử dụng.

### 3.7.2 Công cụ hỗ trợ bởi trí tuệ nhân tạo

Trong quá trình thực hiện đồ án, công cụ trí tuệ nhân tạo (Claude) được sử dụng với hai vai trò: **hỗ trợ viết mã nguồn** trong giai đoạn phát triển sản phẩm, và **hỗ trợ soạn thảo, biên dịch nội dung báo cáo** trong giai đoạn viết tài liệu.

Cần nêu rõ phạm vi của việc hỗ trợ này. Toàn bộ các quyết định về thiết kế hệ thống, lựa chọn kiến trúc phần mềm, cân bằng thông số trò chơi và quá trình chẩn đoán, khắc phục lỗi đều được thực hiện một cách chủ động trong khuôn khổ đồ án, và nguyên lý hoạt động của toàn bộ mã nguồn trong sản phẩm đều được nắm rõ, bao gồm cả những phần có sự hỗ trợ của công cụ nêu trên. Tương tự, toàn bộ nội dung chuyên môn, số liệu và lập luận trình bày trong báo cáo đều xuất phát từ quá trình phát triển thực tế của đồ án.

---

## 3.8 Tổng kết chương

Nhìn chung, các công nghệ được lựa chọn cho WildBound tạo thành một tổ hợp nhất quán và phù hợp với đặc thù của một đồ án cá nhân có thời hạn xác định. Unity 6 cùng URP cung cấp nền tảng kết xuất và mô phỏng, C# đảm nhiệm phần logic, hệ thống uGUI và TextMeshPro xây dựng giao diện, Git và GitHub đảm bảo an toàn dữ liệu và ghi nhận tiến độ, trong khi các gói tài nguyên bên thứ ba giải quyết phần hình ảnh để nguồn lực được tập trung cho việc thiết kế hệ thống.

Đáng chú ý, phần lớn những bài học kỹ thuật có giá trị nhất trong chương này — vấn đề tương thích render pipeline, cơ chế nhận sự kiện của hệ thống giao diện, và thứ tự thực thi trong vòng đời script — đều không đến từ tài liệu hướng dẫn mà đến từ việc trực tiếp gặp phải và xử lý sự cố trong quá trình phát triển.

---

# TÀI LIỆU THAM KHẢO CHO CHƯƠNG 3
### Định dạng RMIT Harvard (Easy Cite)

Chacon S and Straub B (2014) *Pro Git*, 2nd edn, Apress, Git website, accessed 29 July 2026. https://git-scm.com/book/en/v2

Gregory J (2018) *Game engine architecture*, 3rd edn, CRC Press, Boca Raton.

Microsoft (2025) *C# documentation*, Microsoft Learn website, accessed 29 July 2026. https://learn.microsoft.com/en-us/dotnet/csharp/

Nystrom R (2014) *Game programming patterns*, Genever Benning, Game Programming Patterns website, accessed 29 July 2026. https://gameprogrammingpatterns.com/

Unity Technologies (2025a) *Unity user manual*, Unity Documentation website, accessed 29 July 2026. https://docs.unity3d.com/Manual/

Unity Technologies (2025b) *Universal Render Pipeline documentation*, Unity Documentation website, accessed 29 July 2026. https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest

Unity Technologies (2025c) *Asset Store terms of service and EULA*, Unity website, accessed 29 July 2026. https://unity.com/legal/as-terms

Video Game Insights (2025) *The big game engines report of 2025*, VG Insights website, accessed 29 July 2026. https://vginsights.com/assets/reports/The_Big_Game_Engines_Report_of_2025.pdf

---

## Quy ước trích dẫn RMIT Harvard đã áp dụng

| Trường hợp | Cách viết |
|---|---|
| Một tác giả | `(Gregory 2018)` — **không có dấu phẩy** |
| Hai tác giả | `(Chacon and Straub 2014)` — dùng `and`, không dùng `&` |
| Ba tác giả trở lên | `(Nguyen et al. 2020)` |
| Tổ chức là tác giả | `(Unity Technologies 2025a)` |
| Cùng tác giả, cùng năm | Đánh phân biệt `2025a`, `2025b`, `2025c` |
| Trích dẫn nguyên văn | `(Day 2018:3)` — dấu hai chấm trước số trang |
| Nhiều nguồn cùng lúc | `(Gregory 2018; Nystrom 2014)` — xếp theo bảng chữ cái |
