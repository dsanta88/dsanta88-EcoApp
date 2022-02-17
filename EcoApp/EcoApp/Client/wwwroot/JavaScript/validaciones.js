function validarCampos() {
    var a = document.getElementById("inputCorreo").textContent;

    if (a.length == 0) {
        document.getElementById("inputCorreo").textContent = "El campo es obligatorio";

        return false;
    }
    else {
        return true;
    }

}



function MensajeAlerta(mensaje) {
    Swal.fire({
        icon: 'error',
        text: mensaje
    })
}


function MensajeoK(mensaje) {
    Swal.fire({
        icon: 'success',
        title: mensaje,
        showConfirmButton: true
    })
}



function Cargando(mensaje) {
    Swal.fire({
        title: mensaje,
        showConfirmButton: true
    })
}


