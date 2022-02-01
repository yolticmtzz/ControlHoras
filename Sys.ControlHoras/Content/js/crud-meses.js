/*=============================================
CARGAR LA TABLA DINÁMICA DE PROYECTOS
=============================================*/
$(document).ready(function () {
    cargaTabla();

});

function cargaTabla() {
    $.ajaxSetup({ cache: false })

    var filtro = $("#comboAnio").val();
    var busqueda = '';
    if (filtro.length > 0) {
        busqueda += '?Anio=' + filtro
    }

    $('.load').load(urlGeneral + 'Meses/TraerTodo' + busqueda, function (response, status, xhr) {

        $("#meses").DataTable({
            "pageLength": 10
        }
        );

        $("#meses").on("click", "button.btnFinalizarMes", function () {
            var idMes = $(this).attr("idmes");

            var datos = new FormData();
            datos.append("Id", idMes);

            $.ajax({
                url: urlGeneral + 'Meses/Finalizar',
                method: "POST",
                data: datos,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.IsSuccess) {
                        Swal.fire({
                            title: 'El mes ha sido finalizado',
                            icon: "success",
                            confirmButtonText: '¡Cerrar!',
                        }).then((result) => {
                            if (result.isConfirmed) {
                                cargaTabla();
                            }
                        })
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error de servidor',
                            text: data.Message,
                        })
                    }
                }

            });
        });

        $("#meses").on("click", "button.btnActivarMes", function () {
            var idMes = $(this).attr("idmes");

            var datos = new FormData();
            datos.append("Id", idMes);

            $.ajax({
                url: urlGeneral + 'Meses/Activar',
                method: "POST",
                data: datos,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.IsSuccess) {
                        Swal.fire({
                            title: 'El mes ha sido activado',
                            icon: "success",
                            confirmButtonText: '¡Cerrar!',
                        }).then((result) => {
                            if (result.isConfirmed) {
                                cargaTabla();
                            }
                        })
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error de servidor',
                            text: data.Message,
                        })
                    }
                }
            });
        });

        $("#meses").on("click", "button.btnSuspenderMes", function () {
            var idMes = $(this).attr("idmes");

            var datos = new FormData();
            datos.append("Id", idMes);

            $.ajax({
                url: urlGeneral + 'Meses/Suspender',
                method: "POST",
                data: datos,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.IsSuccess) {
                        Swal.fire({
                            title: 'El mes ha sido suspendido',
                            icon: "success",
                            confirmButtonText: '¡Cerrar!',
                        }).then((result) => {
                            if (result.isConfirmed) {
                                cargaTabla();
                            }
                        })
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error de servidor',
                            text: data.Message,
                        })
                    }
                }
            });
        });


        



    });
}

$("#comboAnio").change(function () {
    cargaTabla()
});

$("#meses").on("click", "button.btnFinalizarMes", function() {

    var idMes = $(this).attr("idMes");
    var datos = new FormData();
    datos.append("idFinalizar", idMes);

    $.ajax({

        url: "ajax/meses.ajax.php",
        method: "POST",
        data: datos,
        cache: false,
        contentType: false,
        processData: false,
        dataType: "json",
        success: function(respuesta) {
            Swal.fire({
                title: 'El mes ha sido finalizado',
                icon: "success",
                confirmButtonText: '¡Cerrar!',
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location = "crud-meses";
                }
            })
        }

    });

})

$("#meses").on("click", "button.btnActivarMes", function() {

    var idMes = $(this).attr("idMes");
    var datos = new FormData();
    datos.append("idActivar", idMes);

    $.ajax({

        url: "ajax/meses.ajax.php",
        method: "POST",
        data: datos,
        cache: false,
        contentType: false,
        processData: false,
        dataType: "json",
        success: function(respuesta) {
            Swal.fire({
                title: 'El mes ha sido activado',
                icon: "success",
                confirmButtonText: '¡Cerrar!',
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location = "crud-meses";
                }
            })
        }

    });

})

$("#meses").on("click", "button.btnSuspenderMes", function() {

    var idMes = $(this).attr("idMes");
    var datos = new FormData();
    datos.append("idSuspender", idMes);

    $.ajax({

        url: "ajax/meses.ajax.php",
        method: "POST",
        data: datos,
        cache: false,
        contentType: false,
        processData: false,
        dataType: "json",
        success: function(respuesta) {
            Swal.fire({
                title: 'El mes ha sido suspendido',
                icon: "success",
                confirmButtonText: '¡Cerrar!',
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location = "crud-meses";
                }
            })
        }

    });

})