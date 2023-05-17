﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_Maui.Clases
{
    public class Funciones
    {
        private static List<Contacto> _listaDeContactosOriginal = new List<Contacto>();
        private static List<string> _coloresIconoContactos = LlenarListaDeColores(9);
        private static bool _btnPresionado = false;
        private static bool _swipeLista = false;

        //Getters, Setters
        public static bool SwipeLista { get { return _swipeLista; } set { _swipeLista = value; } }
        public static bool BtnPresionado { get { return _btnPresionado; } set { _btnPresionado = value; } }
        public static List<string> ColoresIconosContactos { get { return _coloresIconoContactos; } set { _coloresIconoContactos = value; } }
        public static List<Contacto> ListaOriginal { get { return _listaDeContactosOriginal;} set {  _listaDeContactosOriginal = value; } }

        //Funciones con Json
        public static void GuardarJsonContactos()
        {
            var JsonContactos = JsonConvert.SerializeObject(_listaDeContactosOriginal.ToArray(), Formatting.Indented);
            File.WriteAllText("@ContactosJSON",JsonContactos);
        }
        public static void LeerJsonContactos()
        {
            try
            {
                _listaDeContactosOriginal = JsonConvert.DeserializeObject<List<Contacto>>(File.ReadAllText("@ContactosJSON"));
            }
            catch { };

            //Al primer contacto se le debe pasar como parametro un string vacio
            _listaDeContactosOriginal.Add(new Contacto("Luis","Galindez","Raze","a@gmail.com","042425836912",_listaDeContactosOriginal.Count+1,""));

            for(int i=0; i<3 ; i++)
            {
                _listaDeContactosOriginal.Add(new Contacto("Alejandro", "Ramirez", "Brimstone", "b@gmail.com", "04123456789", _listaDeContactosOriginal.Count + 1, _listaDeContactosOriginal[_listaDeContactosOriginal.Count - 1].PathImagen));
                _listaDeContactosOriginal.Add(new Contacto("Amando", "Puentes", "Mozart", "c@gmail.com", "04165791212", _listaDeContactosOriginal.Count + 1, _listaDeContactosOriginal[_listaDeContactosOriginal.Count - 1].PathImagen));
                _listaDeContactosOriginal.Add(new Contacto("Gipsander", "Urdaneta", "Neon", "d@gmail.com", "04121045698", _listaDeContactosOriginal.Count + 1, _listaDeContactosOriginal[_listaDeContactosOriginal.Count - 1].PathImagen));
                _listaDeContactosOriginal.Add(new Contacto("Francisco", "Ochoa", "Skye", "e@gmail.com", "04249852000", _listaDeContactosOriginal.Count + 1, _listaDeContactosOriginal[_listaDeContactosOriginal.Count - 1].PathImagen));
                _listaDeContactosOriginal.Add(new Contacto("Luis", "Galindez", "Raze", "a@gmail.com", "042425836912", _listaDeContactosOriginal.Count + 1, _listaDeContactosOriginal[_listaDeContactosOriginal.Count - 1].PathImagen));
            }
        }

        //Funciones Normales
        private static List<string> LlenarListaDeColores(int CantidadDeTonos)
        {
            List<string> ListaDeTonos = new List<string>();

            for (int i = 1; i <= CantidadDeTonos; i++)
            {
                ListaDeTonos.Add(@$"lista{i}contactos.png");
            }

            return ListaDeTonos;
        }
        public static List<Contacto> ListaDeContactosExistentes()
        {
            return _listaDeContactosOriginal.Where(_contacto => _contacto.Eliminado == false).ToList();
        }
        public static List<Contacto> ListaDeFavoritos()
        {
            return _listaDeContactosOriginal.Where(_contacto => _contacto.Eliminado == false && _contacto.Favorito == true).ToList();
        }
        public static Contacto BuscarContacto(int id)
        {
            Contacto _contactoBuscado = new Contacto();

            _contactoBuscado = _listaDeContactosOriginal.First(Contacto => Contacto.Id == id);

            return _contactoBuscado;
        }
        public static void EditarContacto(int id, Contacto contacto)
        {
            if (id != contacto.Id) return;

            //var ContactoEditado = _listaDeContactosOriginal.FirstOrDefault(Contacto => Contacto.Id == id);

            if (contacto != null)
            {
                //Posicion del Contacto a Editar
                int _posicion = _listaDeContactosOriginal.FindIndex(contacto => contacto.Id == id);

                _listaDeContactosOriginal[_posicion].Nombre = contacto.Nombre;
                _listaDeContactosOriginal[_posicion].Apellido = contacto.Apellido;
                _listaDeContactosOriginal[_posicion].Apodo = contacto.Apodo;
                _listaDeContactosOriginal[_posicion].Correo = contacto.Correo;
                _listaDeContactosOriginal[_posicion].NumeroTelefonico = contacto.NumeroTelefonico;
                _listaDeContactosOriginal[_posicion].Favorito = contacto.Favorito;

                //GuardarJsonContactos();
            }
        }
    }
}
