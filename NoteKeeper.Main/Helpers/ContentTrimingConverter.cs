using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NoteKeeper.Main.Helpers
{

    public class ContentTrimingConverter : IValueConverter
    {
        private const int maxLength = 150;

        public static ContentTrimingConverter Instance { get; } = new ();


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            var text = value.ToString();
            var sub =  text.Length > maxLength ? text[..maxLength] : text;

            return sub.Replace("\r\n", " ");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
