const btnBackCart = document.querySelector(".btn-back-to-cart");

btnBackCart.addEventListener("click", backToCart);

function backToCart(){
    window.location.href = '../carrito/Carrito.html';
}