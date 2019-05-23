$(function () {
    $('.exit').on('click', function () {
        $.get('/account/logout', function () {

        })
    })
});
