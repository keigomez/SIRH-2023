
$(document).ready(function () {
    var config = {
        locale: 'es-es',
        uiLibrary: 'bootstrap4',
        format: 'dd/mm/yyyy',
        maxDate: new Date(),
    }

    $('#datepickerEmision').datepicker(config);
    $('#datepickerVence').datepicker(config);

    var now = new Date();
    var day = now.getDate();
    var month = now.getMonth() + 1;
    if (month < 10) month = "0" + month;
    var year = now.getFullYear();
    $('#datepickerEmision').val(day + '/' + month + '/' + year);
    $('#datepickerVence').val(day + '/' + month + '/' + year);

 
});


function ValidarCheck(elem) {
    console.log(elem);
    if (document.getElementById(elem.id).checked) {
        $("#" + elem.id + "-anterior").show();
        $("#" + elem.id + "-actual").show();
    } else {
        $("#" + elem.id + "-anterior").hide();
        $("#" + elem.id + "-actual").hide();
    }
    $("#" + elem.id + "-anterior").val("");
    $("#" + elem.id + "-actual").val("");
}