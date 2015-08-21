using GameServer.Game;
using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;

namespace GameServer.Controllers
{
    [Route("game")]
    public class GameController : ApiController
    {
        [Route("game/{username}")]
        public HttpResponseMessage Get(string username)
        {
            HttpContext currentContext = HttpContext.Current;
            if (currentContext.IsWebSocketRequest ||
                currentContext.IsWebSocketRequestUpgrading)
            {
                //currentContext.AcceptWebSocketRequest(new GameWebSocketService(username));
                return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
            }
            return null;
        }
        [Route("game/{username}/{gamesize}")]
        public HttpResponseMessage Get(string username, int gamesize)
        {
            HttpContext currentContext = HttpContext.Current;
            if (currentContext.IsWebSocketRequest ||
                currentContext.IsWebSocketRequestUpgrading)
            {
                //currentContext.AcceptWebSocketRequest(new GameWebSocketService(username, gamesize));
                return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
            }
            return null;
        }
        private async Task ProcessWebsocketSession(AspNetWebSocketContext context)
        {
            var ws = context.WebSocket;

            new Task(async () =>
            {
                var inputSegment = new ArraySegment<byte>(new byte[1024]);

                while (true)
                {
                    // MUST read if we want the state to get updated...
                    var result = await ws.ReceiveAsync(inputSegment, CancellationToken.None);

                    if (ws.State != WebSocketState.Open)
                    {
                        break;
                    }
                }
            }).Start();

            while (true)
            {
                if (ws.State != WebSocketState.Open)
                {
                    break;
                }
                else
                {
                    byte[] binaryData = { 0xde, 0xad, 0xbe, 0xef, 0xca, 0xfe };
                    var segment = new ArraySegment<byte>(binaryData);
                    await ws.SendAsync(segment, WebSocketMessageType.Binary,
                        true, CancellationToken.None);
                }
            }
        }
    }
}