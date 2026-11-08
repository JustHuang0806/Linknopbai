# Buổi 05 — SportStore với SQL Server và EF6

## Mở và chạy project

Project Buổi 05 là solution độc lập: mở `Buoi5.sln` trong Visual Studio (không mở thư mục ở chế độ Folder View), chọn `Buoi5` làm Startup Project nếu cần, rồi chạy bằng F5. Project dùng .NET Framework 4.8 và IIS Express.

Database `DBSportStore` dùng instance `(localdb)\MSSQLLocalDB` và đã có dữ liệu mẫu: 18 sản phẩm, 4 danh mục, 2 khách hàng, 1 đơn hàng. **Không chạy lại script tạo database** trên instance này.

## Những gì Buổi 05 bổ sung

- `/Store/Index` lấy các sản phẩm đang bán từ bảng `Product`, kèm danh mục từ bảng `Category`; dữ liệu không còn được viết cứng trong `StoreController`.
- `/Products` quản lý sản phẩm bằng EF6: xem, thêm, sửa, xóa. Khi thêm sản phẩm cần chọn ảnh JPG/JPEG/PNG tối đa 2 MB; khi sửa, ảnh hiện tại được giữ nếu không chọn ảnh mới.
- `/Categories`, `/Customers`, `/OrderProes` có các trang CRUD bổ sung theo phần bài tập cuối buổi.
- Ảnh nằm trong `Content/images`; database chỉ lưu tên file trong `ImageUrl`.

## Kiểm tra nhanh

1. Mở `/Store/Index`: phải thấy 18 sản phẩm đang bán, giá, danh mục và ảnh lấy từ database.
2. Mở `/Products`: kiểm tra danh sách quản lý và các liên kết thêm/sửa/chi tiết/xóa.
3. Mở `/Categories`, `/Customers`, `/OrderProes`: xác nhận dữ liệu tương ứng hiển thị.
4. Để thấy rõ dữ liệu trang chủ phụ thuộc database, sửa trạng thái `IsActive` hoặc thông tin một sản phẩm trong `/Products`, lưu lại rồi quay về `/Store/Index`.

Script SQL gốc nằm ở `DBSportStore_Standard_18Products.sql`. Chỉ chạy script khi chủ động dựng database mới trên instance riêng và đã xác nhận database đích chưa tồn tại.
