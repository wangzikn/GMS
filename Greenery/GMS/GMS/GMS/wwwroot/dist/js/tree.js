$(function () {
    //renderTree();
    
    $("#save").on('click', function () {
        getImage();

    })

    

});

function getImage() {
    var img;
    let _this = $('input[type=file]')[0];
    let treeName = $('#tencay-input').val();
    let scientificName = $('#tentienganh-input').val();
    let f = new FormData();
    f.append('File', _this.files[0]);
    f.append('TreeName', treeName);
    f.append('ScientificName', scientificName);
    let onSuccess = function (model) {
        img = (model.file.fileName);
        // add tree
        name = (model.treeName);
        scientificName = (model.scientName);

        window.open('/images/'+img);
        console.log(img);
        console.log(name);
        console.log(sciname);
    };

    $.ajax({
        method: 'POST',
        url: '/tree/UploadAsync',
        data: f,
        processData: false,
        contentType: false
    }).done(onSuccess);

    $('#save').notify("Đã lưu lại thông tin !!", { position: "top", className: "success" });
    setTimeout(() => location.reload(), 2000);
};


function Tree(img, treeName, scientName) {
    this.img = img,
        this.treeName = treeName,
        this.scientName = scientName
}
//$(document).ready(function () {
//    let onchange = function () {
//        let _this = $(this)[0];
//        let f = new FormData();
//        f.append('File', _this.files[0]);
//        let onSuccess = function (model) {
//            console.log(model);
//        };

//        $.ajax({
//            method: 'POST',
//            url: '/tree/upload',
//            data: f,
//            processData: false,
//            contentType: false
//        }).done(onSuccess);
//    };
//    $(document).on('change', 'input[type=file]', onchange);
//});


//function renderTree() {
//    $.get('/tree/information', function (trees) {
//        console.log(trees)
//        var html = trees.map((tree, index) =>
//            `<tr class="content">
//        <td> <input type="checkbox" name="trangthai" value="${tree.Id}"></td>
//        <td>${index + 1}</td>
//        <td><img src="${tree.Url}"></img></td>
//        <td><a href="/User/Edit/${tree.TreeName}" class="edit">${tree.TreeName}</a></td>
//        <td>${user.ScitificName}</td>
        
//        </tr>`).join('');
//        setTimeout(function () { }, 3000);
//        $('tbody').html(html);
//    });
//}