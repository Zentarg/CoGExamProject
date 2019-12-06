using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MainWindow.Models
{
    public class NavigationHandler
    {

        private static NavigationHandler _instance;

        private NavigationHandler() {}

        public static NavigationHandler Instance
        {
            get { return _instance ?? (_instance = new NavigationHandler()); }
        }

        private Frame NavigationFrame { get; set; }

        public void SetNavigationFrame(Frame frame)
        {
            NavigationFrame = frame;
        }

        public void NavigateFrame(Type newPage)
        {
            NavigationFrame.Navigate(newPage);
        }

        public void NavigateFrameBack()
        {
            NavigationFrame.GoBack();
        }

        public void NavigateFrameForwards()
        {
            NavigationFrame.GoForward();
        }

    }
}
