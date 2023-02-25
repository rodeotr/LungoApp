using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace LungoApp.Views.LeftPanel
{
    public class ContextConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string v1 = values[0].ToString();
            object v2 = values[1];
            
            return new Tuple<string,object>(v1,v2);
            //return new Tuple<StorageContext, string>((StorageContext)values[0], "dsa");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
