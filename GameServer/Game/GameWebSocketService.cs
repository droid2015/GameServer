using Microsoft.ServiceModel.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Game
{
    public class GameWebSocketService:WebSocketService
    {
        private static PushFrenzyServer server = new PushFrenzyServer();

        private GameConnection connection;
        private string _username;
        private int _gamesize;    
        public override void OnOpen()
        {
             _username = QueryParameters["nickname"];
             _gamesize = int.Parse(QueryParameters["gamesize"]);
            connection = server.JoinGame(this, _username, _gamesize);
        }
        protected override void OnClose()
        {
            if (connection != null)
                connection.Disconnect();
        }
        public override void OnMessage(string message)
        {
            connection.ProcessCommand(message);
        }
    }
}
