using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Servidor.Models;
using Servidor.ViewModels;
using Servidor.Views;

namespace Servidor.Views
{
    /// <summary>
    /// Lógica de interacción para ComandaView.xaml
    /// </summary>
    public partial class ComandaView : Window
    {
        public ComandaView()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && e.LeftButton == MouseButtonState.Pressed) //doble clic con el boton izquierdo
            {
                var viewmodel = (PedidosViewModel)this.DataContext;
                viewmodel.VetDetallesCommand.Execute(null);
            }
        }
    }
}
