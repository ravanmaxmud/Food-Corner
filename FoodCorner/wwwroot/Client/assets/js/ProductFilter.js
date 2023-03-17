$(document).on("click", ".sort-product", function (e) {
    e.preventDefault();


    let aHref = e.target.href;

    $.ajax(
        {
            type: "GET",
            url: aHref,
            success: function (response) {
                $('.shopPageProduct').html(response);
            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})


$(document).on("click", '.select-catagory', function (e) {
    e.preventDefault();
    let aHref = e.target.href;
    console.log(aHref)


    $.ajax(
        {
            type: "GET",
            url: aHref,

            success: function (response) {
                console.log(response)
                $('.shopPageProduct').html(response);

            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})

$(document).on("click", '.select-tag', function (e) {
    e.preventDefault();
    let aHref = e.target.href;
    console.log(aHref)


    $.ajax(
        {
            type: "GET",
            url: aHref,

            success: function (response) {
                console.log(response)
                $('.shopPageProduct').html(response);

            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})

$(".searchproductPrice").change(function (e) {
    e.preventDefault();

    let minPrice = e.target.parentElement.firstElementChild.children[0].innerText.slice(1);
    let MinPrice = parseInt(minPrice);
    console.log(MinPrice);

    let maxPrice = e.target.parentElement.firstElementChild.children[1].innerText.slice(1);
    let MaxPrice = parseInt(maxPrice);

    let aHref = document.querySelector(".shoppage-url").href;

    $.ajax(
        {
            url: aHref,

            data: {
                MinPrice: MinPrice,
                MaxPrice: MaxPrice
            },

            success: function (response) {
                $('.shopPageProduct').html(response);


            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });


});

$(".searchproductPrice2").change(function (e) {
    e.preventDefault();

    let minPrice = e.target.parentElement.firstElementChild.children[0].innerText.slice(1);
    let MinPrice = parseInt(minPrice);
    console.log(MinPrice);

    let maxPrice = e.target.parentElement.firstElementChild.children[1].innerText.slice(1);
    let MaxPrice = parseInt(maxPrice);

    let aHref = document.querySelector(".shoppage-url").href;

    $.ajax(
        {
            url: aHref,

            data: {
                MinPrice: MinPrice,
                MaxPrice: MaxPrice
            },

            success: function (response) {
                $('.shopPageProduct').html(response);


            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });


});
