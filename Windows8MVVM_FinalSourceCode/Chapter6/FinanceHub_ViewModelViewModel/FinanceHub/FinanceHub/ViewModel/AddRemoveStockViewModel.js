(function () {
    "use strict";

    WinJS.Namespace.define("FinanceHub.ViewModel", {
        AddRemoveStockViewModel: WinJS.Class.define(
            function () {

                var self = this;
                self.symbol = ko.observable();
                self.stocksVM = ko.observableArray();
                self.randomStockData = [];

                //Function for Adding new stock
                self.addStock = function () {
                    if (self.symbol() != undefined) {

                        //check for duplicate
                        for (var i = 0; i < FinanceHub.Data.Stocks.length; i++) {
                            if (FinanceHub.Data.Stocks.getAt(i).Symbol.toUpperCase() == self.symbol().toUpperCase())
                                return;
                        }

                        FinanceHub.Data.Stocks.push(self.getNewStock(self.symbol().toUpperCase()));
                        self.stocksVM.push(new StockViewModel(self.symbol().toUpperCase()));
                        self.symbol("");
                    }
                }

                self.getNewStock = function (symbol) {
                    var num = Math.floor((Math.random() * 3) + 1);
                    var newStock = self.randomStockData[num];
                    newStock.Symbol = symbol;
                    return newStock;
                }

                //Parse SimulatedRandomStocksDetail.csv for sample data
                FinanceHub.LocalStorageHelper.GetRandomStockData().then(function (csvData) {
                    csvData.forEach(showIteration);

                    function showIteration(value, index, array) {
                        self.randomStockData.push(new FinanceHub.Model.Stock("", value[0], value[1], value[2], value[3], value[4]));
                    }
                });

                FinanceHub.Data.Stocks.forEach(showIteration);
                function showIteration(value, index, array) {

                    self.stocksVM.push(new StockViewModel(value.Symbol));

                }

                //Function for removing the stock
                self.removeStock = function () {

                    for (var i = this.stocksVM().length - 1; i >= 0; i--) {
                        if (self.stocksVM()[i].isSelected() == true) {
                            FinanceHub.Data.Stocks.splice(i, 1);
                            self.stocksVM.remove(self.stocksVM()[i]);
                        };

                        document.getElementById("removeStockFlyout").winControl.hide();
                    }
                };

                function StockViewModel(data) {

                    this.stock = ko.observable(data);
                    this.isSelected = ko.observable(false);
                };
            })
    })
})();
