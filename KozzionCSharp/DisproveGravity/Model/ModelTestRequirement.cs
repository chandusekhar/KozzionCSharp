using KozzionCore.Tools;
using KozzionMathematics.Statistics.Test;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisproveGravity.Model
{
    public class ModelTestRequirement : ReactiveObject
    {
        public string Title { get; private set; }
        public TestRequirement TestRequirement { get; private set; }

        public ModelTestRequirement(TestRequirement requirement)
        {
            this.Title = ToolsEnum.EnumToString(requirement);
            this.TestRequirement = requirement;
        }
    }
}
