$(document).ready(function () {
    cargaTabla();

});

function cargaTabla() {
    $.ajaxSetup({ cache: false })

    $('.load').load(urlGeneral + 'Proyectos/TraerTodo', function (response, status, xhr) {

        $("#proyectos").DataTable({
            "pageLength": 25
        });
        $("#proyectos").on("click", "button.btnEditarProyecto", function () {
            var idProyecto = $(this).attr("idproyecto");

            var datos = new FormData();
            datos.append("Id", idProyecto);

            $.ajax({
                url: urlGeneral + 'Proyectos/Traer',
                method: "POST",
                data: "{Id: " + idProyecto + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.IsSuccess) {
                        $("#editarIdProyecto").val(data.Result.IdProyecto);
                        $("#editarDescripcion").val(data.Result.Descripcion);
                        $("#editarIdCliente").val(data.Result.IdCliente);
                        $("#editarFechaInicial").val(data.Result.FechaInicio);
                        $("#editarFechaFinal").val(data.Result.FechaFinal);
                        $("#editarHorasAsignada").val(data.Result.HorasAsignada);
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
        $("#proyectos").on("click", "button.btnActivarProyecto", function () {
            Swal.fire({
                title: "Acción Activar",
                text: "¿Está seguro de activar el proyecto?",
                showCancelButton: true,
                icon: "question",
                confirmButtonText: '¡Sí!',
                cancelButtonText: "¡No!"
            }).then((result) => {
                if (result.isConfirmed) {
                    var idProyecto = $(this).attr("idproyecto");

                    var datos = new FormData();
                    datos.append("Id", idProyecto);

                    $.ajax({
                        url: urlGeneral + 'Proyectos/Activar',
                        method: "POST",
                        data: datos,
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function (data) {
                            if (data.IsSuccess) {
                                Swal.fire({
                                    title: 'Proceso Terminado',
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


                } else {
                    Swal.fire('Proceso Terminado', 'Se ha cancelado la acción', 'error')
                }
            })
        });

        $("#proyectos").on("click", "button.btnFinalizarProyecto", function () {
            Swal.fire({
                title: "Acción Finalizar",
                text: "¿Está seguro de finalizar el proyecto?",
                showCancelButton: true,
                icon: "question",
                confirmButtonText: '¡Sí!',
                cancelButtonText: "¡No!"
            }).then((result) => {
                if (result.isConfirmed) {
                    var idProyecto = $(this).attr("idproyecto");

                    var datos = new FormData();
                    datos.append("Id", idProyecto);

                    $.ajax({
                        url: urlGeneral + 'Proyectos/Finalizar',
                        method: "POST",
                        data: datos,
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function (data) {
                            if (data.IsSuccess) {
                                Swal.fire({
                                    title: 'Proceso Terminado',
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


                } else {
                    Swal.fire('Proceso Terminado', 'Se ha cancelado la acción', 'error')
                }
            })
        });
        $("#proyectos").on("click", "button.btnAnularProyecto", function () {
            Swal.fire({
                title: "Acción Eliminar",
                text: "¿Está seguro de elminar el proyecto? No es modificable.",
                showCancelButton: true,
                icon: "question",
                confirmButtonText: '¡Sí!',
                cancelButtonText: "¡No!"
            }).then((result) => {
                if (result.isConfirmed) {
                    var idProyecto = $(this).attr("idproyecto");

                    var datos = new FormData();
                    datos.append("Id", idProyecto);

                    $.ajax({
                        url: urlGeneral + 'Proyectos/Eliminar',
                        method: "POST",
                        data: datos,
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function (data) {
                            if (data.IsSuccess) {
                                Swal.fire({
                                    title: 'Proceso Terminado',
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


                } else {
                    Swal.fire('Proceso Terminado', 'Se ha cancelado la acción', 'error')
                }
            })
        });

        $("#proyectos").on("click", "button.modalAgregarEmpleadoProyecto", function () {
            var idProyecto = $(this).attr("idProyecto");
            cargaTablaProyectoEmpleado(idProyecto);
            $("#idProyecto_EmpleadoProyecto").val(idProyecto);
            $("#modalAgregarEmpleadoProyecto").modal("show");
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
                        $("#modalAgregarProyecto").modal("hide");
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
                        $("#modalEditarProyecto").modal("hide");
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

function cargaTablaProyectoEmpleado(id) {
    $.ajaxSetup({ cache: false })
    $('.empleados-asignados').load(urlGeneral + 'Proyectos/TraerDetalles?Id=' + id, function (response, status, xhr) {
        $(".btnAddEmpleadoProyecto").click(function (e) {
            e.stopImmediatePropagation();
            var idEmpleado = $("#idEmpleado_EmpleadoProyecto").val();
            var idProyecto = $("#idProyecto_EmpleadoProyecto").val();

            if (idEmpleado.length == 0) {
                alert("Seleccione empleado")
                return;
            }

            var datos = new FormData();
            datos.append("idProyecto", idProyecto);
            datos.append("idEmpleado", idEmpleado);

            $.ajax({
                url: urlGeneral + "Proyectos/AgregarDetalle",
                method: "POST",
                data: datos,
                cache: false,
                contentType: false,
                processData: false,
                dataType: "json",
                success: function (data) {
                    if (data.IsSuccess) {
                        Swal.fire({
                            title: 'Proceso Terminado',
                            text: data.Message,
                            icon: "success",
                            confirmButtonText: '¡Cerrar!',
                        });
                        $("#idEmpleado_EmpleadoProyecto").val("");
                        cargaTablaProyectoEmpleado(data.Id)
                    } else {
                        Swal.fire({
                            icon: "error",
                            title: "Error de ingreso",
                            text: "Verifique que los campos esten correctos"

                        })
                    }

                }

            });
        });

        $(".btnAnularProyectoEmpleado").click(function () {
            var idProyectoEmpleado = $(this).attr("idproyectoempleado");
            var datos = new FormData();
            datos.append("Id", idProyectoEmpleado);

            $.ajax({
                url: urlGeneral+ "Proyectos/EliminarDetalle",
                method: "POST",
                data: datos,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.IsSuccess) {
                        Swal.fire({
                            title: 'Proceso Terminado',
                            text: data.Message,
                            icon: "success",
                            confirmButtonText: '¡Cerrar!',
                        });
                        cargaTablaProyectoEmpleado(data.Id)
                    } else {
                        Swal.fire({
                            icon: "error",
                            title: "Error de ingreso",
                            text: "Verifique que los campos esten correctos"

                        })
                    }

                }

            });
        });
    });
}


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


$('#modalAgregarProyecto').on('hidden.bs.modal', function () {
    $("input").val("");
    $("select").val("");
});






























/*=============================================
REVISAR SI EL PROYECTO YA ESTÁ REGISTRADO??
=============================================*/
/*
$("#nuevoProyecto").change(function(){

	$(".alert").remove();

	var nuevoProyecto = $(this).val();

	var datos = new FormData();
	datos.append("validarProyecto", nuevoProyecto);

	 $.ajax({
	    url:"ajax/proyectos.ajax.php",
	    method:"POST",
	    data: datos,
	    cache: false,
	    contentType: false,
	    processData: false,
	    dataType: "json",
	    success:function(respuesta){

	    	if(respuesta){

	    		$("#nuevoProyecto").parent().after('<div class="alert alert-warning">Este Proyecto ya existe en la base de datos</div>');

	    		$("#nuevoProyecto").val("");

	    	}

	    }

	})
})
*/

/*=============================================
ACTIVAR PROYECTO
=============================================*/
$("#proyectos").on("click", "button.btnActivarProyecto", function() {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: '¿Desea activar este proyecto?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, activar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {

            var idProyecto = $(this).attr("idProyecto");

            var datos = new FormData();
            datos.append("actiProyecto", idProyecto);

            $.ajax({
                url: "ajax/proyectos.ajax.php",
                method: "POST",
                data: datos,
                cache: false,
                contentType: false,
                processData: false,
                success: function(respuesta) {

                    swalWithBootstrapButtons.fire(
                        'Finalizado!',
                        'El proyecto ha sido activado.',
                        'success'
                    )
                    window.location = "crud-proyectos";
                }

            });



        }

    })

})

/*=============================================
FINALIZAR PROYECTO
=============================================*/
$("#proyectos").on("click", "button.btnFinalizarProyecto", function() {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: '¿Desea finalizar este proyecto?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, finalizar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {

            var idProyecto = $(this).attr("idProyecto");

            var datos = new FormData();
            datos.append("finProyecto", idProyecto);

            $.ajax({
                url: "ajax/proyectos.ajax.php",
                method: "POST",
                data: datos,
                cache: false,
                contentType: false,
                processData: false,
                success: function(respuesta) {

                    swalWithBootstrapButtons.fire(
                        'Finalizado!',
                        'El proyecto ha sido finalizado.',
                        'success'
                    )
                    window.location = "crud-proyectos";
                }

            });



        }

    })

})

/*=============================================
ANULAR PROYECTO
=============================================*/
$("#proyectos").on("click", "button.btnAnularProyecto", function() {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: '¿Desea anular este proyecto?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, anular!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {

            var idProyecto = $(this).attr("idProyecto");

            var datos = new FormData();
            datos.append("anulProyecto", idProyecto);

            $.ajax({
                url: "ajax/proyectos.ajax.php",
                method: "POST",
                data: datos,
                cache: false,
                contentType: false,
                processData: false,
                success: function(respuesta) {

                    swalWithBootstrapButtons.fire(
                        '¡Anulado!',
                        'El proyecto ha sido anulado.',
                        'success'
                    )
                    window.location = "crud-proyectos";
                }

            });



        }

    })

})

/*=============================================
EDITAR PROYECTO
=============================================*/
$("#proyectos").on("click", "button.btnEditarProyecto", function() {
    var idProyecto = $(this).attr("idProyecto");
    console.log(idProyecto)
    var datos = new FormData();
    datos.append("idProyecto", idProyecto);

    $.ajax({

        url: "ajax/proyectos.ajax.php",
        method: "POST",
        data: datos,
        cache: false,
        contentType: false,
        processData: false,
        dataType: "json",
        success: function(respuesta) {
            $("#editarIdProyecto").val(respuesta["id_proyecto"]);
            $("#editarEmpleado").val(respuesta["id_empleado"]);
            $("#editarEmpleadoCombo").val(respuesta["id_empleado"]);
            $("#editarCliente").val(respuesta["id_cliente"]);
            $("#editarProyecto").val(respuesta["nombre_proyecto"]);
            $("#editarFecha").val(respuesta["fecha_proyecto"]);
            $("#editarFechaF").val(respuesta["fechaF_proyecto"]);
            $("#editarHora").val(respuesta["hora_proyecto"]);

        }

    });

})