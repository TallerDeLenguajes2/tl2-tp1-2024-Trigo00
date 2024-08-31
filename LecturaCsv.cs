public class LecturaCsv
{
    public static Cadeteria TraerDatosDeCsv(string archivo1, string archivo2, Cadeteria miCadeteria)
    {
        // Leo los datos de la cadetería desde el archivo CSV
        using (StreamReader archivo = new StreamReader(archivo2))
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

        // Leo los datos de los cadetes desde el archivo CSV
        using (StreamReader archivo = new StreamReader(archivo1))
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
