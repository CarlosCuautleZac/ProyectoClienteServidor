using Servidor.Models;
using Servidor.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Servidor.ViewModels
{
    public class PedidosViewModel : INotifyPropertyChanged
    {
        //http://127.0.0.1:2022/pedidos/
        PedidoService service = new();



        public ObservableCollection<Pedido> Pedidos { get; set; }
        public Pedido Pedido { get; set; }

        Dispatcher dispatcher;
        public PedidosViewModel()
        {
            Pedido = new Pedido();  
            dispatcher = Dispatcher.CurrentDispatcher;
            service.Iniciar();
            Pedidos = new();
            service.PedidoRecibido += Service_PedidoRecibido;
        }

        private void Service_PedidoRecibido(Pedido pedido)
        {
            dispatcher.Invoke(() =>
            {
                Pedidos.Add(pedido);
                Actualizar(nameof(Pedidos));
            });
        }


        public void Actualizar(string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
