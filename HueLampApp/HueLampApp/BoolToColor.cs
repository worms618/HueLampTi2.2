using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace HueLampApp
{
    public class BoolToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            System.Diagnostics.Debug.WriteLine($"Convert\nValue: {value}\nTargetType: {targetType}\nParameter: {parameter}\nLanguage: {language}");
            if ((bool)value)
            {
                return new SolidColorBrush(Colors.Green);
            }
            return new SolidColorBrush(Colors.Red);
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            System.Diagnostics.Debug.WriteLine($"ConvertBack\nValue: {value}\nTargetType: {targetType}\nParameter: {parameter}\nLanguage: {language}");
            return true;
        }
    }
}
