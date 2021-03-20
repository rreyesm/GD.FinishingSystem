using System;
using System.Collections.Generic;
using System.Text;

namespace GD.FinishingSystem.WEB.Classes
{
    public interface IPrinterGD : IDisposable
    {
        bool CheckConnection();
        bool Connect();
        void Disconnect();
        string PrintFromFile(string FileLocation, Dictionary<string, string> replaces);
        string PrintFromZPL(string ZPL, Dictionary<string, string> replaces);
        string PrintFromParameter(string Parameter);



    }
}
