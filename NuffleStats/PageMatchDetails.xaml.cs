using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NuffleStats
{
    /// <summary>
    /// Interaction logic for PageMatchDetails.xaml
    /// </summary>
    public partial class PageMatchDetails : Page
    {
        public PageMatchDetails()
        {
            InitializeComponent();
            DataContext = App.dataLayer.matchResult;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("Page_MatchList.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
