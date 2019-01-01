﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function dragAndDropPiece(mainContainer, dragPiece, droppableSquares) {
    $(dragPiece).draggable(
        {
            revert: "invalid",
            containment: mainContainer,
            helper: "clone",
            cursor: "move",
            drag: function (event, ui) {
                //$(ui.helper.prevObject).addClass("board-square-highlighted");
            },
            stop: function (event, ui) {
                //$(ui.helper.prevObject).removeClass("board-square-highlighted");
            }
        });

    var i;
    for (i = 0; i < droppableSquares.length; i++) {
        $(droppableSquares[i]).droppable({
            accept: dragPiece,
            activeClass: "board-square-highlighted",
            drop: function (event, ui) {
                acceptDraggablePiece(ui.draggable, $(this).attr("id"));
            }
        });
    }

    function acceptDraggablePiece(item, square) {
        $("#" + square.toString()).append(item);
        $(item).removeClass("board-square-highlighted");
    }
}