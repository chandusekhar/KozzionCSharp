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
    public class ModelTestAssertion : ReactiveObject
    {
        public string Title { get; private set; }
        public TestAssertion TestAssertion { get; private set; }

        public ModelTestAssertion(TestAssertion asserrtion)
        {
            Title = ToolsEnum.EnumToString(asserrtion);
            TestAssertion = asserrtion;

        }
    }
}
