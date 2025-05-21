// ==============================
// Obtener parámetros desde la URL
// ==============================
const params = new URLSearchParams(window.location.search);
const paymentMethod = params.get('payment-method'); // Método de pago seleccionado
const itemName = params.get('item-name');           // Nombre del producto
const amount = params.get('amount');                // Monto total

// ==============================
// Asignar elementos del DOM
// ==============================
const itemNameElement = document.getElementById('item-name');
const amountElement = document.getElementById('amount');
const paymentIcon = document.getElementById('payment-icon');
const selectedMethodElement = document.getElementById('selected-method');
const cardForm = document.getElementById('card-payment-form');
const confirmBtn = document.getElementById('confirm-button');
const btnPayWithCard = document.querySelector('.btnPay');
const btnPurchase = document.querySelector('.btn-purchase');

// Mostrar información del producto y método de pago
itemNameElement.textContent = itemName;
amountElement.textContent = amount;

// ==============================
// Mostrar ícono y texto según el método de pago
// ==============================

const paymentMethods = {
    'credit-debit': {
      icon: 'https://upload.wikimedia.org/wikipedia/commons/a/a4/Mastercard_2019_logo.svg',
      label: 'Tarjeta de Crédito o Débito'
    },
    'paypal': {
      icon: 'https://upload.wikimedia.org/wikipedia/commons/b/b5/PayPal.svg',
      label: 'PayPal'
    }
}

if (paymentMethods[paymentMethod]) {
    paymentIcon.src = paymentMethods[paymentMethod].icon;
    selectedMethodElement.textContent = paymentMethods[paymentMethod].label;
}

// ==============================
// Evento principal: Confirmar compra
// ==============================
btnPurchase?.addEventListener("click", completePurchase);

function completePurchase() {
    if (paymentMethod === 'credit-debit') {
        // Mostrar el formulario de tarjeta y ocultar el botón de confirmar
        cardForm.style.display = 'block';
        confirmBtn.style.display = 'none';
    
        // Escuchar evento del botón de pago con tarjeta
        btnPayWithCard?.addEventListener("click", handleCardPayment);
  
    } else if (paymentMethod === 'paypal') {
        // Redirigir a PayPal con los datos
        const paypalUrl = `https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=darlynolivo15@gmail.com&item_name=${encodeURIComponent(itemName)}&amount=${encodeURIComponent(amount)}&currency_code=USD`;
        window.location.href = paypalUrl;
    }
}

// ==============================
// Manejar el pago con tarjeta
// ==============================
function handleCardPayment(event) {
    event.preventDefault(); // Evita que se envíe el formulario por defecto
  
    // Aquí podrías validar los campos antes de proceder
  
    swal("¡Pago completado con tarjeta!", "Gracias por su compra", "success")
      .then(() => {
        // TODO: Generar factura PDF
        // window.location.href = "../Pagos/factura.html";
    });
}

// ==============================
// Funciones de navegación
// ==============================
function goBack() {
    window.history.back(); // Volver a la página anterior
}

function backToCart() {
    window.location.href = '../Carrito/Carrito.html'; // Volver al carrito
}