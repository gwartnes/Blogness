﻿$("#tag-btn-control").click(tagRows);

var tagRows = function () {

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
}