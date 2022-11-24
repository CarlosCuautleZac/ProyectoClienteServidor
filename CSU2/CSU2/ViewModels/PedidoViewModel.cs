using CSU2.Models;
using CSU2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace CSU2.ViewModels
{
    public class PedidoViewModel : INotifyPropertyChanged
    {
        #region Copmandos
        public Command OrdenarCommand { get; set; }
        #endregion

        #region Propiedades

        public Pedido Pedido { get; set; }
        public string Error { get; set; }

        public int NumeroMesa { get; set; }

        public int Platillo1 { get; set; } 
        public int Platillo2 { get; set; }
        public int Platillo3 { get; set; }

        public int Bebida1 { get; set; } 
        public int Bebida2 { get; set; } 
        public int Bebida3 { get; set; }

        private bool esplatillo = true;

        public bool EsPlatillo 
        {
            get { return esplatillo; }
            set { esplatillo = value; Actualizar(nameof(EsPlatillo)); }
        }


        #endregion

        #region Objetos
        PedidoService service = new PedidoService();
        #endregion

        //Contructor
        public PedidoViewModel()
        {
            OrdenarCommand = new Command(Ordenar);
            Pedido = new Pedido();
        }

        private async void Ordenar()
        {
            Error = "";
            Pedido = new Pedido();

            AgregarPlatillos();
            AgregarBebidas();

            try
            {
                Pedido.NumeroMesa = NumeroMesa;
                Pedido.Fecha = DateTime.Now;

                if (Pedido.NumeroMesa < 1)
                    Error = "Numero de mesa inválido" + Environment.NewLine;
                if (Pedido.Bebidas.Count + Pedido.Platillos.Count < 1)
                    Error = "Debe ordenar al menos un platillo o una bebida";

                if (Error == "")
                {


                    await service.Ordenar(Pedido);
                    Pedido = new Pedido();
                    NumeroMesa = 0;
                    Platillo1 = 0;
                    Bebida3 = 0;
                    Bebida1 = 0;
                    Bebida2 = 0;
                    Platillo2 = 0;
                    Platillo3 = 0;


                    Error = "";
                    Actualizar();

                    await Application.Current.MainPage.DisplayAlert("¡Listo!", "Su orden ya fue enviada", "Entendido");

                }
                else
                {
                    Actualizar(nameof(Error));
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                Actualizar(nameof(Error));
            }


        }

        private void AgregarBebidas()
        {
            Bebida bebida = new Bebida();

            if (Bebida1 > 0)
            {
                bebida = new Bebida()
                {
                    Cantidad = Bebida1,
                    Nombre = "Coca Cola"
                };

                Pedido.Bebidas.Add(bebida);
            }



            if (Bebida2 > 0)
            {
                bebida = new Bebida()
                {
                    Cantidad = Bebida2,
                    Nombre = "Bebida2"
                };

                Pedido.Bebidas.Add(bebida);
            }

            if (Bebida3 > 0)
            {
                bebida = new Bebida()
                {
                    Cantidad = Bebida3,
                    Nombre = "Bebida3"
                };

                Pedido.Bebidas.Add(bebida);

            }
        }

        private void AgregarPlatillos()
        {

            Platillo platillo = new Platillo();

            if (Platillo1 > 0)
            {
                platillo = new Platillo()
                {
                    Cantidad = Platillo1,
                    Nombre = "Carne Asada"
                };

                Pedido.Platillos.Add(platillo);
            }

            

            if (Platillo2 > 0)
            {
                platillo = new Platillo()
                {
                    Cantidad = Platillo2,
                    Nombre = "Enchiladas Verdes"
                };

                Pedido.Platillos.Add(platillo);
            }

            if (Platillo3 > 0)
            {
                platillo = new Platillo()
                {
                    Cantidad = Platillo3,
                    Nombre = "Platillo3"
                };

                Pedido.Platillos.Add(platillo);

            }
        }

        public void Actualizar(string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
