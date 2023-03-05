let btns = document.querySelectorAll(".add-product-to-basket-btn")

btns.forEach(x => x.addEventListener("click", function (e) {
    e.preventDefault()
    fetch(e.target.parentElement.href)
        .then(response => response.text())
        .then(data => {
            $('.cart-block').html(data);
        })
}))


$(document).on("click", ".add-product-to-basket-modal-btn", function (e) {
    e.preventDefault();

    fetch(e.target.href)
        .then(response => response.text())
        .then(data => {
            $('.cart-block').html(data);
        })
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