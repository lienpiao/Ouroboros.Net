var ouroboros = {
    //Guid
    getGuid: function () {
        var s4 = function () {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        };
        return s4() + s4() + s4() + "-" + s4();
    },
}