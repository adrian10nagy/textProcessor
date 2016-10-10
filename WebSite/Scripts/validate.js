//$("#btnCheckWords").on("click", function () {
//    AjaxUpdateWordsExistance();
//});

//function getWordDisplayed(){
//    alert("function btnWordsSuggestions");
//};

//function getWordSugestions() {
//    alert("get w. suggestions");
//}

//function setWordCorrect(pThis) {
//    $.ajax({
//        url: "/Word/AsyncAddWordToExistance",
//        type: "POST",
//        data: { wordText: (pThis) },
//        error: function (xhr) {
//            alert("error")
//        },
//        success: function (xhr) {
//            alert(xhr.wordId);
//            $("#AddWordDetailsForm").dialog({
//                autoOpen: true,
//                position: { my: "center", at: "top+450", of: window },
//                width: 700,
//                resizable: false,
//                title: pThis,
//                modal: true,
//                open: function () {
//                    $(this).load("Word/AddWordDetailsPV");
//                },
//                buttons: {
//                    "Trimite": function () {
//                        addWordInfo(xhr.wordId);
//                        $(this).dialog("close");
//                    },
//                    Cancel: function () {
//                        $(this).dialog("close");
//                    }
//                }
//            });
//        }
//    })
//    .done(function (partialViewResult) {
//        AjaxUpdateWordsExistance();
//    });
//}



//function AjaxUpdateWordsExistance() {
//    var vMainText = $("#textareaMain").val();

//    $.ajax({
//        url: "/Home/AsyncUpdateWordsExistance",
//        type: "POST",
//        data: { mainText: (vMainText) },
//        error: function (xhr) {
//            alert("error")
//        }
//    })
//    .done(function (partialViewResult) {
//        $("#notExistingWords").html(partialViewResult);
//    });
//}


//function addWordInfo(pWordId) {

//    var wordTypeId = $('#addWordDetailsType').val();

//    switch (wordTypeId) {
//        case '1': {
//            var nounWord = {
//                WordId: '',
//                Case: '',
//                Multiplicity: '',
//                Proper: '',
//                Gender: '',
//            };

//            nounWord.Case = $('#addWordDetailsCase').val();;
//            nounWord.Multiplicity = $('#addWordDetailsMultiplicity').val();
//            nounWord.Proper = $('#addWordDetailsProper').val();
//            nounWord.Gender = $('#addWordDetailsGender').val();
//            nounWord.WordId = pWordId;

//            $.ajax({
//                url: "Word/AjaxAddWordDetailsNoun",
//                type: 'POST',
//                data: nounWord,
//                success: function (data) {
//                }
//            });
//            break;
//        }
//        case '2': {
//            var verbWord = {
//                Time: '',
//                PersonaNumber: '',
//                Multiplicity: '',
//            };

//            verbWord.Time = $('#addWordDetailsTime').val();;
//            verbWord.PersonaNumber = $('#addWordDetailsPerson').val();
//            verbWord.Multiplicity = $('#addWordDetailsMultiplicity').val();

//            $.ajax({
//                url: "Word/AjaxAddWordDetailsVerb",
//                type: 'POST',
//                data: verbWord,
//                success: function (data) {
//                    alert("success");
//                    //if (data) {
//                    //    $(':input', '#AddWordDetails')
//                    //      .not(':button, :submit, :reset, :hidden')
//                    //      .val('')
//                    //      .removeAttr('checked')
//                    //      .removeAttr('selected');
//                    //}
//                }
//            });
//            break;
//        }
//        case '3': {
//            alert("Not implemented yet")
//            break;
//        }
//        case '4': {
//            alert("Not implemented yet")
//            break;
//        }
//    }

   
//}