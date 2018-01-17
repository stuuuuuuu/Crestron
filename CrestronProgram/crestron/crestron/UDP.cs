using System;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using System.Text;
using Crestron.SimplSharp.CrestronSockets;


namespace Socket
{
    public class UDPAPI
    {
        private static byte[] HexStrTobyte(string hexString)    //16进制转换
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
            return returnBytes;
        }
        public void SendData(string data)
        {
            string host = "192.168.188.112";
            int port = 8080;
            UDPServer client = new UDPServer();
            byte[] sendBytes = Encoding.ASCII.GetBytes(data);
            client.EnableUDPServer(host, port);
            client.SendData(sendBytes, sendBytes.Length);
            client.Dispose();
        }
        public void SendData(string h, int p, string data)
        {
            string host = h;
            int port = p;
            byte[] sendBytes = HexStrTobyte(data);
            UDPServer client = new UDPServer();
            client.EnableUDPServer(host, port);
            client.SendData(sendBytes, sendBytes.Length);

            client.Dispose();

        }
        public int Receive(string h, int p)
        {
            string host = h;
            int port = p;
            UDPServer client = new UDPServer();
            client.EnableUDPServer(host, port);
            int n = 0;
            n = client.ReceiveData();
            client.Dispose();
            return n;
        }
    }
}
