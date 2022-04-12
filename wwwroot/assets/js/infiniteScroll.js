var pageSize = 10;
var pageIndex = 0;

$(document).ready(function () {
    GetData();

    $(window).scroll(function () {
        if ($(window).scrollTop() ==
            $(document).height() - $(window).height()) {
            GetData();
        }
    });
});

window.GetData = ()  => {
    DotNet.invokeMethodAsync('StartupEd.UX.Incubation', 'GetNetworkFindData');
    console.log("GetNetworkFindData");
    pageIndex = pageIndex + 1;
}