﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//
// $(function () {
//     var placeHolderElement = $("#PlaceHolderHere");
//     $('button[data-toggle="ajax-modal"]').click(function (event) {
//         var url = $(this).data("url");
//         var decodeUrl = decodeURIComponent(url);
//         $.get(decodeUrl).done(function (data) {
//             placeHolderElement.html(data);
//             placeHolderElement.find(".modal").modal('show');
//         });
//     });
//
//     placeHolderElement.on("click", 'button[data-save="modal"]', function (event) {
//         event.preventDefault();
//         var form = $(this).parents(".modal").find("form");
//         var actionUrl = form.attr("action");
//         var sendData = form.serialize();
//         $.post(actionUrl, sendData).done(function (data) {
//             placeHolderElement.find(".modal").modal("hide");
//             $.get("/Order/OrderListInvoke", function (result) {
//                 $("#OrderListContainer").html(result); 
//             });
//         });
//     });
// }) 
