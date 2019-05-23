$(function () {
    $('#delete').on('click', function () {
        var selectList = [];
        $.each($("input[name='select']:checked"), function () {
            selectList.push($(this).val());
        });
     

        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '/user/DeleteAsync',
            data: JSON.stringify(selectList),
            success: function () {
                
            },
            failure: function (response) {
                $('#result').html(response);
            }, complete: function (data) {
                window.location.reload();
            }
        }); 
    });
})
