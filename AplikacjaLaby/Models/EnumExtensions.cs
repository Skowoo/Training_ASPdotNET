using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AplikacjaLaby.Models
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Extension method created to diplay the value from DataAnnotations "Display" attribute (if exists), 
        /// otherwise returns default variable name.
        /// </summary>
        /// <param name="enumValue">Enum value</param>
        /// <returns> Name of given value </returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            string defaultName = enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .Name;

            var aliasName = enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>();

            if (aliasName is null)
                return defaultName;
            else if (aliasName.GetName() is null)
                return defaultName;
            else
                return aliasName.GetName()!;
        }
    }
}