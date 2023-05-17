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
    protected override void OnAppearing()
    {
        base.OnAppearing();

		if(_contacto.Id != -1) btnEliminar.IsVisible = true;
		else btnEliminar.IsVisible = false;
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
		try
		{
            ControlarNullsEntrys();

			if (!string.IsNullOrEmpty(entryTelefono.Text.Trim()))
			{
                if (long.TryParse(entryTelefono.Text, out long _numeroTelefono))
                {
                    AsignarDatos();
                }
                else throw new Exception("En el Campo de Numero de Telefono solo se Admiten Numeros");
			}
			else throw new Exception("El Campo de Numero de Telefono es Obligatorio");

            if (_contacto.Id != -1)
            {
                Funciones.EditarContacto(_contacto.Id, _contacto);
            }
            else
            {
                if (Funciones.ListaOriginal.Count == 0)
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
		catch (Exception ex)
		{
			DisplayAlert("Error",ex.Message,"Ok");
		}
    }
	private void AsignarDatos()
	{
        if (!string.IsNullOrEmpty(entryNombre.Text.Trim()))
        {
            _contacto.Nombre = entryNombre.Text.Trim();
        }
        if (!string.IsNullOrEmpty(entryApellido.Text.Trim()))
        {
            _contacto.Apellido = entryApellido.Text.Trim();
        }
        if (!string.IsNullOrEmpty(entryApodo.Text.Trim()))
        {
            _contacto.Apodo = entryApodo.Text.Trim();
        }
        if (!string.IsNullOrEmpty(entryCorreo.Text.Trim()))
        {
            _contacto.Correo = entryCorreo.Text.Trim();
        }
        _contacto.NumeroTelefonico = entryTelefono.Text.Trim();
    }
    private void ControlarNullsEntrys()
    {
        if (entryTelefono.Text == null) entryTelefono.Text = "";
        if (entryNombre.Text == null) entryNombre.Text = "";
        if (entryApellido.Text == null) entryApellido.Text = "";
        if (entryApodo.Text == null) entryApodo.Text = "";
        if (entryCorreo.Text == null) entryCorreo.Text = "";
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        var Decision = await DisplayAlert("Se Borrara un Contacto", $"Esta Seguro que desea Eliminar \na {_contacto.Nombre} {_contacto.Apellido} de su Lista de Contactos?", "Ok", "Cancelar");
        if(Decision == true)
        {
            int _posicionContactoABorrar = Funciones.ListaOriginal.FindIndex(contacto => contacto.Id == _contacto.Id);
            Funciones.ListaOriginal.RemoveAt(_posicionContactoABorrar);
            await Shell.Current.GoToAsync("..");
        }
    }
}