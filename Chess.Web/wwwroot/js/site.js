// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function highlightSquares(squareIds) {
    var highlightClass = "board-square-highlighted";
    var lightClass = "board-square-white";
    var darkClass = "board-square-black";

    var i;
    for (i = 0; i < squareIds.length; i++) {
        var element = document.getElementById(squareIds[i]);

        element.removeClass(lightClass);
        element.addClass(highlightClass);

        if (element.classList.contains(lightClass)) {
            element.removeClass(lightClass);
            element.addClass(highlightClass);
        }
        else if (element.classList.contains(darkClass)) {
            element.removeClass(darkClass);
            element.addClass(highlightClass);
        }
        else if (element.classList.contains(highlightClass)) {
            element.removeClass(highlightClass);
            element.addClass(highlightClass);
        }
    }
}

function dragAndDropPiece(mainContainer, dragPiece, droppableSquares) {
    $(dragPiece).draggable(
        {
            revert: "invalid",
            containment: mainContainer,
            helper: "clone",
            cursor: "move",
            drag: function (event, ui) {
                $(ui.helper.prevObject).addClass("board-square-highlighted");
            },
            stop: function (event, ui) {
                $(ui.helper.prevObject).removeClass("board-square-highlighted");
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