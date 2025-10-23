using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class CadeteriaController : ControllerBase 
{
    private Cadeteria cadeteria; 
    private AccesoADatosCadeteria ADCadeteria;
    private AccesoADatosCadetes ADCadetes;
    private AccesoADatosPedidos ADPedidos;

    public CadeteriaController()
    {
        ADCadeteria = new AccesoADatosCadeteria();
        ADCadetes = new AccesoADatosCadetes();
        ADPedidos = new AccesoADatosPedidos();

        cadeteria = ADCadeteria.Obtener();
        cadeteria.AgregarListaCadetes(ADCadetes.Obtener());
        cadeteria.AgregarListaPedidos(ADPedidos.Obtener());
    }

    /* [HttpPost("PostCadeteria")]   
    public ActionResult<Cadeteria> PostCadeteria(string Nombre, double Telefono)
    {
        var cadeteiraPrueba = new Cadeteria(Nombre,Telefono);
        ADCadeteria.Guardar(cadeteiraPrueba);
        return Ok(cadeteiraPrueba);
    } */

    [HttpGet("GetCadeteria")] 
    public IActionResult GetCadeteria()  
    {
        return cadeteria == null ? BadRequest("No se encontraron Datos de la Cadeteria") : Ok(cadeteria);
    }

    [HttpGet("GetPedidos")]  // Funcionando
    public ActionResult<List<Pedido>> GetPedidos()
    {
        return cadeteria.ListadoPedidos.Count() == 0 ? BadRequest("Lista de Pedidos Vacia") : Ok(cadeteria.ListadoPedidos);
    }

    [HttpGet("GetCadetes")]  // Funcionando
    public ActionResult<List<Cadeteria>> GetCadetes()
    {
        return cadeteria.ListadoCadetes.Count() == 0 ? BadRequest("Lista de Cadetes Vacia") : Ok(ADCadetes);
    }

    [HttpGet("GetInforme")] // Funcionando
    public IActionResult GetInforme()
    {
        var cadete = cadeteria.ListadoCadetes;
        var pedidos = cadeteria.ListadoPedidos;

        var informe = new
        {
            TotalCadetes = cadete.Count,
            TotalPedidos = pedidos.Count,
            Cadetes = cadete
        };
        return Ok(informe);
    }

    [HttpPost("PostAgregarPedido")]  // Funcionando
    public ActionResult<Pedido> AgregarPedido([FromBody] Pedido pedido)
    {
        var pedidos = cadeteria.ListadoPedidos;
        pedidos.Add(pedido);
        ADPedidos.Guardar(cadeteria.ListadoPedidos);
        return Ok(pedido);
    }

    [HttpPost("PostAsignarPedido")] // Funcionando
    public ActionResult<Pedido> AsignarPedido(int IdCadete, int numPedido)
    {
        var cadetes = cadeteria.ListadoCadetes;
        var pedidos = cadeteria.ListadoPedidos;

        var pedido = pedidos.FirstOrDefault(p => p.NumPedido == numPedido);
        if (pedido == null) return NotFound($"El pedido numero: {numPedido}\nNo fue encontrado");
        var cadete = cadetes.FirstOrDefault(q => q.ID == IdCadete);
        if (cadete == null) return NotFound($"El cadete: {IdCadete}\nNo fue encontrado");
        pedido.CadeteAsignado = cadete;
        ADPedidos.Guardar(cadeteria.ListadoPedidos);
        return Ok(pedido);
    }

    [HttpPost("PostCambiarEstado")] // Funcionando
    public ActionResult<Pedido> CambiarEstado(int numPedido, int NuevoEstado)
    {
        var pedidos = cadeteria.ListadoPedidos;
        var pedido = pedidos.FirstOrDefault(p => p.NumPedido == numPedido);
        if (pedido == null) return NotFound($"El pedido numero: {numPedido}\nNo fue encontrado");
        pedido.CambiarEstado(NuevoEstado);
        ADPedidos.Guardar(cadeteria.ListadoPedidos);
        return Ok(pedido);
    }

    [HttpPost("PostCambiarCadetePedido")] // Funcionando
    public IActionResult CambiarCadetePedido(int IdCadete, int numPedido)
    {
        var cadetes = cadeteria.ListadoCadetes;
        var pedidos = cadeteria.ListadoPedidos;

        var pedido = pedidos.FirstOrDefault(p => p.NumPedido == numPedido);
        if (pedido == null) return NotFound($"El pedido numero: {numPedido}\nNo fue encontrado");
        var cadete = cadetes.FirstOrDefault(q => q.ID == IdCadete);
        if (cadete == null) return NotFound($"El cadete: {IdCadete}\nNo fue encontrado");
        pedido.CadeteAsignado = cadete;
        ADPedidos.Guardar(cadeteria.ListadoPedidos);
        return Ok(pedido);
    }
}