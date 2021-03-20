using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GD.FinishingSystem.Entities.ViewModels;

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
                    Text = v.ToString().SplitCamelCase(),
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

    }
}
