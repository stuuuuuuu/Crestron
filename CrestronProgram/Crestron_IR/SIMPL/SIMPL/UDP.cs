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
            byte[] sendBytes = Encoding.ASCII.GetBytes(data);
            client.EnableUDPServer(host, port);
            client.SendData(sendBytes, sendBytes.Length);
            client.DisableUDPServer();
            client.Dispose();
        }
        UDPServer client = new UDPServer();
        public void SendData(string h, int p, string data)
        {
            string host = h;
            int port = p;
            byte[] sendBytes = Encoding.ASCII.GetBytes(data);
            
            client.EnableUDPServer(host, port);
            client.SendData(sendBytes, sendBytes.Length);
            client.DisableUDPServer();
            client.Dispose();

        }
  
        public void ReceiveCallback(UDPServer myUDPServer, int numberOfBytesReceived)
        {

            string host = "192.168.188.112";
            int port = 8080;
            myUDPServer.EnableUDPServer(host,port);
            byte[] sendBytes = Encoding.ASCII.GetBytes("as");
                
      
            myUDPServer.DisableUDPServer();
            

        }
       


        public void ReceiveAsync()
        {
    
            UDPServer udp = new UDPServer();
            udp.ReceiveDataAsync(ReceiveCallback);
            
        }

      
    }
}
