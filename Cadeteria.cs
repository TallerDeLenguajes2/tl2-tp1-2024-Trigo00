
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

    public Cadeteria(string nombre, string telefono, string direccion)
    {
        Nombre = nombre;
        Telefono = telefono;
        Direccion = direccion;
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

    public bool AsignarPedidoACadete(int idPedido)
    {  
        Console.Write("Ingrese el ID del cadete al que se le asignará el pedido: ");
        int idCadete = int.Parse(Console.ReadLine());
        return RealizarAccion(idCadete, idPedido);
    }

    private bool RealizarAccion(int idCadete, int idPedido)
    {
        
        var cadeteElegido = listadoCadetes.Find(c => c.Id == idCadete);
        var pedidoElegido = listadoPedidos.Find(p => p.Nro == idPedido);
        if(cadeteElegido != null && pedidoElegido != null){
            pedidoElegido.CadeteAsignado = cadeteElegido;
            return true;
        }
        return false;
    
    }
}


