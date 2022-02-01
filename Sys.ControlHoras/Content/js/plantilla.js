/*=============================================
SideBar Menu
=============================================*/

//$('.sidebar-menu').tree()

/*=============================================
Inicializar Select2
=============================================*/

$.fn.modal.Constructor.prototype.enforceFocus = function() {};

$('.select2').select2();

//Initialize Select2 Elements
$('.select2bs4').select2({
    theme: 'bootstrap4'
});

/*=============================================
Data Table
=============================================*/

$(".tablas").DataTable({

    "language": {

        "sProcessing": "Procesando...",
        "sLengthMenu": "Mostrar _MENU_ registros",
        "sZeroRecords": "No se encontraron resultados",
        "sEmptyTable": "Ningún dato disponible en esta tabla",
        "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_",
        "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0",
        "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
        "sInfoPostFix": "",
        "sSearch": "Buscar:",
        "sUrl": "",
        "sInfoThousands": ",",
        "sLoadingRecords": "Cargando...",
        "oPaginate": {
            "sFirst": "Primero",
            "sLast": "Último",
            "sNext": "Siguiente",
            "sPrevious": "Anterior"
        },
        "oAria": {
            "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
            "sSortDescending": ": Activar para ordenar la columna de manera descendente"
        }

    }

});

/*=============================================
FUNCION VALIDA CORREO
=============================================*/

function checkCorreo(e) {
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }

    // Patron de entrada, en este caso solo acepta numeros y letras
    patron = /[A-Za-z0-9.@]/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);

};

/*=============================================
FUNCION VALIDA CLAVE
=============================================*/

function checkClave(e) {
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }

    // Patron de entrada, en este caso solo acepta numeros y letras
    patron = /[A-Za-z0-9._-]/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);

};

/*=============================================
FUNCION VALIDA CLAVE
=============================================*/

function checkLN(e) {
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }

    // Patron de entrada, en este caso solo acepta numeros y letras
    patron = /[A-Za-z0-9]/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);

};

/*=============================================
FUNCION VALIDA SOLO TEXTO
=============================================*/

function checkText(e) {
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }

    // Patron de entrada, en este caso solo acepta numeros y letras
    patron = /[A-Za-zñÑáéíóúÁÉÍÓÚ0-9 ]/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);

};


function checkNumeros(txt, evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 46) {
        if (txt.value.length == 0 && txt.value.indexOf('.') === -1) {
            return false;
        }
        if (txt.value.indexOf('.') === -1) {
            return true;
        } else {
            return false;
        }
    } else {
        if (charCode > 31 &&
            (charCode < 48 || charCode > 57))
            return false;
    }
    return true;
}