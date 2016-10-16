// For an introduction to the Navigation template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232506
(function () {
    "use strict";

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;
    var nav = WinJS.Navigation;
    WinJS.strictProcessing();

    app.addEventListener("activated", function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                // TODO: This application has been newly launched. Initialize
                // your application here.
                var vm = new FinanceHub.ViewModel.StocksPageViewModel();
                var pageVM = WinJS.Binding.as(vm);

                WinJS.Binding.processAll(document.body, pageVM).then(function () {

                    //Load previous state 
                    FinanceHub.LocalStorageHelper.LoadStocks().then(function (csvData) {
                        if (csvData != null && csvData.length > 0) {
                            csvData.forEach(showIteration);
                        }
                        else {
                           FinanceHub.Data.Stocks.push(FinanceHub.Command.GetNewStock("MSFT"));
                        }

                        function showIteration(value, index, array) {
                            if (value != "") {
                                FinanceHub.Data.Stocks.push(new FinanceHub.Model.Stock(value[0], value[1], value[2], value[3], value[4], value[5]));
                            }
                        }
                    }).then(function () {

                        //Populate the array for remove stock view
                        //var arr1 = [];
                        //FinanceHub.Data.Stocks.forEach(showIteration);
                        //function showIteration(value, index, array) {
                        //    arr1.push(value.Symbol);
                        //}

                        // Activates knockout.js
                        ko.applyBindings(new FinanceHub.ViewModel.AddRemoveStockViewModel());
                    });
                });

            } else {
                // TODO: This application has been reactivated from suspension.
                // Restore application state here.
            }

            if (app.sessionState.history) {
                nav.history = app.sessionState.history;
            }
            args.setPromise(WinJS.UI.processAll().then(function () {
                if (nav.location) {
                    nav.history.current.initialPlaceholder = true;
                    return nav.navigate(nav.location, nav.state);
                } else {
                    return nav.navigate(Application.navigator.home);
                }
            }));
        }
    });

    app.oncheckpoint = function (args) {
        // TODO: This application is about to be suspended. Save any state
        // that needs to persist across suspensions here. If you need to 
        // complete an asynchronous operation before your application is 
        // suspended, call args.setPromise().
        app.sessionState.history = nav.history;
        FinanceHub.LocalStorageHelper.SaveStocks();
    };

    app.start();
})();
