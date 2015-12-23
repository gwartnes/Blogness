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
    formData.append('__RequestVerificationToken', $('#post-form input[name="__RequestVerificationToken"]').val());
    var objId = $('#Id').val();
    formData.append('id', objId);
    $.ajax({
        type: 'POST',
        url: window.location.origin + "/Admin/Upload/",
        data: formData,
        success: function(status){
            if (status.Success != false ){
                img = new Image();
                img.src = window.location.origin + "/img/" + objId + "/" + status.FileName;
                img.alt = status.FileName;
                img.setAttribute("class", "img-thumbnail col-md-1");
                img.style.marginLeft = "5px;";
                img.style.marginRight = "5px;";
                $('#uploaded-images').append(img);
            }
        },
        processData: false,
        contentType: false,
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong! " + errorThrown);
        }
    });
}

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

$(document).ready(function () {
    $(".post-body p img").addClass("img-responsive");
});


$(document).scroll(function () {

    $('div.post-head:visible:not(:isInView)').css('background-color', 'white');
    $('div.post-head:visible:isInView:not(:first)').css('background-color', 'white');
    $('div.post-head:visible:isInView:first').css('background-color', 'grey');

});


jQuery.extend(jQuery.expr[':'], {
    isInView: function (element) {
        if (typeof jQuery === "function" && element instanceof jQuery) {
            element = element[0];
        }
        var rect = element.getBoundingClientRect();

        var height = $(element).height();

        return (
            rect.top >= 30 - height &&
            rect.left >= 0 &&
            rect.bottom <= (window.innerHeight || $(window).height()) && 
            rect.right <= (window.innerWidth || $(window).width()) 
        );
    }
});
