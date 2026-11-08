# SportStore Bài 7

Bản Bài 7 tiếp nối project Buổi 6, bổ sung giỏ hàng theo Session và guest checkout. Project dùng ASP.NET MVC 5, EF6 và SQL Server LocalDB.

## Mở và chạy

1. Mở `22DH114533_Buoi7.sln` bằng Visual Studio.
2. Project kết nối tới `(localdb)\MSSQLLocalDB`, database `DBSportStore`.
3. Nếu database hiện tại chưa được cập nhật cho Bài 7, chạy `B07_UpdateDatabase_GuestCheckout.sql` đúng một lần hoặc chạy lại an toàn. Script giữ dữ liệu hiện có, cho phép email guest lặp lại, giữ `UserID` có giá trị duy nhất và bổ sung thông tin người nhận/số điện thoại vào đơn hàng.
4. Chạy `Run-B07.cmd` để khởi động LocalDB và mở website tại `http://localhost:56846/Store/Index`.

Không chạy lại script `DBSportStore_Standard_18Products.sql` trên database đang sử dụng; đó là script tạo dữ liệu ban đầu, không phải migration Bài 7.

## Nội dung Bài 7

- Thêm sản phẩm vào giỏ Session; cập nhật số lượng, xóa một dòng hoặc xóa toàn bộ giỏ.
- Hiển thị số lượng và tổng tiền trên navbar; chặn sản phẩm hết hàng.
- Guest checkout với kiểm tra dữ liệu.
- Tạo Customer guest, OrderPro và OrderDetail trong transaction; trừ tồn kho khi lưu thành công.
- Hiển thị trang đặt hàng thành công, thông tin giao hàng và chi tiết đơn.
