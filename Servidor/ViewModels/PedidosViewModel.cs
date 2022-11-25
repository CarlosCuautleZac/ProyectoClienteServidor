using GalaSoft.MvvmLight.Command;
using Servidor.Models;
using Servidor.Services;
using Servidor.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Servidor.ViewModels
{
    public class PedidosViewModel : INotifyPropertyChanged
    {
        //http://127.0.0.1:2022/pedidos/

        public ICommand VetDetallesCommand { get; set; }
        public ICommand EntregarPedidoCommand { get; set; }

        public ObservableCollection<Pedido> Pedidos { get; set; }
        public Pedido Pedido { get; set; }

        Dispatcher dispatcher;
        PedidoService service = new();
        DetallesPedidoView? detallesView;

        public PedidosViewModel()
        {
            Pedidos = new();
            Pedido = new Pedido();
            dispatcher = Dispatcher.CurrentDispatcher;

            VetDetallesCommand = new RelayCommand(VerDetalles);
            EntregarPedidoCommand = new RelayCommand(EntregarPedido);

            service.Iniciar();
            service.PedidoRecibido += Service_PedidoRecibido;
        }

        private void EntregarPedido()
        {
            if (Pedido != null)
            {
                Pedidos.Remove(Pedido);
                if (detallesView != null)
                    detallesView.Close();

                Pedidos = new(Pedidos.OrderBy(x => x.Fecha)); 
                Actualizar();
            }
        }

        private void VerDetalles()
        {
            if (Pedido != null)
            {
                detallesView = new DetallesPedidoView() { DataContext = this };
                detallesView.ShowDialog();
            }
        }

        private void Service_PedidoRecibido(Pedido pedido)
        {
            dispatcher.Invoke(() =>
            {
                Pedidos.Add(pedido);
                Actualizar(nameof(Pedidos));
            });

            System.Media.SoundPlayer player = new System.Media.SoundPlayer("Assets/Sounds/Campana.wav");
            player.Play();
        }


        public void Actualizar(string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
