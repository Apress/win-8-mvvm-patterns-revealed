(function () {
    "use strict";

    WinJS.Namespace.define("FinanceHub.ViewModel", {
        StocksPageViewModel: WinJS.Class.define(
            function () {
                var self = this;
                self.stocks = new WinJS.Binding.List();
                //self.stocks.push(new FinanceHub.Model.Stock("MSFT", 25, 24, 2, "24.59 - 25.92", "23.22 - 25.76"));
                WinJS.Utilities.markSupportedForProcessing(this.stockClicked);
                WinJS.Utilities.markSupportedForProcessing(this.stockSelectionChanged);


                //Expose the data source
                WinJS.Namespace.define("FinanceHub.Data", {
                    Stocks: self.stocks

                });

                //Expose the events and methods
                WinJS.Namespace.define("FinanceHub.Command", {
                    StockClicked: self.stockClicked,
                    StockSelectionChanged: self.stockSelectionChanged,
                });
            }
            , {
                stocks: undefined,
                stockClicked: function (e) {
                    WinJS.Navigation.navigate("/View/StockDetails.html").then(function () {

                        FinanceHub.Command.StockSelectionChanged(e);

                    });
                },

                stockSelectionChanged: function (e) {
                    var ele = document.getElementById('stockInfoView');
                    WinJS.Binding.processAll(ele, FinanceHub.Data.Stocks.getAt(e.detail.itemIndex));
                },

            }),
    });
})();
