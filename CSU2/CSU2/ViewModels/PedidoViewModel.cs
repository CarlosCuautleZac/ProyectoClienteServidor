using CSU2.Models;
using CSU2.Services;
using CSU2.Views;
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
        public Command VerResumenCommand { get; set; }
        public Command RegresarCommand { get; set; }
        #endregion

        #region Propiedades

        public Pedido Pedido { get; set; }
        public string Error { get; set; }

        public int NumeroMesa { get; set; }

        private int platillo1;

        public int Platillo1
        {
            get { return platillo1; }
            set { platillo1 = value; Actualizar(nameof(Platillo1)); }
        }

        private int platillo2;

        public int Platillo2
        {
            get { return platillo2; }
            set { platillo2 = value; Actualizar(nameof(Platillo2)); }
        }

        private int platillo3;
        public int Platillo3
        {
            get { return platillo3; }
            set { platillo3 = value; Actualizar(nameof(Platillo3)); }
        }

        private int bebida1;

        public int Bebida1
        {
            get { return bebida1; }
            set { bebida1 = value; Actualizar(nameof(Bebida1)); }
        }

        private int bebida2;

        public int Bebida2
        {
            get { return bebida2; }
            set { bebida2 = value; Actualizar(nameof(Bebida2)); }
        }

        private int bebida3;
        public int Bebida3
        {
            get { return bebida3; }
            set { bebida3 = value; Actualizar(nameof(Bebida3)); }
        }

        private bool esplatillo = true;

        public bool EsPlatillo
        {
            get { return esplatillo; }
            set { esplatillo = value; Actualizar(nameof(EsPlatillo)); }
        }


        #endregion

        #region Objetos
        PedidoService service = new PedidoService();
        DetallesPedidoView DetallesPedidoView;
        #endregion



        //Contructor
        public PedidoViewModel()
        {
            OrdenarCommand = new Command(Ordenar);
            VerResumenCommand = new Command(VerResumen);
            RegresarCommand = new Command(Regresar);
            Pedido = new Pedido();
            DetallesPedidoView = new DetallesPedidoView() { BindingContext = this };

        }

        private async void Regresar()
        {
            Error = "";
            Actualizar();
            await Application.Current.MainPage.Navigation.PopAsync();

        }

        private async void VerResumen()
        {
            Error = "";
            AgregarPlatillos();
            AgregarBebidas();
        
            if (Pedido.NumeroMesa < 1)
                Error = "Numero de mesa inválido" + Environment.NewLine;
            if (Pedido.Bebidas.Count + Pedido.Platillos.Count < 1)
                Error = "Debe ordenar al menos un platillo o una bebida";

            

            if (Error == "")
            {
                
                Pedido = new Pedido();

                Pedido.NumeroMesa = NumeroMesa;
                

                Actualizar();

                await Application.Current.MainPage.Navigation.PushAsync(DetallesPedidoView);
            }
            else
            {
                Actualizar(nameof(Error));
            }

        }

        private async void Ordenar()
        {
            try
            {
                Pedido.Fecha = DateTime.Now;


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
                    Nombre = "Pepsi"
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
                    Nombre = "Sopa"
                };

                Pedido.Platillos.Add(platillo);
            }

            if (Platillo3 > 0)
            {
                platillo = new Platillo()
                {
                    Cantidad = Platillo3,
                    Nombre = "Enchiladas Verdes"
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
