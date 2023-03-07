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

    console.log(SizeId)
    console.log(size)
    $.ajax(
        {
            type: "POST",
            url: aHref,
            data: {
                SizeId : SizeId
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