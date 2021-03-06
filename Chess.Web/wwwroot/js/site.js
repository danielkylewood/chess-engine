﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function dragAndDropPieces(validMoves, uniqueSquares) {
    // Tag pieces with valid squares
    validMoves.forEach(function (validMove) {
        var i;
        var pieceId = document.getElementById(validMove.PiecePosition.substring(1));
        for (i = 0; i < validMove.PieceMoves.length; i++) {
            pieceId.classList.add(validMove.PieceMoves[i].substring(1));
        }
    });

    // Set pieces to be draggable
    validMoves.forEach(function(validMove) {
        $(validMove.PiecePosition).draggable(
            {
                revert: "invalid",
                containment: "#main-container",
                cursor: "move",
                zIndex: "1000",
                drag: function(event, ui) {
                },
                stop: function(event, ui) {}
            });
    });

    // Set squares to accept tagged pieces
    uniqueSquares.forEach(function (uniqueSquare) {
        $(uniqueSquare).droppable({
            accept: ("." + uniqueSquare.substring(1)),
            activeClass: "board-square-highlighted",
            drop: function (event, ui) {
                var squareId = $(this).attr("id").toString();
                acceptDraggablePiece(ui.draggable, squareId);
            }
        });

        function acceptDraggablePiece(item, square) {
            var endId = square.substring(7);
            var startId = item.attr("id").toString().substring(6);
            var moveRequest = { Start: startId, End: endId };
            
            $.ajax({
                contentType: "application/json",
                data: JSON.stringify(moveRequest),
                type: "PUT",
                url: "/game/movepiece"
            })
            .always(function (partialViewResult) {
                $("#board-view").html(partialViewResult);
            });
        }
    });
}