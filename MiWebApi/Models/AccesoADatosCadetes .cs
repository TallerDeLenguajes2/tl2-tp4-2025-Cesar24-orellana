using System.Text.Json;

public class AccesoADatosCadetes
{
    private readonly string _path;

    public AccesoADatosCadetes()
    {
        _path = "cadetes.json";
    }

    public List<Cadete> Obtener()
    {
        if (File.Exists(_path)) return new List<Cadete>();
        var ListaDeCadetes = JsonSerializer.Deserialize<List<Cadete>>(File.ReadAllText(_path)) ?? new List<Cadete>();
        return ListaDeCadetes;
    }
}