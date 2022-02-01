/*=============================================
CARGAR LA TABLA DINÁMICA DE PROYECTOS
=============================================*/
$(document).ready(function () {
    cargaTabla();

});

function cargaTabla() {
    $.ajaxSetup({ cache: false })

    $('.load').load(urlGeneral + 'Clientes/TraerTodo', function (response, status, xhr) {

        $("#clientes").DataTable();
        $("#clientes").on("click", "button.btnEditarCliente", function () {
            var idCliente = $(this).attr("idcliente");

            var datos = new FormData();
            datos.append("Id", idCliente);

            $.ajax({
                url: urlGeneral + 'Clientes/Traer',
                method: "POST",
                data: datos,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.IsSuccess) {
                        $("#editarIdCliente").val(data.Result.IdCliente);
                        $("#editarNombre").val(data.Result.Nombre);
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
        $("#clientes").on("click", "button.btnAnularCliente", function () {
            var idCliente = $(this).attr("idcliente");

            var datos = new FormData();
            datos.append("Id", idCliente);

            $.ajax({
                url: urlGeneral + 'Clientes/Eliminar',
                method: "POST",
                data: datos,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.IsSuccess) {
                        Swal.fire({
                            title: "Proceso terminado",
                            text: data.Message,
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

/*=============================================
REVISAR SI EL CLIENTE YA ESTÁ REGISTRADO??
=============================================*/

$("#nuevoCliente").change(function() {

    $(".alert").remove();

    var nuevoProyecto = $(this).val();

    var datos = new FormData();
    datos.append("validarCliente", nuevoCliente);

    $.ajax({
        url: "ajax/clientes.ajax.php",
        method: "POST",
        data: datos,
        cache: false,
        contentType: false,
        processData: false,
        dataType: "json",
        success: function(respuesta) {

            if (respuesta) {

                $("#nuevoCliente").parent().after('<div class="alert alert-warning">Este Cliente ya existe en la base de datos</div>');

                $("#nuevoCliente").val("");

            }

        }

    })
})


/*=============================================
FINALIZAR CLIENTE
=============================================*/
$("#clientes").on("click", "button.btnFinalizarCliente", function() {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: '¿Desea finalizar este cliente?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, finalizar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {

            var idCliente = $(this).attr("idCliente");

            var datos = new FormData();
            datos.append("finCliente", idCliente);

            $.ajax({
                url: "ajax/clientes.ajax.php",
                method: "POST",
                data: datos,
                cache: false,
                contentType: false,
                processData: false,
                success: function(respuesta) {

                    swalWithBootstrapButtons.fire(
                        'Finalizado!',
                        'El cliente ha sido finalizado.',
                        'success'
                    )
                    window.location = "crud-clientes";
                }

            });



        }

    })

})

/*=============================================
ANULAR CLIENTE
=============================================*/
$("#clientes").on("click", "button.btnAnularCliente", function() {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: '¿Desea anular este cliente?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, anular!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {

            var idCliente = $(this).attr("idCliente");

            var datos = new FormData();
            datos.append("anulCliente", idCliente);

            $.ajax({
                url: "ajax/clientes.ajax.php",
                method: "POST",
                data: datos,
                cache: false,
                contentType: false,
                processData: false,
                success: function(respuesta) {

                    swalWithBootstrapButtons.fire(
                        '¡Anulado!',
                        'El cliente ha sido anulado.',
                        'success'
                    )
                    window.location = "crud-clientes";
                }

            });



        }

    })

})

/*=============================================
EDITAR CLIENTE
=============================================*/
$("#clientes").on("click", "button.btnEditarCliente", function() {

    var idCliente = $(this).attr("idCliente");

    var datos = new FormData();
    datos.append("idCliente", idCliente);

    $.ajax({

        url: "ajax/clientes.ajax.php",
        method: "POST",
        data: datos,
        cache: false,
        contentType: false,
        processData: false,
        dataType: "json",
        success: function(respuesta) {
            //console.log(respuesta);

            $("#editarIdCliente").val(respuesta["id_cliente"]);
            $("#editCliente").val(respuesta["nombre"]);
            $("#editCodigo").val(respuesta["codigo"]);


        }

    });

})

/*=============================================
EDITAR CLIENTE
=============================================*/
$("#clientes").on("click", "button.btnEliminarCliente", function() {
    var idCliente = $(this).attr("idCliente");

    var datos = new FormData();
    datos.append("deleteCliente", idCliente);
    $.ajax({

        url: "ajax/clientes.ajax.php",
        method: "POST",
        data: datos,
        cache: false,
        contentType: false,
        processData: false,
        dataType: "json",
        success: function(respuesta) {

            /*Swal({
                title: "El cliente ha sido actualizado",
                type: "success",
                confirmButtonText: "¡Cerrar!"
            }).then(function(result) {
                if (result.value) {

                    window.location = "crud-clientes";

                }


            });*/
            Swal.fire({
                title: 'El cliente ha sido actualizado',
                icon: "success",
                confirmButtonText: '¡Cerrar!',
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location = "crud-clientes";
                }
            })
        }

    });

})




//forms add
var validAjaxAdd;
var formAjaxAdd = $('.formAjaxAdd');
$("button.btnAjaxAdd").on('click', function (e) {
    e.preventDefault();
    validAjaxAdd = formAjaxAdd.validate({
        //== Validate only visible fields
        ignore: ":hidden",
        //== Display error
        rules: {

        },
        messages: {
            required: "Por favor, no deje campos vacíos o sin elegir"
        },
        invalidHandler: function (event, validAjax) {
            Swal.fire({
                icon: "error",
                title: "Error de datos",
                text: "Los datos capturados no son correctos."
            })
        },
        //== Submit valid form
        submitHandler: function (form) {

        }
    });
    if (validAjaxAdd.form() == false) {
        $("#userSubs-error").hide();
        return;
    }
    formAjaxAdd.ajaxSubmit({
        success: function (response) {
            if (response.IsSuccess) {
                Swal.fire({
                    title: "Proceso terminado",
                    text: response.Message,
                    icon: "success",
                    confirmButtonText: '¡Cerrar!',
                }).then((result) => {
                    if (result.isConfirmed) {
                        cargaTabla();
                        $("#modalAgregarCliente").modal("hide");
                        removeInfoForm(formAjaxAdd);
                    }
                })
            } else {
                Swal.fire({
                    title: "Proceso terminado",
                    text: response.Message,
                    icon: "error"
                });
            }

        },
        error: function (request, status, error) {
            Swal.fire({
                icon: "error",
                title: "Error de servidor",
                text: "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
            })
        }
    });
});


//forms edit
var validAjaxEdit;
var formAjaxEdit = $('.formAjaxEdit');
$("button.btnAjaxEdit").on('click', function (e) {
    e.preventDefault();
    validAjaxEdit = formAjaxEdit.validate({
        //== Validate only visible fields
        ignore: ":hidden",
        //== Display error
        rules: {

        },
        messages: {
            required: "Por favor, no deje campos vacíos o sin elegir"
        },
        invalidHandler: function (event, validAjax) {
            Swal.fire({
                icon: "error",
                title: "Error de datos",
                text: "Los datos capturados no son correctos."
            })
        },
        //== Submit valid form
        submitHandler: function (form) {

        }
    });
    if (validAjaxEdit.form() == false) {
        $("#userSubs-error").hide();
        return;
    }
    formAjaxEdit.ajaxSubmit({
        success: function (response) {
            if (response.IsSuccess) {
                Swal.fire({
                    title: "Proceso terminado",
                    text: response.Message,
                    icon: "success",
                    confirmButtonText: '¡Cerrar!',
                }).then((result) => {
                    if (result.isConfirmed) {
                        cargaTabla();
                        $("#modalEditarCliente").modal("hide");
                        removeInfoForm(formAjaxEdit);
                    }
                })
            } else {
                Swal.fire({
                    title: "Proceso terminado",
                    text: response.Message,
                    icon: "error"
                });
            }

        },
        error: function (request, status, error) {
            Swal.fire({
                icon: "error",
                title: "Error de servidor",
                text: "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
            })
        }
    });
});


function removeInfoForm(formAjaxAdd) {
    $(formAjaxAdd).find("input[type=text]").val("");
    $(formAjaxAdd).find("input[type=number]").val("");
    $(formAjaxAdd).find("input[type=password]").val("");
    $(formAjaxAdd).find("input[type=email]").val("");
    $("#modal-dropzone").find("input[type=file]").empty();
    $("#modal-dropzone").removeClass("dz-started");
    $(".dz-preview").remove();
    $(formAjaxAdd).find("select").val("");
}


$('#modalAgregarCliente').on('hidden.bs.modal', function () {
    $("input").val("");
    $("select").val("");
    $("#anexosUpload").empty();
});

