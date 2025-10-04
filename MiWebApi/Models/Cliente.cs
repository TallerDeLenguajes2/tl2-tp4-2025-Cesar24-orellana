using System.Globalization;

public class Cliente
{
    public string? Nombre { get; set; }
    public string? Direccion { get; set; }
    public double Telefono { get; set; }
    public string? DatosRefereciaDireccion { get; set; }

    public Cliente(string? nombre, string? direccion, double telefono, string? datosRefereciaDireccion)
    {
        this.Nombre = nombre;
        this.Direccion = direccion;
        this.Telefono = telefono;
        this.DatosRefereciaDireccion = datosRefereciaDireccion;
    }
}