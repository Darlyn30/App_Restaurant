const btnBackCart = document.querySelector(".btn-back-to-cart");
const paymentContainer = document.querySelector(".payment-options");
const URL_PAYMENT = "https://localhost:7075/api/Payment";
const btnGoToConfirmPage = document.querySelector(".btn-primary");
document.addEventListener("DOMContentLoaded", async () => {
    await renderPayment(URL_PAYMENT);
})

btnGoToConfirmPage.addEventListener("click", GoToConfirmationPage);

async function renderPayment(url){

    await fetch(url, {
        method: "GET",
        headers : {
            //"Authorization" : "Bearer TOKEN", // <-necesitaremos auth en la fase final
            "Content-Type" : "application/json"
        }
    })
    .then(async res => await res.json())
    .then(data => {
        console.log("hola");
        data.forEach(method => {
            const label = document.createElement("label");
            label.className = "payment-option";
            label.innerHTML = `
                <input type="radio" id="${method.id}" name="payment-method" value="${method.id}" required />
                <img src="${method.imgUrl}" alt="${method.name}" class="payment-icon" />
                <span>${method.name}</span>
            `;
            paymentContainer.appendChild(label);
        })
    })
}

btnBackCart.addEventListener("click", backToCart);

function backToCart(){
    window.location.href = '../carrito/Carrito.html';
}

function GoToConfirmationPage(){
    const selected = document.querySelector('input[name="payment-method"]:checked');
    if (!selected) {
        alert("Selecciona un método de pago");
        return;
    }

    console.log(selected);
    console.log(`pagaras con el metodo: ${selected.value}`);
    // sessionStorage.setItem("selectedPaymentMethod", selected.value);
    //mas que el Id le pasare un objeto con la cuenta(monto) y el metodo de pago
    //window.location.href = "../Confirmation/Confirmation.html";
}