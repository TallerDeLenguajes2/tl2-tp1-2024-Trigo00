
public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;

    // Constructor sin parámetros requerido para la deserialización
    public Cadete() { }

    public Cadete(int idC, string nombreC, string direccionC, string telefonoC)
    {
        Id = idC;
        Nombre = nombreC;
        Direccion = direccionC;
        Telefono = telefonoC;
    }

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Telefono { get => telefono; set => telefono = value; }

}

