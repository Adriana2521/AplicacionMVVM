using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.ComponentModel;
using Joyeria.Models;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Newtonsoft.Json;
using System.IO;

namespace Joyeria.ViewModels
{
    public class CategoriaJoyasViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<CatalogoJoyas> lista { get; set; } = new ObservableCollection<CatalogoJoyas>();

        private CatalogoJoyas cata;

        public  CatalogoJoyas Cata
        {
            get { return cata; }
            set {
                cata = value;
                PropertyChange("Cata");
            }
        }

        int position;
        public string Vista { get; set; } = "ver";

        public string Error { get; set; }

        public ICommand CambiarVistaCommand{ get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand EditarCommand { get; set; }

       
        public CategoriaJoyasViewModel()
        {
            CambiarVistaCommand = new RelayCommand<string>(CambiarVista);
            AgregarCommand = new RelayCommand(Agregar);
            CancelarCommand = new RelayCommand(Cancelar);
            EliminarCommand = new RelayCommand(Eliminar);
            EditarCommand = new RelayCommand(Editar);
         
        }
        private void Editar()
        {
            if (Cata!= null)
            {
                lista[position] = Cata;
                GuardarDatos();
                Cancelar();
            }
        }

        public void CambiarVista(string vista)
        {
            Vista = vista;

            switch (vista)
            {
                case "agregar":
                    Cata = new CatalogoJoyas();
                    break;
            }

            PropertyChange();
        }
        public void Agregar()
        {
            if (string.IsNullOrWhiteSpace(Cata.Destino))
            {
                Error = "Los El campo debe estar lleno";
                PropertyChange(Error);
                return;
            }
          
            if (string.IsNullOrWhiteSpace(Cata.Pais))
            {
                Error = "Los El campo debe estar lleno";
                PropertyChange(Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(Cata.Foto))
            {
                Error = "Los El campo debe estar lleno. Escriba una URL";
                PropertyChange(Error);
                return;
            }

            Uri uri;
            if (!Uri.TryCreate(Cata.Foto, UriKind.Absolute, out uri))
            {
                Error = "Teclee una URL valida";
                PropertyChange(Error);
                return;
            }
            lista.Add(Cata);
            GuardarDatos();
            CambiarVista("ver");
           

        }
        public void Cancelar()
        {
            Cata = null;
            CambiarVista("ver");
        }

        public void Eliminar()
        {
            if (cata!=null)
            {
                lista.Remove(Cata);
                GuardarDatos();
                PropertyChange();
            }
        }




        void PropertyChange( string propiedad= null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        public void GuardarDatos()
        {
            var json = JsonConvert.SerializeObject(lista);
            File.WriteAllText("Cata.json", json);
        }

    }
}
