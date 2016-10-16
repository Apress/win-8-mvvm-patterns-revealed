using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FinanceHub.Infrastructure
{
    public interface INavigationService
    {
        void InitializeFrame(Frame frame);
        event NavigatingCancelEventHandler Navigating;
        bool Navigate(string type);
        bool Navigate(string type, object parameter);
        void GoBack();
    }
}
