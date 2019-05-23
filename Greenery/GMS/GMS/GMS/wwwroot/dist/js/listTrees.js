$(function () {
    $.get('/tree/TreeInformation', function (trees) {
        renderTree(trees);
    })
    $('#btn-del').on('click', function () { deletree() })


});

function renderTree(trees) {
    var html = trees.map((tree, index) =>
        `<tr class="content">
        <td> <input type="checkbox" name="trangthai" value="${tree.id}"></td>
        <td>${index + 1}</td>
        <td><a href="/user/AddTree/${tree.name}" class="edit" value="${tree.id}">${tree.name}</a></td>
        <td id="${tree.id}">${tree.scientificName}</td>
        <td ><img id ="img${tree.id}" src="${tree.url}"></img></td>     
        </tr>`).join('');
    $('tbody').html(html);
    if (trees.length > 0)
        paging();
}

function deletree() {
    var selectList = [];
    $.each($("input[name='trangthai']:checked"), function () {
        selectList.push($(this).val());
    });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '/tree/deleteTree',
        data: JSON.stringify(selectList),
        success: function () {
            $('#result').html('"PassThings()" successfully called.');
        },
        failure: function (response) {
            $('#result').html(response);
        }, complete: function () {
            setTimeout(() => window.location = "/User/trees", 2000);
        }
    });

};

function paging() {
    var pageSize = 2;
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

