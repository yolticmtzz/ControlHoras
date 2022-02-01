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
                        window.location.href = '/Account/Index'
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