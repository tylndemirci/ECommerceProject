// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    var placeHolderElement = $("#PlaceHolderHere");
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data("url");
        var decodeUrl = decodeURIComponent(url);
        $.get(decodeUrl).done(function (data) {
            placeHolderElement.html(data);
            placeHolderElement.find(".modal").modal("show");
        });
    });

    placeHolderElement.on("click", '[data-save="modal"]', function (event) {
        event.preventDefault();
            var form = $(this).parents(".modal").find("form");
            var actionUrl = form.attr("action");
            var sendData = form.serialize();
            $.post(actionUrl, sendData).done(function(data) {
                placeHolderElement.find("modal").modal("hide");
            });
        });
})


 


//jQueryAjaxPost = form => {
//    try {
//        $.ajax({
//            type: 'POST',
//            url: form.action,
//            data: new FormData(form),
//            contentType: false,
//            processData: false,
//            success: function (res) {
//                if (res.isValid) {
//                    $('#view-all').html(res.html)
//                    $('#form-modal .modal-body').html('');
//                    $('#form-modal .modal-title').html('');
//                    $('#form-modal').modal('hide');
//                }
//                else
//                    $('#form-modal .modal-body').html(res.html);
//            },
//            error: function (err) {
//                console.log(err)
//            }
//        })
//        //to prevent default form submit event
//        return false;
//    } catch (ex) {
//        console.log(ex)
//    }
//}