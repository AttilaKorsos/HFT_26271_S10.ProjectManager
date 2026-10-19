using HFT_26271_S10.ProjectManager.DataAccess.DataProviders;
using HFT_26271_S10.ProjectManager.Models.Attributes;
using HFT_26271_S10.ProjectManager.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HFT_26271_S10.ProjectManager.DataAccess.Exporters
{
    public class ReflectionFileExporter
    {
        List<object> objects;

        public ReflectionFileExporter(List<object> tasks)
        {
            this.objects = tasks;
        }

        public void Fejlec(ProjectTask tasks)
        {

            Type type = typeof(ProjectTask);
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                int x = 0;
                DisplayLabelAttribute? attr = prop.GetCustomAttribute<DisplayLabelAttribute>();
                if (attr != null)
                {

                }
            }
        }
        public void Adatok()
        {
            StreamWriter writer = new StreamWriter("adatok.txt");
            foreach (var item in objects)
            {
                Type type = item.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (PropertyInfo prop in propertyInfos)
                {
                    DisplayLabelAttribute? attr = prop.GetCustomAttribute<DisplayLabelAttribute>();
                    if (attr != null)
                    {
                        prop.GetValue(item);
                    }
                }
            }
        }
    }
}
