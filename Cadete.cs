public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    private List<Pedido> listadoPedidos;

    public Cadete(int idC, string nombreC, string direccionC, string telefonoC)
    {
        Id = idC;
        Nombre = nombreC;
        Direccion = direccionC;
        Telefono = telefonoC;
        ListadoPedidos = new List<Pedido>();
    }

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

    public double JornalACobrar()
    {
        int contador = 0;
        foreach (var item in listadoPedidos)
        {
            if (item.EstadoPedido == Pedido.Estado.Entregado)
            {
                contador++;
            }
        }

        return contador * 500;
    }

    public void AgregarPedido(Pedido pedido)
    {
        listadoPedidos.Add(pedido);
    }

    public void EliminarPedido(Pedido pedido)
    {
        listadoPedidos.Remove(pedido);
    }

    public int ObtenerPedidosTotales()
    {
        return listadoPedidos.Count();
    }

    public bool TienePedido(Pedido pedido)
    {
        return listadoPedidos.Contains(pedido);
    }
}

