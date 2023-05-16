using Prueba_Maui.Views;

namespace Prueba_Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		//Agregamos nuevas ruta
		//En donde, es necesario pasar como parametro, un string con la direccion y el tipo de la direccion

		Routing.RegisterRoute(nameof(MainContactos), typeof(MainContactos));
		Routing.RegisterRoute(nameof(AgregarEditarContacto), typeof(AgregarEditarContacto));
	}
	
}
