/*Ajax scripts for asincrono function*/

function Limpiar()
{
    var controles = document.getElementsByClassName("form-control");
    var control;
    for (var i = 0; i < controles.length; i++) {
        control = controles[i];//forma1
        control.value = "";
    }
}

var frmsearch = document.getElementById("frmsearch");
var Busqueda = document.getElementById("Busqueda");
Busqueda.onkeyup = function ()
{
    $("#frmsearch").trigger("submit");

}

function Agregar()
{
    Limpiar();
    document.getElementById("Diverror").innerHTML = "";
    document.getElementById("Titulo").value = -1;
}

function AgregarEmpleados(res)
{
    Limpiar();
    if (res == "1" || res == "0")
    {
        $("#frmsearch").trigger("submit");
        document.getElementById("btnclose").click();
        $.notify("Datos agregados correctamente!", "success");
    }
    else
    {
        $.notify("Datos incorrectos", "error");
    }
}

function Editar(IdEmp)
{
    /*Al momento de hacer click en editar se le asigna el idEmpleado a la variable Titulo*/
    Limpiar();
    document.getElementById("Diverror").innerHTML = "";
    $.get("Empleados/RecuperarEmpleados/?idempleado=" + IdEmp, function (data)
    {
        document.getElementById("Cedula").value = data.Cedula;
        document.getElementById("Nombre").value = data.Nombre;
        document.getElementById("Apellidos").value = data.Apellidos;
        document.getElementById("Sexo").value = data.Sexo;
        document.getElementById("Telefono").value = data.Telefono;
        document.getElementById("Email").value = data.Email;
        document.getElementById("Direccion").value = data.Direccion;
        document.getElementById("Puesto").value = data.Puesto;
        document.getElementById("Estado").value = data.Estado;
    })

    document.getElementById("Titulo").value = IdEmp;
}

function LlamarModal(IdEmpl)
{
    document.getElementById("Titulo").value = IdEmpl;
    //Asigna el valor recibido en (IdEmpl) a la variable Titulo
}

function EliminarRegistro() {
    var Nid = document.getElementById("Titulo").value;

    $.get("Empleados/EliminarEmpleado/?IdEmpleado=" + Nid, function (data)
    {
        if (data == "1")
        {
            $.notify("Datos eliminados correctamente!", "error");
            $("#frmsearch").trigger("submit");
            document.getElementById("btnCerrarConf").click();
        }
        else
        {
            $.notify("Ocurrio un error", "error");
        }
    })
}