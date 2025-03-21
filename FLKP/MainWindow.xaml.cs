using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FLKP
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        private SQLiteHelper sQLiteHelper = new SQLiteHelper();
        public MainWindow()
        {
            this.InitializeComponent();
            DisplayChecks();
        }


        private void DisplayChecks()
        {
            DateTime now = DateTime.Now;
            DateTime the_week_start = now.AddDays(-(int)now.DayOfWeek);
            DateTime the_week_end = now.AddDays(7 - (int)now.DayOfWeek);
            int weekday_index = 0;
            while (the_week_start < the_week_end)
            {
                int _date = Convert.ToInt32(the_week_start.ToString("yyMMdd"));
                long _checks = sQLiteHelper.ReadChecks(_date);
                for (int i = 0; i < 12; i++)
                {
                    var day_name = $"day{weekday_index}_text";
                    var name = $"cb{weekday_index}{Int2String(i + 1)}";
                    CheckBox? cb = main_grid.FindName(name) as CheckBox;
                    TextBlock? textBlock = main_grid.FindName(day_name) as TextBlock;
                    if (cb != null)
                    {
                        cb.IsChecked = GetIndexChecks(_checks, i) == 1;
                    }
                    if (textBlock != null)
                    {
                        textBlock.Text = the_week_start.ToString("yyMMdd");
                    }
                }
                the_week_start = the_week_start.AddDays(1);
                weekday_index += 1;
            }
            RedDisplayTodayAndIngoreOthers((int)now.DayOfWeek);
        }

        private long SetIndexChecksTrue(long checks, int index)
        {
            return checks | (long)(1 << index);
        }

        private long SetIndexChecksFalse(long checks, int index)
        {
            return checks & (long)(~(1 << index));
        }

        private int GetIndexChecks(long checks, int index)
        {
            return (int)((checks >> index) & 1);
        }


        private void RedDisplayTodayAndIngoreOthers(int now_weekday)
        {
            switch (now_weekday)
            {
                case 0:
                    day0_text.IsColorFontEnabled = true;
                    day0_text.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red);
                    break;
                case 1:
                    day1_text.IsColorFontEnabled = true;
                    day1_text.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red);
                    break;
                case 2:
                    day2_text.IsColorFontEnabled = true;
                    day2_text.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red);
                    break;
                case 3:
                    day3_text.IsColorFontEnabled = true;
                    day3_text.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red);
                    break;
                case 4:
                    day4_text.IsColorFontEnabled = true;
                    day4_text.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red);
                    break;
                case 5:
                    day5_text.IsColorFontEnabled = true;
                    day5_text.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red);
                    break;
                case 6:
                    day6_text.IsColorFontEnabled = true;
                    day6_text.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red);
                    break;
            }
            for (int i = 0; i < 12; i++)
            {
                var name = $"cb{now_weekday}{Int2String(i + 1)}";
                CheckBox? cb = main_grid.FindName(name) as CheckBox;
                if (cb != null)
                {
                    cb.IsEnabled = true;
                }
            }
        }

        private void Option_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb)
            {
                string cbn = cb.Name;
                var day = String2Int(cbn.Substring(2, 1));
                var type = String2Int(cbn.Substring(3, 1));
                if (day != -1 && type != -1)
                {
                    int _date = Convert.ToInt32(DateTime.Now.ToString("yyMMdd"));
                    long _checks = sQLiteHelper.ReadChecks(_date);
                    _checks = SetIndexChecksTrue(_checks, type - 1);
                    sQLiteHelper.UpdateCheck(_date, _checks);
                }
            }
        }

        private void Option_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb)
            {
                string cbn = cb.Name;
                var day = String2Int(cbn.Substring(2, 1));
                var type = String2Int(cbn.Substring(3, 1));
                if (day != -1 && type != -1)
                {
                    int _date = Convert.ToInt32(DateTime.Now.ToString("yyMMdd"));
                    long _checks = sQLiteHelper.ReadChecks(_date);
                    _checks = SetIndexChecksFalse(_checks, type - 1);
                    sQLiteHelper.UpdateCheck(_date, _checks);
                }
            }
        }

        private int String2Int(string s)
        {
            int i = 0;
            if (int.TryParse(s, out i))
            {
                return i;
            }
            switch (s)
            {
                case "A":
                case "a":
                    return 10;
                case "B":
                case "b":
                    return 10;
                case "C":
                case "c":
                    return 10;
                default:
                    return -1;
            }

        }

        private string Int2String(int i)
        {
            if (i < 10)
            {
                return i.ToString();
            }
            else
            {
                switch (i)
                {
                    case 10:
                        return "A";
                    case 11:
                        return "B";
                    case 12:
                        return "C";
                    default:
                        return "X";
                }
            }
        }


        private async Task ShownInfoDialog(string title, string content)
        {
            await ShowMessageAsync(title, content, Content.XamlRoot);
        }

        public static async Task ShowMessageAsync(string title, string content, XamlRoot xamlRoot)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = title;
            dialog.Content = content;
            dialog.CloseButtonText = "OK";
            dialog.XamlRoot = xamlRoot;
            await dialog.ShowAsync();
        }

    }

}
