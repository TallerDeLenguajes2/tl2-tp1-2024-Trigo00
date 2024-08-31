using System.Text.Json;

// Clase base abstracta
public abstract class AccesoADatos
{
    public abstract Cadeteria CargarDatos(string archivoCadeteria, string archivoCadetes, Cadeteria miCadeteria, string extension);
}

// Clase derivada para acceso a CSV
public class AccesoCSV : AccesoADatos
{
    public override Cadeteria CargarDatos(string archivoCadeteria, string archivoCadetes, Cadeteria miCadeteria, string extension)
    {
        string carpetaCsv = @"C:\Users\trigo\OneDrive\Escritorio\Taller de Lenguajes 2\tl2-tp1-2024-Trigo00\Archivos_Csv";

        if (!Directory.Exists(carpetaCsv))
        {
            Directory.CreateDirectory(carpetaCsv);
        }

        string rutaArchivoCadeteria = Path.Combine(carpetaCsv, archivoCadeteria + extension);
        string rutaArchivoCadetes = Path.Combine(carpetaCsv, archivoCadetes + extension);

        if (!File.Exists(rutaArchivoCadeteria))
        {
            Console.WriteLine($"El archivo {archivoCadeteria + extension} no existe en la ruta especificada: {rutaArchivoCadeteria}");
            return miCadeteria;
        }

        if (!File.Exists(rutaArchivoCadetes))
        {
            Console.WriteLine($"El archivo {archivoCadetes + extension} no existe en la ruta especificada: {rutaArchivoCadetes}");
            return miCadeteria;
        }

        using (StreamReader archivo = new StreamReader(rutaArchivoCadeteria))
        {
            string separador = ",";
            string linea;
            while ((linea = archivo.ReadLine()) != null)
            {
                string[] fila = linea.Split(separador);
                miCadeteria.Nombre = fila[0];
                miCadeteria.Direccion = fila[1];
                miCadeteria.Telefono = fila[2];
            }
        }

        using (StreamReader archivo = new StreamReader(rutaArchivoCadetes))
        {
            string separador = ",";
            string linea;
            while ((linea = archivo.ReadLine()) != null)
            {
                string[] fila = linea.Split(separador);
                int idCadete = int.Parse(fila[0]);
                Cadete cadeteExistente = miCadeteria.ListadoCadetes.FirstOrDefault(c => c.Id == idCadete);

                if (cadeteExistente == null)
                {
                    Cadete nuevoCadete = new Cadete(idCadete, fila[1], fila[2], fila[3]);
                    miCadeteria.ListadoCadetes.Add(nuevoCadete);
                }
            }
        }

        return miCadeteria;
    }
}

// Clase derivada para acceso a JSON usando System.Text.Json
public class AccesoJSON : AccesoADatos
{
    public override Cadeteria CargarDatos(string archivoCadeteria, string archivoCadetes, Cadeteria miCadeteria, string extension)
    {
        string carpetaJson = @"C:\Users\trigo\OneDrive\Escritorio\Taller de Lenguajes 2\tl2-tp1-2024-Trigo00\Archivos_Json";

        if (!Directory.Exists(carpetaJson))
        {
            Directory.CreateDirectory(carpetaJson);
        }

        string rutaArchivoCadeteria = Path.Combine(carpetaJson, archivoCadeteria + extension);
        string rutaArchivoCadetes = Path.Combine(carpetaJson, archivoCadetes + extension);

        if (!File.Exists(rutaArchivoCadeteria))
        {
            Console.WriteLine($"El archivo {archivoCadeteria + extension} no existe en la ruta especificada: {rutaArchivoCadeteria}");
            return miCadeteria;
        }

        if (!File.Exists(rutaArchivoCadetes))
        {
            Console.WriteLine($"El archivo {archivoCadetes + extension} no existe en la ruta especificada: {rutaArchivoCadetes}");
            return miCadeteria;
        }

        try
        {
            // Lee y deserializa el archivo JSON de la cadetería
            string contenidoCadeteriaJson = File.ReadAllText(rutaArchivoCadeteria);
            miCadeteria = JsonSerializer.Deserialize<Cadeteria>(contenidoCadeteriaJson) ?? miCadeteria;

            // Lee y deserializa el archivo JSON de los cadetes
            string contenidoCadetesJson = File.ReadAllText(rutaArchivoCadetes);
            List<Cadete> cadetes = JsonSerializer.Deserialize<List<Cadete>>(contenidoCadetesJson) ?? new List<Cadete>();

            // Asigna los cadetes a la cadetería
            miCadeteria.ListadoCadetes = cadetes;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error al leer o deserializar los archivos JSON: {ex.Message}");
        }

        return miCadeteria;
    }
}