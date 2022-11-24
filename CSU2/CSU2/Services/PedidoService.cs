using CSU2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSU2.Services
{
    public class PedidoService
    {
        HttpClient client = new HttpClient();

        public PedidoService()
        {
            client.BaseAddress = new Uri("https://37fc-187-209-230-156.ngrok.io/pedidos/");
        }


        public async Task Ordenar(Pedido pedido)
        {
            var json = JsonConvert.SerializeObject(pedido);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("ordenar", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
