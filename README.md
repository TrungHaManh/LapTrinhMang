# LapTrinhMang

Bài tập lớn - Mô tả bài toán 
- Nhóm lớp: 7 
- Hà Mạnh Trung, Đoàn Duy Tùng, Cao Minh Chúng


I.	Chức năng

  
	1.	Cho phép sử dụng/ ngăn chặn USB (USB chứa dữ liệu)
	2.	Cho phép sử dụng/ ngăn chặn tính năng chia sẻ file       
		-Bật/ tắt discovery
  
	3.	Cho phép sử dụng/ ngăn chặn Clipboard 
	4.	Chặn danh mục ứng dụng           
		-Liệt kê danh sách ứng dụng trên máy bị giám sát.            
		-Máy giám sát có quyền tắt 1 ứng dụng bất kỳ.  
	5.	Chat từ máy giám sát sang máy bị giám sát ( Chức năng bổ sung ).

II.	Môi trường phát triển
  
	1.	Công cụ phát triển
		- Unity (C#)  
			+ Trong thư mục Assets\Scripts chứa tất cả mã nguồn c#.   
			+ Chức năng một số hàm trong Unity sẽ được mô tả trong code.      
			+ Được sử dụng để xây dựng module client.

		- Visual studio (C#)
			+ Công cụ lập trình quen thuộc của Microsoft.       
			+ Được sử dụng để xây dựng module server và thư viện rpc. 
	2.	Kỹ thuật
		- TPC
			+ Là giao thức hướng kết nối.
			+ Giao thức này đảm bảo chuyển giao dữ liệu tới nơi nhận một cách đáng tin cậy và đúng thứ tự.
		- RPC/RMI
			+ Các phương thức trong các lớp thực thi interface IMonitor(RpcHandler/RpcHandler/User/IMonitor.cs) sẽ được dùng để thực hiện RPC
		- Reflection
			+ Kỹ thuật cho phép tạo 1 đối tượng, và gọi bất kỳ phương thước nào thông qua 1 chuỗi xử lý.
			+ Điều này giúp cho “máy bị giám sát” có thể tạo 1 đối tượng và gọi tới phương thức từ các thông tin (là 1 chuỗi) mà “máy giám sát” yêu cầu.

III. 	Cài đặt

	1.	RpcHandler
		- Đây là thư viện Rpc được compile ra file .dll
		- Được cài đặt trên 2 module client và server (2 môi trường cài đặt khác nhau)
	a. 	Môi trường cài đặt
		- Visual studio 2019
		- Project template: Class library (.Net framework)
	b.	Cấu trúc
		- Được cài đặt giống như kỹ thuật RPC/RMI
		- Gồm bộ đóng/mở gói:
			+ Class ClientExport (ClientStub): đóng gói dữ liệu gửi đi
			+ Class ServerImport (ServerStub): Mở gói dữ liệu đến
		- Dữ liệu gửi đi là 1 đối tượng( xem class Request).
		- Dữ liệu phản hồi cũng là đối tượng ( xem class Response).
		- Class ServerSocket, ClientSocket là các lớp quản lý kết nối mạng
		- Class MiniJSON(Mã nguồn mở): Chuyển dữ liệu thành mỗi chuỗi json để gửi đi.
		
	2.	ModuleServer
		- Chạy trên win console
		- Sử dụng như server trên máy tính bị giám sát   
	a.	Môi trường cài đặt
		- Visual studio 2019
		- Project template: Console app(.Net framework)
	b.	Cấu trúc
		- Gồm các lớp thực hiện các chức năng chính:
			+ class UsbHandler: đóng ngắt Usb
			+ class ClipBoardHandler: đóng ngắt clipboard,...
		- Class TestServer chứa triển khai của interface IMonitor: Các phương thức được triển khai sẽ thực hiện các chức năng được yêu cầu, và trả về kết quả, kết quả sẽ được đưa vào đối tượng Response, để gửi trả lại bên gọi(Xem RpcHandler/Server/ServerImport.Run() ).
	c.	Cách hoạt động
		- Đầu tiên cần khởi tạo 1 ServerImport ( Server stub) dùng để mở gói gói tin.
			+ Để khởi tạo ServerImport chúng ta cần truyền vào 1 namespace (dưới dạng chuỗi) là nơi chứa của lớp thực thi Interface IMonitor(Xem ModuleServer/TestXongXoa/Program.Main() ).
			+ Việc khởi tạo namespace này sẽ được dùng trong kỹ thuật Reflection (Xem RpcHandler/Server/ServerImport.Run()).
		- Sau đó cần khởi tạo 1 ServerSocket và tiến hành lắng nghe kết nối từ client.
		- Nhận yêu cầu:
			+ Khi yêu cầu được nhận dưới dạng 1 đối tượng Request và được xử trong RpcHandler/RpcHandler/Server/ServerImport.Run
		- Xử lý yêu cầu:
			+ USB: usb sẽ được ngắt/mở bằng cách set giá trị của Key trong RegistryKey (Xem ModuleServer/TestXongXoa/UsbHandler.cs).
			+ Clipboard: để xóa clipboard chúng ta sẽ dùng 1 vòng lặp để xóa( Xem ModuleServer/TestXongXoa/ClipBoardHandler.cs)
			+ Lấy danh sách ứng dụng đang chạy: Xem ModuleServer/TestXongXoa/TestServer.GetAllCurrentProcess
			+ Tắt 1 ứng dụng bất kỳ dựa vào id: Xem ModuleServer/TestXongXoa/TestServer.DisableProcessWithID (id do ModuleClient_Unity gửi sang dưới dạng 1 tham số cho cuộc gọi RPC - id này nhận được từ quá trình GetAllCurrentProcess ở trên).
			+ ShareFile: Tắt/mở discovery (Xem ModuleServer/TestXongXoa/TestServer.SetFileShare)
			+ Chat: Phương thức này hiển thị thông điệp nhận được dưới dạng 1 thông báo Window.
			+ Ping: Phương thức này được 2 bên ModuleClient_Unity và ModuleServer dùng để kiểm tra kết nối.
		
	3.	ModuleClient_Unity
		- Cửa sổ có giao diện người dùng
		- Sử dụng như Client trên máy giám sát
	a.	Môi trường cài đặt
		- Unity 2019.1.11f
		- Visual studio 2019 (để code)
	b.	Cấu trúc
		- Code sẽ được đặt tại ModuleClient_Unity/Assets/Scripts/
		- Class ClientMonitor Là class được kế thừa từ ClientExport và thực thi interface IMonitor
			+ Class phương thức thực thi từ IMonitor trong class này sẽ được dùng để gọi RPC (thay vì thực hiện và trả về kết quả như bên ModuleServer).
		- Class NetworkManager quản lý các thao tác từ yêu cầu người dùng.
	c.	Cách hoạt động
		- Khởi tạo:
			+ Đầu tiên khi chương trình bắt đầu chạy NetworkManager sẽ được yêu cầu khởi tạo( Xem ModuleClient_Unity/Assets/Scripts/UIManager.Start() ).
			+ Trong quá trình khởi tạo NetworkManager sẽ scan địa chỉ IP của những máy đã cài ứng dụng bên ModuleServer( Xem ModuleClient_Unity/Assets/Scripts/Network/NetworkManager.ScanIP() ) nếu có phản hồi thì sẽ hiển thị kết quả nhận được lên màn hình.
			+ Lưu ý: trong hàm tạo của ClientMonitor, "TestServer" được mặc định truyền vào - tên này chình là lớp thực thi Interface IMonitor bên ModuleServer, namespace của nó chúng ta cũng đã khởi tạo bên ModuleServer. Class name này sẽ được chúng ta gửi đi, để bên ModuleServer có thể khởi tạo 1 đối tượng dựa vào kỹ thuật Reflection (Xem RpcHandler/RpcHandler/Client/ClientExport.cs).
		- Hoạt động:
			+ Trong quá trình hoạt động của chương trình tương ứng với các thao tác của người dùng là các phương thức trong class NetworkManager.
		- Nhận phản hồi:
			+ Ứng với mỗi đối tượng gửi đi (Class Request) sẽ có 1 đối tượng được nhận về (Class Response) đối tượng này sẽ được đưa vào hàng đợi để chờ xử lý (Xem RpcHandler/RpcHandler/Client/ClientSocket.BeginConnectCallbackFunc ).
			+ Phương thức Update trong ModuleClient_Unity/Assets/Scripts/UIManager.cs sẽ được tự động gọi lại sau mỗi frame. Phương thức này sẽ kiểm tra hàng đợi và lấy đối tượng phản hồi ra xử lý.
			+ Trong quá trình xử lý để phân biệt nó là phản hồi của hành động nào chúng ta dựa vào Response.Phrase - nó sẽ chứa tên của hàm mà chúng ta đã gọi ban đầu.
