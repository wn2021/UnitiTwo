var global = {
    setUser: function (userGuid, userName) {
        window.localStorage['userInfo'] = JSON.stringify({
            UserGuid: userGuid || '',
            UserName: userName || ''
        });
    },
    getUser: function () {
        var userInfoJson = window.localStorage['userInfo'];
        var userInfo = {
            UserGuid: '',
            UserName: ''
        };
        if (userInfoJson != undefined && userInfoJson != null && userInfoJson.length > 0) {
            var userInfoObj = JSON.parse(userInfoJson);
            userInfo.UserGuid = userInfoObj.UserGuid;
            userInfo.UserName = userInfoObj.UserName;
        }
        return userInfo;
    }
}