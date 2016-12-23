'use strict';
app.service('userService', function ($resource, $http, blockUI, $timeout, localStorageService, $q) {
    this.getUsers = function () {
        blockUI.start();
        var res = $resource('/api/users/:UserId', { UserId: "@User" });
        blockUI.stop();
        return res;
       
    }
    this.getVendors = function () {
        var res = $resource('/api/vendors/:VendorId', { VendorId: "@Vendor" });
        return res;
    }
    //this.login = function () {
    //    var res = $resource('Token', null,
    //        {
    //            'loginUser': {
    //                method: 'POST',
    //                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
    //                transformRequest: function (data, headersGetter) {
    //                    var str = [];
    //                    for (var d in data)
    //                        str.push(encodeURIComponent(d) + "=" + encodeURIComponent(data[d]));
    //                    return str.join("&");
    //                }
    //            }
    //        })
    //    return res;
    //}

    //this.search = function (email) {
    //    var res = $resource('/api/Vendors/Search/:email', { email: "@Vendor" });
    //    return res;
    //}
    //this.login = function (userlogin) {
    //    var resp = $http({
    //        url: "/TOKEN",
    //        method: "POST",
    //        data: $.param({ grant_type: 'password', username: userlogin.username, password: userlogin.password }),
    //        headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
    //    });
    //    return resp;
    //}
    //var _authentication = {
    //    isAuth: false,
    //    userName: ""
    //};



   

});