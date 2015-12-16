$("#tag-btn-control").click(function () {

    var newDiv = document.createElement("div");
    newDiv.setAttribute("class", "form-group")

    var newDivChild = document.createElement("div");
    newDivChild.setAttribute("class", "col-md-offset-2 col-md-7")

    var newInput = document.createElement("input");
    newInput.setAttribute("class", "form-control");
    newInput.setAttribute("type", "text");
    newInput.setAttribute("id", "Tags");
    newInput.setAttribute("name", "Tags");

    newDiv.appendChild(newDivChild);
    newDivChild.appendChild(newInput);

    console.log(newDiv);

    $("#tag-group").append(newDiv);
    $(".form-group:nth-last-child(1)").append($("#tag-btn-control"));
});


function sendImage(file) {

    var formData = new FormData();
    formData.append('file', $('#image-upload')[0].files[0]);
    $.ajax({
        type: 'POST',
        url: window.location.origin + "/Admin/Upload/",
        data: formData,
        success: function(status){
            if (status.Success != false ){
                var path = window.location.origin + "/img/" + status.FileName;
                $('#uploaded-image').attr("src", path);
            }
        },
        processData: false,
        contentType: false,
        error: function () {
            alert("Something went wrong!" + status);
        }
    });
}

//var _url = window.URL || window.webkitURL;
$("#image-upload").on('change', function () {
    var file, img;
    if (file = this.files[0]) {
        img = new Image();
        img.onload = function () {
            sendImage(file);
        };
        img.onerror = function () {
            alert("Not valid file: " + file.type);
        };
        img.src = window.URL.createObjectURL(file);
    }
});

function getFile() {
    $("#image-upload").click();
};


