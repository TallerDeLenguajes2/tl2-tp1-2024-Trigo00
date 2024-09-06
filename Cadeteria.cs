
public class Cadeteria
{
    private string nombre;
    private string telefono;
    private string direccion;
    private List<Cadete> listadoCadetes;
    private List<Pedido> listadoPedidos;

    public string Nombre { get => nombre; set => nombre = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
    public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

    public Cadeteria()
    {
        ListadoCadetes = new List<Cadete>();
        ListadoPedidos = new List<Pedido>();
    }

    public double JornalACobrar(int idCadete)
    {
        int contador = 0;
        foreach (var item in listadoPedidos)
        {
            if (item.CadeteAsignado != null && idCadete == item.CadeteAsignado.Id && item.EstadoPedido == Pedido.Estado.Entregado)
            {
                contador++;
            }
        }

        return contador * 500;
    }

    public bool AsignarPedidoACadete(Cadeteria miCadeteria, List<Pedido> pedidosSinAsignar)
    {
        return RealizarAccion(miCadeteria, pedidosSinAsignar);
    }

    private bool RealizarAccion(Cadeteria miCadeteria, List<Pedido> pedidosSinAsignar)
    {
        if (pedidosSinAsignar == null || pedidosSinAsignar.Count == 0)
        {
            return false;
        }
        else
        {
            foreach (var pedidoSA in pedidosSinAsignar)
            {

                Console.Write("Ingrese el ID del cadete al que se le asignará el pedido: ");
                int idCadete = int.Parse(Console.ReadLine());
                Cadete cadete = miCadeteria.ListadoCadetes.FirstOrDefault(c => c.Id == idCadete);

                if (cadete != null)
                {
                    pedidoSA.CadeteAsignado = cadete;
                    pedidoSA.EstadoPedido = Pedido.Estado.Pendiente;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}


