$(function () {
    $('#birthday, #dateOfIssue').datepicker({
        format: 'dd-mm-yyyy'
    });

    validationMethod();
    fillVal();
    $('#save').on('click', function () {
        if ($('#editing').valid()) {
            postData();
            $('#save').notify("Đã lưu lại thông tin dù bạn có thay đổi bất kì nội dung nào hay không", { position: "top", className: "success" });
            setTimeout(() => window.history.back(), 4000);
        }
    })

    //Customize validation method
    $.validator.addMethod("validatePassword", function (value, element) {
        return /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,16}$/i.test(value) || $('#password').val() == "";
    }, "Mật khẩu phải bao gồm số, chữ hoa và chữ thường.");

    $.validator.addMethod("validateName", function (value, element) {
        var name = removeSign($('#fullName').val());
        return /^[a-z][a-z\s]*$/i.test(name) && /\s/.test(name);
    }, "Tên không hợp lệ");

    $.validator.addMethod("validateBirthday", function (value, element) {
        const [day1, month1, year1] = $('#birthday').val().split("-")
        var birthday = new Date(year1, month1 - 1, day1);
        var currentDate = new Date();
        var age = currentDate.getFullYear() - birthday.getFullYear();
        if (birthday > currentDate || age < 15 || year1 < 1800)
            return false;
        else return true;
    }, "Ngày sinh không hợp lệ");

    $.validator.addMethod("validatePhone", function (value, element) {
        var phoneNumber = $('#phoneNumber').val().replace(/\s/g, '');
        phoneNumber = phoneNumber.split('');
        if (phoneNumber[0] == 0 && phoneNumber.length == 10)
            return true;
        else if (phoneNumber[0] === "+" && phoneNumber[1] == 8 && phoneNumber[2] == 4 && phoneNumber.length === 12)
            return true;
    }, "Số điện thoại không hợp lệ.");

    $.validator.addMethod("validateIdentityCard", function (value, element) {
        return $('#identity').val().length == 9 || /^[0-9]+$/.test(value)
    }, "Số chứng minh nhân dân không hợp lệ.");


    $.validator.addMethod("validateDateOfIDCard", function (value, element) {
        const [day1, month1, year1] = $('#birthday').val().split("-")
        var birthday = new Date(year1, month1 - 1, day1);
        const [day2, month2, year2] = $('#dateOfIssue').val().split("-")
        var idDate = new Date(year2, month2 - 1, day2);

        if (idDate.getFullYear() - birthday.getFullYear() < 15)
            return false;
        else return true;
    }, "Ngày cấp không hợp lệ.");

    $.validator.addMethod("validatePlaceOfIDCard", function (value, element) {
        return ($("#placeOfIssue").val() != 0)
    }, "Ngày cấp không hợp lệ.");
})

function fillVal() {
    var username = location.pathname.substring(location.pathname.lastIndexOf('/') + 1);
    $.get('/user/GetInfo/' + username, function (user) {
        $('#userName').val(user.userName);
        //$('#password').val(user.password);
        $('#fullName').val(user.fullName);

        var birthday = formatDate(user.birthday);
        $('#birthday').val(birthday);
        $('#phoneNumber').val(user.phoneNumber);
        $('#identity').val(user.identity);

        var dateOfIssue = formatDate(user.dateOfIssue);
        $('#dateOfIssue').val(dateOfIssue);
        $('#placeOfIssue').val(user.placeOfIssue);
        $('#address').val(user.address);
        $('#email').val(user.email);
        $('#role').val(user.role);
        $('#rolePersonal').val(user.rolePersonal);
        $('#organization').val(user.organization);
    })
}

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [day, month, year].join('-');
}

function validationMethod() {
    $('#editing').validate({
        onkey: true,
        rules: {
            userName: {
                required: true,
                rangelength: [5, 16]
            },
            password: {
                rangelength: [8, 16],
                validatePassword: true
            },
            rePassword: {
                required: true,
                equalTo: password,
                minlength: 8
            },
            fullName: {
                required: true,
                validateName: true
            },
            birthday: {
                required: true,
                validateBirthday: true
            },
            phoneNumber: {
                required: true,
                validatePhone: true
            },
            identity: {
                required: true,
                rangelength: [9, 9]
            },
            dateOfIssue: {
                required: true,
                validateDateOfIDCard: true
            },
            placeOfIssue: {
                validatePlaceOfIDCard: true
            },
            address: {
                required: true
            },
            email: {
                email: true
            }
        },
        messages: {
            userName: {
                required: "Vui lòng nhập tài khoản người dùng.",
                rangelength: "Chỉ chứa 5 đến 16 kí tự, không kí tự đặc biệt & khoảng trắng."
            },
            password: {
                rangelength: "Mật khẩu chỉ chứa 8-16 kí tự."
            },
            rePassword: {
                required: "Vui lòng nhập lại mật khẩu đã điền vào trước đó.",
                equalTo: "Mật khẩu chưa trùng khớp.",
                minlength: "Mật khẩu chưa trùng khớp."
            },
            fullName: {
                required: "Vui lòng nhập đầy đủ họ tên."
            },
            birthday: {
                required: "Vui lòng nhập ngày sinh."
            },
            phoneNumber: {
                required: "Vui lòng nhập số điện thoại."
            },
            identity: {
                required: "Vui lòng nhập số chứng minh nhân dân.",
                rangelength: "Số chứng minh nhân dân không hợp lệ."
            },
            dateOfIssue: {
                required: "Ngày cấp không hợp lệ."
            },
            placeOfIssue: {
                required: "Nơi cấp không hợp lệ."
            },
            address: {
                required: "Vui lòng điền đầy đủ thông tin địa chỉ."
            },
            email: {
                email: "Email không hợp lệ."
            }
        },

        errorPlacement: function (error, element) {
            var place = $(element).parents('.form-group').children('.error-placement');
            error.appendTo(place);
        },
    })
}

function postData() {
    var user = {
    };

    $("#editing input, #editing select, #editing texarea").each(function () {


        var propertyName = $(this).attr("name");

        user[propertyName] = $(this).val();

        if ($(this).attr("name") === 'birthday' || $(this).attr("name") === 'dateOfIssue') {
            user[propertyName] = convertDate(user[propertyName]);
        }
    })

    $.post("/user/Edituser", user, function () {

    })
}

function convertDate(date) {
    return date.split("-").reverse().join("-")
}

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