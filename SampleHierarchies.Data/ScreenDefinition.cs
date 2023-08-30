using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Data
{
    public class ScreenDefinition
    {
        public List<ScreenLineEntry> LineEntries  { get; set; } = new List<ScreenLineEntry>();
    }
}
