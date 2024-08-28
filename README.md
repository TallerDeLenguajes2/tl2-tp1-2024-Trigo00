● ¿Cuál de estas relaciones considera que se realiza por composición y cuál por agregación?
Composición: La relación entre Pedido y Cliente es de composición. Esto se debe a que un Pedido no puede existir sin un Cliente, y si se elimina un Pedido, el Cliente asociado también debe ser eliminado. En otras palabras, el Cliente es parte integral del Pedido.
Agregación: La relación entre Cadete y Pedido es de agregación. Un Cadete puede tener uno o más Pedidos asignados, pero estos Pedidos pueden existir independientemente del Cadete. Además, un Pedido puede ser reasignado a otro Cadete, lo que refuerza la idea de que los Pedidos no dependen completamente de un Cadete específico.

● ¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?
Clase Cadetería:

AsignarPedido(Cadete cadete, Pedido pedido): Asigna un pedido a un cadete específico.
ReasignarPedido(Pedido pedido, Cadete nuevoCadete): Permite reasignar un pedido a otro cadete.
EliminarPedido(Pedido pedido): Elimina un pedido y, en consecuencia, elimina también el cliente asociado.
GenerarInforme(): Genera un informe con el total de envíos realizados por cada cadete y el total general, incluyendo el monto ganado.

Clase Cadete:

AgregarPedido(Pedido pedido): Agrega un pedido a la lista de pedidos del cadete.
EliminarPedido(Pedido pedido): Elimina un pedido de la lista de pedidos del cadete.
ObtenerTotalPedidos(): Devuelve el número total de pedidos completados por el cadete.

● Teniendo en cuenta los principios de abstracción y ocultamiento, que atributos, propiedades y métodos deberían ser públicos y cuáles privados.
Todos los atributos de las clases deben ser privadas, asi no puedan ser accedidas de cualquier lugar. Lo que si puede ser publico, son los metodos.

● ¿Cómo diseñaría los constructores de cada una de las clases?
El constructor de la clase cadeteria podria ir vacio o con nombre y direccion, en la clase cadete con los datos personales del cadete mientras que el ID se inicializa solo y la lista de pedidos igual, en la clase pedidos se le puede pasar el cliente en el constructor y en la clase cliente pueden ser todos los datos

● ¿Se le ocurre otra forma que podría haberse realizado el diseño de clases?
Si, podria agregar en la clase de pedido, un campo que haga referencia al cadete encargado de ese pedido.
En la clase de cadete, quitar el atributo de listadoPedidos pero poner algo que cuente la cantidad de pedidos entrego.
La clase cliente tambien podemos introducirla en la clase de pedidos.
