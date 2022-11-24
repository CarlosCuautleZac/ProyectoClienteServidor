using System;
using System.Collections.Generic;
using System.Text;

namespace CSU2.Models
{
    public class Pedido
    {
        public int NumeroMesa { get; set; }

        public List<Platillo> Platillos { get; set; } = new List<Platillo>();

        public List<Bebida> Bebidas { get; set; } = new List<Bebida>();

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
