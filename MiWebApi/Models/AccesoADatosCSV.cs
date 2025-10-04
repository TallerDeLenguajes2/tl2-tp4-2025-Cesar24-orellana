using System.Globalization;
using System.Linq;
public class AccesoADatosCSV<T> : IAccesoADatos<T>
{
    public List<T> Cargar(string archivo)
    {
        if (!File.Exists(archivo))
        {
            Console.WriteLine($"El archivo {archivo} no fue encontrado!!");
            return new List<T>();
        }
        var lineas = File.ReadAllLines(archivo);
        var Lista = new List<T>();

        for (int i = 1; i < lineas.Length; i++)
        {
            var Separar = lineas[i].Split(',');
            if (typeof(T) == typeof(Pedido))
            {
                int NumPedido = int.Parse(Separar[0]);
                string? Obs = Separar[1];
                //Solució Momentanea para cargar la variable estado
                var estadoActual = Pedido.EstadoPedido.Pendiente; ;
                switch (int.Parse(Separar[2]))
                {
                    case 0:
                        estadoActual = Pedido.EstadoPedido.Entregado;
                        break;
                    case 1:
                        estadoActual = Pedido.EstadoPedido.Pendiente;
                        break;
                    case 2:
                        estadoActual = Pedido.EstadoPedido.Cancelado;
                        break;
                }
                string? Nombre = Separar[3];
                string? Direccion = Separar[4];
                double Telefono = int.Parse(Separar[5]);
                string? DatosRefereciaDireccion = Separar[6];
                var ClienteActual = new Cliente(Nombre, Direccion, Telefono, DatosRefereciaDireccion);
                var pedidoCarga = new Pedido(NumPedido, Obs, ClienteActual, estadoActual);
                Lista.Add((T)(object)pedidoCarga);
            }
            else
            {
                var ID = int.Parse(Separar[0]);
                var Nombre = Separar[1];
                var Direccion = Separar[2];
                var Telefono = int.Parse(Separar[3]);

                var CadeteCarga = new Cadete(ID, Nombre, Direccion, Telefono);
                Lista.Add((T)(object)CadeteCarga);
            }
        }
        return Lista;
    }

    public void Guardar(List<T> datos, string ruta)
    {
        using (var lineas = new StreamWriter(ruta))
        {
            if (typeof(T) == typeof(Cadete))
            {
                lineas.WriteLine("ID,Nombre,Direccion,Telefono");
                foreach (var item in datos)
                {
                    if (item is Cadete cadete)
                    {
                        lineas.WriteLine($"{cadete.ID}{cadete.Nombre}{cadete.Direccion}{cadete.Telefono}");
                    }
                }
            }
            else if (typeof(T) == typeof(Pedido))
            {
                lineas.WriteLine("NumPedido,Obs,Estado,ClienteNombre,ClienteDireccion,ClienteTelefono,ClienteDatosReferencia");
                foreach (var item in datos)
                {
                    if (item is Pedido pedido)
                    {
                        int estado = pedido.Estado switch
                        {
                            Pedido.EstadoPedido.Entregado => 0,
                            Pedido.EstadoPedido.Pendiente => 1,
                            _ => 2
                        };
                        lineas.WriteLine($"{pedido.NumPedido}{pedido.Obs}{estado}{pedido.cliente.Nombre}"+
                                $"{pedido.cliente.Direccion}{pedido.cliente.Telefono}{pedido.cliente.DatosRefereciaDireccion}");
                    }
                }
            }
        }
    }
}