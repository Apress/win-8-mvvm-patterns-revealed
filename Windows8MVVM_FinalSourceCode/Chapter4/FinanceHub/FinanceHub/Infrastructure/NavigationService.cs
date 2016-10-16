using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FinanceHub.Infrastructure
{
    public class NavigationService : INavigationService
    {
        private Frame _mainFrame;
        public event NavigatingCancelEventHandler Navigating;

        public void InitializeFrame(Frame frame)
        {
            _mainFrame = frame;
        }

        public bool Navigate(string type, object parameter)
        {
            return _mainFrame.Navigate(Type.GetType(type), parameter);
        }

        public bool Navigate(string type)
        {
            return _mainFrame.Navigate(Type.GetType(type));
        }

        public void GoBack()
        {
            if (_mainFrame.CanGoBack)
            {
                _mainFrame.GoBack();
            }
        }
     }
}
