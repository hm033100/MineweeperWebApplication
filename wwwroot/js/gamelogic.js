var gamestart = false;
var sec = 0;
function pad(val) { return val > 9 ? val : "0" + val;}

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

function saveGame() {
    $.ajax({
        url: '/Game/SaveGame',
        data: {
            time: sec
        },
        success: function (res) {
            window.location.href = '../Game/SavedGames'
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
        if (!gamestart) {
            gamestart = true;
        }
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
    $(document).on('mousedown', '#saveButton', function (event) {
        event.preventDefault();
        switch (event.which) {
            case 1:
                console.log("HTML");
                $("#gameBoard").addClass("pause-game");
                $('#myModal').modal('show');
                gamestart = false;
                break;
        }
    });
    $(document).on('mousedown', '#popup-close', function (event) {
        event.preventDefault();
        switch (event.which) {
            case 1:
                $('#myModal').modal('toggle');
                $("#gameBoard").removeClass("pause-game");
                gamestart = true;
                break;
        }
    });
    $(document).on('mousedown', '#popup-confirm', function (event) {
        event.preventDefault();
        switch (event.which) {
            case 1:
                saveGame();
                $('#myModal').modal('toggle');
                break;
        }
    });
});

setInterval(function () {
    if (gamestart) {
        $("#seconds").html(pad(++sec % 60));
        $("#minutes").html(pad(parseInt(sec / 60, 10)));
    }
}, 1000);