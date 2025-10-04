using System.Globalization;

public class Pedido
{
    public int NumPedido { get; set; }
    public string? Obs { get; set; }
    public Cliente cliente { get; private set; }
    public EstadoPedido Estado { get; private set; } = EstadoPedido.Pendiente;
    public Cadete CadeteAsignado { get; set; }
    
    public Pedido(int numPedido, string? obs, Cliente cliente, EstadoPedido estado = default, Cadete cadeteAsignado = null)
    {
        this.NumPedido = numPedido;
        this.Obs = obs;
        this.cliente = cliente;
        this.Estado = estado;
        this.CadeteAsignado = cadeteAsignado;
    }

    public string VerDireccionCliente()
    {
        string direccion = cliente.Direccion;
        return direccion;
    }
    public string MostrarDatosCliente()
    {
        string DatosCliente = $"Nombre: {cliente.Nombre}\nDatos de Referencia: {cliente.DatosRefereciaDireccion}\nTelefono: "+ cliente.Telefono;
        return DatosCliente;
    }

    public void CambiarEstado(EstadoPedido NuevoEstado)
    {
        Estado = NuevoEstado;
    }

    public object MostrarPedido()
    {
        string datos = MostrarDatosCliente();
        string direccion = VerDireccionCliente();
        string pedidoString = $"Pedido: {NumPedido} - Obs: {Obs} - Estado: {Estado}\n";
        pedidoString += $"{datos} {direccion} \n";
        if (CadeteAsignado != null) pedidoString += $"\nCadete Asignado: {CadeteAsignado}\n";
        else pedidoString += "\nNingun Cadete Asignado\n";
        return pedidoString;
    }

    public enum EstadoPedido
    {
        Entregado,
        Pendiente,
        Cancelado,
    }
}