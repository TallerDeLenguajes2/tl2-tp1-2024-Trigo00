using System.Text.Json;

public interface IAccesoADatos
{
    public bool Existe(string nombreArchivo);
    public List<Cadete> LeerCadetes(string nombreArchivo);
    public Cadeteria LeerCadeteria(string nombreArchivo);
    Cadeteria CrearCadeteria(string nombreArchivoCadeteria, string nombreArchivoCadetes);
 
}

public class AccesoCSV : IAccesoADatos
{
    public bool Existe(string nombreArchivo)
    {
        string ruta = "Archivos_Csv/" + nombreArchivo;
        return File.Exists(ruta);
    }

    public List<Cadete> LeerCadetes(string nombreArchivo)
    {
        string ruta = "Archivos_Csv/" + nombreArchivo;
        List<Cadete> cadetes = new List<Cadete>();
        using (var archivoOpen = new FileStream(ruta, FileMode.Open))
        {
            using (var strReader = new StreamReader(archivoOpen))
            {
                string linea;
                while ((linea = strReader.ReadLine()) != null)
                {
                    var datos = linea.Split(',');
                    var cadete = new Cadete(int.Parse(datos[0]), datos[1],  datos[2], datos[3]);
                    cadetes.Add(cadete);
                }
            }
        }

        return cadetes;
    }

    public Cadeteria LeerCadeteria(string nombreArchivo)
    {
        string ruta = "Archivos_Csv/" + nombreArchivo;
        string informacionCadeteria;
        using (var archivoOpen = new FileStream(ruta, FileMode.Open))
        {
            using (var strReader = new StreamReader(archivoOpen))
            {
                informacionCadeteria = strReader.ReadToEnd();
                archivoOpen.Close();
            }
        }  
        string[] datos = informacionCadeteria.Split(",");
        Cadeteria cadeteria = new Cadeteria(datos[0], datos[1], datos[2]);

        return cadeteria;
    }

    public Cadeteria CrearCadeteria(string nombreArchivoCadeteria, string nombreArchivoCadetes)
    {
        Cadeteria cadeteria = LeerCadeteria(nombreArchivoCadeteria);
        List<Cadete> cadetes = LeerCadetes(nombreArchivoCadetes);
        cadeteria.ListadoCadetes = cadetes;

        return cadeteria;
    }
}

public class AccesoJSON : IAccesoADatos
{
    public bool Existe(string nombreArchivo)
    {
        string ruta = "Archivos_Json/" + nombreArchivo;
        return File.Exists(ruta);
    }

    public List<Cadete> LeerCadetes(string nombreArchivo)
    {
        string ruta = "Archivos_Json/" + nombreArchivo;
        string cadetesJson;
        using (var archivoOpen = new FileStream(ruta, FileMode.Open))
        {
            using (var strReader = new StreamReader(archivoOpen))
            {
                cadetesJson = strReader.ReadToEnd();
                archivoOpen.Close();
            }
        }
        var cadetes = JsonSerializer.Deserialize<List<Cadete>>(cadetesJson);

        return cadetes;
    }

    public Cadeteria LeerCadeteria(string nombreArchivo)
    {
        string ruta = "Archivos_Json/" + nombreArchivo;
        string cadeteriaJson;
        using (var archivoOpen = new FileStream(ruta, FileMode.Open))
        {
            using (var strReader = new StreamReader(archivoOpen))
            {
                cadeteriaJson = strReader.ReadToEnd();
                archivoOpen.Close();
            }
        }
        var cadeteria = JsonSerializer.Deserialize<Cadeteria>(cadeteriaJson);
        
        return cadeteria;
    }

    public Cadeteria CrearCadeteria(string nombreArchivoCadeteria, string nombreArchivoCadetes)
    {
        Cadeteria cadeteria = LeerCadeteria(nombreArchivoCadeteria);
        List<Cadete> cadetes = LeerCadetes(nombreArchivoCadetes);
        cadeteria.ListadoCadetes = cadetes;

        return cadeteria;
    }
}

// Clase base abstracta
// public abstract class AccesoADatos
// {
//     public abstract Cadeteria CargarDatos(string archivoCadeteria, string archivoCadetes, Cadeteria miCadeteria, string extension);
// }

// Clase derivada para acceso a CSV
// public class AccesoCSV : AccesoADatos
// {
//     public override Cadeteria CargarDatos(string archivoCadeteria, string archivoCadetes, Cadeteria miCadeteria, string extension)
//     {
//         string carpeta = @"C:\Users\trigo\OneDrive\Escritorio\Taller de Lenguajes 2\tl2-tp1-2024-Trigo00\Archivos_Csv";
//         ExistenciaCarpeta(carpeta);

//         string rutaArchivoCadeteria = Path.Combine(carpeta, archivoCadeteria + extension);
//         string rutaArchivoCadetes = Path.Combine(carpeta, archivoCadetes + extension);
//         miCadeteria = ExistenciaArchivoCadeteria(archivoCadeteria, miCadeteria, extension, rutaArchivoCadeteria);
//         miCadeteria = ExistenciaArchivoCadetes(archivoCadetes, miCadeteria, extension, rutaArchivoCadetes);
//         LeerCadeteria(miCadeteria, rutaArchivoCadeteria);
//         LeerCadetes(miCadeteria, rutaArchivoCadetes);

//         return miCadeteria;
//     }

//     private static void LeerCadetes(Cadeteria miCadeteria, string rutaArchivoCadetes)
//     {
//         using (StreamReader archivo = new StreamReader(rutaArchivoCadetes))
//         {
//             string separador = ",";
//             string linea;
//             while ((linea = archivo.ReadLine()) != null)
//             {
//                 string[] fila = linea.Split(separador);
//                 int idCadete = int.Parse(fila[0]);
//                 Cadete cadeteExistente = miCadeteria.ListadoCadetes.FirstOrDefault(c => c.Id == idCadete);

//                 if (cadeteExistente == null)
//                 {
//                     Cadete nuevoCadete = new Cadete(idCadete, fila[1], fila[2], fila[3]);
//                     miCadeteria.ListadoCadetes.Add(nuevoCadete);
//                 }
//             }
//         }
//     }

//     private static void LeerCadeteria(Cadeteria miCadeteria, string rutaArchivoCadeteria)
//     {
//         using (StreamReader archivo = new StreamReader(rutaArchivoCadeteria))
//         {
//             string separador = ",";
//             string linea;
//             while ((linea = archivo.ReadLine()) != null)
//             {
//                 string[] fila = linea.Split(separador);
//                 miCadeteria.Nombre = fila[0];
//                 miCadeteria.Direccion = fila[1];
//                 miCadeteria.Telefono = fila[2];
//             }
//         }
//     }

//     private static Cadeteria ExistenciaArchivoCadetes(string archivoCadetes, Cadeteria miCadeteria, string extension, string rutaArchivoCadetes)
//     {
//         if (!File.Exists(rutaArchivoCadetes))
//         {
//             Console.WriteLine($"El archivo {archivoCadetes + extension} no existe en la ruta especificada: {rutaArchivoCadetes}");
//             return miCadeteria;
//         }
//         return new Cadeteria();
//     }

//     private static Cadeteria ExistenciaArchivoCadeteria(string archivoCadeteria, Cadeteria miCadeteria, string extension, string rutaArchivoCadeteria)
//     {
//         if (!File.Exists(rutaArchivoCadeteria))
//         {
//             Console.WriteLine($"El archivo {archivoCadeteria + extension} no existe en la ruta especificada: {rutaArchivoCadeteria}");
//             return miCadeteria;
//         }
//         return new Cadeteria();
//     }

//     private static void ExistenciaCarpeta(string carpeta)
//     {
//         if (!Directory.Exists(carpeta))
//         {
//             Directory.CreateDirectory(carpeta);
//         }
//     }
// }

// //Clase derivada para acceso a JSON 
// public class AccesoJSON : AccesoADatos
// {
//     public override Cadeteria CargarDatos(string archivoCadeteria, string archivoCadetes, Cadeteria miCadeteria, string extension)
//     {
//         string carpetaJson = @"C:\Users\trigo\OneDrive\Escritorio\Taller de Lenguajes 2\tl2-tp1-2024-Trigo00\Archivos_Json";

//         if (!Directory.Exists(carpetaJson))
//         {
//             Directory.CreateDirectory(carpetaJson);
//         }

//         string rutaArchivoCadeteria = Path.Combine(carpetaJson, archivoCadeteria + extension);
//         string rutaArchivoCadetes = Path.Combine(carpetaJson, archivoCadetes + extension);

//         if (!File.Exists(rutaArchivoCadeteria))
//         {
//             Console.WriteLine($"El archivo {archivoCadeteria + extension} no existe en la ruta especificada: {rutaArchivoCadeteria}");
//             return miCadeteria;
//         }

//         if (!File.Exists(rutaArchivoCadetes))
//         {
//             Console.WriteLine($"El archivo {archivoCadetes + extension} no existe en la ruta especificada: {rutaArchivoCadetes}");
//             return miCadeteria;
//         }

//         try
//         {
//             string contenidoCadeteriaJson = File.ReadAllText(rutaArchivoCadeteria);
//             miCadeteria = JsonSerializer.Deserialize<Cadeteria>(contenidoCadeteriaJson) ?? miCadeteria;

//             string contenidoCadetesJson = File.ReadAllText(rutaArchivoCadetes);
//             List<Cadete> cadetes = JsonSerializer.Deserialize<List<Cadete>>(contenidoCadetesJson) ?? new List<Cadete>();

//             // Asigno los cadetes a la cadetería
//             miCadeteria.ListadoCadetes = cadetes;
//         }
//         catch (JsonException ex)
//         {
//             Console.WriteLine($"Error al leer o deserializar los archivos JSON: {ex.Message}");
//         }

//         return miCadeteria;
//     }
// }