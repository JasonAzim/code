// These functions can be called from inside Blazor Pages

function OpenWindow(url) {
        window.open(url, 'Test', 'width=800,height=860,scrollbars=no,toolbar=no,location=no');
    return false
}

window.DocModule = {
    downloadDocument: function (url) {
        var wnd = window.open(url, 'Download', 'width=800,height=860,scrollbars=no,toolbar=no,location=no');
        setTimeout(function () { wnd.close(); }, 9000);
    }
};

window.getWindowDimensions = function () {
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
};
