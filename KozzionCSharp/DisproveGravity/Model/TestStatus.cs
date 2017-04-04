using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisproveGravity.Model
{
    public enum TestStatus
    { 
        NotApplicable,        //Blue,   Test requirements are not met.
        UnfoundedFailure,     //Orange, Test requirements are met, test assumptions are not met, test result is not significant
        UnfoundedSuccesfull,  //Yellow, Test requirements are met, test assumptions are not met, test result is significant
        FoundedFailure,       //Red,    Test requirements are met, test assumptions are met, test result is not significant
        FoundedSuccesfull,    //Green,  Test requirements are met, test assumptions are met, test result is significant
        FoundedContradictory, //Purple, Test requirements are met, test assumptions are met, test result is significant, but violates user forced asumptions
    }
}
