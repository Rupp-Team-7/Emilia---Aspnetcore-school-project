


// Write your JavaScript code.
$(document).ready(function(){

    $("#btnNav").click(function(e){
        $("#dash-side").toggleClass("active");

    });

    $("#form-post-logo").submit(function(e){
        e.preventDefault();
        var form = $(this)[0];

        var data = new FormData();
        data.append("Files", form.Files.files[0]);
        
        $.ajax({
            method: "POST",
            url: "/ShopManagement/ChangeLogo",
            data:data,
            processData:false,
            contentType:false
        }).done(function(model, code){
          //  alert(code + ":" + model.source)
            $("#shop_logo").attr("src", model.source);
            $("#LogoPath").attr("value", model.source);
        });

    });
});

