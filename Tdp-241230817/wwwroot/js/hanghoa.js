// Câu 4: bấm vào loại hàng trên thanh điều hướng -> nạp danh sách bằng AJAX
$(function () {
    $('.menu').on('click', 'a.menu-loai', function (e) {
        var $area = $('#ds-hanghoa');
        if ($area.length === 0) return;      // đang ở trang khác (Create) -> cho link chạy bình thường

        e.preventDefault();
        var $a = $(this);

        $('.menu li').removeClass('active');
        $a.parent().addClass('active');

        // .load() gửi header X-Requested-With => controller trả PartialView
        $area.load($a.attr('href'));
    });
});
