let btns = document.querySelectorAll(".add-product-to-basket-btn")

btns.forEach(x => x.addEventListener("click", function (e) {
    e.preventDefault()

     let aHref = e.target.parentElement.href;

    
    document.getElementById('toaster').style.opacity = '1'
    setTimeout(() => {
        document.getElementById('toaster').style.opacity = '0'
    }, 1000);

    $.ajax(
        {
            type: "POST",
            url: aHref,
            success: function (response) {
                console.log(response)
                $('.cart-block').html(response);


            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });
    //fetch(e.target.parentElement.href,{
    //    method: "POST"
    //})

    //    .then(response => response.text())
    //    .then(data => {
    //        $('.cart-block').html(data);
    //    })
}))


$(document).on("click", ".add-product-to-basket-modal-btn", function (e) {
    e.preventDefault();

    document.getElementById('toaster').style.opacity = '1'
    setTimeout(() => {
        document.getElementById('toaster').style.opacity = '0'
    }, 1000);

    let aHref = e.target.href;
    let size = e.target.parentElement.parentElement.parentElement.firstElementChild.children[1].children[0].children[0];
    let SizeId = size.value;


    let quantity = e.target.parentElement.parentElement.firstElementChild.children[0].children[1];
    let Quantity = quantity.value;
    console.log(quantity)

    $.ajax(
        {
            type: "POST",
            url: aHref,
            data: {
                SizeId: SizeId,
                Quantity: Quantity
            },
            success: function (response) {

                $('.cart-block').html(response);


            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });


    //fetch(e.target.href, {
    //    method:"POST"
    //})
    //    .then(response => response.text())
    //    .then(data => {
    //        $('.cart-block').html(data);
    //    })
})


$(document).on("click", ".add-product-to-basket-single-btn", function (e) {
    e.preventDefault();

    document.getElementById('toaster').style.opacity = '1'
    setTimeout(() => {
        document.getElementById('toaster').style.opacity = '0'
    }, 1000);

    let aHref = e.target.href;
    let size = e.target.parentElement.parentElement.parentElement.firstElementChild.children[1].children[0].children[0];
    let SizeId = size.value;


    let quantity = e.target.parentElement.parentElement.firstElementChild.children[0].children[1];
    let Quantity = quantity.value;
    console.log(quantity)

    $.ajax(
        {
            type: "POST",
            url: aHref,
            data: {
                SizeId: SizeId,
                Quantity: Quantity
            },
            success: function (response) {

                $('.cart-block').html(response);


            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });


    //fetch(e.target.href, {
    //    method:"POST"
    //})
    //    .then(response => response.text())
    //    .then(data => {
    //        $('.cart-block').html(data);
    //    })
})

$(document).on("click", ".add-basket-to-wish", function (e) {
    e.preventDefault();

    let aHref = e.target.href;

    document.getElementById('toaster').style.opacity = '1'
    setTimeout(() => {
        document.getElementById('toaster').style.opacity = '0'
    }, 1000);


    $.ajax(
        {
            type: "POST",
            url: aHref,
            success: function (response) {

                $('.cart-block').html(response);


            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });


    //fetch(e.target.href, {
    //    method:"POST"
    //})
    //    .then(response => response.text())
    //    .then(data => {
    //        $('.cart-block').html(data);
    //    })
})




$(document).on("click", ".remove-product-to-basket-btn", function (e) {
    e.preventDefault();

    console.log(e.target.parentElement.href)
    fetch(e.target.parentElement.href)
        .then(response => response.text())
        .then(data => {
            console.log(data)
            $('.cart-block').html(data);
        })
})

$(document).on("click", ".remove-product-to-basket-page", function (e) {
    e.preventDefault();

    console.log(e.target.parentElement.href)
    fetch(e.target.parentElement.href)
        .then(response => response.text())
        .then(data => {
            $('.basket-block').html(data);


            fetch(e.target.parentElement.nextElementSibling.href)
                .then(response => response.text())
                .then(data => {
                    console.log(data)
                    $('.cart-block').html(data);
                })
        })
})


$(document).on("click", ".plus-btn", function (e) {
    e.preventDefault();

    var aHref = e.target.href;

    let size = e.target.nextElementSibling.nextElementSibling;
    let SizeId = size.value;

    let updateHref = e.target.nextElementSibling.href;
    $.ajax(
        {
            type: "POST",
            url: aHref,
            data: {
                SizeId: SizeId,
            },
            success: function (response) {

                $('.basket-block').html(response);

                $.ajax(
                    {
                        type: "GET",
                        url: updateHref,
                        success: function (response) {

                            $('.cart-block').html(response);


                        },
                        error: function (err) {
                            $(".product-details-modal").html(err.responseText);

                        }

                    });

            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });
    
})


$(document).on("click", ".minus-btn", function (e) {
    e.preventDefault();

    var aHref = e.target.href;

    let size = e.target.nextElementSibling.nextElementSibling.nextElementSibling.nextElementSibling;
    let SizeId = size.value;

    let updateHref = e.target.nextElementSibling.nextElementSibling.nextElementSibling.href;
    $.ajax(
        {
            type: "GET",
            url: aHref,
            data: {
                SizeId: SizeId,
            },
            success: function (response) {

                $('.basket-block').html(response);

                $.ajax(
                    {
                        type: "GET",
                        url: updateHref,
                        success: function (response) {

                            $('.cart-block').html(response);


                        },
                        error: function (err) {
                            $(".product-details-modal").html(err.responseText);

                        }

                    });

            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})


$(document).on("click", '.select-blog-category', function (e) {
    e.preventDefault();
    let aHref = e.target.href;
    let category = e.target.previousElementSibling;
    let CategoryId = category.value;


    console.log(CategoryId)

    console.log(aHref)



    $.ajax(
        {
            type: "GET",
            url: aHref,

            data: {
                CategoryId: CategoryId
            },

            success: function (response) {
                console.log(response)
                $('.blog-pro').html(response);

            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})


$(document).on("click", '.select-blog-tag', function (e) {
    e.preventDefault();
    let aHref = e.target.href;
    let tag = e.target.previousElementSibling
    let TagId = tag.value;


    console.log(TagId)

    console.log(aHref)



    $.ajax(
        {
            type: "GET",
            url: aHref,

            data: {
                TagId: TagId
            },

            success: function (response) {
                console.log(response)
                $('.blog-pro').html(response);

            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})



$(document).on("click", '.add-wishlist', function (e) {
    e.preventDefault();

    let aHref = e.target.parentElement.href;
    console.log(aHref);


    document.getElementById('wishToaster').style.opacity = '1'
    setTimeout(() => {
        document.getElementById('wishToaster').style.opacity = '0'
    }, 1000);

    $.ajax(
        {
            type: "GET",
            url: aHref,

            success: function (response) {

            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})