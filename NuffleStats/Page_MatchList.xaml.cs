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
using System.ComponentModel;

namespace NuffleStats
{
    /// <summary>
    /// Interaction logic for Page_MatchList.xaml
    /// </summary>
    public partial class Page_MatchList : Page
    {

        //DataLayer dataLayer;
        public Page_MatchList()
        {
            InitializeComponent();
            //dataLayer = new DataLayer();
            //dataLayer.LoadIndexXML();
            //DataContext =  (this.Parent as Frame).DataContext;

            DataContext = App.dataLayer;
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            BasicMatchListing selectedMatch = ((FrameworkElement)sender).DataContext as BasicMatchListing;
            App.dataLayer.selectedMatch = selectedMatch;
            App.dataLayer.LoadReplayXML(selectedMatch.replayFile);

            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("PageMatchDetails.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //DataContext = (this.Parent as Frame).DataContext;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //DataGrid dataGrid = (DataGrid)e.OriginalSource;
            //var firstCol = dataGrid.Columns.First();
            //firstCol.SortDirection = ListSortDirection.Descending;
            //dataGrid.Items.SortDescriptions.Add(new SortDescription(firstCol.SortMemberPath, ListSortDirection.Ascending));
        }

        
    }
}
