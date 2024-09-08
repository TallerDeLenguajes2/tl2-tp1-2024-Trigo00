Console.WriteLine("1. CSV");
Console.WriteLine("2. JSON");
Console.Write("\nSeleccione el tipo de acceso a datos: ");
string opcion = Console.ReadLine();

IAccesoADatos accesoDatos;
string extension;

switch (opcion)
{
    case "1":
        accesoDatos = new AccesoCSV();
        extension = ".csv";
        break;
    case "2":
        accesoDatos = new AccesoJSON();
        extension = ".json";
        break;
    default:
        Console.WriteLine("Opción no válida. Se utilizará acceso CSV por defecto.");
        accesoDatos = new AccesoCSV();
        extension = ".csv";
        break;
}

string nombreArchivoCadeteria = $"Cadeteria{extension}";
string nombreArchivoCadetes = $"Cadete{extension}";

if (accesoDatos.Existe(nombreArchivoCadeteria) && accesoDatos.Existe(nombreArchivoCadetes))
{
    Cadeteria miCadeteria = accesoDatos.CrearCadeteria(nombreArchivoCadeteria, nombreArchivoCadetes);

    GestionPedidos gestion = new GestionPedidos(miCadeteria);
    gestion.MostrarMenu();
}
else
{
    Console.WriteLine("Uno o ambos archivos no existen. Asegúrese de que los archivos estén en la carpeta correcta.");
}

