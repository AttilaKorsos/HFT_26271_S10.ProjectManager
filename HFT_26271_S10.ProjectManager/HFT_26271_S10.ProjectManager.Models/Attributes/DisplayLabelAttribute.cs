using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFT_26271_S10.ProjectManager.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayLabelAttribute : Attribute
    {
        public string Label { get;}

        public DisplayLabelAttribute(string label)
        {
            Label = label;
        }

    }
}
