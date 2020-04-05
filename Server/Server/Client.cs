using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Client
    {
        public static int dataBuffersize = 4096;
        public int id;
        public TCP tcp;

        public Client(int _clientid)
        {
            id = _clientid;
            tcp = new TCP(id);
        }

        public class TCP{
            public TcpClient socket; 
            
            private readonly int id;
            private NetworkStream stream;
            private byte[] receiveBuffer;
           
            public TCP(int _id)
            {
                id = _id;
            }

            public void Connect(TcpClient _socket)
            {
                socket = _socket;
                socket.ReceiveBufferSize = dataBuffersize;
                socket.SendBufferSize = dataBuffersize;

                stream = socket.GetStream();

                receiveBuffer = new byte[dataBuffersize];

                stream.BeginRead(receiveBuffer, 0, dataBuffersize, ReceiveCallback, null);

                ServerSend.Welcome(id, "Welcome!!!");
            }

            public void SendData(Packet _packet)
            {
                try
                {
                    if (socket != null)
                    {
                        stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                    }
                }
                catch (Exception _ex)
                {
                    Console.WriteLine($"Error sending data to player {id} via TCP: {_ex}");
                }
            }

            private void ReceiveCallback(IAsyncResult _results)
            {
                try
                {
                    int _byteLength = stream.EndRead(_results);
                    if (_byteLength <= 0)
                    {
                        //TODO: disconnect
                        return;
                    }

                    byte[] _data = new byte[_byteLength];
                    Array.Copy(receiveBuffer, _data, _byteLength);

                    //TODO: handle data
                    stream.BeginRead(receiveBuffer, 0, dataBuffersize, ReceiveCallback, null);  
                }
                catch (Exception _ex)
                {
                    Console.WriteLine($"Error receiving TCP data: {_ex}");
                    // TODO: disconnect;
                }
            }
        }
    }
}
