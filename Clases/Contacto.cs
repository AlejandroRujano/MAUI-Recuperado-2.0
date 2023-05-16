using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Prueba_Maui.Clases
{
    public class Contacto : INotifyPropertyChanged
    {
        private int _id;
        private string _nombre;
        private string _apellido;
        private string _apodo;
        private string _correo;
        private string _pathImagen;
        private string _numeroTelefonico;
        private bool _eliminado;
        private bool _favorito;

        //Constructos por Default, sin parametros
        public Contacto()
        {
            _id = -1;
            _nombre = "Sin Nombre";
            _apellido = "Sin Apellido";
            _apodo = "Sin Apodo";
            _correo = "NoEsUnCorreo@gmail.com";
            _numeroTelefonico = "";
            _pathImagen = "";
            _eliminado = false;
            _favorito = false;
        }

        //Constructor Parametrico
        public Contacto(string nombre, string apellido, string apodo, string correo, string numeroTelefonico, int id, string UltimoTonoImagen)
        {
            _id = id;
            _nombre = nombre;
            _apellido = apellido;
            _apodo = apodo;
            _correo = correo;
            _numeroTelefonico = numeroTelefonico;
            _pathImagen = EscogerTonoImagen(UltimoTonoImagen);
            _eliminado = false;
            _favorito = false;

            //Comando para poder Eliminarlo desde el Main

            Eliminar = new Command(
                execute: () =>
                {
                    _eliminado = true;
                });
            MarcarComoFavorito = new Command(
                execute: () =>
                {
                    if (_favorito == false)
                    {
                        _favorito = true;
                    }
                    else if (_favorito == true)
                    {
                        _favorito = false;
                    }
                });
        }

        //Getters, Setters
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Apellido { get { return _apellido; } set { _apellido = value; } }
        public string Apodo { get { return _apodo; } set { _apodo = value; } }
        public string PathImagen { get { return _pathImagen; } set { _pathImagen = value; } }
        public string Correo { get { return _correo; } set { _correo = value; } }
        public string NumeroTelefonico { get { return _numeroTelefonico; } set { _numeroTelefonico = value; } }
        public bool Eliminado { get { return _eliminado; } set { _eliminado = value; } }
        public bool Favorito { get { return _favorito; } set { _favorito = value; } }
        public ICommand Eliminar { private set; get; }
        public ICommand MarcarComoFavorito { private set; get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public string EscogerTonoImagen(string UltimoTono)
        {
            List<string> Colores = Funciones.ColoresIconosContactos;

            if (UltimoTono == "") return Colores[0];

            for (int i = 0; i < Colores.Count; i++)
            {
                if (UltimoTono == Colores[Colores.Count - 1])
                {
                    return Colores[0];
                }
                else if (UltimoTono == Colores[i])
                {
                    return Colores[i + 1];
                }
            }

            return "";
        }
    }
}