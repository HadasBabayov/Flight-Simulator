using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Ex1
{
    public class RunFlight
    {
        private volatile bool shouldStop;
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private StreamReader file;
        private volatile int pace;
        private volatile bool isConnectionIsSet = false;

        public RunFlight() { setConnection(); pace = 100; }

        public void setConnection()
        {
            tcpClient = new TcpClient("localhost", 5400);
            networkStream = tcpClient.GetStream();
            file = new StreamReader(@"reg_flight.csv");
            isConnectionIsSet = true;
        }

        public void CloseConnection()
        {
            file.Close();
            networkStream.Close();
            tcpClient.Close();
        }

        public void Run()
        {
            if(isConnectionIsSet == false)
            {
                setConnection();
            }
            shouldStop = false;
            Thread t = new Thread(
                delegate ()
                {
                    shouldStop = false;
                    string line;
                    while ((line = file.ReadLine()) != null && !shouldStop)
                    {
                        line += "\r\n";
                        byte[] msgToSend = Encoding.ASCII.GetBytes(line);
                        networkStream.Write(msgToSend, 0, msgToSend.Length);
                        networkStream.Flush();
                        Console.WriteLine(line);
                        Thread.Sleep(pace);
                    }
                }
            );
            t.Start();
        }

        public void StopFlight() { shouldStop = true; }

        public void StartFlightOver()
        {
            StopFlight();
            CloseConnection();
            setConnection();
            Run();
        }
    }
}
