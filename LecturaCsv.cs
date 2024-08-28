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

                // Creo instancia de Cliente
                Cliente cliente = new Cliente(fila[6], fila[7], fila[8], fila[9]);

                // Creo instancia de Pedido
                Pedido.Estado estado;
                if (Enum.TryParse(fila[10], true, out estado))
                {
                    Pedido pedido = new Pedido(int.Parse(fila[4]), fila[5], cliente, estado);

                    // Creo o agrego un pedido a la instancia de Cadete
                    int idCadete = int.Parse(fila[0]);
                    Cadete cadeteExistente = miCadeteria.ListadoCadetes.FirstOrDefault(c => c.Id == idCadete);

                    if (cadeteExistente == null)
                    {
                        Cadete nuevoCadete = new Cadete(idCadete, fila[1], fila[2], fila[3]);
                        nuevoCadete.AgregarPedido(pedido);
                        miCadeteria.ListadoCadetes.Add(nuevoCadete);
                    }
                    else
                    {
                        cadeteExistente.AgregarPedido(pedido);
                    }
                }
                else
                {
                    Console.WriteLine($"Estado inválido en la línea: {linea}");
                }

            }
        }

        return miCadeteria;
    }
}
