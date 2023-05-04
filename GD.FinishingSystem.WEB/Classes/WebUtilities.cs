using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GD.FinishingSystem.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Bussines;

namespace GD.FinishingSystem.WEB.Classes
{
    public class WebUtilities
    {
        public static List<SelectListItem> Create<TEnum>(bool includeEmptyOption = false, string textEmptyOption = "--Select an item--", int selectedValue = 0)
        {
            var type = typeof(TEnum);
            if (!type.IsEnum)
            {
                throw new ArgumentException(
                    "type must be an enum",
                    nameof(type)
                );
            }

            var result = Enum.
                GetValues(type).
                Cast<IFormattable>()
                .Select(v => new SelectListItem
                {
                    Text = v.ToString(), //.SplitCamelCase(),
                    Value = v.ToString("d", null),
                    Selected = v.ToString("d", null) == selectedValue.ToString()
                }).ToList();

            if (includeEmptyOption)
            {
                // Insert the empty option
                // at the beginning
                if (selectedValue == 0)
                    result.Insert(0, new SelectListItem(textEmptyOption, "0", true));
                else
                    result.Insert(0, new SelectListItem(textEmptyOption, "0"));
            }

            return result;
        }

        public static List<SelectListItem> Create<T>(IEnumerable<T> list, string nameFieldValue = "Id", string nameFieldText = "Name", bool includeEmptyOption = false, string textEmptyOption = "--Select an item--", int selectedValue = 0)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            Type elementType = typeof(T);

            string fieldId = string.Empty;
            string fieldName = string.Empty;
            int count = 0;

            foreach (T item in list)
            {
                count = 0;
                foreach (var propInfo in elementType.GetProperties())
                {
                    if (nameFieldValue.Equals("Id", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (propInfo.Name.Equals("Id", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var value = propInfo.GetValue(item, null) ?? DBNull.Value;
                            fieldId = value.ToString();
                            count++;
                        }

                    }
                    else
                    {
                        if (propInfo.Name.Equals(nameFieldValue, StringComparison.InvariantCultureIgnoreCase))
                        {
                            var value = propInfo.GetValue(item, null) ?? DBNull.Value;
                            fieldId = value.ToString();
                            count++;
                        }
                    }
                    if (nameFieldText.Equals("Name", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (propInfo.Name.Equals("Name", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var value = propInfo.GetValue(item, null) ?? DBNull.Value;
                            fieldName = value.ToString();
                            count++;
                        }
                    }
                    else
                    {
                        if (propInfo.Name.Equals(nameFieldText, StringComparison.InvariantCultureIgnoreCase))
                        {
                            var value = propInfo.GetValue(item, null) ?? DBNull.Value;
                            fieldName = value.ToString();
                            count++;
                        }
                    }

                    if (count == 2)
                    {
                        if (selectedValue != 0 && selectedValue == int.Parse(fieldId))
                            result.Add(new SelectListItem(fieldName, fieldId, true));
                        else
                            result.Add(new SelectListItem(fieldName, fieldId));
                        break;
                    }

                }
            }

            if (includeEmptyOption)
            {
                //Insert the empty option at the beginning
                if (selectedValue == 0)
                    result.Insert(0, new SelectListItem(textEmptyOption, "0", true));
                else
                    result.Insert(0, new SelectListItem(textEmptyOption, "0"));
            }

            return result;
        }

        public static List<SelectListItem> CreateFromStringList(IEnumerable<String> list, bool includeEmptyOption = false, int selectedValue = 0)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            int count = 0;
            foreach (var item in list)
            {
                if (selectedValue != 0 && selectedValue == count)
                    result.Add(new SelectListItem(item, count.ToString(), true));
                else
                    result.Add(new SelectListItem(item, count.ToString()));

                count++;
            }

            if (includeEmptyOption)
            {
                //Insert the empty option at the beginning
                result.Insert(0, new SelectListItem("--Select an item--", "0"));
            }

            return result;
        }


        public static (bool IsOk, string IP) GetMachineIP(HttpContext context)
        {
            var isOk = true;
            var ip = context.Connection.RemoteIpAddress;
            var resultIP = string.Empty;

            try
            {
                // If we got an IPV6 address, then we need to ask the network for the IPV4 address 
                // This usually only happens when the browser is on the same machine as the server.
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    ip = System.Net.Dns.GetHostEntry(ip).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                }
                resultIP = ip.ToString();

            }
            catch (Exception)
            {
                isOk = false;
            }

            return (isOk, resultIP);
        }

        public async static Task<SystemPrinter> GetSystemPrinter(FinishingSystemFactory factory, HttpContext context)
        {
            var resultIP = GetMachineIP(context);

#if DEBUG
            //////Server
            //resultIP.IP = "192.168.182.193";
            ////local
            resultIP.IP = "192.168.182.193"; //This IPs is for simulate capture from KTM machine
#endif

            var systemPrinterList = await factory.SystemPrinters.GetSystemPrinterList();
            var systemPrinter = systemPrinterList.Where(x => x.PCIP == resultIP.IP).FirstOrDefault();
            
            return systemPrinter;
        }
    }
}
