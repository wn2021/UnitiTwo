var kit = {
    getQueryString: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) {
            return decodeURI(r[2]);
        }
        return null;
    },
    getIFrameUrl: function (url) {
        let iframeUrl = ''
        if (/^iframe:.*/.test(url)) {
            iframeUrl = url.replace('iframe:', '')
        } else if (/^http[s]?:\/\/.*/.test(url)) {
            iframeUrl = url.replace('http://', '')
            iframeUrl = iframeUrl.replace('https://', '')
        }
        return iframeUrl
    },
    isNullObj: function (obj) {
        if (obj == undefined || obj == null) {
            return true;
        }
        return false;
    },
    isEmptyList: function (obj) {
        if (obj == undefined || obj == null || obj.length <= 0) {
            return true;
        }
        return false;
    },
    isEmptyStr: function (obj) {
        if (obj == undefined || obj == null || obj.length <= 0) {
            return true;
        }
        return false;
    }
}