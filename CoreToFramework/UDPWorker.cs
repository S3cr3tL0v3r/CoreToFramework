using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Messenger
{
    public class UDPWorker
    {
        private readonly int port;
        private readonly UdpClient udpReceiver;
        private IPEndPoint remoteEP;

        public UDPWorker(int port = 8192)
        {
            this.port = port;
            udpReceiver = new UdpClient(port);
            remoteEP = new IPEndPoint(IPAddress.Any, port);
        }

        public string[] Receive()
        {
            try
            {
                byte[] bytes = udpReceiver.Receive(ref remoteEP);
                string[] data = { remoteEP.ToString().Split(':')[0], Encoding.UTF8.GetString(bytes) };
                return data;
            }
            catch (SocketException)
            {
                return null;
            }
        }

        public void EndReceive()
        {
            udpReceiver.Close();
        }

        public void Send(string ipAdress, string message)
        {
            UdpClient udpSender = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipAdress), port);
            udpSender.Connect(ep);
            byte[] dgram = Encoding.UTF8.GetBytes(message);
            udpSender.Send(dgram, dgram.Length);
            udpSender.Close();
        }
    }
}
