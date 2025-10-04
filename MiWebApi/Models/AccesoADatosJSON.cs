using System.Globalization;
using System.Linq;
using System.Text.Json;
public class AccesoADatosJSON<T> : IAccesoADatos<T>
{
    public List<T> Cargar(string archivo)
    {
        if (!File.Exists(archivo))
        {
            Console.WriteLine($"El archivo {archivo} no fue encontrado!!");
            return new List<T>();
        }

        string textoJson = File.ReadAllText(archivo);
        return JsonSerializer.Deserialize<List<T>>(textoJson) ?? new List<T>();
    }

    public void Guardar(List<T> datos, string ruta)
    {
        var Opcion = new JsonSerializerOptions { WriteIndented = true };
        string infoJson = JsonSerializer.Serialize(datos, Opcion);
        File.WriteAllText(ruta, infoJson);
    }
}