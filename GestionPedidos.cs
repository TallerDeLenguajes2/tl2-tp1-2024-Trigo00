public class GestionPedidos
{
    private Cadeteria miCadeteria;
    private Pedido pedidoSinAsignar;

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
            Console.WriteLine("2. Mostrar Pedidos");
            Console.WriteLine("3. Asignar pedido a cadete");
            Console.WriteLine("4. Cambiar estado de pedido");
            Console.WriteLine("5. Reasignar pedido a otro cadete");
            Console.WriteLine("6. Mostrar información cadeteria");
            Console.WriteLine("7. Mostrar Informe");
            Console.WriteLine("8. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    pedidoSinAsignar = DarDeAltaPedido();
                    miCadeteria.ListadoPedidos.Add(pedidoSinAsignar);
                    break;
                case "2":
                    MostrarPedidosSinAsignar(miCadeteria.ListadoPedidos);
                    break;
                case "3":
                    miCadeteria.AsignarPedidoACadete(miCadeteria, miCadeteria.ListadoPedidos);
                    break;
                case "4":
                    CambiarEstadoPedido();
                    break;
                case "5":
                    ReasignarPedido();
                    break;
                case "6":
                    MostrarInfoCadeteria();
                    break;
                case "7":
                    GenerarInforme();
                    break;
                case "8":
                    Console.WriteLine("Saliendo del sistema...");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    break;
            }
        } while (opcion != "8");
    }

    private Pedido DarDeAltaPedido()
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
        Pedido nuevoPedido = new Pedido(nroPedido, observaciones, nuevoCliente, Pedido.Estado.NA);

        Console.WriteLine("Pedido tomado exitosamente.");

        return nuevoPedido;

    }

    public void CambiarEstadoPedido()
    {
        Console.Write("Ingrese el número del pedido: ");
        int nroPedido = int.Parse(Console.ReadLine());
        Pedido pedido = BuscarPedido(nroPedido, miCadeteria);

        if (pedido != null)
        {
            Console.WriteLine("Seleccione el nuevo estado del pedido:");
            Console.WriteLine("1. Pendiente");
            Console.WriteLine("2. Entregado");
            Console.WriteLine("3. No Asignado(NA)");
            int opcionEstado = int.Parse(Console.ReadLine());

            switch (opcionEstado)
            {
                case 1:
                    pedido.EstadoPedido = Pedido.Estado.Pendiente;
                    break;
                case 2:
                    pedido.EstadoPedido = Pedido.Estado.Entregado;
                    break;
                case 3:
                    pedido.EstadoPedido = Pedido.Estado.NA;
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

    private void ReasignarPedido()
    {
        Console.Write("Ingrese el número del pedido a reasignar: ");
        int nroPedido = int.Parse(Console.ReadLine());
        Pedido pedido = BuscarPedido(nroPedido, miCadeteria);

        if (pedido != null)
        {
            Console.Write("Ingrese el ID del nuevo cadete: ");
            int idNuevoCadete = int.Parse(Console.ReadLine());
            Cadete nuevoCadete = miCadeteria.ListadoCadetes.FirstOrDefault(c => c.Id == idNuevoCadete);

            if (nuevoCadete != null)
            {
                pedido.CadeteAsignado = nuevoCadete;
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

    public static Pedido BuscarPedido(int idPedido, Cadeteria miCadeteria)
    {
        foreach (var miPedido in miCadeteria.ListadoPedidos)
        {
            if (miPedido.Nro == idPedido)
            {
                return miPedido;
            }
        }
        return null;
    }

    private void MostrarInfoCadeteria()
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
        }

        if (miCadeteria.ListadoPedidos != null && miCadeteria.ListadoPedidos.Count > 0)
        {
            Console.WriteLine("Informacion de Pedidos\n");

            foreach (var pedido in miCadeteria.ListadoPedidos)
            {
                Console.WriteLine("Pedido Nro: " + pedido.Nro);
                Console.WriteLine("Observacion del Pedido: " + pedido.Obs);
                Console.WriteLine("");

                Console.WriteLine("Informacion Cliente \n");
                Console.WriteLine("Nombre: " + pedido.Cliente.Nombre);
                Console.WriteLine("Direccion: " + pedido.Cliente.Direccion);
                Console.WriteLine("Telefono: " + pedido.Cliente.Telefono);
                Console.WriteLine("Alguna referencia para ubicar al cadete: " + pedido.Cliente.DatosReferenciaDireccion);
                Console.WriteLine("\nEstado del Pedido: " + pedido.EstadoPedido);
                if (pedido.CadeteAsignado != null)
                {
                    Console.WriteLine("Cadete designado: " + pedido.CadeteAsignado.Nombre);
                }
                else
                {
                    Console.WriteLine("Cadete designado: Sin asignar");
                    Console.WriteLine("");
                }
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
            // Filtro los pedidos asignados al cadete y con estado Entregado
            IEnumerable<Pedido> pedidosDelCadete = miCadeteria.ListadoPedidos
                .Where(pedido => pedido.CadeteAsignado != null &&
                                pedido.CadeteAsignado.Id == cadete.Id &&
                                pedido.EstadoPedido == Pedido.Estado.Entregado);
            int cantidadEnvios = pedidosDelCadete.Count();
            double montoGanado = miCadeteria.JornalACobrar(cadete.Id);
            Console.WriteLine($"Cadete: {cadete.Nombre}");
            Console.WriteLine($"Cantidad de Envíos: {cantidadEnvios}");
            Console.WriteLine($"Monto Ganado: ${montoGanado}\n");
        }

        // Calculo el total de envíos de todos los cadetes
        int totalEnvios = 0;
        foreach (var pedido in miCadeteria.ListadoPedidos)
        {
            if (pedido.EstadoPedido == Pedido.Estado.Entregado)
            {
                totalEnvios++;
            }
        }

        // Calculo el promedio de envíos por cadete
        double promedioEnvios = miCadeteria.ListadoCadetes.Count > 0 ? (double)totalEnvios / miCadeteria.ListadoCadetes.Count : 0;

        Console.WriteLine("=== Resumen ===");
        Console.WriteLine($"Total de Envíos: {totalEnvios}");
        Console.WriteLine($"Promedio de Envíos por Cadete: {promedioEnvios:F2}\n");
    }

    private void MostrarPedidosSinAsignar(List<Pedido> Lista)
    {
        Console.WriteLine("\n//// Pedidos ////\n");

        if (Lista == null || Lista.Count == 0)
        {
            Console.WriteLine("No tienes pedidos");
        }
        else
        {
            foreach (var pedido in Lista)
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
                if (pedido.CadeteAsignado != null)
                {
                    Console.WriteLine("Cadete designado: " + pedido.CadeteAsignado.Nombre);
                }
                else
                {
                    Console.WriteLine("Cadete designado: Sin asignar");
                    Console.WriteLine("");
                }

            }
        }
    }

}
