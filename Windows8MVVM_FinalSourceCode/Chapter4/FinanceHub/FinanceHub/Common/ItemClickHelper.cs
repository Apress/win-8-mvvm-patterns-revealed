using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FinanceHub.Common
{
    public class ItemClickHelper
    {

        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(ItemClickHelper), new PropertyMetadata(null, CommandPropertyChanged));

        private static void CommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Attach click handler
            (d as ListViewBase).ItemClick += ItemClickHelper_ItemClick;
        }

        private static void ItemClickHelper_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Get GridView
            var itemView = (sender as ListViewBase);

            // Get command
            ICommand command = GetCommand(itemView);

            // Execute command
            command.Execute(e.ClickedItem);
        }


        
    }
}
