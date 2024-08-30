public class GestionPedidos
{
    private Cadeteria miCadeteria;

    public GestionPedidos(Cadeteria cadeteria)
    {
        miCadeteria = cadeteria;
    }

    public void MostrarMenu()
    {
        string opcion;
        do
        {
            Console.WriteLine("\n=== Sistema de Gestión de Pedidos ===");
            Console.WriteLine("1. Dar de alta un pedido");
            Console.WriteLine("2. Asignar pedido a cadete");
            Console.WriteLine("3. Cambiar estado de pedido");
            Console.WriteLine("4. Reasignar pedido a otro cadete");
            Console.WriteLine("5. Mostrar info");
            Console.WriteLine("6. Mostrar Informe");
            Console.WriteLine("7. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    DarDeAltaPedido();
                    break;
                case "2":
                    Console.Write("Ingrese el ID del pedido: ");
                    int idPedido = int.Parse(Console.ReadLine());
                    Console.Write("Ingrese el ID del cadete al que quiere agregar el pedido: ");
                    int idCadete = int.Parse(Console.ReadLine());
                    miCadeteria.AsignarPedidoACadete(idCadete, idPedido);
                    break;
                case "3":
                    CambiarEstadoPedido();
                    break;
                case "4":
                    ReasignarPedido();
                    break;
                case "5":
                    MostrarPedidos();
                    break;
                case "6":
                    GenerarInforme();
                    break;
                case "7":
                    Console.WriteLine("Saliendo del sistema...");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    break;
            }
        } while (opcion != "7");
    }

    public void DarDeAltaPedido()
    {
        Console.Write("Ingrese el número del pedido: ");
        int nroPedido = int.Parse(Console.ReadLine());
        Console.Write("Ingrese observaciones del pedido: ");
        string observaciones = Console.ReadLine();
        Console.Write("Ingrese el nombre del cliente: ");
        string nombreCliente = Console.ReadLine();
        Console.Write("Ingrese la dirección del cliente: ");
        string direccionCliente = Console.ReadLine();
        Console.Write("Ingrese el teléfono del cliente: ");
        string telefonoCliente = Console.ReadLine();
        Console.Write("Ingrese alguna referencia: ");
        string referencia = Console.ReadLine();

        Cliente nuevoCliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente, referencia);
        Pedido nuevoPedido = new Pedido(nroPedido, observaciones, nuevoCliente, Pedido.Estado.Pendiente);

        Console.Write("Ingrese el ID del cadete al que se le asignará el pedido: ");
        int idCadete = int.Parse(Console.ReadLine());
        Cadete cadete = miCadeteria.ListadoCadetes.FirstOrDefault(c => c.Id == idCadete);

        if (cadete != null)
        {
            cadete.AgregarPedido(nuevoPedido, miCadeteria);
            Console.WriteLine("Pedido asignado exitosamente al cadete.");
        }
        else
        {
            Console.WriteLine("Cadete no encontrado.");
        }
    }

    public void CambiarEstadoPedido()
    {
        Console.Write("Ingrese el número del pedido: ");
        int nroPedido = int.Parse(Console.ReadLine());
        Pedido pedido = BuscarPedidoPorNumero(nroPedido, miCadeteria);

        if (pedido != null)
        {
            Console.WriteLine("Seleccione el nuevo estado del pedido:");
            Console.WriteLine("1. Pendiente");
            Console.WriteLine("2. Entregado");
            int opcionEstado = int.Parse(Console.ReadLine());

            switch (opcionEstado)
            {
                case 1:
                    pedido.EstadoPedido = Pedido.Estado.Pendiente;
                    break;
                case 2:
                    pedido.EstadoPedido = Pedido.Estado.Entregado;
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            Console.WriteLine("Estado del pedido actualizado.");
        }
        else
        {
            Console.WriteLine("Pedido no encontrado.");
        }
    }

    public void ReasignarPedido()
    {
        Console.Write("Ingrese el número del pedido a reasignar: ");
        int nroPedido = int.Parse(Console.ReadLine());
        Pedido pedido = BuscarPedidoPorNumero(nroPedido, miCadeteria);

        if (pedido != null)
        {
            Console.Write("Ingrese el ID del nuevo cadete: ");
            int idNuevoCadete = int.Parse(Console.ReadLine());
            Cadete nuevoCadete = miCadeteria.ListadoCadetes.FirstOrDefault(c => c.Id == idNuevoCadete);

            if (nuevoCadete != null)
            {
                var cadeteAnterior = miCadeteria.ListadoCadetes.FirstOrDefault(c => c.TienePedido(pedido, miCadeteria));
                if (cadeteAnterior != null)
                {
                    cadeteAnterior.EliminarPedido(pedido, miCadeteria);
                }
                nuevoCadete.AgregarPedido(pedido, miCadeteria);
                Console.WriteLine("Pedido reasignado exitosamente.");
            }
            else
            {
                Console.WriteLine("Nuevo cadete no encontrado.");
            }
        }
        else
        {
            Console.WriteLine("Pedido no encontrado.");
        }
    }

    public static Pedido BuscarPedidoPorNumero(int idPedido, Cadeteria miCadeteria)
    {
        foreach (var miPedido in miCadeteria.ListadoPedidos)
        {
            if(miPedido.Nro == idPedido){
                return miPedido;
            }else{
                return null;
            }
        }
        return null;
    }

    private void MostrarPedidos()
    {

        Console.WriteLine("\nInformacion de Cadeteria\n");
        Console.WriteLine("Nombre Cadeteria: " + miCadeteria.Nombre);
        Console.WriteLine("Direccion Cadeteria: " + miCadeteria.Direccion);
        Console.WriteLine("Telefono Cadeteria: " + miCadeteria.Telefono);
        Console.WriteLine("");

        foreach (var cadete in miCadeteria.ListadoCadetes)
        {
            Console.WriteLine("Informacion de Cadete\n");
            Console.WriteLine("ID: " + cadete.Id);
            Console.WriteLine("Nombre: " + cadete.Nombre);
            Console.WriteLine("Domicilio: " + cadete.Direccion);
            Console.WriteLine("Telefono: " + cadete.Telefono);
            Console.WriteLine("");

            foreach (var pedido in miCadeteria.ListadoPedidos)
            {
                Console.WriteLine("Informacion del Pedido\n");
                Console.WriteLine("Pedido Nro: " + pedido.Nro);
                Console.WriteLine("Observacion del Pedido: " + pedido.Obs);
                Console.WriteLine("");

                Console.WriteLine("Informacion Cliente \n");
                Console.WriteLine("Nombre: " + pedido.Cliente.Nombre);
                Console.WriteLine("Direccion: " + pedido.Cliente.Direccion);
                Console.WriteLine("Telefono: " + pedido.Cliente.Telefono);
                Console.WriteLine("Alguna referencia para ubicar al cadete: " + pedido.Cliente.DatosReferenciaDireccion);
                Console.WriteLine("\nEstado del Pedido: " + pedido.EstadoPedido);
                Console.WriteLine("");
            }
        }
    }

     private void GenerarInforme()
    {
        Console.WriteLine("=== Informe de Pedidos - Fin de Jornada ===\n");

        // Calculo el monto ganado y la cantidad de envíos de cada cadete
        foreach (var cadete in miCadeteria.ListadoCadetes)
        {
            int cantidadEnvios = miCadeteria.ListadoPedidos.Count;
            double montoGanado = miCadeteria.JornalACobrar(cadete.Id); 
            Console.WriteLine($"Cadete: {cadete.Nombre}");
            Console.WriteLine($"Cantidad de Envíos: {cantidadEnvios}");
            Console.WriteLine($"Monto Ganado: ${montoGanado}\n");
        }

        // Calculo el total de envíos de todos los cadetes
        int totalEnvios = 0;
        foreach (var pedido in miCadeteria.ListadoPedidos)
        {
            if(pedido.EstadoPedido == Pedido.Estado.Entregado){
                totalEnvios++;
            }
        }

        // Calculo el promedio de envíos por cadete
        double promedioEnvios = miCadeteria.ListadoCadetes.Count > 0 ? (double)totalEnvios / miCadeteria.ListadoCadetes.Count : 0;

        Console.WriteLine("=== Resumen ===");
        Console.WriteLine($"Total de Envíos: {totalEnvios}");
        Console.WriteLine($"Promedio de Envíos por Cadete: {promedioEnvios:F2}\n");
    }
}
