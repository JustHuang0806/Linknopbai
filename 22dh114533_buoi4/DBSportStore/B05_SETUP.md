# Buổi 05 — SportStore với SQL Server và EF6

## Trạng thái bộ file

Các file giao diện và ảnh trong thư mục này là phần nội dung để chép vào một project ASP.NET MVC 5 (.NET Framework 4.8). Thư mục hiện tại không có `.sln`, `.csproj`, `Web.config`, `Global.asax` hoặc cấu hình NuGet, nên chưa thể mở và chạy riêng như một solution Visual Studio. Hãy dùng project MVC5 đầy đủ đã tạo ở Buổi 04 làm project gốc.

Mã B05 trong thư mục này giả định EF Database First sinh các kiểu trong namespace `DBSportStore.Models`, context tên `DBSportStoreEntities`, và các DbSet `Products`, `Categories`, `Customers`, `OrderProes`. Giữ đúng tên context khi tạo EDMX.

Database `DBSportStore` đã được tạo trên `(localdb)\MSSQLLocalDB`; script chạy thành công và đã nạp dữ liệu mẫu. Không chạy lại script tạo database trên instance này.

## Thiết lập lần đầu

1. Mở solution MVC5 Buổi 04 trong Visual Studio. Kiểm tra target `.NET Framework 4.8`, rồi chép các thư mục `Controllers`, `Models`, `Views`, `Content` và `App_Start` của bộ bài này vào project. Chọn **Include In Project** cho các file mới nếu Solution Explorer chưa hiện chúng.
2. Ảnh 18 sản phẩm đã được giải nén vào `Content/images`. Script database nằm ở `DBSportStore_Standard_18Products.sql`.
3. Mở script trong SQL Server Management Studio hoặc SQL Server Object Explorer và kiểm tra `Server name` trước khi chạy. Bản script trong project không tự xóa database cũ. Nếu `DBSportStore` đã tồn tại, hãy kiểm tra/sao lưu trước và không chạy lại phần tạo database.
4. Cài package **EntityFramework 6.x** cho project MVC5 bằng **Tools → NuGet Package Manager → Manage NuGet Packages for Solution**.
5. Trong thư mục `Models`, chọn **Add → New Item → ADO.NET Entity Data Model**; đặt tên `DBSportStoreModel.edmx`; chọn **EF Designer from database**.
6. Tạo kết nối tới database `DBSportStore` trên SQL Server vừa chạy script. Với LocalDB thường dùng `(localdb)\MSSQLLocalDB`; nếu dùng SQL Server Express hoặc server khác thì chọn đúng tên server trên máy.
7. Lưu connection string vào `Web.config`; chọn đủ 7 bảng: `Role`, `UserAccount`, `Customer`, `Category`, `Product`, `OrderPro`, `OrderDetail`. Đặt model namespace `DBSportStore.Models` và giữ context name `DBSportStoreEntities`.
8. Hoàn tất wizard và **Build → Rebuild Solution**. Khi EDMX tạo context/entity thành công, các file trong `Controllers` và `Views` của B05 có thể được build cùng project.

## Chạy và kiểm tra

1. Đặt project MVC5 làm Startup Project và chạy bằng **F5**.
2. Mở `/Store/Index`; trang chủ đọc sản phẩm đang bán từ bảng `Product` và nối tên danh mục cùng ảnh.
3. Mở `/Products` để quản lý sản phẩm. Create nhận JPG/JPEG/PNG tối đa 2 MB; Edit giữ ảnh hiện tại nếu không chọn ảnh mới. Database lưu tên file, còn file được lưu trong `Content/images`.
4. Các controller và View CRUD cho danh mục, khách hàng, đơn hàng đã được thêm vào bộ file. Sau khi EDMX build thành công, menu quản trị mở `/Categories`, `/Customers`, `/OrderProes`.

## CRUD cuối buổi

Sau khi EDMX và context đã build, các controller và View CRUD có sẵn trong `Controllers/CategoriesController.cs`, `Controllers/CustomersController.cs`, `Controllers/OrderProesController.cs` cùng ba thư mục tương ứng trong `Views`. Danh sách khách hàng hiển thị Họ tên, Điện thoại, Email, Địa chỉ; danh sách đơn hàng hiển thị Mã đơn, Ngày đặt, Khách hàng, Địa chỉ giao hàng, Tổng tiền, Trạng thái. Các mục menu đã nối tới các controller này.

Nếu giảng viên yêu cầu thao tác scaffold trong Visual Studio để thực hành, dùng **Add → New Scaffolded Item → MVC 5 Controller with views, using Entity Framework** cho từng entity sau khi EDMX build. Tránh ghi đè các controller/View đã có nếu muốn giữ giao diện tiếng Việt.

## Ghi chú dữ liệu mẫu

Trong bản SQL đã chép vào project, đơn giá chi tiết của `Home Fitness Kit` được chỉnh thành 990.000đ để khớp giá sản phẩm và tổng đơn 2.970.000đ. File gốc trong Downloads không bị thay đổi.

````````

This is the code block that represents the suggested code change:

````````markdown
Install-Package EntityFramework
````````


# Response
````````markdown
# Buổi 05 — SportStore với SQL Server và EF6

## Trạng thái bộ file

Các file giao diện và ảnh trong thư mục này là phần nội dung để chép vào một project ASP.NET MVC 5 (.NET Framework 4.8). Thư mục hiện tại không có `.sln`, `.csproj`, `Web.config`, `Global.asax` hoặc cấu hình NuGet, nên chưa thể mở và chạy riêng như một solution Visual Studio. Hãy dùng project MVC5 đầy đủ đã tạo ở Buổi 04 làm project gốc.

Mã B05 trong thư mục này giả định EF Database First sinh các kiểu trong namespace `DBSportStore.Models`, context tên `DBSportStoreEntities`, và các DbSet `Products`, `Categories`, `Customers`, `OrderProes`. Giữ đúng tên context khi tạo EDMX.

Database `DBSportStore` đã được tạo trên `(localdb)\MSSQLLocalDB`; script chạy thành công và đã nạp dữ liệu mẫu. Không chạy lại script tạo database trên instance này.

## Thiết lập lần đầu

1. Mở solution MVC5 Buổi 04 trong Visual Studio. Kiểm tra target `.NET Framework 4.8`, rồi chép các thư mục `Controllers`, `Models`, `Views`, `Content` và `App_Start` của bộ bài này vào project. Chọn **Include In Project** cho các file mới nếu Solution Explorer chưa hiện chúng.
2. Ảnh 18 sản phẩm đã được giải nén vào `Content/images`. Script database nằm ở `DBSportStore_Standard_18Products.sql`.
3. Mở script trong SQL Server Management Studio hoặc SQL Server Object Explorer và kiểm tra `Server name` trước khi chạy. Bản script trong project không tự xóa database cũ. Nếu `DBSportStore` đã tồn tại, hãy kiểm tra/sao lưu trước và không chạy lại phần tạo database.
4. Cài package **EntityFramework 6.x** cho project MVC5 bằng **Tools → NuGet Package Manager → Manage NuGet Packages for Solution**.
5. Trong thư mục `Models`, chọn **Add → New Item → ADO.NET Entity Data Model**; đặt tên `DBSportStoreModel.edmx`; chọn **EF Designer from database**.
6. Tạo kết nối tới database `DBSportStore` trên SQL Server vừa chạy script. Với LocalDB thường dùng `(localdb)\MSSQLLocalDB`; nếu dùng SQL Server Express hoặc server khác thì chọn đúng tên server trên máy.
7. Lưu connection string vào `Web.config`; chọn đủ 7 bảng: `Role`, `UserAccount`, `Customer`, `Category`, `Product`, `OrderPro`, `OrderDetail`. Đặt model namespace `DBSportStore.Models` và giữ context name `DBSportStoreEntities`.
8. Hoàn tất wizard và **Build → Rebuild Solution**. Khi EDMX tạo context/entity thành công, các file trong `Controllers` và `Views` của B05 có thể được build cùng project.

## Chạy và kiểm tra

1. Đặt project MVC5 làm Startup Project và chạy bằng **F5**.
2. Mở `/Store/Index`; trang chủ đọc sản phẩm đang bán từ bảng `Product` và nối tên danh mục cùng ảnh.
3. Mở `/Products` để quản lý sản phẩm. Create nhận JPG/JPEG/PNG tối đa 2 MB; Edit giữ ảnh hiện tại nếu không chọn ảnh mới. Database lưu tên file, còn file được lưu trong `Content/images`.
4. Các controller và View CRUD cho danh mục, khách hàng, đơn hàng đã được thêm vào bộ file. Sau khi EDMX build thành công, menu quản trị mở `/Categories`, `/Customers`, `/OrderProes`.

## CRUD cuối buổi

Sau khi EDMX và context đã build, các controller và View CRUD có sẵn trong `Controllers/CategoriesController.cs`, `Controllers/CustomersController.cs`, `Controllers/OrderProesController.cs` cùng ba thư mục tương ứng trong `Views`. Danh sách khách hàng hiển thị Họ tên, Điện thoại, Email, Địa chỉ; danh sách đơn hàng hiển thị Mã đơn, Ngày đặt, Khách hàng, Địa chỉ giao hàng, Tổng tiền, Trạng thái. Các mục menu đã nối tới các controller này.

Nếu giảng viên yêu cầu thao tác scaffold trong Visual Studio để thực hành, dùng **Add → New Scaffolded Item → MVC 5 Controller with views, using Entity Framework** cho từng entity sau khi EDMX build. Tránh ghi đè các controller/View đã có nếu muốn giữ giao diện tiếng Việt.

## Ghi chú dữ liệu mẫu

Trong bản SQL đã chép vào project, đơn giá chi tiết của `Home Fitness Kit` được chỉnh thành 990.000đ để khớp giá sản phẩm và tổng đơn 2.970.000đ. File gốc trong Downloads không bị thay đổi.
