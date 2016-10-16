using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace FinanceHub.Common
{
    class UIHelper
    {
        public static Popup ShowPopup(FrameworkElement source, UserControl control)
        {
            Popup flyout = new Popup();

            var windowBounds = Window.Current.Bounds;
            var rootVisual = Window.Current.Content;

            //Define Flyout Control Position, Enable Light Dismiss and Display Popup
            GeneralTransform gt = source.TransformToVisual(rootVisual);

            var absolutePosition = gt.TransformPoint(new Point(0, 0));

            control.Measure(new Size(Double.PositiveInfinity, double.PositiveInfinity));

            flyout.VerticalOffset = windowBounds.Height - control.Height - 120;
            flyout.HorizontalOffset = (absolutePosition.X + source.ActualWidth / 2) - control.Width / 2;
            flyout.IsLightDismissEnabled = true;

            flyout.Child = control;
            var transitions = new TransitionCollection();
            transitions.Add(new PopupThemeTransition() { FromHorizontalOffset = 0, FromVerticalOffset = 100 });
            flyout.ChildTransitions = transitions;
            flyout.IsOpen = true;

            // Handling the virtual keyboard
            int flyoutOffset = 0;
            Windows.UI.ViewManagement.InputPane.GetForCurrentView().Showing += (s, args) =>
            {
                flyoutOffset = (int)args.OccludedRect.Height;
                flyout.VerticalOffset -= flyoutOffset;
            };
            Windows.UI.ViewManagement.InputPane.GetForCurrentView().Hiding += (s, args) =>
            {
                flyout.VerticalOffset += flyoutOffset;
            };

            return flyout;
        }
    }
}
