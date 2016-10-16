(function () {
    "use strict";

    WinJS.Namespace.define("FinanceHub.LocalStorageHelper", {

        ParseCSV: function (data) {
            var allTextLines = data.split(/\r\n|\n/);
            var rows = [];

            for (var i = 0; i < allTextLines.length; i++) {
                var data = allTextLines[i].split(',');
                var temp = [];
                for (var j = 0; j < data.length; j++) {
                    temp.push(data[j]);
                }
                rows.push(temp);
            }
            return rows;
        },

        GetRandomStockData: function () {
            var url = "/Model/SimulatedRandomStocksDetail.csv";
            return WinJS.xhr({ url: url }).then(function (response) {
                return FinanceHub.LocalStorageHelper.ParseCSV(response.responseText);
            });
        },

        stockFile: "StocksFile.csv",
        csvString: "",
        SaveStocks: function () {
            var app = WinJS.Application;
            FinanceHub.Data.Stocks.forEach(showIteration);
            function showIteration(value, index, array) {
                var temp = [];
                temp.push(value.Symbol);
                temp.push(value.OpenPrice);
                temp.push(value.CurrentPrice);
                temp.push(value.Change);
                temp.push(value.DaysRange);
                temp.push(value.Range52Week);
                FinanceHub.LocalStorageHelper.csvString = FinanceHub.LocalStorageHelper.csvString.concat(temp.join(), "\r\n");
            }

            app.local.writeText(FinanceHub.LocalStorageHelper.stockFile, FinanceHub.LocalStorageHelper.csvString);
        },

        LoadStocks: function () {
            var app = WinJS.Application;

            return app.local.exists(FinanceHub.LocalStorageHelper.stockFile).then(function (exists) {
                if (exists)
                    return app.local.readText(FinanceHub.LocalStorageHelper.stockFile).then(function (data) {
                        return FinanceHub.LocalStorageHelper.ParseCSV(data);

                    });
                else return null;
            });
        },


    });
})();
