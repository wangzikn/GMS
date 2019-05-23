$(function () {
    $('#submit').on('click', function () {
        if (getData())
            $(location).prop('href', '/user')
    })
    
  

})

function getdata() {
    var UserName = $('#Username').val();
    var Password = $('#Password').val();

    $.get('/account/login', users, function () {
        users.find(user => user.UserName === UserName && user.Password === Password);
        return true;
    } )
}