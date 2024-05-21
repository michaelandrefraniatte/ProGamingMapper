using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace PGM
{
    public class Network
    {
        public static string localip;
        public static string port;
        public static WebSocketServer wss;
        public static byte[] rawdataavailable;
        public static void Connect()
        {
            try
            {
                localip = Form1.networkip;
                port = Form1.networkport;
                String connectionString = "ws://" + localip + ":" + port;
                wss = new WebSocketServer(connectionString);
                wss.AddWebSocketService<Control>("/Control");
                wss.Start();
            }
            catch { }
        }
        public static void Disconnect()
        {
            wss.RemoveWebSocketService("/Control");
            wss.Stop();
        }
    }
    public class Control : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            base.OnMessage(e);
            while (Form1.scriptrunning)
            {
                try
                {
                    Send(Network.rawdataavailable);
                    Form1.unloadxc = false;
                    Form1.unloadkm = false;
                }
                catch { }
                System.Threading.Thread.Sleep(Form1.sleeptime + 1);
            }
        }
    }
}