$(function () {
    //showdDependentDropDown();
    validationMethod();

    $("#submit").on("click", function () {
        if ($('#registration').valid()) {
            postData();
            $('#submit').notify("Tạo tài khoản thành công", { position: "top", className: "success" });
            setTimeout(() => window.location = ("/user/index"), 4000);
        }
    })

    $('#birthday, #dateOfIssue').datepicker({
        format: 'dd-mm-yyyy'
    });

    $.validator.addMethod("validatePassword", function (value, element) {
        return /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,16}$/i.test(value);
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
    }, "Nơi cấp không hợp lệ.");


})

//function showdDependentDropDown() {
//    var locationObject = {
//        "Sài Gòn": {
//            "Quận 1": ["Bến Thành", "Đa Kao", "Phạm Ngũ Lão"],
//            "Quận 7": ["Phú Mỹ", "Phú Nhuận"],
//            "Quận 10": ["Phường 1", "Phường 2"],
//        },
//        "Hà Nội": {
//            "Đống Đa": ["Quốc Tử Giám", "Văn Miếu"],
//            "Hoàn Kiếm": ["Hàng Bông", "Hàng Mã"]
//        }
//    }

//    var provinceSel = document.getElementById("province"),
//        districtSel = document.getElementById("district"),
//        wardSel = document.getElementById("ward");
//    for (var province in locationObject) {
//        provinceSel.options[provinceSel.options.length] = new Option(province, province);
//    }
//    provinceSel.onchange = function () {
//        districtSel.length = 1; // remove all options bar first
//        wardSel.length = 1; // remove all options bar first
//        if (this.selectedIndex < 1) return; // done 
//        for (var district in locationObject[this.value]) {
//            districtSel.options[districtSel.options.length] = new Option(district, district);
//        }
//    }
//    provinceSel.onchange(); // reset in case page is reloaded
//    districtSel.onchange = function () {
//        wardSel.length = 1; // remove all options bar first
//        if (this.selectedIndex < 1) return; // done 
//        var ward = locationObject[provinceSel.value][this.value];
//        for (var i = 0; i < ward.length; i++) {
//            wardSel.options[wardSel.options.length] = new Option(ward[i], ward[i]);
//        }
//    }
////}


function validationMethod() {
    $('#registration').validate({
        onkey: true,
        rules: {
            userName: {
                required: true,
                rangelength: [5, 16]
            },
            password: {
                required: true,
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
                required: "Vui lòng nhập mật khẩu.",
                rangelength: "Mật khẩu phải chứa ít nhất 8 kí tự."
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


//$.post('/user/AddUserAsync', {userName: 'fasdfad', password: '~Aasdf'}, function(){})


// Read data
function postData() {
    var user = {
    };

    $("#registration input, #registration select, #registration texarea").each(function () {


        var propertyName = $(this).attr("name");

        user[propertyName] = $(this).val();

        if ($(this).attr("name") === 'birthday' || $(this).attr("name") === 'dateOfIssue') {
            user[propertyName] = convertDate(user[propertyName]);
        }
    })

    $.post("/user/AddUser", user, function () {

    })
    //fail - notify

    // $.post('some.php', {name: 'John'})
    // .done(function(msg){  })
    // .fail(function(xhr, status, error) {
    //     // error handling
    // });
}

function convertDate(date) {
    return date.split("-").reverse().join("-")
}


function rand(min, max) {
    return Math.floor((max - min) * Math.random());
}

function genTest() {
    $('#userName').val('testusername' + rand(1000, 1000000));
    $('#password').val('Abcde12345@');
    $('#fullName').val('Abc def');
    $('#birthday').val("20-02-1990");
    $('#phoneNumber').val("0909990009");
    $('#identity').val("123456789");
    $('#dateOfIssue').val("20-02-2019");
    $('#numOfHouse').val("12e");
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
