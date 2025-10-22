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
        // if (File.Exists(_path))
        // {
        //     var NuevaCadeteria = JsonSerializer.Deserialize<Cadeteria>(File.ReadAllText(_path)) ?? new Cadeteria();
        //     return NuevaCadeteria;
        // }
        //return new Cadeteria();
        if (!File.Exists(_path)) return new Cadeteria();
        string stringCadeteria = File.ReadAllText(_path);
        var NuevaCadeteria = JsonSerializer.Deserialize<Cadeteria>(stringCadeteria);  // <- Ubicacion del Error
        return NuevaCadeteria;
    }

    /* public void Guardar(Cadeteria cadeteria){
        string CadeteriaString = JsonSerializer.Serialize(cadeteria);
        FileStream fs = new FileStream(_path, FileMode.Create);
        using (StreamWriter st = new StreamWriter(fs))
        {
            st.WriteLine("{0}",CadeteriaString);
            st.Close();
        }
    } */
}