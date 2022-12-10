// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl);
});

var toastElList = [].slice.call(document.querySelectorAll('.toast'));
var toastList = toastElList.map(function (toastEl) {
    return new bootstrap.Toast(toastEl)
});
toastList.forEach(toast => toast.show());

// Write your JavaScript code.
$('#imgFormInput').change(() => {
    const file = $("#imgFormInput").prop('files')[0];
    if(file) {
        $("#imgRenderView").attr("src", URL.createObjectURL(file));
        $("#imgRenderView").removeClass("visually-hidden");
        $("#imgRenderFormGroup").removeClass("visually-hidden");
        $('#imgNameEdit').attr('value', "");
    }
});

function doDeleteImg() {
    var imgName = $('#imgNameEdit').val();
    $('#imgNameEdit').attr('value', "delete:" + imgName);
    $("#imgRenderView").addClass("visually-hidden");
    $("#imgRenderFormGroup").addClass("visually-hidden");
    $("#imgFormInput").val(null);
}

function displayToast(title, message, icon) {
    var htmlData = `<div class="toast" role="alert" data-bs-delay="7500" aria-live="assertive" aria-atomic="true">
                <div class="toast-header">
                    <strong class="me-auto"><i class="fa-solid fa-${icon}"></i> ${title}</strong>
                    <small class="text-muted">just now</small>
                    <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
                <div class="toast-body">${message}</div>
            </div>`;
    $('#toastEvents').append(htmlData);

    /* Show last notif */
    toastElList = [].slice.call(document.querySelectorAll('.toast'));
    toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl)
    });
    toastList[toastList.length - 1].show();
}

$('#colorPickerInput').minicolors({

    // hue, brightness, saturation, or wheel
    control: 'hue',

    // default color
    defaultValue: '',

    // hex or rgb
    format: 'rgb',

    // is inline mode?
    inline: false,

    // a comma-separated list of keywords that the control should accept (e.g. inherit, transparent, initial).
    keywords: '',

    // uppercase or lowercase
    letterCase: 'lowercase',

    // enables opacity slider
    opacity: true,

    // custom position
    position: 'bottom left',

    // additional theme class
    theme: 'bootstrap',

    // an array of colors that will show up under the main color <a href="https://www.jqueryscript.net/tags.php?/grid/">grid</a>
    swatches: [
        '#fff',
        '#000',
        '#f00',
        '#0f0',
        '#00f',
        '#ff0',
        'rgba(0,0,255,0.5)'
    ],

    change: function (value, opacity) {
        if (!value) return;
        if (value.startsWith('rgba')) {
            var data = value.replace(/\s/g, '').slice(5, value.length-1).split(',');
            $('#BrickColorRed').attr('value', data[0]);
            $('#BrickColorGreen').attr('value', data[1]);
            $('#BrickColorBlue').attr('value', data[2]);
            $('#BrickColorAlpha').attr('value', opacity);
        }
    }
});