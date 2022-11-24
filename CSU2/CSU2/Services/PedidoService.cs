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
            client.BaseAddress = new Uri("https://ee80-2806-108e-7eae-4447-f0ec-cb3-c2c.ngrok.io/pedidos/");
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
