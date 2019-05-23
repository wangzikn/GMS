$(function () {
    $.get("/area/gettree", function (data) { createJSTree(data) })
    search();
    save();
})

function createJSTree(data) {
    var data = $('#jstree-div').jstree({
        'core': {
            'data': data,
            "check_callback": true
        },
        "types": {
            "default": {
                "icon": "/images/location-logo.png"
            }
        },
        "plugins": ["wholerow", "sort", "types", "contextmenu", "search", "unique"]
        //"plugins": ["sort", "types"]
    });

    console.log($("#jstree-div").jstree(true).get_json())

};
function search() {
    $("#search-form").submit(function (e) {
        e.preventDefault();
        $("#jstree-div").jstree(true).search($("#searchInput").val());
    });
}

function save() {
    $('#save').on('click', function () {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '/area/SaveTree',
            data: JSON.stringify($("#jstree-div").jstree(true).get_json()),
            success: function () {
                $('#result').html('"PassThings()" successfully called.');
            },
            failure: function (response) {
                $('#result').html(response);
            }
        }); 
    })
}


$('#jstree-div').on("changed.jstree", function (e, data) {
    console.log(data.selected);
});