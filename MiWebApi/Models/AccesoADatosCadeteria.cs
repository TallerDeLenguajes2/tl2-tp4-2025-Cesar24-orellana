using System.Text.Json;

public class AccesoADatosCadeteria
{
    private readonly string _path;
    public AccesoADatosCadeteria()
    {
        _path = "cadeteria.json";
    }

    public Cadeteria Obtener()
    {
        if (File.Exists(_path)) return new Cadeteria();
        var NuevaCadeteria = JsonSerializer.Deserialize<Cadeteria>(File.ReadAllText(_path)) ?? new Cadeteria();
        return NuevaCadeteria;
    }
}