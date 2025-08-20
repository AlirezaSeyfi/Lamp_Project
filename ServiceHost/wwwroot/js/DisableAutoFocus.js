$(document).ready(function () {
    // غیرفعال کردن فوکوس خودکار در تمام فرم‌ها
    $.validator.setDefaults({
        focusInvalid: false,
        focusCleanup: true // پاک کردن فوکوس پس از اعتبارسنجی
    });

    // اطمینان از عدم فوکوس پس از ارسال فرم AJAX
    $("form[data-ajax='true']").on("submit", function () {
        $(".form-control").blur(); // حذف فوکوس از تمام فیلدها
    });
});
