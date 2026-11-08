# SportStore Buổi 06

Mở `DBSportStore_B06_Complete.sln` trong Visual Studio và chạy project bằng F5. Bản này tiếp nối project Buổi 5, dùng lại database `DBSportStore` trên `(localdb)\MSSQLLocalDB` và giữ các trang CRUD quản lý sản phẩm, danh mục.

Trang `/Store/Index` hiện có:

- Danh mục tải động từ bảng `Category` và liên kết lọc sản phẩm theo danh mục.
- Tìm kiếm theo tên hoặc mô tả sản phẩm.
- Lọc theo giá tối thiểu và tối đa.
- Phân trang sáu sản phẩm mỗi trang, giữ các điều kiện lọc khi chuyển trang.
- Sắp xếp giá thấp đến cao hoặc cao đến thấp.
- Hiển thị số kết quả, thông báo khi không có sản phẩm và liên kết đến trang chi tiết `/Products/Details/{id}`.

Không chạy lại `DBSportStore_Standard_18Products.sql` trên database đang dùng. Có thể kiểm tra trang cửa hàng trước, sau đó thử từng bộ lọc trên `/Store/Index` và mở chi tiết sản phẩm từ một thẻ.
