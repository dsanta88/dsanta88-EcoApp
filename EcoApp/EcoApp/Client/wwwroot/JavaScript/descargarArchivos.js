function descargarArchivo(nombreArchivoCliente,ruta) {
    var source = ruta;
    var a = document.createElement('a');
    a.download = nombreArchivoCliente;
    a.target = '_blank';
    a.href = source;
    a.click();
}


function convertirTextoArchivoPlano(filename, fileContent) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:text/plain;charset=utf-8," + encodeURIComponent(fileContent);
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}


function bloquearDisplay() {
    document.getElementById("bloquea").style.display = "block";
}
