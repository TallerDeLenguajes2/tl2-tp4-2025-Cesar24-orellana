using System.Text.Json;

public class AccesoADatosCadetes
{
    private readonly string _path;

    public AccesoADatosCadetes(){
        _path = "cadetes.json";
    }

    public Cadete Obtener(){
        if (File.Exists(_path))
        {
            var NuevoCadete = JsonSerializer.Deserialize<Cadete>(File.ReadAllText(_path));
            return NuevoCadete;
        }
        return new Cadete();
    }
}