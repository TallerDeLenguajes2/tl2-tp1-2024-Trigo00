Console.WriteLine("1. CSV");
Console.WriteLine("2. JSON");
Console.Write("\nSeleccione el tipo de acceso a datos:");
string opcion = Console.ReadLine();

AccesoADatos accesoDatos;
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

Cadeteria miCadeteria = new Cadeteria(); 
miCadeteria = accesoDatos.CargarDatos("Cadeteria", "Cadete", miCadeteria, extension);

GestionPedidos gestion = new GestionPedidos(miCadeteria);
gestion.MostrarMenu();
