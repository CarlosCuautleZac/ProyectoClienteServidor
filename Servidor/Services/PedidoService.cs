using Newtonsoft.Json;
using Servidor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.Services
{
    public class PedidoService
    {

        HttpListener listener;
        public event Action<Pedido>? PedidoRecibido;

        public Pedido Pedido { get; set; }

        public PedidoService()
        {
            Pedido = new Pedido();
            listener = new();
            listener.Prefixes.Add("http://*:2022/pedidos/");
        }


        #region Metodos

        public void Iniciar()
        {
            if (!listener.IsListening)
            {
                listener.Start();
                listener.BeginGetContext(ContextoRecibido, null);
            }

        }

        private void ContextoRecibido(IAsyncResult ar)
        {
            var context = listener.EndGetContext(ar);
            listener.BeginGetContext(ContextoRecibido, null);

            //Atender
            if (context.Request.Url != null)
            {
                //Si ordenaron
                if (context.Request.Url.LocalPath == "/pedidos/ordenar" && context.Request.HttpMethod == "POST")
                {

                    var stream = new StreamReader(context.Request.InputStream);
                    var json = stream.ReadToEnd();
                    Pedido? pedido = JsonConvert.DeserializeObject<Pedido>(json);


                    if (pedido != null)
                    {
                        PedidoRecibido?.Invoke(pedido);
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }


                    context.Response.Close();

                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }

                context.Response.Close();
            }


        }

        #endregion
    }
}
