using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace GD.FinishingSystem.WEB.Classes
{
        public class ZEBRA_IP_PRINTER : IPrinterGD
        {
            public ZEBRA_IP_PRINTER(IPAddress ip, int port)
            {
                Ip = ip;
                Port = port;
            }
            public ZEBRA_IP_PRINTER(string ip, int port)
            {
                Ip = IPAddress.Parse(ip);
                Port = port;
            }

            public IPAddress Ip { get; }
            public int Port { get; }

            TcpClient client = new TcpClient();
            public bool CheckConnection()
            {
                TcpClient subClient = new TcpClient();
                try
                {
                    subClient.Connect(Ip, Port);
                }
                catch
                {

                }
                return subClient.Connected;
            }

            public bool Connect()
            {
                if (CheckConnection())
                {
                    try
                    {
                        if (client.Connected) Disconnect();
                        client = new TcpClient();
                        client.Connect(Ip, Port);

                    }
                    catch (Exception)
                    {

                    }

                    return client.Connected;
                }
                else return false;
            }



            public void Disconnect()
            {
                Disconnect(client.Connected);
            }

            void Disconnect(bool status)
            {
                if (status)
                {
                    client.Close();
                }
            }
            public void Dispose()
            {
                client = null;
            }

            public string PrintFromFile(string FileLocation, Dictionary<string, string> replaces)
            {
                if (File.Exists(FileLocation))
                {

                    var readed = File.ReadAllText(FileLocation);
                    replaces.Keys.ToList().ForEach(
                        o =>
                        {
                            readed = readed.Replace(o, replaces[o]);
                        }
                        );
                    return PrintFromParameter(readed);
                }
                return string.Empty;
            }

            public string PrintFromParameter(string ZPLString)
            {
                NetworkStream stream = client.GetStream();
                Byte[] data = Encoding.ASCII.GetBytes(ZPLString);
                stream.Write(data, 0, data.Length);
                data = new Byte[8192];
                String responseData = String.Empty;
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.ASCII.GetString(data, 0, bytes);

                stream.Close();


                return responseData;

            }


        }
    }

    IPrinterGD _PRINTER = new ZEBRA_USB_PRINTER(Properties.Settings.Default.ParisinaStickerPrinter);


            if (_PRINTER.CheckConnection())
            {
                _PRINTER.Connect();
                _PRINTER.PrintFromFile(Properties.Settings.Default.ParisinaStickerLocation, replaces).Replace(" ", "");
    _PRINTER.Disconnect();
            }
            else
{
    Messages.Error("Please check printer connections or ip adress!!!");
}
if (!File.Exists(Settings.Default.ParisinaStickerLocation))
{
    Messages.Error("Please check sticker or sticker location!!!");
    return;
}


Dictionary<string, string> replaces = new Dictionary<string, string>();

replaces.Add("prmTipo", resrec.Tipo);


replaces.Add("prmSKU", resrec.SKU);

replaces.Add("prmDesc", resrec.Description);

replaces.Add("prmMetros", Math.Round(rollMtr, 1).ToString());

var ancho = Math.Round(rollWidth, 1);
replaces.Add("prmAncho", ancho.ToString());



IPrinterGD _PRINTER = new ZEBRA_USB_PRINTER(Properties.Settings.Default.ParisinaStickerPrinter);


if (_PRINTER.CheckConnection())
{
    _PRINTER.Connect();
    _PRINTER.PrintFromFile(Properties.Settings.Default.ParisinaStickerLocation, replaces).Replace(" ", "");
    _PRINTER.Disconnect();
}
else
{
    Messages.Error("Please check printer connections or ip adress!!!");
}
}
