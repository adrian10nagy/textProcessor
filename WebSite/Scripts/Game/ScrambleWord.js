$("#resetWord").click(function () {
    resetWord();
});


function resetWord() {
    var y = document.getElementById("maingameWordKeeper");

    for (var i = 0; i < y.value.length; i++) {
        document.getElementById("maingamewordchar" + i).innerHTML = "";
    }
    document.getElementById("maingameWordKeeper").value = "";
}

function addTofinalWord(char) {
    var wordKeeper = document.getElementById("maingameWordKeeper");
    var wordOriginal = document.getElementById("maingameWordOriginal");

    if (wordKeeper.value.length == wordOriginal.value.length) {
        resetWord();
    }

    document.getElementById("maingamewordchar" + wordKeeper.value.length).innerHTML = char;
    wordKeeper.value = wordKeeper.value + char;


    //check if word is finish in size
    if (wordOriginal.value.length == wordKeeper.value.length) {
        if (wordOriginal.value == wordKeeper.value) {
            correctWord();

        } else {
            resetWord();
            alert("reset")
        }

    }
}

function correctWord() {
    $.ajax({
        url: "/Game/ScrambleWordCorrect",
        type: 'POST',
        success: function (xhr) {
            alert("success");
            $("#scrambleWordModal").dialog({
                autoOpen: true,
                position: { my: "center", at: "top+300", of: window },
                width: 700,
                resizable: false,
                title: "Pasul următor",
                modal: true,
                open: function () {
                    $(this).load("/Game/_ScrambleWordCorrectAnswer");
                },
                buttons: {
                    "Închide": function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
    })
};