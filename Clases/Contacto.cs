using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Prueba_Maui.Clases
{
    public class Contacto
    {
        private int _id;
        private string _nombre;
        private string _apellido;
        private string _apodo;
        private string _correo;
        private string _pathImagen;
        private long _numeroTelefonico;
        private List<string> _tonosIconoImagen = LlenarListaDeTonos(9);
        public Contacto()
        {
            _id = -1;
            _nombre = "Sin Nombre";
            _apellido = "Sin Apellido";
            _apodo = "Sin Apodo";
            _correo = "NoEsUnCorreo@gmail.com";
            _numeroTelefonico = 0;
            _pathImagen = "";
        }
        public Contacto(string nombre, string apellido, string apodo, string correo, long numeroTelefonico, int id, string UltimoTonoImagen)
        {
            _id = id;
            _nombre = nombre;
            _apellido = apellido;
            _apodo = apodo;
            _correo = correo;
            _numeroTelefonico = numeroTelefonico;
            _pathImagen = EscogerTonoImagen(UltimoTonoImagen);
        }
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Apellido { get { return _apellido; } set { _apellido = value; } }
        public string Apodo { get { return _apodo; } set { _apodo = value; } }
        public string PathImagen { get { return _pathImagen; } set { _pathImagen = value; } }
        public string Correo { get { return _correo; } set { _correo = value; } }
        public long NumeroTelefonico { get { return _numeroTelefonico; } set { _numeroTelefonico = value; } }
        private static List<string> LlenarListaDeTonos(int CantidadDeTonos)
        {
            List<string> ListaDeTonos = new List<string>();

            for(int i=1; i<=CantidadDeTonos; i++)
            {
                ListaDeTonos.Add(@$"lista{i}contactos.png");
            }

            return ListaDeTonos;
        }
        private string EscogerTonoImagen(string UltimoTono)
        {
            if(UltimoTono == "") return _tonosIconoImagen[0];

            for (int i = 0; i<_tonosIconoImagen.Count; i++)
            {
                if(UltimoTono == _tonosIconoImagen[_tonosIconoImagen.Count-1])
                {
                    return _tonosIconoImagen[0];
                }
                else if(UltimoTono == _tonosIconoImagen[i])
                {
                    return _tonosIconoImagen[i + 1];
                }
            }

            return "";
        }
    }
}