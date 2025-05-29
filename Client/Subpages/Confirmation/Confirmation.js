const methodId = sessionStorage.getItem("selectedPaymentMethod") || ""; //aqui lo que guardamos durante la sesion es solo
//el id del tipo de pago
const TOKEN = JSON.parse(localStorage.getItem("token")) || "";
const URL_PAYMENT_ID = `https://localhost:7075/api/Payment/${methodId}`; // <- le pasamos el id a la URL
//traemos el metodo de pago a traves del ID
const USER = JSON.parse(localStorage.getItem("account")) || "";

const paymentSpan = document.getElementById("selected-method");
const paymentIcon = document.getElementById("payment-icon");


const btnBack = document.getElementById("btn-back");

const btnPayNow = document.getElementById("confirm-button");


const cardPayContainer = document.getElementById("card-payment-form");
const paymentContainer = document.querySelector(".payment-container");

document.addEventListener("DOMContentLoaded", async () => {
    await methodPay(URL_PAYMENT_ID);
    btnPayNow.addEventListener("click", payByMethod(methodId, btnPayNow, cardPayContainer));
})

btnBack.addEventListener("click", backPaymentSelectMenu);

async function methodPay(url){
    await fetch(url, {
        method: "GET",
        headers : {
            //"Authorization" : `Bearer ${TOKEN}`,
            "Content-Type" : "application/json"
        }
    })
    .then(async res => await res.json())
    .then(data => {
        console.log(data);
        paymentSpan.innerHTML = data.name;
        paymentIcon.src = data.imgUrl;
    })
}

function backPaymentSelectMenu() {
    sessionStorage.removeItem("selectedPaymentMethod");
    window.location.href = "../Pagos/Pagos.html";
}

function payByMethod(id, btnPayNow, cardPayContainer){
    if(id == 1) {
        //si es tarjeta
        btnPayNow.addEventListener("click", () => {
            cardPayContainer.style.display = "block";
            btnPayNow.style.display = "none";

        })
        cardPayContainer.addEventListener("submit", (e) => {
            e.preventDefault();
            payByCard();
        })
    } else {

        btnPayNow.addEventListener("click", () => {
            payByPaypal();
        })
    }

}

function payByPaypal() {
    // Alerta de procesamiento
    Swal.fire({
        title: 'Procesando pago...',
        html: 'Por favor espera...',
        timer: 3000,
        timerProgressBar: true,
        didOpen: () => {
            Swal.showLoading();
        },
        willClose: () => {
            // Alerta de éxito
            Swal.fire({
            icon: 'success',
            title: '¡Pagado!',
            text: 'Tu pago se ha realizado correctamente.'
            });
        }
    });
}

function payByCard(){
    paymentContainer.style.display = "block";
    setTimeout(() => {
        document.getElementById('spinner').style.display = 'none';
        document.getElementById('status').style.display = 'none';
        // document.getElementById('paidText').style.display = 'block';
        paymentContainer.style.display = "none";
        Swal.fire({
          icon: 'success',
          title: '¡Pago exitoso!',
          text: 'Gracias por tu compra. Tu pedido será procesado en breve.'
        })
    }, 4000);
}