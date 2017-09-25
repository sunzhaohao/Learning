using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetFrame
{
    public class UserToken
    {
       public Socket conn;

       public SocketAsyncEventArgs receiveSAEA;
       public SocketAsyncEventArgs sendSAEA;

       public UserToken()
        {
            receiveSAEA = new SocketAsyncEventArgs();
            sendSAEA = new SocketAsyncEventArgs();
            receiveSAEA.UserToken = this;
            sendSAEA.UserToken = this;
        }

    }
}
