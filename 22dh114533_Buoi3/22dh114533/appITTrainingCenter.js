// ktra kết nối js
console.log("IT Training Center - JavaScript loaded");

// getElementById
const centerName = "IT Training Center";
const slogan = "Learn Technology - Build Your Future";

const welcomeTitle = document.getElementById("welcome-title");
const welcomeMessage = document.getElementById("welcome-message");

if (welcomeTitle) {
    welcomeTitle.innerHTML = "Welcome to " + centerName;
}
if (welcomeMessage) {
    welcomeMessage.innerText = slogan;
}

// Bài 1: Available Courses - Array + querySelectorAll + forEach
const courseNames = [
    "HTML & CSS Masterclass",
    "JavaScript Essentials",
    "ASP.NET Core MVC",
    "Database Systems & SQL"
];

const courseTitleElements = document.querySelectorAll(".course-name");

courseTitleElements.forEach((element, index) => {
    if (courseNames[index]) {
        element.innerHTML = courseNames[index];
    }
});

// Bài 2: Right Aside - Array + for...of 
const quickCourses = [
    "HTML & CSS",
    "JavaScript",
    "ASP.NET Core",
    "SQL Server",
    "ReactJS (New)"
];

function showQuickCourses() {
    const quickCourseListEl = document.getElementById("quick-course-list");

    if (quickCourseListEl) {
        let output = "";
        for (const course of quickCourses) {
            output += `<li>${course}</li>`;
        }
        quickCourseListEl.innerHTML = output;
    }
}

showQuickCourses();

// Bài 3: Course Detail - Object + for...in + onclick
const coursesData = {
    html: {
        name: "HTML & CSS Masterclass",
        duration: "4 Weeks",
        tuition: "$100",
        level: "Beginner"
    },
    js: {
        name: "JavaScript Essentials",
        duration: "6 Weeks",
        tuition: "$150",
        level: "Basic"
    },
    net: {
        name: "ASP.NET Core MVC",
        duration: "8 Weeks",
        tuition: "$200",
        level: "Intermediate"
    },
    db: {
        name: "Database Systems & SQL",
        duration: "5 Weeks",
        tuition: "$150",
        level: "Basic"
    }
};

function showCourseDetail() {
    const selectEl = document.getElementById("select-course");
    const courseDetailEl = document.getElementById("course-detail");

    if (selectEl && courseDetailEl) {
        const selectedKey = selectEl.value;
        const currentCourse = coursesData[selectedKey];

        let output = "<h4>Course Details</h4>";
        for (const key in currentCourse) {
            output += `<p><strong>${key}:</strong> ${currentCourse[key]}</p>`;
        }
        courseDetailEl.innerHTML = output;
    }
}

// Gọi hiển thị mặc định khi vừa mở trang
window.onload = function () {
    showCourseDetail();
};

// Bài 4.2: Hàm tính học phí - switch...case
function getTuition(course) {
    let tuition = 0;
    switch (course) {
        case "HTML & CSS":
            tuition = 100;
            break;
        case "JavaScript":
            tuition = 150;
            break;
        case "ASP.NET MVC":
            tuition = 200;
            break;
        case "Database":
            tuition = 150;
            break;
        default:
            tuition = 0;
            break;
    }
    return tuition;
}

// Bài 4.3: Hàm xử lý dky khóa học - DOM + if...else
function registerCourse() {
    const resultEl = document.getElementById("registration-result");

    const fullName = document.getElementById("fullName").value.trim();
    const email = document.getElementById("email").value.trim();
    const phone = document.getElementById("phone").value.trim();
    const course = document.getElementById("course").value;
    const learningModeEl = document.querySelector('input[name="learningMode"]:checked');

    // Ktra dữ liệu rỗng
    if (!fullName || !email || !phone || !course) {
        if (resultEl) {
            resultEl.innerHTML = `<p style="color: red; font-weight: bold;">Lỗi: Vui lòng nhập đầy đủ thông tin!</p>`;
        }
        return;
    }
    // 2. Kiểm tra họ tên hợp lệ
    const nameRegex = /^[a-zA-ZÀ-ỹ\s]+$/;
    if (!nameRegex.test(fullName)) {
        resultEl.innerHTML = "<p style='color: red;'>Lỗi: Họ tên không được chứa số hoặc ký tự đặc biệt!</p>";
        return;
    }

    // 3. ktra email hợp lệ
    const gmailRegex = /^[a-zA-Z0-9._-]+@gmail\.com$/;
    if (!gmailRegex.test(email)) {
        resultEl.innerHTML = "<p style='color: red;'>Lỗi: Email không hợp lệ (phải kết thúc bằng @gmail.com và không chứa ký tự đặc biệt ngoài . _ -)!</p>";
        return;
    }
    // Ktra sđt đúng 10 chữ số
    const phoneRegex = /^[0-9]{10}$/;
    if (!phoneRegex.test(phone)) {
        if (resultEl) {
            resultEl.innerHTML = `<p style="color: red; font-weight: bold;">Lỗi: Số điện thoại phải gồm đúng 10 chữ số!</p>`;
        }
        return;
    }

    // ktra hình thức học (đánh dấu tick vào ô chọn)
    if (!learningModeEl) {
        if (resultEl) {
            resultEl.innerHTML = `<p style="color: red; font-weight: bold;">Lỗi: Vui lòng chọn hình thức học!</p>`;
        }
        return;
    }

    const learningMode = learningModeEl.value;
    const tuition = getTuition(course);

    // Hiển thị kết quả đăng ký thành công
    if (resultEl) {
        resultEl.innerHTML = `
            <div style="background: #e8f4fd; padding: 15px; border-radius: 6px; margin-top: 15px; border: 1px solid #b6effb;">
                <h4 style="color: #0d6efd; margin-bottom: 10px;">Registration Successful!</h4>
                <p><strong>Student:</strong> ${fullName}</p>
                <p><strong>Email:</strong> ${email}</p>
                <p><strong>Phone:</strong> ${phone}</p>
                <p><strong>Course:</strong> ${course}</p>
                <p><strong>Learning Mode:</strong> ${learningMode}</p>
                <p><strong>Tuition:</strong> $${tuition}</p>
            </div>
        `;
    }
}
// Bài 5 Đánh giá khóa học - getElementsByClassName()
const reviewNames = ["Charlie Puth", "Tran Minh Hieu", "Son Tung MTP"];
const reviewElements = document.getElementsByClassName("review-name");
for (let i = 0; i < reviewElements.length; i++) {
    if (reviewNames[i]) {
        reviewElements[i].innerHTML = reviewNames[i];
    }
}
// Bài 6: Footer - getElementsByTagName()
const footerData = [
    "Email: support@ittraining.edu.vn",
    "Phone: 0909123456",
    "Address: 123 Đường Tên Lửa, Quận Bình Tân, TP.HCM",
    "Copyright © 2026 IT Training Center. All rights reserved."
];

const footer = document.getElementById("footer-info");
if (footer) {
    const footerItems = footer.getElementsByTagName("p");
    for (let i = 0; i < footerItems.length; i++) {
        if (footerData[i]) {
            footerItems[i].innerHTML = footerData[i];
        }
    }
}
// Bài 7 DOM READING - innerHTML, innerText, textContent
function showInnerHTML() {
    const demoEl = document.getElementById("demo-content");
    const resultEl = document.getElementById("content-result");
    if (demoEl && resultEl) {
        resultEl.textContent = demoEl.innerHTML;// Dùng innerHTML nhưng gán vào textContent của dữ liệu đầu ra để hiển thị nguyên dạng chuỗi HTML

    }
}

function showInnerText() {
    const demoEl = document.getElementById("demo-content");
    const resultEl = document.getElementById("content-result");
    if (demoEl && resultEl) {
        resultEl.textContent = demoEl.innerText;// innerText bỏ qua các phần bị ẩn
    }
}

function showTextContent() {
    const demoEl = document.getElementById("demo-content");
    const resultEl = document.getElementById("content-result");
    if (demoEl && resultEl) {
        resultEl.textContent = demoEl.textContent;
    }
}
// Mở rộng câu A – ẩn hiện bảng đánh giá
function toggleReviews() {
    const reviewsContainer = document.getElementById("reviews-container");
    const btnToggle = document.getElementById("btn-toggle-reviews");

    if (reviewsContainer && btnToggle) {
        if (reviewsContainer.style.display === "none") {
            reviewsContainer.style.display = "grid";
            btnToggle.innerText = "Hide Reviews";
        } else {
            reviewsContainer.style.display = "none";
            btnToggle.innerText = "Show Reviews";
        }
    }
}