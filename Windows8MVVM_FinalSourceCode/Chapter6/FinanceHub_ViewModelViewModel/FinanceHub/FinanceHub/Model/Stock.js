(function () {
    "use strict";

    WinJS.Namespace.define("FinanceHub.Model", {
        Stock: WinJS.Class.define(
            function (symbol, currentPrice, openPrice, change, daysRange, range52Week) {

                this.Symbol = symbol;
                this.OpenPrice = openPrice;
                this.CurrentPrice = currentPrice;
                this.Change = change;
                this.DaysRange = daysRange;
                this.Range52Week = range52Week;
            }
            , {

                Symbol: undefined,
                OpenPrice: undefined,
                CurrentPrice: undefined,
                Change: undefined,
                DaysRange: undefined,
                Range52Week: undefined
            }),
    });
})();
