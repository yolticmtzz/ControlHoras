/*=============================================
CARGAR LA TABLA DINÁMICA DE USUARIOS
=============================================*/
$(document).ready(function () {
    cargaTabla();

});

function cargaTabla() {
    $.ajaxSetup({ cache: false })

    $('.load').load(urlGeneral + 'Empleados/TraerTodo', function (response, status, xhr) {

        $("#empleados").DataTable();

        $("#empleados").on("click", "button.btnEditarEmpleado", function () {
            var idEmpleado = $(this).attr("idempleado");

            var datos = new FormData();
            datos.append("Id", idEmpleado);

            $.ajax({
                url: urlGeneral + 'Empleados/Traer',
                method: "POST",
                data: datos,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.IsSuccess) {
                        $("#editarPerfil").val(data.Result.IdPerfil);
                        $("#editarIdEmpleado").val(data.Result.IdEmpleado);
                        $("#editarNombre").val(data.Result.Nombre);
                        $("#editarRut").val(data.Result.Rut);
                        $("#editarClave").val("");
                        $("#editarCorreo").val(data.Result.Correo);
                        $("#modalEditarEmpleado").modal("show");
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

        $("#empleados").on("click", "button.btnAnularEmpleado", function () {
            var idEmpleado = $(this).attr("idempleado");

            var datos = new FormData();
            datos.append("Id", idEmpleado);

            $.ajax({
                url: urlGeneral + 'Empleados/Eliminar',
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
                        $("#modalAgregarEmpleado").modal("hide");
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
                        $("#modalEditarEmpleado").modal("hide");
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
















$("#empleados").on("click", "button.btnEditarEmpleado", function () {
    console.log("Entra a edit")

    var idEmpleado = $(this).attr("idempleado");

    var datos = new FormData();
    datos.append("idempleado", idEmpleado);

    $.ajax({
        url: urlGeneral + 'Empleados/Traer',
        method: "POST",
        data: datos,
        cache: false,
        contentType: false,
        processData: false,
        success: function (respuesta) {

        }

    });

})


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


function eliminarImagen(val, divEliminar) {
    $("#archivos").val($("#archivos").val().replace(val + "|", ""));
    $("#" + divEliminar).remove();
}

$('#modalAgregarEmpleado').on('hidden.bs.modal', function () {
    $("input").val("");
    $("select").val("");
});





























































/*=============================================
EDITAR USUARIO
=============================================*/
$("#empleados").on("click", "button.btnEditarEmpleado", function () {

    var idEmpleado = $(this).attr("idEmpleado");

    var datos = new FormData();
    datos.append("idEmpleado", idEmpleado);

    $.ajax({

        url: "ajax/empleados.ajax.php",
        method: "POST",
        data: datos,
        cache: false,
        contentType: false,
        processData: false,
        dataType: "json",
        success: function (respuesta) {
            //console.log(respuesta);

            $("#editarIdEmpleado").val(respuesta["id_empleado"]);
            $("#editarPerfil").val(respuesta["id_perfil"]);
            $("#editarNombre").val(respuesta["nombre_empleado"]);
            $("#editarRut").val(respuesta["rut_empleado"]);
            $("#editarClave").val("");
            $("#claveAnterior").val(respuesta["clave_empleado"]);
            $("#editarCorreo").val(respuesta["correo_empleado"]);


        }

    });

})

/*=============================================
ACTIVAR USUARIO
=============================================*/
$("#empleados").on("click", "button.btnActivar", function () {

    var idEmpleado = $(this).attr("idEmpleado");
    var estadoEmpleado = $(this).attr("estadoEmpleado");

    var datos = new FormData();
    datos.append("activarIdEmp", idEmpleado);
    datos.append("activarEmpleado", estadoEmpleado);

    $.ajax({

        url: "ajax/empleados.ajax.php",
        method: "POST",
        data: datos,
        cache: false,
        contentType: false,
        processData: false,
        success: function (respuesta) {

            if (window.matchMedia("(max-width:767px)").matches) {

                swal({
                    title: "El usuario ha sido actualizado",
                    type: "success",
                    confirmButtonText: "¡Cerrar!"
                }).then(function (result) {
                    if (result.value) {

                        window.location = "usuarios";

                    }


                });

            }

        }

    })

    if (estadoEmpleado == 0) {

        $(this).removeClass('btn-success');
        $(this).addClass('btn-danger');
        $(this).html('Desactivado');
        $(this).attr('estadoEmpleado', 1);

    } else {

        $(this).addClass('btn-success');
        $(this).removeClass('btn-danger');
        $(this).html('Activado');
        $(this).attr('estadoEmpleado', 0);

    }

})

/*=============================================
REVISAR SI EL USUARIO YA ESTÁ REGISTRADO
=============================================*/

$("#nuevoCorreo").change(function () {

    $(".alert").remove();

    var correo = $(this).val();

    var datos = new FormData();
    datos.append("validarCorreo", correo);

    $.ajax({
        url: "ajax/empleados.ajax.php",
        method: "POST",
        data: datos,
        cache: false,
        contentType: false,
        processData: false,
        dataType: "json",
        success: function (respuesta) {

            if (respuesta) {

                $("#nuevoCorreo").parent().after('<div class="alert alert-warning">Este Correo ya existe en la base de datos</div>');

                $("#nuevoCorreo").val("");

            }

        }

    })
})