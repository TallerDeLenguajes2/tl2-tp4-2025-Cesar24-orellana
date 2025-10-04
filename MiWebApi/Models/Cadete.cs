using System.Globalization;

public class Cadete
{
    public int ID { get; private set; }
    public string? Nombre { get; private set; }
    public string? Direccion { get; private set; }
    public double Telefono { get; private set; }

    public Cadete(int iD, string? nombre, string? direccion, double telefono)
    {
        this.ID = iD;
        this.Nombre = nombre;
        this.Direccion = direccion;
        this.Telefono = telefono;
    }

    public string MostrarInformacionCadete()
    {
        string MensajeCadete = $"Cargando Cadete\n - - - -\n"
                            + $"Cadete ID: {ID} - Nombre: {Nombre} - Dirección: {Direccion} - Telefono: {Telefono}";
        return MensajeCadete;
    }

}