function renderFlag(cell) {
    $.ajax({
        url: '/Board/RightClick',
        data: {
            rowAndColumn: cell
        },
        success: function (res) {
            console.log(cell);
            $("#" + cell).html(res);
            console.log(res);
        }
    });
};

function renderCell(cell) {
    $.ajax({
        url: '/Board/HandleButtonClick',
        data: {
            rowAndColumn: cell
        },
        success: function (res) {
            console.log(cell);
            $("#gameBoard").html(res.boardString);
            if (res.gameWon) {
                gameWon();
            }
            else if (res.gameLost) {
                gameLost();
            }
            console.log(res);
        }
    });
};

function gameWon() {
    alert("You have won the game! :)");
};

function gameLost() {
    alert("Sorry, you Lost!");
};

$(document).contextmenu(function () {
    return false;
});

$(function () {
    $(document).on('mousedown', '.game-button', function (event) {
        event.preventDefault();
        console.log("Inside Click");
        switch (event.which) {
            case 1:
                renderCell($(this).prop("value"));
                break;
            case 3:
                renderFlag($(this).prop("value"));
                break;
        }
    });
});