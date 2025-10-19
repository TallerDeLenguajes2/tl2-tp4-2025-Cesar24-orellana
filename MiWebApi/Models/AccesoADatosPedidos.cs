using System.Text.Json;

public class AccesoADatosPedidos
{
    private readonly string _path;

    public AccesoADatosPedidos()
    {
        _path = "pedidos.json";
    }

    public List<Pedido> Obtener()
    {
        if (!File.Exists(_path)) return new List<Pedido>();
        var ListaDePedidos = JsonSerializer.Deserialize<List<Pedido>>(File.ReadAllText(_path)) ?? new List<Pedido>();
        return ListaDePedidos;

    }

    public void Guardar(List<Pedido> ListaDePedidos)
    {
        string PedidoString = JsonSerializer.Serialize(ListaDePedidos);
        FileStream fs = new FileStream(_path, FileMode.Create);
        using (StreamWriter st = new StreamWriter(fs))
        {
            st.WriteLine(PedidoString);
            st.Close();
        }
    }
}