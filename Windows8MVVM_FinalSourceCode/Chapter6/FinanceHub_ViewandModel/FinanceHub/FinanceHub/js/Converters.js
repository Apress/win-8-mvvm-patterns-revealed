(function () {
    "use strict";

    WinJS.Namespace.define("FinanceHub.Converters", {
        ChangeArrowConverter: WinJS.Binding.converter(function (value) {
            return value >= 0 ? "..\\images\\up.png" : "..\\images\\down.png";
        }),

        ChangeColorConverter: WinJS.Binding.converter(function (value) {
            return value >= 0 ? "#108104" : "#C02C01";
        })
    });
})();
