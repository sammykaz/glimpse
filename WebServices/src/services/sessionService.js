'use strict';

app.factory('sessionService', ['', function(){
    return{
        set: function(key,value){
            sessionStorage.setItem(key,value);
        },
        get: function(){
            sessionStorage.getItem(key);
        },
        destroy: function(key,value){
            sessionStorage.setItem(key,value);
        }
    };
}])
