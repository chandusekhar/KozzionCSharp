using DisproveGravity.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace DisproveGravity.Control
{

    [ValueConversion(typeof(TestStatus), typeof(Brush))]
    public class ValueConvertorColorTestStatus : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TestStatus))
            {
                throw new Exception("value not of type TestStatus");
            }
            TestStatus status = (TestStatus)value;
            switch (status)
            {
                case TestStatus.NotApplicable: return new SolidColorBrush(Color.FromRgb(0, 0, 255));   //Blue  
                case TestStatus.UnfoundedFailure: return new SolidColorBrush(Color.FromRgb(255, 128, 0));//Orange
                case TestStatus.UnfoundedSuccesfull: return new SolidColorBrush(Color.FromRgb(255, 255, 0)); //Yellow
                case TestStatus.FoundedFailure: return new SolidColorBrush(Color.FromRgb(255, 0, 0)); //Red
                case TestStatus.FoundedSuccesfull: return new SolidColorBrush(Color.FromRgb(0, 255, 0)); //Green
                case TestStatus.FoundedContradictory: return new SolidColorBrush(Color.FromRgb(255, 0, 255)); //Purple
                default:
                    throw new Exception("Unknown status");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TestStatus.NotApplicable;
        }
    }
}
