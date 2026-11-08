# Bài 6. Luồng MVC khi truy cập /Store/Index

```
Browser → RouteConfig → StoreController.Index() → List<ProductCardViewModel>
→ return View(products) → Views/Store/Index.cshtml → _StoreLayout.cshtml → HTML trả về Browser
```

1. **Browser** gửi request `GET /Store/Index` (hoặc `/` vì route mặc định đã trỏ về Store/Index).
2. **RouteConfig** so khớp URL với mẫu `{controller}/{action}/{id}` → xác định Controller = `Store`, Action = `Index`.
3. **StoreController.Index()** được gọi; Controller tạo `List<ProductCardViewModel>` chứa dữ liệu mẫu (8 sản phẩm, có `ShortDescription`).
4. `return View(products)` chuyển danh sách này sang View **Views/Store/Index.cshtml** làm Model (`@model IEnumerable<ProductCardViewModel>`).
5. View dùng Razor `@foreach (var item in Model)` để sinh ra mỗi sản phẩm một product card (ảnh, danh mục, tên, mô tả, giá).
6. View khai báo `Layout = "~/Views/Shared/_StoreLayout.cshtml"`; Layout cung cấp topbar, header/navbar, footer và chèn nội dung View vào chỗ `@RenderBody()`.
7. Razor biên dịch ra **HTML hoàn chỉnh**, gửi về Browser; Browser tải thêm `sportstore.css` và ảnh trong `Content/images` rồi hiển thị.

**Tóm lại:** Controller lấy/chuẩn bị dữ liệu (ViewModel) → View hiển thị dữ liệu → Layout bao khung chung. Controller không viết HTML, View không xử lý nghiệp vụ.
