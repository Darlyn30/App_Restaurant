// ==============================
// Obtener parámetros de la URL
// ==============================

const params = new URLSearchParams(window.location.search);
const paymentMethod = params.get('payment-method'); //obtener el metodo de pago
const itemName = params.get('item-name'); //obtener el nombre del producto
const amount = params.get('amount'); //obtener el precio del producto

//asignar los elementos del HTML
document.getElementById('item-name').textContent = itemName;
document.getElementById('amount').textContent = amount;

// ==============================
// Referencias a elementos del DOM
// ==============================

const paymentIcon = document.getElementById('payment-icon');     // Icono del método de pago
const selectedMethodElement = document.getElementById('selected-method'); // Texto del método de pago
const btnPay = document.querySelector('.btnPay');                // Botón principal para pagar
const confirmButton = document.getElementById('confirm-button'); // Botón de "Confirmar y Pagar"
const cardPaymentForm = document.getElementById('card-payment-form'); // Formulario de tarjeta

// ==============================
// Mostrar icono y nombre según el método de pago
// ==============================

switch(paymentMethod){
    case 'credit-debit':
        paymentIcon.src = 'https://upload.wikimedia.org/wikipedia/commons/a/a4/Mastercard_2019_logo.svg';
        selectedMethodElement.textContent = 'Tarjeta de Credito o Debito';
        break;
    case 'paypal':
        paymentIcon.src = 'https://upload.wikimedia.org/wikipedia/commons/b/b5/PayPal.svg';
        selectedMethodElement.textContent = 'PayPal';
        break;
    default:
        console.error('Método de pago no válido');
}

// ==============================
// Evento para botón "Confirmar y Pagar"
// ==============================

btnPay.addEventListener('click', completePurchase);

function completePurchase(){
    if(paymentMethod === 'credit-debit'){
        cardPaymentForm.style.display = 'block';
        confirmButton.style.display = 'none';

        // Agregar evento al botón de pagar con tarjeta
        document.querySelector(".btnPay").addEventListener("click", function(event) {
            event.preventDefault(); // Prevenir que el formulario se envíe automáticamente
            swal("Pago completado con tarjeta!", "Gracias por su compra", "success")
            // .then(() => {
            //     //aqui tiene que generarle un pdf al usuario con la factura
            //     window.location.href = "../Pagos/factura.html";
            // })
        })
    } else if(paymentMethod === 'paypal'){
        // Redirigir a PayPal con el monto y el producto
        window.location.href = `https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=darlynolivo15@gmail.com&item_name=${encodeURIComponent(itemName)}&amount=${encodeURIComponent(amount)}&currency_code=USD`;
    }
}

// ==============================
// Función para volver a la pantalla anterior
// ==============================

function goBack(){
    window.history.back();
}

// ==============================
// Función para volver al carrito
// ==============================

function backToCart(){
    window.location.href = '../carrito/Carrito.html';
}

// ==============================
// Función para ir a la página de confirmación
// con los datos del método de pago seleccionado
// ==============================

function goToConfirmationPage(){
     // Obtener el método seleccionado desde un input (radio)
    const selectedPaymentMethod = document.querySelector('input[name="payment-method"]:checked').value;
    // Obtener los parámetros actuales (producto y monto)
    const productName = params.get('item-name');
    const amount = params.get('amount');

    // Redirigir a la página de confirmación con los datos

    if(selectedPaymentMethod === 'credit-debit'){
        window.location.href = `../Confirmation/Confirmation.html?item-name=${encodeURIComponent(productName)}&amount=${amount}&payment-method=credit-debit`;
    } else if(selectedPaymentMethod === 'paypal'){
        window.location.href = `../Confirmation/Confirmation.html?item-name=${encodeURIComponent(productName)}&amount=${amount}&payment-method=paypal`;
    }
}