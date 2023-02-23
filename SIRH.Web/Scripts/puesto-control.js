var error = false;

function beginData() {
    $('#error').attr("hidden", "hidden");
    //if ($('#codpuesto').val().length < 1 &&
    //            $('#codclase').val().length < 1 &&
    //            $('#codespecialidad').val().length < 1 &&
    //            $('#codocupacion').val().length < 1) {
    //    $('#error').removeAttr("hidden");
    //    error = true;
    //    $('#error').show();
    //    $('#error').html("Debe digitar al menos un parámetro de búsqueda");
    //    $('#target').hide();
    //    return false;
    //}
    //else {
    //    error = false;
    //    $('#error').hide();
    //    $("#progressbar").removeAttr("hidden");
    //    $('#progressbar').show();

    //    $("#btnBusca").hide();
    //}

    $('#error').hide();
    $("#progressbar").removeAttr("hidden");
    $('#progressbar').show();

    $("#btnBusca").hide();
}

function successData() {
    // Animate
    $('#progressbar').hide();
    $("#btnBusca").show();
    if (!error) {
        $('#target').show();
        error = false;
    }
}


/***************************************************Controles modals*******************************************************/
var errorJugador = false;
var errorClase2 = false;
var errorClase = false;
var errorEspecialidad = false;
var errorEspecialidad2 = false;
var errorOcupacion = false;
var errorSubespecialidad = false;

$(document).ready(function () {
    $('#DialogClase').click(function () {
        //$('#buscar-clase').dialog('open');
        $("#buscar-clase").appendTo("body");
        $('#buscar-clase').modal('show');
        return false;
    });

    $('#DialogClase2').click(function () {
        //$('#buscar-clase').dialog('open');
        $("#buscar-clase2").appendTo("body");
        $('#buscar-clase2').modal('show');
        return false;
    });

    $('#DialogEspecialidad').click(function () {
        //$('#buscar-especialidad').dialog('open');
        $("#buscar-especialidad").appendTo("body");
        $('#buscar-especialidad').modal('show');
        return false;
    });

    $('#DialogEspecialidad2').click(function () {
        //$('#buscar-especialidad').dialog('open');
        $("#buscar-especialidad2").appendTo("body");
        $('#buscar-especialidad2').modal('show');
        return false;
    });

    $('#DialogSubespecialidad').click(function () {
        //$('#buscar-especialidad').dialog('open');
        $("#buscar-subespecialidad").appendTo("body");
        $('#buscar-subespecialidad').modal('show');
        return false;
    });

    $('#DialogOcupacion').click(function () {
        //$('#buscar-ocupacion').dialog('open');
        $("#buscar-ocupacion").appendTo("body");
        $('#buscar-ocupacion').modal('show');
        return false;
    });

    $('#edit_nivel').click(function () {
        //$('#buscar-ocupacion').dialog('open');
        $("#txt_nivel").prop("disabled", false);
        return false;
    });


    $("#DialogClase").button({
        label: 'Buscar por Clase',
        icons: {
            primary: 'ui-icon-search'
        },
        text: false
    })

    $("#DialogClase2").button({
        label: 'Buscar por Clase',
        icons: {
            primary: 'ui-icon-search'
        },
        text: false
    })

    $("#DialogEspecialidad").button({
        label: 'Buscar por Especialidad',
        icons: {
            primary: 'ui-icon-search'
        },
        text: false
    })

    $("#DialogEspecialidad2").button({
        label: 'Buscar por Especialidad',
        icons: {
            primary: 'ui-icon-search'
        },
        text: false
    })

    $("#DialogSubespecialidad").button({
        label: 'Buscar por Subespecialidad',
        icons: {
            primary: 'ui-icon-search'
        },
        text: false
    })

    $("#DialogOcupacion").button({
        label: 'Buscar por Ocupacion real',
        icons: {
            primary: 'ui-icon-search'
        },
        text: false
    })


    $("#CleanClase").button({
        label: 'Limpiar búsqueda',
        icons: {
            primary: 'ui-icon-arrowrefresh-1-w'
        },
        text: false
    }).click(function () {
        $('#codclase').val('');
    })

    $("#CleanClase2").button({
        label: 'Limpiar búsqueda',
        icons: {
            primary: 'ui-icon-arrowrefresh-1-w'
        },
        text: false
    }).click(function () {
        $('#codclase2').val('');
    })

    $("#CleanEspecialidad").button({
        label: 'Limpiar búsqueda',
        icons: {
            primary: 'ui-icon-arrowrefresh-1-w'
        },
        text: false
    }).click(function () {
        $('#codespecialidad').val('');
    })

    $("#CleanEspecialidad2").button({
        label: 'Limpiar búsqueda',
        icons: {
            primary: 'ui-icon-arrowrefresh-1-w'
        },
        text: false
    }).click(function () {
        $('#codespecialidad2').val('');
    })

    $("#CleanSubespecialidad").button({
        label: 'Limpiar búsqueda',
        icons: {
            primary: 'ui-icon-arrowrefresh-1-w'
        },
        text: false
    }).click(function () {
        $('#codsubespecialidad').val('');
    })

    $("#CleanOcupacion").button({
        label: 'Limpiar búsqueda',
        icons: {
            primary: 'ui-icon-arrowrefresh-1-w'
        },
        text: false
    }).click(function () {
        $('#codocupacion').val('');
    })
});


function beginDataClase() {
    if ($('#codigoclase').val().length < 1 &&
         $('#nomclase').val().length < 1) {
        errorClase = true;
        $('#error-clase').removeAttr("hidden");
        $('#error-clase').show();
        $('#error-clase').html("Debe digitar al menos un parámetro de búsqueda");
        $('#target-clase').hide();
    }
    else {
        errorClase = false;
        $('#error-clase').hide();
        $("#progressbarclase").removeAttr("hidden");
        $('#progressbarclase').show();
        
        $("#btnBuscaClase").hide();
    }
}

function beginDataClase2() {
    if ($('#codigoclase2').val().length < 1 &&
        $('#nomclase2').val().length < 1) {
        errorClase2 = true;
        $('#error-clase2').removeAttr("hidden");
        $('#error-clase2').show();
        $('#error-clase2').html("Debe digitar al menos un parámetro de búsqueda");
        $('#target-clase2').hide();
    }
    else {
        errorClase2 = false;
        $('#error-clase2').hide();
        $("#progressbarclase2").removeAttr("hidden");
        $('#progressbarclase2').show();

        $("#btnBuscaClase2").hide();
    }
}

function beginDataEspecialidad() {
    if ($('#codigoEspecialidad').val().length < 1 &&
         $('#nomEspecialidad').val().length < 1) {
        errorEspecialidad = true;
        $('#error-especialidad').removeAttr("hidden");
        $('#error-especialidad').show();
        $('#error-especialidad').html("Debe digitar al menos un parámetro de búsqueda");
        $('#target-especialidad').hide();
    }
    else {
        errorEspecialidad = false;
        $('#error-especialidad').hide();
        $("#progressbarespecialidad").removeAttr("hidden");
        $('#progressbarespecialidad').show();
        
        $("#btnBuscaEspecialidad").hide();
    }
}

function beginDataEspecialidad2() {
    if ($('#codigoEspecialidad2').val().length < 1 &&
        $('#nomEspecialidad2').val().length < 1) {
        errorEspecialidad = true;
        $('#error-especialidad2').removeAttr("hidden");
        $('#error-especialidad2').show();
        $('#error-especialidad2').html("Debe digitar al menos un parámetro de búsqueda");
        $('#target-especialidad2').hide();
    }
    else {
        errorEspecialidad = false;
        $('#error-especialidad2').hide();
        $("#progressbarespecialidad2").removeAttr("hidden");
        $('#progressbarespecialidad2').show(); $("#btnBuscaEspecialidad2").hide();
    }
}


function beginDataSubespecialidad() {
    if ($('#codigoSubespecialidad').val().length < 1 &&
         $('#nomSubespecialidad').val().length < 1) {
        errorSubespecialidad = true;
        $('#error-subespecialidad').removeAttr("hidden");
        $('#error-subespecialidad').show();
        $('#error-subespecialidad').html("Debe digitar al menos un parámetro de búsqueda");
        $('#target-subespecialidad').hide();
    }
    else {
        errorSubespecialidad = false;
        $('#error-subespecialidad').hide();
        $("#progressbarsubespecialidad").removeAttr("hidden");
        $('#progressbarsubespecialidad').show();

        $("#btnBuscaSubespecialidad").hide();
    }
}

function beginDataOcupacion() {
    if ($('#codigoOcupacion').val().length < 1 &&
         $('#nomOcupacion').val().length < 1) {
        errorOcupacion = true;
        $('#error-ocupacion').removeAttr("hidden");
        $('#error-ocupacion').show();
        $('#error-ocupacion').html("Debe digitar al menos un parámetro de búsqueda");
        $('#target-ocupacion').hide();
    }
    else {
        errorOcupacion = false;
        $('#error-ocupacion').hide();
        $("#progressbarocupacion").removeAttr("hidden");
        $('#progressbarocupacion').show();
        
        $("#btnBuscaOcupacion").hide();
    }
}

//$(function () {
//    ////$('#buscar-clase').dialog({
//    ////    autoOpen: false,
//    ////    modal: true,
//    ////    height: 400,
//    ////    width: 600
//    ////});

//    $('#DialogClase').click(function () {
//        //$('#buscar-clase').dialog('open');
//        $("#buscar-clase").appendTo("body");
//        $('#buscar-clase').modal('show');
//        return false;
//    });
//});

//$(function () {
//    //$('#buscar-especialidad').dialog({
//    //    autoOpen: false,
//    //    modal: true,
//    //    height: 400,
//    //    width: 600
//    //});

//    $('#DialogEspecialidad').click(function () {
//        //$('#buscar-especialidad').dialog('open');
//        $("#buscar-especialidad").appendTo("body");
//        $('#buscar-especialidad').modal('show');
//        return false;
//    });
//});

//$(function () {
//    //$('#buscar-ocupacion').dialog({
//    //    autoOpen: false,
//    //    modal: true,
//    //    height: 400,
//    //    width: 600
//    //});

//    $('#DialogOcupacion').click(function () {
//        //$('#buscar-ocupacion').dialog('open');
//        $("#buscar-ocupacion").appendTo("body");
//        $('#buscar-ocupacion').modal('show');
//        return false;
//    });
//});

//$(function () {
//    $("#DialogClase").button({
//        label: 'Buscar por Clase',
//        icons: {
//            primary: 'ui-icon-search'
//        },
//        text: false
//    })
//});

//$(function () {
//    $("#DialogEspecialidad").button({
//        label: 'Buscar por Especialidad',
//        icons: {
//            primary: 'ui-icon-search'
//        },
//        text: false
//    })
//});

//$(function () {
//    $("#DialogOcupacion").button({
//        label: 'Buscar por Ocupación Real',
//        icons: {
//            primary: 'ui-icon-search'
//        },
//        text: false
//    })
//});

//$(function () {
//    $("#CleanClase").button({
//        label: 'Limpiar búsqueda',
//        icons: {
//            primary: 'ui-icon-arrowrefresh-1-w'
//        },
//        text: false
//    }).click(function () {
//        $('#codclase').val('');
//    })
//});

//$(function () {
//    $("#CleanEspecialidad").button({
//        label: 'Limpiar búsqueda',
//        icons: {
//            primary: 'ui-icon-arrowrefresh-1-w'
//        },
//        text: false
//    }).click(function () {
//        $('#codespecialidad').val('');
//    })
//});

//$(function () {
//    $("#CleanOcupacion").button({
//        label: 'Limpiar búsqueda',
//        icons: {
//            primary: 'ui-icon-arrowrefresh-1-w'
//        },
//        text: false
//    }).click(function () {
//        $('#codocupacion').val('');
//    })
//});

function successDataClase() {
    // Animate
    $('#progressbarclase').hide();
    $("#btnBuscaClase").show();
    if (!errorClase) {
        $('#target-clase').show();
    }
}

function successDataClase2() {
    // Animate
    $('#progressbarclase2').hide();
    $("#btnBuscaClase2").show();
    if (!errorClase2) {
        $('#target-clase2').show();
    }
}

function successDataEspecialidad() {
    // Animate
    $('#progressbarespecialidad').hide();
    $("#btnBuscaEspecialidad").show();
    if (!errorEspecialidad) {
        $('#target-especialidad').show();
    }
}

function successDataEspecialidad2() {
    // Animate
    $('#progressbarespecialidad2').hide();
    $("#btnBuscaEspecialidad2").show();
    if (!errorEspecialidad2) {
        $('#target-especialidad2').show();
    }
}

function successDataSubespecialidad() {
    // Animate
    $('#progressbarsubespecialidad').hide();
    $("#btnBuscaSubespecialidad").show();
    if (!errorSubespecialidad) {
        $('#target-subespecialidad').show();
    }
}

function successDataOcupacion() {
    // Animate
    $('#progressbarocupacion').hide();
    $("#btnBuscaOcupacion").show();
    if (!errorOcupacion) {
        $('#target-ocupacion').show();
    }
}