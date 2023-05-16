using Prueba_Maui.Clases;

namespace Prueba_Maui.Views;

//Query; informacion que recibe de la Ventana Anterior
[QueryProperty(nameof(ContactoId),"Id")]
public partial class AgregarEditarContacto : ContentPage
{
	private Contacto _contacto = new Contacto();
	public AgregarEditarContacto()
	{
		InitializeComponent();
	}

	public string ContactoId
	{
		set
		{
			if (Convert.ToInt32(value) != -1)
			{
                _contacto = Funciones.BuscarContacto(Convert.ToInt32(value));
				lblNombreCompleto.Text = $"{_contacto.Nombre} {_contacto.Apellido}";
				entryNombre.Text = _contacto.Nombre;
				entryApellido.Text = _contacto.Apellido;
				entryApodo.Text = _contacto.Apodo;
				entryTelefono.Text = _contacto.NumeroTelefonico.ToString();
				entryCorreo.Text = _contacto.Correo;
            }
			else
			{
				lblNombreCompleto.Text = "Agregar Contacto";
			}
		}
	}

    private void btnRegresar_Clicked(object sender, EventArgs e)
    {
		Funciones.BtnPresionado = false;
        Shell.Current.GoToAsync("..");
    }

    private void btnGuardar_Clicked(object sender, EventArgs e)
    {
        _contacto.Nombre = entryNombre.Text.Trim();
        _contacto.Apellido = entryApellido.Text.Trim();
        _contacto.Apodo = entryApodo.Text.Trim();
        _contacto.NumeroTelefonico = entryTelefono.Text.Trim();
        _contacto.Correo = entryCorreo.Text.Trim();

        if (_contacto.Id != -1)
		{
            Funciones.EditarContacto(_contacto.Id, _contacto);
        }
		else
		{
			if(Funciones.ListaOriginal.Count == 0)
			{
				_contacto.Id = 1;
				_contacto.PathImagen = _contacto.EscogerTonoImagen("");
            }
			else
			{
                _contacto.Id = Funciones.ListaOriginal.Count + 1;
				_contacto.PathImagen = _contacto.EscogerTonoImagen(Funciones.ListaOriginal[Funciones.ListaOriginal.Count - 1].PathImagen);
            }
			Funciones.ListaOriginal.Add(_contacto);
			//Funciones.GuardarJsonContactos();
		}

		Funciones.BtnPresionado = false;
        Shell.Current.GoToAsync("..");
    }
}