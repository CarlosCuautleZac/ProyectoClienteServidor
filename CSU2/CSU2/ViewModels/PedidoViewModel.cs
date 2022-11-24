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

        public int CarneAsada { get; set; } 
        public int EnchiladasVerdes { get; set; }
        public int Sopa { get; set; }

        public int CocaCola { get; set; } 
        public int Tecate { get; set; } 
        public int Jugo { get; set; }

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
                    Error = "Numero de mesa equivocado" + Environment.NewLine;
                if (Pedido.Bebidas.Count + Pedido.Platillos.Count < 1)
                    Error = "Debe ordenar por lo menos un platillo o una bebida";

                if (Error == "")
                {


                    await service.Ordenar(Pedido);
                    Pedido = new Pedido();
                    NumeroMesa = 0;
                    CarneAsada = 0;
                    Jugo = 0;
                    CocaCola = 0;
                    Tecate = 0;
                    EnchiladasVerdes = 0;
                    Sopa = 0;


                    Error = "";
                    Actualizar();

                    await Application.Current.MainPage.DisplayAlert("Advertencia", "Se ha enviado la orden a cocina", "OK");

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

            if (CocaCola > 0)
            {
                bebida = new Bebida()
                {
                    Cantidad = CocaCola,
                    Nombre = "Coca Cola"
                };

                Pedido.Bebidas.Add(bebida);
            }



            if (Tecate > 0)
            {
                bebida = new Bebida()
                {
                    Cantidad = Tecate,
                    Nombre = "Tecate"
                };

                Pedido.Bebidas.Add(bebida);
            }

            if (Jugo > 0)
            {
                bebida = new Bebida()
                {
                    Cantidad = Jugo,
                    Nombre = "Jugo"
                };

                Pedido.Bebidas.Add(bebida);

            }
        }

        private void AgregarPlatillos()
        {

            Platillo platillo = new Platillo();

            if (CarneAsada > 0)
            {
                platillo = new Platillo()
                {
                    Cantidad = CarneAsada,
                    Nombre = "Carne Asada"
                };

                Pedido.Platillos.Add(platillo);
            }

            

            if (EnchiladasVerdes > 0)
            {
                platillo = new Platillo()
                {
                    Cantidad = EnchiladasVerdes,
                    Nombre = "Enchiladas Verdes"
                };

                Pedido.Platillos.Add(platillo);
            }

            if (Sopa > 0)
            {
                platillo = new Platillo()
                {
                    Cantidad = Sopa,
                    Nombre = "Sopa"
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
