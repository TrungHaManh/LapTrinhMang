# LapTrinhMang

Bài tập lớn - Mô tả bài toán



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
		- Class MiniJSON: Chuyển dữ liệu thành mỗi chuỗi json để gửi đi
		
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
		- Class TestServer chứa triển khai của interface IMonitor: Các phương thức được triển khai sẽ thực hiện các chức năng được yêu cầu, và trả về kết quả, kết quả sẽ được đưa vào đối tượng Response, để gửi trả lại bên gọi(Xem ).
