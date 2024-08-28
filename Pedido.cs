
public class Pedido
{
    private int nro;
    private string obs;
    private Cliente cliente;
    private Estado estadoPedido;

    public int Nro { get => nro; set => nro = value; }
    public string Obs { get => obs; set => obs = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public Estado EstadoPedido { get => estadoPedido; set => estadoPedido = value; }

    public Pedido(int idP, string obsP, Cliente clienteP, Estado estadoP)
    {
        Nro = idP;
        Obs = obsP;
        Cliente = clienteP;
        EstadoPedido = estadoP;
    }

    public string VerDireccionCliente(Cliente miCliente)
    {
        return miCliente.Direccion;
    }

    public string VerDatosCliente(Cliente miCliente)
    {
        return "Nombre: " + miCliente.Nombre + "\nDireccion: " + miCliente.Direccion + "\nTelefono: " + miCliente.Telefono + "\nReferencia: " + miCliente.DatosReferenciaDireccion;
    }

    public enum Estado
    {
        Pendiente,
        Entregado
    }
}



