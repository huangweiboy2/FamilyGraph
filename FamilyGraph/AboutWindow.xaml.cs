using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace FamilyGraph
{
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_OnClick(object sender, RoutedEventArgs e)
        {
            var hyperlink = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(hyperlink?.NavigateUri.AbsoluteUri));
        }
    }
}