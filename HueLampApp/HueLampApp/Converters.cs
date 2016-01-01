using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace HueLampApp
{
    public class BoolToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //System.Diagnostics.Debug.WriteLine($"Convert\nValue: {value}\nTargetType: {targetType}\nParameter: {parameter}\nLanguage: {language}");
            if ((bool)value)
            {
                return new SolidColorBrush(Colors.Green);
            }
            return new SolidColorBrush(Colors.Red);

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            //System.Diagnostics.Debug.WriteLine($"ConvertBack\nValue: {value}\nTargetType: {targetType}\nParameter: {parameter}\nLanguage: {language}");
            return true;
        }
    }

    public class LampColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Color c = ColorUtil.getColor((Tuple<int, long, int>)value);
            //System.Diagnostics.Debug.WriteLine(c.ToString());
            return new SolidColorBrush(c);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class IntToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //System.Diagnostics.Debug.WriteLine($"Value.getType: {value.GetType()}\nType: {targetType}");
            return value + "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            //System.Diagnostics.Debug.WriteLine($"Value.getType: {value.GetType()}\nType: {targetType}");
            return int.Parse((string)value);
        }
    }

    public class BoolToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value != null && value is bool)
            {
                bool b = (bool)value;
                return b ? Visibility.Visible : Visibility.Collapsed;
            }            
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
