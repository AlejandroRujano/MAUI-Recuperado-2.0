//using Java.Lang;
using Microsoft.Maui.Controls.Internals;
using Prueba_Maui.Clases;
using System.Collections.ObjectModel;

namespace Prueba_Maui.Views;
public partial class MainContactos : ContentPage
{
	//private List<Contacto> _listaDeContactos = new List<Contacto>();
    private ObservableCollection<Contacto> _listaDeContactos;
    private bool _ordenarNombreContacto = true;
    private bool _ordenarNumeroContacto = true;
    private string _ultimaFormaDeOrdenado = "";
    //private int Contador = 0;
    public MainContactos()
	{
		InitializeComponent();

        Funciones.LeerJsonContactos();

        _listaDeContactos = new ObservableCollection<Contacto>(Funciones.ListaDeContactosExistentes());

        //_listaDeContactos = Funciones.ListaOriginal;
		CollectionViewContactos.ItemsSource = _listaDeContactos;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();

        //Funciones.LeerJsonContactos();
        _listaDeContactos = new ObservableCollection<Contacto>(Funciones.ListaDeContactosExistentes());

        CollectionViewContactos.ItemsSource = _listaDeContactos;

        Funciones.BtnPresionado = false;

        _ultimaFormaDeOrdenado = "";
    }
    private void btnAgregarContacto_Clicked(object sender, EventArgs e)
    {
        if (btnAgregarContacto.IsEnabled==true && Funciones.BtnPresionado == false)
        {
            Funciones.BtnPresionado = true;
            
            Shell.Current.GoToAsync($"{nameof(AgregarEditarContacto)}?Id=-1");
        }
    }
    private void CollectionViewContactos_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		if(CollectionViewContactos.SelectedItem != null && Funciones.BtnPresionado == false && Funciones.SwipeLista == false)
		{
            Funciones.BtnPresionado = true;
            Shell.Current.GoToAsync($"{nameof(AgregarEditarContacto)}?Id={((Contacto)CollectionViewContactos.SelectedItem).Id}");
        }
        CollectionViewContactos.SelectedItem = null;
    }
    private void entryBusqueda_TextChanged(object sender, TextChangedEventArgs e)
    {
        //List<Contacto> ListaBusqueda = new List<Contacto>().ToList();

        string Busqueda = ((Entry)sender).Text.ToUpper();

        if (Busqueda.Length != 0)
        {
            _listaDeContactos = new ObservableCollection<Contacto>
                            (from BusquedaC in _listaDeContactos
                             where BusquedaC.Apellido.StartsWith(Busqueda) || BusquedaC.Nombre.StartsWith(Busqueda) || BusquedaC.Apodo.StartsWith(Busqueda)
                             select BusquedaC);

            if (_listaDeContactos.Count > 0)
            {
                CollectionViewContactos.ItemsSource = _listaDeContactos;
            }

        }
        else
        {
            _listaDeContactos = new ObservableCollection<Contacto>(Funciones.ListaDeContactosExistentes());
            CollectionViewContactos.ItemsSource = _listaDeContactos;
            _ultimaFormaDeOrdenado = "";
        }
    }
    private void SwipeListaContactos_SwipeStarted(object sender, SwipeStartedEventArgs e)
    {
        Funciones.SwipeLista = true;
    }
    private void SwipeListaContactos_SwipeEnded(object sender, SwipeEndedEventArgs e)
    {
        Funciones.SwipeLista = false;
    }
    private void SwipeItemEliminar_Invoked(object sender, EventArgs e)
    {
        _listaDeContactos = new ObservableCollection<Contacto>(Funciones.ListaDeContactosExistentes());

        if(_ultimaFormaDeOrdenado == "Numeros")
        {
            if (_ordenarNumeroContacto == true) _ordenarNumeroContacto = false;
            else _ordenarNumeroContacto = true;

            OrdenarPorNumero();
        }
        else if(_ultimaFormaDeOrdenado == "Nombres")
        {
            if (_ordenarNombreContacto == true) _ordenarNombreContacto = false;
            else _ordenarNombreContacto = true;

            OrdenarPorNombre();
        }

        CollectionViewContactos.ItemsSource = _listaDeContactos;
    }
    private void btnFavoritos_Clicked(object sender, EventArgs e)
    {
        _ultimaFormaDeOrdenado = "";

        if (_listaDeContactos.Count != Funciones.ListaDeFavoritos().Count)
        {
            _listaDeContactos = new ObservableCollection<Contacto>(Funciones.ListaDeFavoritos());
        }
        else
        {
            _listaDeContactos = new ObservableCollection<Contacto>(Funciones.ListaDeContactosExistentes());
        }
        CollectionViewContactos.ItemsSource = _listaDeContactos;
    }
    private void btnNombre_Clicked(object sender, EventArgs e)
    {
        //List<Contacto> ListaOrdenada = new List<Contacto>();

        OrdenarPorNombre();
        _ultimaFormaDeOrdenado = "Nombres";
    }
    private void OrdenarPorNombre()
    {
        if (_ordenarNombreContacto == true)
        {
            _listaDeContactos = new ObservableCollection<Contacto>
                                (from Lista in _listaDeContactos
                                 orderby Lista.Nombre ascending
                                 select Lista);

            CollectionViewContactos.ItemsSource = _listaDeContactos;

            _ordenarNombreContacto = false;
        }
        else
        {
            _listaDeContactos = new ObservableCollection<Contacto>
                                (from Lista in _listaDeContactos
                                 orderby Lista.Nombre descending
                                 select Lista);

            CollectionViewContactos.ItemsSource = _listaDeContactos;

            _ordenarNombreContacto = true;
        }
    }
    private void btnNumero_Clicked(object sender, EventArgs e)
    {
        //List<Contacto> ListadeNumeros = new List<Contacto>();

        OrdenarPorNumero();
        _ultimaFormaDeOrdenado = "Numeros";
    }
    private void OrdenarPorNumero()
    {
        if (_ordenarNumeroContacto == true)
        {
            _listaDeContactos = new ObservableCollection<Contacto>
                                (from Numero in _listaDeContactos
                                 orderby Numero.NumeroTelefonico ascending
                                 select Numero);

            CollectionViewContactos.ItemsSource = _listaDeContactos;

            _ordenarNumeroContacto = false;
        }
        else
        {
            _listaDeContactos = new ObservableCollection<Contacto>
                                (from Numero in _listaDeContactos
                                 orderby Numero.NumeroTelefonico descending
                                 select Numero);

            CollectionViewContactos.ItemsSource = _listaDeContactos;

            _ordenarNumeroContacto = true;
        }
    }
}