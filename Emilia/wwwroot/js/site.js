
// Write your JavaScript code.
$(document).ready(function () {

    $("#btnNav").click(function (e) {
        $("#dash-side").toggleClass("active");

        if($("#dash-side").hasClass("active"))
            document.cookie = "dash_active=active;path=/;";
        else
            document.cookie = "dash_active=null;path=/;";
        
           // console.log(document.cookie);
    });

    $("button[data-target='#logo_uploader']").click(function (sender) {
        $("#form-post-logo").attr("data-route", $(this).attr("data-change"));
    });

    $("#form-post-logo").submit(function (e) {
        $("#logo_uploader").modal("hide");
        e.preventDefault();
        var route = $("#form-post-logo").attr("data-route");
        var form = $(this)[0];
        var data = new FormData();
        data.append("Files", form.Files.files[0]);

        $.ajax({
            method: "POST",
            url: "/ShopManagement/ChangeLogo",
            data: data,
            processData: false,
            contentType: false,
            beforeSend: DoBeforeSend
        }).done(function (model, code) {

            if (route == "ChangeLogo") {
                OnChangeLogo(model);
            }
            else if (route == "ChangeBackground") {
                OnChangeBackground(model);

            }

        }).fail(function () {
            alert("Unable to upload phtoto");
            $("#" + route + "_Spin").removeClass("fa-spin");
        });

        function OnChangeLogo(model) {
            $("#shop_logo").attr("src", model.source);
            $("#LogoPath").attr("value", model.source);
            $("#" + route + "_Spin").removeClass("fa-spin");
        }

        function OnChangeBackground(model) {
            $("#shop_cover").attr("src", model.source);
            $("#BackgroundPath").attr("value", model.source);
            $("#" + route + "_Spin").removeClass("fa-spin");
        }

        function DoBeforeSend() {
            $("#" + route + "_Spin").addClass("fa-spin");
        }

    });

    $("#form_product_photo_uploader").submit(function (e) {
        e.preventDefault();

        var form = $(this)[0];
        var data = new FormData(form);

        $.ajax({
            method: "POST",
            url: "/Upload/Photos",
            data: data,
            processData: false,
            contentType: false,
            beforeSend: DoBeforeSend
        })
        .done(function (model, code) {
            if (code === "success") {
                var path = (model.photoPath + "").split(";");
                $("#newPhoto").empty();
                for (var i = 0; i < path.length - 1; i++) {
                    $("#newPhoto").prepend(
                        $("<img>").addClass("inline_box").attr("src", "/" + path[i]).attr("data-id", "-1")
                    );
                }
                $("#photo-label").text("Photo:" + (path.length - 1));
                var newValue = $("#photo-data-holder").text() + model.photoPath;
                $("#photo-data-holder").text(newValue);
            }
        })
        .fail(function () {
            alert("Unable to upload phtoto");
        });


        function DoBeforeSend() {
            $("#newPhoto").empty();
            $("#newPhoto").append($("<span></span>").addClass("fa fa-circle-o-notch fa-3x fa-fw fa-spin text-info"))
        }

    });

    $("#btn-product-create-clear").click(function () {
        $("#newPhoto").empty();
        $("#photo-label").text("Photo: 0");
        for (var i = 0; i < 4; i++) {
            $("#newPhoto").prepend(
                $("<div></div>").addClass("inline_box")
            );
        }
    });

    $("#form-create-product").submit(function(){
        $("#PhotoPath").val($("#photo-data-holder").text());
    });

    $("button._remover").click(function(e){
        var imgId = parseInt($(this).attr("data-remove-img"));
        var img = $("img[data-id='" + imgId + "']");

        var path = $("#photo-data-holder").text();
        path = path.split(";");
        console.log("path[]" + path);

        img.remove();
        $(this).remove();

        var newPath = "";
        $("img[data-id]").each(function(i, item){
           newPath += ($(item).attr("src") + ";").replace("/", ""); 
        });


        $("#photo-data-holder").text(newPath);
    });

    $(".btnUnpub").click(function(e){
        e.preventDefault();
        var btn = $(this);
        url = btn.attr("href");
        $.ajax(
            {
                method: "POST",
                url: url,
                data: { published: false },
                processData: true
            }
        )
        .done(function(code, model){

            $(btn).toggleClass("d-none");
            $(".btnPub[data-order=" + $(btn).attr('data-order') + "]").toggleClass("d-none");
        });
    });

    $(".btnPub").click(function(e){
        e.preventDefault();
        var btn = $(this);
        url = $(this).attr("href");
    
        $.ajax(
            {
                method: "POST",
                url: url,
                data: { published: true },
                processData:true
            }
        )
        .done(function(code, model){

            $(btn).toggleClass("d-none");
            $(".btnUnpub[data-order=" + $(btn).attr('data-order') + "]").toggleClass("d-none");
        });
    });
    $("button").click(function () {
        //alert("#" + $(this).text());
        if ($(this).text() == 'Delivery') {
            $(this).attr('disabled', 'disabled');
            $("#" + $(this).attr('id')).text("Deliveried");
            $("#" + $(this).attr('id')).removeClass("btn-primary");
            $("#" + $(this).attr('id')).addClass("btn-dark");
            url = $(this).attr("href");
        $.ajax(
            {
                method: "POST",
                url: url,
                data: { id: $(this).attr('id'), ord: true },
                processData: true
            });
        }
    });


    $("#txtQty").change(function () {
        var unitprice = $('#txtUnitPrice').text();
        var qty = $('#txtQty').val();
        var total = double.parseDouble(unitprice) * double.parseDouble(qty);
        $("#txtTotalPrice").text(total);
    });

    $("#orderingproduct").click(function (e) {
        e.preventDefault();
        var url = $(this).attr("href");
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = d.getFullYear() + '/' +
            (month < 10 ? '0' : '') + month + '/' +
            (day < 10 ? '0' : '') + day;
        var s = confirm("Are You Sure!");
        if (s = true) {
            $.ajax(
                {
                    method: "POST",
                    url: "/Home/Product",
                    data: { ProductId: $('#txtProId').val(), Quanity: $('#txtQty').val(), TotalPrice: $('#txtTotalPrice').text(), orderDate: output, CustomerId: "1", SellerId: $('#txtSellerId').val(), Delivery: "False" },
                    processData: true
                }).done(function (data, code) {
                   // alert(code);
                    if (code === "success") {
                        window.location.href = "/Ordering/History";
                    } else if (code === "nocontent") {
                        window.location.href = "/account/login";
                    }
                });
        }

        //var dataObject = JSON.stringify({
        //    ProductId: $('#txtProId').val(),
        //    Quanity: $('#txtQty').val(),
        //    TotalPrice: $('#txtTotalPrice').text(),
        //    orderDate: output,
        //    CustomerId: "1",
        //    SellerId :$('#txtSellerId').val(),
        //    Delivery: "False"
        //});
        //alert(dataObject);
        
    });
});


