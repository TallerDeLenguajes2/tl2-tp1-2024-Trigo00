
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
            if (idCadete == item.CadeteAsignado.Id && item.EstadoPedido == Pedido.Estado.Entregado)
            {
                contador++;
            }
        }

        return contador * 500;
    }

    public void AsignarPedidoACadete(int idCadete, int idPedido)
    {
        
        Cadeteria miCadeteria = new Cadeteria();
        Pedido pedido = GestionPedidos.BuscarPedidoPorNumero(idPedido, miCadeteria);

        if (pedido != null)
        {
            int bandera = 0;
            foreach (var miCadete in miCadeteria.listadoCadetes)
            {
                if(miCadete.Id == idCadete){
                    miCadete.AgregarPedido(pedido, miCadeteria);
                    bandera = 1;
                    Console.WriteLine("Pedido asignado exitosamente al cadete.");
                }
            }
            if (bandera == 0)
            {
                Console.WriteLine("Cadete no encontrado.");
            }
        }
        else
        {
            Console.WriteLine("Pedido no encontrado.");
        }
    }

}


