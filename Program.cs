Cadeteria miCadeteria = new Cadeteria();
miCadeteria = LecturaCsv.TraerDatosDeCsv("Cadete.csv", "Cadeteria.csv", miCadeteria);

GestionPedidos gestion = new GestionPedidos(miCadeteria);
gestion.MostrarMenu();