using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.Models
{
    public class Pedido
    {
        public int NumeroMesa { get; set; }

        public List<Platillo> Platillos { get; set; } = new();

        public List<Bebida> Bebidas { get; set; } = new();

        public DateTime Fecha { get; set; }
    }
}
