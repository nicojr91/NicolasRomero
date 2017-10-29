$(function () {

    $(".item").on("click", ".add-cart", function (e) {
        var productId = $(e.currentTarget).parent().parent().data("id");
        //guarda cookie con los datos del usuario*productID,cantidad-...
        if ($.cookie("newbuy") === undefined) {
            //crea cookie
            $.cookie("newbuy", "1*"+productId+",0", { expire: 30 });
        } else {
            //agrega nuevo prod
            var string = $.cookie("newbuy").split('*')[1].split("-");
            for (var x in string) {
                if (string[x].split(',')[0] == productId) {
                    console.log("asdasdasd");
                }
            }
            var cookie = $.cookie("newbuy");
            cookie += "-" + productId + ",0";
            $.cookie("newbuy", cookie, { expire: 30 });
        }

        
    });

});