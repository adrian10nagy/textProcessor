window.onload = function () {

    $(function () {
        $(".draggable").draggable(
            { revert: true }
        );

        $(".droppable").droppable({
            drop: function (event, ui) {
                var draggableVal = ui.draggable.text().trim();
                var droppableVal = parseInt($(this).text().replace('?', '').replace('(', '').replace(')', ''));
                var mainGameWordVal = document.getElementById("maingamewordhiddenchar" + droppableVal).value.toUpperCase();
                if (draggableVal == mainGameWordVal) {
                    $(this).text() = draggableVal
                    $(this).droppable("destroy");
                }
            },
        });
    });
};

function GameGuessWordAddChar(char) {
    isCorrectChar(char);
}

function isCorrectChar(char) {
    var nrMissingChars = document.getElementById("gameGuessMissingChars").value;
    var gameGuessWordPositionKeeper = document.getElementById("gameGuessWordPositionKeeper").value;
    var gameGuessWordCorrect = document.getElementById("gameGuessWordCorrect").value;
    
    var missingChars = nrMissingChars.split(',');
    var index;
    for (index = 0; index < missingChars.length; ++index) {
        if (gameGuessWordPositionKeeper == missingChars[index]) {
            var it = document.getElementById("maingamewordhiddenchar" + gameGuessWordPositionKeeper);

            if (it.value == char.toLowerCase()) {
                document.getElementById("maingamewordchar" + gameGuessWordPositionKeeper).innerHTML = it.value;
                document.getElementById("mainFeedback").innerHTML = "Correct! <b>" + gameGuessWordCorrect + "</b> este cuvântul căutat! <b>" + it.value
                    + " </b> este litera lipsă <a href='~/Game/GuessWord'><button type='submit' class='btn btn-default'>Joc nou!</button></a>";
            }
            else
            {
                document.getElementById("mainFeedback").innerHTML = "Incorrect! <b>" +  it.value + "</b> nu este litera căutată, mai încearcă";
            }
        }
    }
}
