using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace NetFrame
{

    public class ServerStart
    {
        Socket server; // 服务器监听对象
        int macClient; // 最大客户连接数目

        UserTokenPool pool; // 连接池
        Semaphore acceptClients; // 信号量

        private int port; // 端口
        public ServerStart(int max,int port)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
            this.macClient = max;
            this.port = port;

            pool = new UserTokenPool(max);
            acceptClients = new Semaphore(max, max);
            for(int i = 0; i < max; i++)
            {
                UserToken userToken = new UserToken();
               
                userToken.receiveSAEA.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                userToken.sendSAEA.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);

                pool.push(userToken);
            }
        }

        public void Start()
        {
            server.Bind(new IPEndPoint(IPAddress.Any,port));
            server.Listen(10);

            StartAccept(null);
        }

        public void StartAccept(SocketAsyncEventArgs e)
        {
            // 如果为空，表示新的客户端连接监听事件； 否则移除当前客户端连接
            if (e == null)
            {
                e = new SocketAsyncEventArgs();
                e.Completed += new EventHandler<SocketAsyncEventArgs>(Accept_Completed);
            }
            else
                e.AcceptSocket = null;

            acceptClients.WaitOne();
            bool result = server.AcceptAsync(e);
            if (!result)
                ProcessAccept(e);
        }

        public void ProcessAccept(SocketAsyncEventArgs e)
        {
            UserToken token = pool.pop();
            token.conn = e.AcceptSocket;

            StartReceive(token);

            StartAccept(e);
        }
        public void Accept_Completed(object sender,SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        public void StartReceive(UserToken token)
        {
            bool result=token.conn.ReceiveAsync(token.receiveSAEA);
            if (result == false)
                ProcessReceive(token.receiveSAEA);
        }

        public void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            if (e.LastOperation == SocketAsyncOperation.Receive)
                 ProcessReceive(e);
            else
                 ProcessSend(e);
        }

        public void ProcessReceive(SocketAsyncEventArgs e)
        {
            UserToken token = e.UserToken as UserToken;
            if(token.receiveSAEA.BytesTransferred>0 && token.receiveSAEA.SocketError == SocketError.Success)
            {
                byte[] message = new byte[token.receiveSAEA.BytesTransferred];
                Buffer.BlockCopy(token.receiveSAEA.Buffer, 0, message, 0, token.receiveSAEA.BytesTransferred);
                StartReceive(token);
            }
        }
        public void ProcessSend(SocketAsyncEventArgs e)
        {

        }

    }
}
