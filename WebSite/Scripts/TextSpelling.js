$('#textareaMain').keyup(function (e) {
    var editorTextarea = $('#textareaMain');
    checkText(editorTextarea);
});

function checkText(editorTextarea) {
    var previewDiv = $('#previewDiv');

    $.ajax({
        url: "/Text/AsyncCheckText",
        type: "POST",
        data: { textToCheck: (editorTextarea.val()) }
    })
         .done(function (partialViewResult) {
             previewDiv.html(partialViewResult);
         });
}

$('#mainTextDisplayId').keyup(function (e) {
    var editorTextarea = $('#mainTextDisplayId');
    checkTextSpelling(editorTextarea);
});

function checkTextSpelling(editorTextarea) {
    var previewDiv = $('#previewDiv');

    $.ajax({
        url: "/Text/AsyncCheckTextSpelling",
        type: "POST",
        data: { textToCheck: (editorTextarea.val()) }
    })
         .done(function (partialViewResult) {
             previewDiv.html(partialViewResult);
         });
}

function btnSpellCheck() {
    var editorTextarea = $('#mainTextDisplayId');
    checkTextSpelling(editorTextarea);
}