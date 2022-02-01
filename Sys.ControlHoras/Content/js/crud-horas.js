$(document).ready(function () {
    cargaTabla();
});

function cargaTabla() {
    $(".load").load(urlGeneral + 'IngresoHoras/TraerTodo', function () {
        $("#horas").DataTable();

        $("#horas").on("click", "button.btnEditarHora", function () {
            var idIngresoHoras = $(this).attr("idingresohora");

            var datos = new FormData();
            datos.append("Id", idIngresoHoras);

            $.ajax({

                url: urlGeneral + "Ingresohoras/Traer",
                method: "POST",
                data: datos,
                cache: false,
                contentType: false,
                processData: false,
                dataType: "json",
                success: function (respuesta) {
                    $("#editarIdIngresoHora").val(respuesta.Result.IdIngresoHora)
                    $("#editarIdProyectoEmpleado").val(respuesta.Result.IdProyectoEmpleado);
                    $("#editarFecha").val(respuesta.Result.Fecha);
                    $("#editarHora").val(respuesta.Result.Hora);
                }

            });
        });

        $("#horas").on("click", "button.btnAnularhora", function () {
            var idIngresoHoras = $(this).attr("idingresohora");

            var datos = new FormData();
            datos.append("Id", idIngresoHoras);

            $.ajax({

                url: urlGeneral + "Ingresohoras/Eliminar",
                method: "POST",
                data: datos,
                cache: false,
                contentType: false,
                processData: false,
                dataType: "json",
                success: function (data) {
                    cargaTabla();
                    Swal.fire({
                        title: 'Proceso Terminado',
                        text: data.Message,
                        icon: "success",
                        confirmButtonText: '¡Cerrar!',
                    });

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
                        $("#modalAgregarHora").modal("hide");
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
                        $("#modalEditarHora").modal("hide");
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
    $(formAjaxAdd).find("input[type=text]").val("1");
    $(formAjaxAdd).find("input[type=number]").val("");
    $(formAjaxAdd).find("input[type=password]").val("");
    $(formAjaxAdd).find("input[type=email]").val("");
    $("#modal-dropzone").find("input[type=file]").empty();
    $("#modal-dropzone").removeClass("dz-started");
    $(".dz-preview").remove();
    $(formAjaxAdd).find("select").val("");
}


$('#modalAgregarHora').on('hidden.bs.modal', function () {
    $("input[type=date]").val("");
    $("input[type=text]").val("1");
    $("select").val("");
});
