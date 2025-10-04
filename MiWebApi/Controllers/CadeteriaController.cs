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
        return accesoADatosJSONPedido.Cargar("../data/pedido.json");
    }
}