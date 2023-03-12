AOS.init({
  duration: 1500,

})


let log = document.querySelector('.user-icon');
$('.user-icon-btn').click(function(){
    log.classList.toggle('active')
})

let searchModal = document.querySelector('.search-modal');
$('.search-btn-index').click(function(){
    searchModal.classList.toggle('active')
})
$('.search-close').click(function(){
    searchModal.classList.toggle('active')
})

////////////////////////////////////////////

$('.js-show-cart').on('click',function(){
  $('.js-panel-cart').addClass('show-header-cart');
});

$('.js-hide-cart').on('click',function(){
  $('.js-panel-cart').removeClass('show-header-cart');
});


const menuToggleBtn = document.querySelector(".menu-toggle-btn");
const primaryNav = document.querySelector(".primary-navigation");

menuToggleBtn.addEventListener('click',() => {
  let isNavOpen = menuToggleBtn.getAttribute('aria-expanded') === 'true';
  isNavOpen ? primaryMenuClose() : primaryMenuOpen();
})

function primaryMenuOpen(){
  menuToggleBtn.setAttribute('aria-expanded','true');
  primaryNav.setAttribute('data-state','opened');
}
function primaryMenuClose(){
  menuToggleBtn.setAttribute('aria-expanded','false');
  primaryNav.setAttribute('data-state','closing');
  primaryNav.addEventListener('animationend',() => {
    primaryNav.setAttribute('data-state','closed');
  },{once:true})
}

/////////////////////////////////////////////////////



$('.responsive').slick({
  dots: false,
  infinite: true,
  speed: 1100,
  slidesToShow: 4,
  slidesToScroll: 2,
  autoplay: true,
  autoplaySpeed: 1200,
  nextArrow: '<span class = "next"><i class="fa-solid fa-arrow-right"></i></span>',
  prevArrow: '<span class = "prew"><i class="fa-solid fa-arrow-left"></i></span>',
  responsive: [{
          breakpoint: 1024,
          settings: {
              slidesToShow: 3,
              slidesToScroll: 3,
              infinite: true,
              dots: true
          }
      },
      {
          breakpoint: 600,
          settings: {
              slidesToShow: 1,
              slidesToScroll: 1
          }
      },
      {
          breakpoint: 480,
          settings: {
              slidesToShow: 1,
              slidesToScroll: 1,
              prevArrow: '<span class = "d-none next"><i class="fa-solid fa-arrow-right"></i></span>',
              nextArrow: '<span class = "d-none prew"><i class="fa-solid fa-arrow-left"></i></span>'
          }
      }
  ]
});

$('.smresponsive').slick({
  dots: false,
  infinite: true,
  speed: 1100,
  slidesToShow: 3,
  slidesToScroll: 1,
  autoplay: true,
  autoplaySpeed: 1200,
  nextArrow: '<span class = "next"><i class="fa-solid fa-arrow-right"></i></span>',
  prevArrow: '<span class = "prew"><i class="fa-solid fa-arrow-left"></i></span>',
  responsive: [{
          breakpoint: 1024,
          settings: {
              slidesToShow: 3,
              slidesToScroll: 3,
              infinite: true,
              dots: true
          }
      },
      {
          breakpoint: 600,
          settings: {
              slidesToShow: 2,
              slidesToScroll: 2
          }
      },
      {
          breakpoint: 480,
          settings: {
              slidesToShow: 1,
              slidesToScroll: 1,
              prevArrow: '<span class = "d-none next"><i class="fa-solid fa-arrow-right"></i></span>',
              nextArrow: '<span class = "d-none prew"><i class="fa-solid fa-arrow-left"></i></span>'
          }
      }
      // You can unslick at a given breakpoint now by adding:
      // settings: "unslick"
      // instead of a settings object
  ]
});

$('.responsive-explore').slick({
  rows: 2,
  dots: false,
  infinite: true,
  speed: 1100,
  slidesToShow: 4,
  slidesToScroll: 2,
  autoplay: true,
  autoplaySpeed: 1200,
  nextArrow: '<span class = "next"><i class="fa-solid fa-arrow-right"></i></span>',
  prevArrow: '<span class = "prew"><i class="fa-solid fa-arrow-left"></i></span>',
  responsive: [{
          breakpoint: 1024,
          settings: {
              slidesToShow: 3,
              slidesToScroll: 3,
              infinite: true,
              dots: true
          }
      },
      {
          breakpoint: 600,
          settings: {
              slidesToShow: 2,
              slidesToScroll: 2
          }
      },
      {
          breakpoint: 480,
          settings: {
              slidesToShow: 1,
              slidesToScroll: 1,
              prevArrow: '<span class = "d-none next"><i class="fa-solid fa-arrow-right"></i></span>',
              nextArrow: '<span class = "d-none prew"><i class="fa-solid fa-arrow-left"></i></span>'
          }
      }
      // You can unslick at a given breakpoint now by adding:
      // settings: "unslick"
      // instead of a settings object
  ]
});

$('.responsivee').slick({
  centerMode: true,
  rows: 1,
  dots: false,
  infinite: true,
  centerPadding: '30px',
  speed: 1100,
  slidesToShow: 3,
  slidesToScroll: 1,
  autoplay: true,
  autoplaySpeed: 1200,
  nextArrow: '<span class = "next"><i class="fa-solid fa-arrow-right"></i></span>',
  prevArrow: '<span class = "prew"><i class="fa-solid fa-arrow-left"></i></span>',
  responsive: [{
          breakpoint: 1024,
          settings: {
              slidesToShow: 3,
              slidesToScroll: 3,
              infinite: true,
              dots: true
          }
      },
      {
          breakpoint: 600,
          settings: {
              slidesToShow: 2,
              slidesToScroll: 2
          }
      },
      {
          breakpoint: 480,
          settings: {
              slidesToShow: 1,
              slidesToScroll: 1,
              prevArrow: '<span class = "d-none next"><i class="fa-solid fa-arrow-right"></i></span>',
              nextArrow: '<span class = "d-none prew"><i class="fa-solid fa-arrow-left"></i></span>'
          }
      }
      // You can unslick at a given breakpoint now by adding:
      // settings: "unslick"
      // instead of a settings object
  ]
});



$('.js-show-modal1').on('click',function(e){
  e.preventDefault();
    $('.js-modal1').addClass('show-modal1');
    var url = e.target.parentElement.href;


    //$.ajax(
    //    {
    //        type: "GET",
    //        url: url,
    //        success: function (response) {

    //            console.log(response)
    //            $('.product-details-modal').html(response);


    //        },
    //        error: function (err) {
    //            $(".product-details-modal").html(err.responseText);

    //        }

    //    });

    fetch(url)
        .then(response => response.text())
        .then(data => {
            console.log(data);
            $('.product-details-modal').html(data);

        })

});

$('.js-hide-modal1').on('click',function(){
  $('.js-modal1').removeClass('show-modal1');
});



$('.myaccount-tab-menu a').filter(function () {
    return this.href === location.href;
}).addClass('active');


