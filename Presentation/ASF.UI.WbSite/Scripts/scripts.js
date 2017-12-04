$(function () {

    $('#ModalCart').on('show.bs.modal', function (e) {

        var cookie = readCookie();

        if (cookie !== undefined) {
            $.get("/cart/getcart", JSON.stringify(cookie), function (data) {
                $("#table-cart").find("tbody").children().remove();
                $("#table-cart").find("tbody").append(data);

                calculateTotal();
            });
        }
        
    });

    $("#ModalCart").on("hide.bs.modal", function (e) {
        //Guardar cookie al cerrar modal
        storeCookie();
    });

    $("#ModalCart").on("click", ".remove-item", function (e) {

        $(e.currentTarget).parents("tr").remove();
        calculateTotal();
        storeCookie();
    });

    $("#ModalCart").on("keyup", ".quantity", function () {
        calculateTotal();
    });

    $('#ModalCart').on('click', '.buy', function (e) {
        
        var order = {};

        order.clientId = $("#clientId").val();
        order.lines = [];

        $("#table-cart").find("tr").each(function (i, e) {
            var line = {};
            if ($(this).find("th").length < 1) {
                line.product = $($(this).children()[0]).data("id");
                line.quantity = $($(this).children()[1]).children().val();

                order.lines.push(line);
            }
        });

        $.post("/order/create", JSON.stringify(order), function (data) {
            if (data.status != "error") {
                $.removeCookie("newbuy");
                window.location.href = data.url;
            }
        });

        

    });

    $(".scroll").click(function (event) {
        event.preventDefault();
        $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 1000);
    });
    
});

function readCookie()
{
    var cookie;
    var string = $.cookie("newbuy");

    if (string !== undefined) {
        if ($.cookie("newbuy").indexOf("*") != -1) {
            cookie = {};
            cookie.client = $.cookie("newbuy").split('*')[0];
            cookie.items = [];
            var string = $.cookie("newbuy").split('*')[1].split("-");

            for (var x in string) {
                var pq = string[x].split(',');
                cookie.items[x] = { "product": pq[0], "quantity": pq[1] };
            }
        }
    }

    return cookie;
}

function storeCookie()
{
    if ($.cookie("newbuy") !== undefined && $.cookie("newbuy").indexOf("*") != -1) {
        var string = $("#clientId").val() + "*";
        $("#table-cart").find("tr").each(function (i, e) {
            if ($(this).find("th").length < 1) {
                string += $($(this).children()[0]).data("id") + ",";
                string += $($(this).children()[1]).children().val() + "-";
            }
        });
        string = string.substring(0, string.length - 1);

        $.cookie("newbuy", string, { expire: 30 });

    }
}

function calculateTotal()
{
    var total = 0;
    $("#table-cart").find("tr").each(function (i, e) {
        if ($(this).find("th").length < 1) {
            var quantity = $($(this).children()[1]).children().val();
            var price = $($(this).children()[2]).text();
            price = price.substring(1, price.length);

            total += price * quantity;
        }
    });

    $("span#total").text(total);
}