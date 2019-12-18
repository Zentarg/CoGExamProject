using System;
using Windows.UI.Xaml.Controls;

namespace MainWindow.Models
{
    public class NavigationHandler
    {

        private static NavigationHandler _instance;

        private NavigationHandler() {}

        /// <summary>
        /// The singleton instance of NavigationHandler.
        /// </summary>
        public static NavigationHandler Instance
        {
            get { return _instance ?? (_instance = new NavigationHandler()); }
        }

        private Frame NavigationFrame { get; set; }


        /// <summary>
        /// Sets the frame to handle navigation on.
        /// </summary>
        /// <param name="frame">The frame to handle navigation on.</param>
        public void SetNavigationFrame(Frame frame)
        {
            NavigationFrame = frame;
        }

        /// <summary>
        /// Navigates the frame to passed page.
        /// </summary>
        /// <param name="newPage">The page to navigate to.</param>
        public void NavigateFrame(Type newPage)
        {
            NavigationFrame.Navigate(newPage);
        }

        /// <summary>
        /// Navigates the frame back one step.
        /// </summary>
        public void NavigateFrameBack()
        {
            NavigationFrame.GoBack();
        }

        /// <summary>
        /// Navigates the frame forwards one step.
        /// </summary>
        public void NavigateFrameForwards()
        {
            NavigationFrame.GoForward();
        }

    }
}
