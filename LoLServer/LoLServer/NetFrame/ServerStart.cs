using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
namespace NetFrame
{

    public class ServerStart
    {
        Socket server; // 服务器监听对象
        int macClient; // 最大客户连接数目
        public ServerStart(int max,int port)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
            this.macClient = max;
        }
    }
}
