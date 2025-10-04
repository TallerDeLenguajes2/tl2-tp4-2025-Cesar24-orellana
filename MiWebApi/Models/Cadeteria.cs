using System.Diagnostics;
using System.Globalization;

public class Cadeteria
{
    public string? Nombre { get; private set; }
    public double Telefono { get; private set; }
    public List<Cadete> ListadoCadetes { get; private set; }
    public List<Pedido> ListadoPedidos { get; private set; }

    public Cadeteria(string? nombre, double telefono, List<Cadete> listadoCadetes, List<Pedido> listadoPedidos)
    {
        this.Nombre = nombre;
        this.Telefono = telefono;
        this.ListadoCadetes = listadoCadetes;
        this.ListadoPedidos = listadoPedidos;
    }

    public string AsignarPedido(int idCadete, int numPedido)
    {
        var pedido = ListadoPedidos.FirstOrDefault(p => p.NumPedido == numPedido);
        var cadete = ListadoCadetes.FirstOrDefault(j => j.ID == idCadete);
        if (pedido == null || cadete == null) return "cadete o pedido no existe!!";
        pedido.CadeteAsignado = cadete;
        return "Cadete fue asignado con exito!!";
    }

    public double JornalACobrar(int Id)
    {
        return ListadoPedidos
            .Where(p => p.CadeteAsignado?.ID == Id && p.Estado == Pedido.EstadoPedido.Entregado)
            .Count() * 500;
    }

    public int PedidosEntregados(int idCadete)
    {
        var cadete = ListadoCadetes.FirstOrDefault(p => p.ID == idCadete);
        if (cadete == null) return 0;
        int CantEntregados = 0;
        foreach (var PedidoActual in ListadoPedidos)
        {
            if (PedidoActual.Estado == Pedido.EstadoPedido.Entregado && PedidoActual.CadeteAsignado == cadete)
            {
                CantEntregados++;
            }
        }
        return CantEntregados;
    }

    public void CambiarEstado(int numPedido, int newState)
    {
        var NuevoEstado = newState switch  // No me gusta usar un switch pero solo sera por esta vez
        {
            0 => Pedido.EstadoPedido.Entregado,
            1 => Pedido.EstadoPedido.Pendiente,
            _ => Pedido.EstadoPedido.Cancelado
        };
        var pedido = ListadoPedidos.FirstOrDefault(p => p.NumPedido == numPedido);
        if (pedido != null) pedido.CambiarEstado(NuevoEstado);
        //return "Pedido encontrado y actualizado";
    }

    public string DarDeAlta(int numero, string? obs, string? nombre, string? direccion, double telefono, string? datosRef)
    {
        var NuevoCliente = new Cliente(nombre, direccion, telefono, datosRef);
        var nuevoPedido = new Pedido(numero, obs, NuevoCliente, Pedido.EstadoPedido.Pendiente);
        ListadoPedidos.Add(nuevoPedido);
        return "Nuevo Pedido fue cargado con exito!";
    }

    public string ListaPedidoString()
    {
        if (ListadoPedidos.Count == 0) return "\nLista de Pedidos Vacia";
        string Lista = "\nLista de Pedidos\n";
        foreach (var PedidoActual in ListadoPedidos)
        {
            Lista += PedidoActual.MostrarPedido();
            Lista += "- - - -\n";
        }
        return Lista;
    }

    public string ListaCadetes()
    {
        if (ListadoCadetes.Count == 0) return "\nLista de Cadetes Vacia!";
        string Lista = "\nLista de Cadetes\n";
        foreach (var CadeteActual in ListadoCadetes)
        {
            Lista += CadeteActual.MostrarInformacionCadete()
                    + "- - - \n";
        }
        return Lista;
    }

    public int ultimoNumeroPedido()
    {
        if (ListadoPedidos.Count == 0) return 0;
        return ListadoPedidos.Max( p => p.NumPedido) + 1;
    }
}