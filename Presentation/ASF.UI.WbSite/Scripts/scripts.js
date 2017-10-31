$(function () {

    $('#ModalCart').on('show.bs.modal', function (e) {
        var string = $.cookie("newbuy");
        var json = [];

        if (string === undefined) {

        } else {
            if ($.cookie("newbuy").indexOf("*") != -1) {
                var string = $.cookie("newbuy").split('*')[1].split("-");

                for (var x in string) {
                    var pq = string[x].split(',');
                    json[x] = { "product": pq[0], "quantity": pq[1] };
                }
            }
        }

        $.get("http://localhost:24005/cart/getcart", JSON.stringify(json), function (data) {
            $("#table-cart").find("tbody").children().remove();
            $("#table-cart").find("tbody").append(data);
        });
    });

    $("#ModalCart").on("hide.bs.modal", function (e) {
        //Guardar cookie al cerrar modal
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
    });

    $("#ModalCart").on("click", ".remove-item", function (e) {
        $(e.currentTarget).parents("tr").remove();
    });
});