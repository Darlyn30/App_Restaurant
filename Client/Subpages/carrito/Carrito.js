const TOKEN = JSON.parse(localStorage.getItem("token")) || null;
const USER = JSON.parse(localStorage.getItem("account")) || null;
const cartContainer = document.getElementById('cart-item');
const emptyCart = document.getElementById('empty-cart');
const clearCartContainer = document.querySelector(".cart-header");
const clearCartBtn = document.querySelector(".btn-vaciar");
const totalCount = document.getElementById("total");

const URLs = {
    URL_GET_CART : `https://localhost:7075/api/Cart/user/${USER.id}`,
    URL_DELETE_ITEM_CART : `https://localhost:7075/api/Cart/item/`, // <- Id_item pending
    URL_DELETE_ALL_ITEMS : `https://localhost:7075/api/Cart/deleteAllItems/${USER.id}`,
    URL_GET_TOTAL_PRICE : `https://localhost:7075/api/Cart/GetTotalPrice/`, // <-CART_ID PENDIENTE
}

const img404 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRkjuaCHtdwkToFkspREKsD_FTNvuxsg7CFbg&s"; // cuando algo no tenga imagen o no carga

const checkoutModal = document.getElementById('checkout-modal');

window.onload = async function () {

    await renderCartItems(URLs.URL_GET_CART, URLs.URL_DELETE_ITEM_CART);

    clearCartBtn.addEventListener("click", async () => {
        
        await deleteAllItems(URLs.URL_DELETE_ALL_ITEMS, URLs.URL_GET_CART, URLs.URL_DELETE_ITEM_CART);
    })

    await calcSubtotal(URLs.URL_GET_TOTAL_PRICE, URLs.URL_GET_CART);
};

// Botón para abrir modal de pago
document.getElementById('checkout-btn').addEventListener('click', () => {
    checkoutModal.style.display = 'flex';
});

// Cerrar modal
document.getElementById('close-modal').addEventListener('click', () => {
    checkoutModal.style.display = 'none';
    window.location.href = "./Carrito.html";
});

// Cerrar modal al hacer clic fuera del contenido
window.onclick = function(event) {
    if (event.target === checkoutModal) {
        checkoutModal.style.display = "none";
    }
};

// Continuar comprando
document.getElementById('continue-btn').addEventListener('click', () => {
    window.location.href = '../home/Home.html'; 
});

async function deleteCartItem(url, item){
    try {
        swal({
            title: "Are you sure?",
            text: `Seguro que deseas borrar este producto: ${item.foodName}`,
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
        .then((willDelete) => {
            if (willDelete) {
                swal("Has borrado el siguiente producto", {
                    icon: "success",
                })
                .then(async rs => {
                    const res = await apiCall(url, "DELETE");
                    window.location.href = "./Carrito.html";
                });
            } else {
                swal("Puedes borrar el producto cuando quieras");
            }
        });

    } catch(err) {
        throw Error(err);
    }
}

async function apiCall(url, method, body = null) {
    const options = {
        method,
        headers : {
            "Authorization" : `Bearer ${TOKEN}`, // para los endpoints que debemos de tener authorizacion de usuario
            "Content-Type" : "application/json"
        }
    }

    if(body) options.body = JSON.stringify(body);

    //esto pasa cuando el token se vence, los endpoints daran un 401 Unauthorized y me sacara de la sesion, enviandome al login
    try {
        const res = await fetch(url, options);

        if(res.status === 401){
            // 🔒 Token expirado
            localStorage.removeItem("token");
            localStorage.removeItem("account");
            swal(
                "Sesión expirada", 
                "Por favor inicia sesión de nuevo", 
                "warning")
            .then(() => {
                window.location.href = "../../Index.html";
            });

            return;
        }

        const data = await res.json();


        if(!res.ok) throw new Error(data.message || "Unknow Error");

        return data;

    } catch(err){
        console.error("Error en la peticion: ", err.message);
        throw err;
    }
}

async function renderCartItems(url, urlDeleteItem) {
    let res;

    try {
        res = await apiCall(url, "GET");
    } catch (err) {
        console.error("Fallo al obtener los datos del carrito");
        emptyCart.style.display = "block";
        return;
    }

    if (!res || res.message === "cart not found" || !res.items || res.items.length === 0) {
        emptyCart.style.display = "block";
        return;
    }

    clearCartContainer.style.display = "block";


    res.items.forEach(item => {
        const div = document.createElement('div');
        div.className = 'cart-item';
        div.innerHTML = `
            <img src="${item.imgUrl || img404}" alt="${item.name}" class="cart-item-img">
            <div class="cart-item-details">
                <h4>${item.foodName}</h4>
                <span class="price">$${item.price.toFixed(2)}</span>
            </div>
            <div class="cart-item-actions">
                <button class="btn-delete">🗑️</button>
            </div>
        `;
        cartContainer.appendChild(div);

        // 👇 Esta parte puede fallar si hay varios .btn-delete, usa currentTarget
        const deleteBtn = div.querySelector(".btn-delete");
        deleteBtn.addEventListener("click", async () => {
            const fullUrl = `${urlDeleteItem}${item.itemId}`;
            await deleteCartItem(fullUrl, item);
        });
    })
}


async function calcSubtotal(url, urlGet) {
    const res = await apiCall(urlGet, "GET");
    const fullUrl = `${url}${res.id}`;
    const data = await apiCall(fullUrl, "GET");
    console.log(data);

    totalCount.innerHTML = `$${parseFloat(data)}`;
}

async function deleteAllItems(url){
    console.log("hola");
    
    try {
        swal({
            title: "Are you sure?",
            text: `Seguro que deseas borrar todo el carrito?`,
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
        .then((willDelete) => {
            if (willDelete) {
                swal("se ha vaciado el carrito", {
                    icon: "success",
                })
                .then(async () => {
                    await apiCall(url, "DELETE");
                    window.location.href = "./Carrito.html";
                });
            } else {
                swal("Puedes borrar el producto cuando quieras");
            }
        });

    } catch(err) {
        throw Error(err);
    }
}