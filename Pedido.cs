
public class Pedido
{
    private int nro;
    private string obs;
    private Cliente cliente;
    private Estado estadoPedido;
    private Cadete cadeteAsignado;

    public int Nro { get => nro; set => nro = value; }
    public string Obs { get => obs; set => obs = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public Estado EstadoPedido { get => estadoPedido; set => estadoPedido = value; }
    public Cadete CadeteAsignado { get => cadeteAsignado; set => cadeteAsignado = value; }

    public Pedido(int idP, string obsP, Cliente clienteP, Estado estadoP)
    {
        Nro = idP;
        Obs = obsP;
        Cliente = clienteP;
        EstadoPedido = estadoP;
    }

    public enum Estado
    {
        Pendiente,
        Entregado,
        NA
    }
}



