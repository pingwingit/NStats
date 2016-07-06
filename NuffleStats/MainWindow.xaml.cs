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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataLayer dataLayer;

        public MainWindow()
        {
            InitializeComponent();

            //dataLayer = new DataLayer();
            //dataLayer.LoadIndexXML();
            //dataLayer.LoadReplayXML();

            

            DataContext = App.dataLayer;
        }

        private void frame_LoadCompleted(object sender, NavigationEventArgs e)
        {
            DataContext = dataLayer;
        }

        //private void frame_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    UpdateFrameDataContext(sender, e);
        //}
        //private void frame_LoadCompleted(object sender, NavigationEventArgs e)
        //{
        //    UpdateFrameDataContext(sender, e);
        //}
        //private void UpdateFrameDataContext(object sender, NavigationEventArgs e)
        //{
        //    var content = frame.Content as FrameworkElement;
        //    if (content == null)
        //        return;
        //    content.DataContext = frame.DataContext;
        //}
    }
}
