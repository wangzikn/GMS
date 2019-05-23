
$(function () {

    $.get('/user/information', function (users) {
        renderUser(users)
    })

    $("#selectAll").click(function () {
        $('input:checkbox').not(this).prop('checked', this.checked);
    });

    $('#delete').prop('disabled', true);

    $("table").on("click", "input[name=select]", function () {
        if ($("input[name=select]:checked").length >0) {
            $('#delete').prop('disabled', false);
        } else $('#delete').prop('disabled', true);
    })
    
});

function renderUser(users) {
    var html = users.map((user, index) =>
        `<tr class="content">
        <td> <input type="checkbox" name="select" value="${user.id}"></td>
        <td>${index + 1}</td>
        <td><a href="/User/Edit/${user.userName}" class="edit">${user.userName}</a></td>
        <td>${user.fullName}</td>
        <td>${user.phoneNumber}</td>
        <td>${user.address}</td>
        </tr>`).join('');

    $('tbody').html(html);
    if (users.length > 0)
        paging();
};


function paging() {
    var pageSize = 6;
    var pagesCount = $(".content").length;
    var totalPages = Math.ceil(pagesCount / pageSize);
    $('#paging').twbsPagination({
        totalPages: totalPages,
        visiblePages: 3,
        next: 'Trang sau',
        prev: 'Trang trước',
        first: 'Trang đầu',
        last: 'Trang cuối',
        onPageClick: function (event, page) {
            //fetch content and render here
            showPage = function () {
                $(".content").hide().each(function (n) {
                    if (n >= pageSize * (page - 1) && n < pageSize * page)
                        $(this).show();
                });
            }
            showPage();
        }
    });
}