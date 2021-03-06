﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Collections.ObjectModel;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace FinanceHub.View
{
    #region Stock class
    public class Stock
    {
        public string Symbol { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal Change { get; set; }
        public decimal CurrentPrice { get; set; }
    }
    #endregion
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class StocksPage : FinanceHub.Common.LayoutAwarePage
    {
        public StocksPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var collection = new ObservableCollection<Stock>();
            collection.Add(new Stock { Symbol = "MSFT", OpenPrice = 30.05M, Change = 0.25M, CurrentPrice = 30.30M });
            this.DefaultViewModel["Items"] = collection;
        }

        void ClickedStock(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(StockDetails));
        }

    }
}
