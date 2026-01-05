using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace LogicLayer.Utilities
{
    static public class NamesManager
    {
        static public string GetArabicPropertyName(Type objectType, string propertyName)
        {
            var property = objectType.GetProperty(
                propertyName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy
            );

            if (property == null)
                return propertyName;

            // 1️ DisplayAttribute
            var displayAttr = property.GetCustomAttribute<DisplayAttribute>();
            if (displayAttr != null)
                return displayAttr.GetName();

            return propertyName;
        }

        static public string GetArabicEntityName(Type objectType)
        {
            var displayAttr = objectType.GetCustomAttribute<DisplayAttribute>();
            return displayAttr?.GetName() ?? objectType.Name;
        }

        

    }
}
