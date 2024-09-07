public class GestionPedidos
{
    private Cadeteria miCadeteria;
    //private Pedido pedidoSinAsignar;

    public GestionPedidos(Cadeteria cadeteria)
    {
        miCadeteria = cadeteria;
    }

    public List<string> MostrarMenu()
    {
        List<string> resultados = new List<string>();
        return GenerarOpciones(resultados);

    }

    private List<string> GenerarOpciones(List<string> resultados)
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
                    var pedidoSinAsignar = DarDeAltaPedido();
                    miCadeteria.ListadoPedidos.Add(pedidoSinAsignar);
                    resultados.Add("Pedido agregado correctamente.");
                    break;

                case "2":
                    return MostrarPedidosSinAsignar(miCadeteria.ListadoPedidos);

                case "3":
                    if (miCadeteria.AsignarPedidoACadete(miCadeteria, miCadeteria.ListadoPedidos))
                    {
                        resultados.Add("Pedido agregado al cadete correctamente.");
                    }
                    else
                    {
                        resultados.Add("Ocurrio un error, no pudiste tomar el pedido");
                    }
                    break;

                case "4":
                    if (CambiarEstadoPedido())
                    {
                        resultados.Add("Cambiaste el estado del pedido correctamente.");
                    }
                    else
                    {
                        resultados.Add("No pudo cambiar el estado del pedido, intente nuevamente.");
                    }
                    break;

                case "5":
                    if (ReasignarPedido())
                    {
                        resultados.Add("Pedido reasignado correctamente.");
                    }
                    else
                    {
                        resultados.Add("No pudo reasignar el pedido, intentelo nuevamente.");
                    }
                    break;
                case "6":
                    return MostrarInfoCadeteria();

                case "7":
                    return GenerarInforme();

                case "8":
                    resultados.Add("Saliendo del sistema...");
                    break;

                default:
                    resultados.Add("Opción no válida. Intente nuevamente.");
                    break;
            }
        } while (opcion != "8");

        return resultados;
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

        return nuevoPedido;

    }

    private bool CambiarEstadoPedido()
    {
        Console.Write("Ingrese el número del pedido: ");
        if (!int.TryParse(Console.ReadLine(), out int nroPedido))
        {
            return false;
        }

        Pedido pedido = BuscarPedido(nroPedido, miCadeteria);

        if (pedido != null)
        {
            Console.WriteLine("Seleccione el nuevo estado del pedido:");
            Console.WriteLine("1. Pendiente");
            Console.WriteLine("2. Entregado");
            Console.WriteLine("3. No Asignado (NA)");

            if (!int.TryParse(Console.ReadLine(), out int opcionEstado))
            {
                return false;
            }

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
                    return false;
            }

            return true;
        }

        return false;
    }


    private bool ReasignarPedido()
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
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
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

    private List<string> MostrarInfoCadeteria()
    {
        List<string> informacionCadeteria = new List<string>
    {
        "\nInformacion de Cadeteria\n",
        "Nombre Cadeteria: " + miCadeteria.Nombre,
        "Direccion Cadeteria: " + miCadeteria.Direccion,
        "Telefono Cadeteria: " + miCadeteria.Telefono,
        ""
    };

        foreach (var cadete in miCadeteria.ListadoCadetes)
        {
            informacionCadeteria.Add("ID: " + cadete.Id);
            informacionCadeteria.Add("Nombre: " + cadete.Nombre);
            informacionCadeteria.Add("Domicilio: " + cadete.Direccion);
            informacionCadeteria.Add("Telefono: " + cadete.Telefono);
        }

        if (miCadeteria.ListadoPedidos != null && miCadeteria.ListadoPedidos.Count > 0)
        {
            foreach (var pedido in miCadeteria.ListadoPedidos)
            {
                informacionCadeteria.Add("Pedido Nro: " + pedido.Nro);
                informacionCadeteria.Add("Observacion del Pedido: " + pedido.Obs);

                informacionCadeteria.Add("Nombre: " + pedido.Cliente.Nombre);
                informacionCadeteria.Add("Direccion: " + pedido.Cliente.Direccion);
                informacionCadeteria.Add("Telefono: " + pedido.Cliente.Telefono);
                informacionCadeteria.Add("Alguna referencia para ubicar al cadete: " + pedido.Cliente.DatosReferenciaDireccion);
                informacionCadeteria.Add("\nEstado del Pedido: " + pedido.EstadoPedido);

                if (pedido.CadeteAsignado != null)
                {
                    informacionCadeteria.Add("Cadete designado: " + pedido.CadeteAsignado.Nombre);
                }
                else
                {
                    informacionCadeteria.Add("Cadete designado: Sin asignar");
                }
            }
        }
        return informacionCadeteria;
    }


    private List<string> GenerarInforme()
    {
        List<string> informe = new List<string>
    {
        "=== Informe de Pedidos - Fin de Jornada ===\n"
    };
        foreach (var cadete in miCadeteria.ListadoCadetes)
        {

            IEnumerable<Pedido> pedidosDelCadete = miCadeteria.ListadoPedidos
                .Where(pedido => pedido.CadeteAsignado != null &&
                                 pedido.CadeteAsignado.Id == cadete.Id &&
                                 pedido.EstadoPedido == Pedido.Estado.Entregado);

            int cantidadEnvios = pedidosDelCadete.Count();
            double montoGanado = miCadeteria.JornalACobrar(cadete.Id);

            informe.Add($"Cadete: {cadete.Nombre}");
            informe.Add($"Cantidad de Envíos: {cantidadEnvios}");
            informe.Add($"Monto Ganado: ${montoGanado}\n");
        }

        int totalEnvios = miCadeteria.ListadoPedidos
            .Count(pedido => pedido.EstadoPedido == Pedido.Estado.Entregado);

        double promedioEnvios = miCadeteria.ListadoCadetes.Count > 0
            ? (double)totalEnvios / miCadeteria.ListadoCadetes.Count
            : 0;

        informe.Add($"Total de Envíos: {totalEnvios}");
        informe.Add($"Promedio de Envíos por Cadete: {promedioEnvios:F2}\n");

        return informe;
    }


    private List<string> MostrarPedidosSinAsignar(List<Pedido> Lista)
    {
        List<string> pedidosSinAsignar = new List<string>();

        if (Lista == null || Lista.Count == 0)
        {
            return pedidosSinAsignar;
        }
        else
        {
            foreach (var pedido in Lista)
            {
                List<string> informacionPedido = new List<string>
            {
                "Informacion del Pedido\n",
                "Pedido Nro: " + pedido.Nro,
                "Observacion del Pedido: " + pedido.Obs + "\n",
                "Informacion Cliente \n",
                "Nombre: " + pedido.Cliente.Nombre,
                "Direccion: " + pedido.Cliente.Direccion,
                "Telefono: " + pedido.Cliente.Telefono,
                "Alguna referencia para ubicar al cadete: " + pedido.Cliente.DatosReferenciaDireccion + "\n",
                "Estado del Pedido: " + pedido.EstadoPedido
            };

                if (pedido.CadeteAsignado != null)
                {
                    informacionPedido.Add("Cadete designado: " + pedido.CadeteAsignado.Nombre);
                }
                else
                {
                    informacionPedido.Add("Cadete designado: Sin asignar\n");
                }
                pedidosSinAsignar.AddRange(informacionPedido);
            }
            return pedidosSinAsignar;
        }
    }


}
