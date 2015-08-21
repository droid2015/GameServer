using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Game
{
    public class GameHandler:WebSocketHandler
    {
        private static PushFrenzyServer server;
        private GameConnection connection;
        private string _username;
        private int _gamesize;
        public PushFrenzyServer Server
        {
            get
            {
                if(server==null)
                {
                    server = new PushFrenzyServer();
                }
                return server;
            }
        }
        public GameHandler(string username)
        {
            _username = username;

        }
        public GameHandler(string username, int gamesize)
        {
            _username = username;
            _gamesize = gamesize;
        }
        
    }
}
