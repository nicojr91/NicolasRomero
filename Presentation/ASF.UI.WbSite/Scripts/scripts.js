$(function () {

    $('#ModalCart').on('show.bs.modal', function (e) {
        var string = $.cookie("newbuy");
        var json = {};

        if (string === undefined) {

        } else {
            var string = $.cookie("newbuy").split('*')[1].split("-");
            
            for (var x in string) {
                var pq = string[x].split(',');
                json[x] = { "product": pq[0], "quantity": pq[1] };
            }
        }
        $.get("http://localhost:24005/cart/getcart", json, function (data) {
            console.log(data);
        });
    })

});