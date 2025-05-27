const methodId = sessionStorage.getItem("selectedPaymentMethod") || ""; //aqui lo que guardamos durante la sesion es solo
//el id del tipo de pago

const URL_PAYMENT_ID = `https://localhost:7075/api/Payment/${methodId}`; // <- le pasamos el id a la URL