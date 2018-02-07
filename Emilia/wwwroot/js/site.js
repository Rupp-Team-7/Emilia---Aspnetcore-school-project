
// Write your JavaScript code.
$(document).ready(function () {

    $("#btnNav").click(function (e) {
        $("#dash-side").toggleClass("active");

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
                $("#photo_container").empty();
                for (var i = 0; i < path.length - 1; i++) {
                    $("#photo_container").prepend(
                        $("<img>").addClass("inline_box").attr("src", "/" + path[i])
                    );
                }
                $("#photo-label").text("Photo:" + (path.length - 1));
                $("#PhotoPath").val(model.photoPath);
            }
        })
        .fail(function () {
            alert("Unable to upload phtoto");
        });


        function DoBeforeSend() {
            $("#photo_container").empty();
            $("#photo_container").append($("<span></span>").addClass("fa fa-circle-o-notch fa-3x fa-fw fa-spin text-info"))
        }

    });

    $("#btn-product-create-clear").click(function () {
        $("#photo_container").empty();
        $("#photo-label").text("Photo: 0");
        for (var i = 0; i < 4; i++) {
            $("#photo_container").prepend(
                $("<div></div>").addClass("inline_box")
            );
        }
    });
});

