using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class CadeteriaController : ControllerBase
{
    private readonly AccesoADatosJSON<Cadete> accesoADatosJSONCadete;
    private readonly AccesoADatosJSON<Pedido> accesoADatosJSONPedido;
    public CadeteriaController()
    {
        accesoADatosJSONCadete = new AccesoADatosJSON<Cadete>();
        accesoADatosJSONPedido = new AccesoADatosJSON<Pedido>();
    }

    [HttpGet("GetPedidos")]
    public List<Pedido> GetPedidos()
    {
        return accesoADatosJSONPedido.Cargar("data/pedido.json");
    }

    [HttpGet("GetCadetes")]
    public List<Cadete> GetCadetes()
    {
        return accesoADatosJSONCadete.Cargar("data/cadetes.json");
    }

    [HttpGet("GetInforme")]
    public IActionResult GetInforme()
    {
        var cadete = accesoADatosJSONCadete.Cargar("data/cadetes.json");
        var pedidos = accesoADatosJSONPedido.Cargar("data/pedido.json");

        var informe = new
        {
            TotalCadetes = cadete.Count,
            TotalPedidos = pedidos.Count,
            Cadetes = cadete
        };
        return Ok(informe);
    }

    [HttpPost("AgregarPedido")]
    public IActionResult AgregarPedido(Pedido pedido)
    {
        var pedidos = accesoADatosJSONPedido.Cargar("data/pedido.json");
        pedidos.Add(pedido);
        accesoADatosJSONPedido.Guardar(pedidos, "data/pedido.json");
        return Ok(pedido);
    }
}