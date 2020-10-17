function EliminarRegistro() {
    var findproducto = document.getElementById("Titulo").value;
    $.get("Productos/EliminarRegistro/?idproducto=" + findproducto, function(data) {
        if (data == 1) {
            //alert("La accion se ejecuto correctamente...");
            $.notify("Datos eliminados correctamente!", "error");
            $("#frmsearch").trigger("submit");
            document.getElementById("btnCerrarConf").click();
        } else {
            $.notify("Ocurrio un error", "error");
        }
    })
} /*End eliminar registro*/


var Buscar = document.getElementById("Busqueda");
Buscar.onkeyup = function() {
    $("#frmsearch").trigger("submit");
    /*Asigna lo que se encentra en el TexBox Busqueda a la variable
    Buscar y refresca el formulario de manera asincrona*/
}

function Limpiar() {
    var controles = document.getElementsByClassName("form-control");
    var control;
    for (var i = 0; i < controles.length; i++) {
        controles[i].value = "";
    }
}

function Agregar() {
    Limpiar();
    document.getElementById("error").innerHTML = "";
    document.getElementById("Titulo").value = -1;
    /*Asigna el valor (-1) al texbox Titulo el cual lo envia al controlador*/
}

function LanzarModalEliminar(idproducto) {
    //Llama el modal que realizara la accin eliminar
    document.getElementById("Titulo").value = idproducto;
}

function Editar(IdProducto) {
    Limpiar();
    document.getElementById("error").innerHTML = "";
    $.get("Productos/RecuperarProductos/?IdProductos=" + IdProducto, function(data) {
        document.getElementById("Producto").value = data.Producto;
        document.getElementById("Descripcion").value = data.Descripcion;
        document.getElementById("Estado").value = data.Estado;
    })
    document.getElementById("Titulo").value = IdProducto;
    /*Recupera los datos del registro especificado y los asigna a cada texbox, mediante
    la funcion (RecuperarProductos)*/
}

function AgregarProductos(res) {
    /*Recibe un parametro respuesta desde el controlador y de acuerdo a su valor realiza las accines*/
    if (res == "1" || res == "0") {
        $("#frmsearch").trigger("submit");
        document.getElementById("btnclose").click();
        $.notify("Datos agregados correctamente!", "success");
        //Si >=0, realiza el almacenamiento de manera asincrona, cierra el formulario y envia una notificacion.
    } else {
        if (res == "-1") {
            $.notify("Existe un registro con estos datos!", "warn");
        } else {
            $.notify("Datos incorrectos", "error");
        }
    }
}


/*=======================***************************************************=======================
       ***************************************-------FUNCIONES---*****************************************    
       =======================***************************************************=======================*/
//IEnumerable<Productos> MostrarProductos()
//{
//    using (var db = new MiHaciendaEntities())
//    {
//        return db.Productos.ToList<Productos>();

//    }
//}