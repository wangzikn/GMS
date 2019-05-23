$(function () {

    $("#chontatcaquyen").click(function () {
        $('#1 input:checkbox').not(this).prop('checked', this.checked);
    });

    $("#chontatcaphanquyen").click(function () {
        $('#2 input:checkbox').not(this).prop('checked', this.checked);
    });

    $("#chontatcaquyencayxanh").click(function () {
        $('#3 input:checkbox').not(this).prop('checked', this.checked);

    });

    $("#checkAll").click(function () {
        $('#usertable input:checkbox').not(this).prop('checked', this.checked);
    });

    $('button.btn-success').click(function () { btnclick() });

    $("button.btn-warning").click(function () {
        $('input:checkbox').prop('checked', false);
    });


    renderUser();



    $("#searchFilter").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#usertablebody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

    //$('#save').notify("Đã lưu lại thông tin !!", { position: "top", className: "success" });
    //setTimeout(() => location.reload(), 3000);


});

function removeSign(str) {
    var AccentsMap = [
        "aàảãáạăằẳẵắặâầẩẫấậ",
        "AÀẢÃÁẠĂẰẲẴẮẶÂẦẨẪẤẬ",
        "dđ", "DĐ",
        "eèẻẽéẹêềểễếệ",
        "EÈẺẼÉẸÊỀỂỄẾỆ",
        "iìỉĩíị",
        "IÌỈĨÍỊ",
        "oòỏõóọôồổỗốộơờởỡớợ",
        "OÒỎÕÓỌÔỒỔỖỐỘƠỜỞỠỚỢ",
        "uùủũúụưừửữứự",
        "UÙỦŨÚỤƯỪỬỮỨỰ",
        "yỳỷỹýỵ",
        "YỲỶỸÝỴ"
    ];
    for (var i = 0; i < AccentsMap.length; i++) {
        var re = new RegExp('[' + AccentsMap[i].substr(1) + ']', 'g');
        var char = AccentsMap[i][0];
        str = str.replace(re, char);
    }
    return str;
}

function renderUser() {
    $.get('/user/information', function (users) {
        var html = users.map((user, index) =>
            `<tr class="content">
        <td> <input type="checkbox" name="trangthai" value="${user.id}" id="tt${index + 1}"></td>
        <td>${index + 1}</td>
        <td><a href="javascript:void(0)" class="edit" value="${user.id}" name="tt${index + 1}">${user.userName}</a></td>
        <td>${user.fullName}</td>
        <td>${user.phoneNumber}</td>
        <td>${user.address}</td>
        </tr>`).join('');
        $('tbody').html(html);
        if (users.length > 0)
            paging();
        
        $(".edit").on("click", function () {
            var val = $(this).attr('value');
            var id = $(this).attr('name');
            $('input:checkbox').prop('checked', false);
            document.getElementById(id).click();
            getRoleById(val);
        });
    })

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


function btnclick() {
    var userId = [];
    var userRole;
    var decentralRole;
    var treeRole;
    $.each($("input[name='qlnguoidung']:checked"), function () {
        userRole |= ($(this).val());
    });
    $.each($("input[name='dqphanquyen']:checked"), function () {
        decentralRole |= ($(this).val());
    });
    $.each($("input[name='qlcayxanh']:checked"), function () {
        treeRole |= ($(this).val());
    });
    $.each($("input[name='trangthai']:checked"), function () {
        userId.push($(this).val());
    });

    //setTimeout(function () { }, 3000);

    var user = [];
    userId.forEach(userId => {
        user.push(new User(userId, userRole, decentralRole, treeRole));
    });


    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '/user/addrole',
        data: JSON.stringify(user),
        success: function () {
            $('#result').html('"PassThings()" successfully called.');
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
    $('#chonquyen input:checkbox').prop('checked', false);

    $('#save').notify("Đã lưu lại thông tin !!", { position: "top", className: "success" });
    setTimeout(() => location.reload(), 1500);

}

function User(userId, userRole, decentralRole, treeRole) {
    //var role = [userRole, decentralRole, treeRole],
    this.UserId = userId
    if (userRole > 0) {
        this.UserRole = userRole
    } else this.userRole = 0;
    if (treeRole > 0) {
        this.TreeCatalogRole = treeRole
    } else this.treeRole = 0;
    if (decentralRole > 0) {
        this.DecentralizationRole = decentralRole
    } else this.decentralRole = 0;



}

function getRoleById(id) {

    $.get('/user/getId?id=' + id, function (data) {
        var userRole = ["quyentao", "quyenxoa", "quyenxem", "quyensua"];
        var decel = ["dqphanquyen"];
        var treeRole = ["themcaymoi", "xoacay", "xemdscay", "suathongtincay"];
        var i = 1
        var j = 0
        for (; i < 16;) {
            if ((data[0] & i) != 0) {
                document.getElementById(userRole[j]).click();
            }
            if ((data[2] & i) != 0) {
                document.getElementById(treeRole[j]).click();
            }
            i = i << 1;
            j++;
        }
        if ((data[1] & 1) != 0)
            document.getElementById(decel[0]).click();
        if (data[0] == 15)
            document.getElementById("chontatcaquyen").click();
        if (data[2] == 15)
            document.getElementById("chontatcaquyencayxanh").click();
    })
}
