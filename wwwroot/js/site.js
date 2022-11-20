// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#imgFormInput').change(() => {
    const file = $("#imgFormInput").prop('files')[0];
    if(file) {
        $("#imgRenderView").attr("src", URL.createObjectURL(file));
        $("#imgRenderView").removeClass("visually-hidden");
        
    }
});

function doDeleteImg() {
    var imgName = $('#imgNameEdit').val();
    $('#imgNameEdit').attr('value', "delete:" + imgName);
    $("#imgRenderView").addClass("visually-hidden");
}