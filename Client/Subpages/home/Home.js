const img404 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRkjuaCHtdwkToFkspREKsD_FTNvuxsg7CFbg&s"; // cuando algo no tenga imagen o no carga
// localStorage.removeItem("token");
// localStorage.removeItem("account");

const TOKEN = JSON.parse(localStorage.getItem("token")) || null;
const USER = JSON.parse(localStorage.getItem("account")) || null;
const btn_logout = document.querySelector(".logout-btn");
const cat_container = document.getElementById("category-list");
const res_container = document.getElementById("restaurant-list");
const cart_nav = document.getElementById("cart-icon"); // es un div que se esta usando como boton, el que muestra el carrito

const URLS = {
    URL_RESTAURANT: "https://localhost:7075/api/Restaurant",
    URL_CATEGORY: "https://localhost:7075/api/Category",
    URL_FOOD_BY_RESTAURANT: "https://localhost:7075/api/Food/restaurant/", // <- Id del restaurante
    URL_GET_CART : `https://localhost:7075/api/Cart/user/${USER.id}`, // <- lleva un userId para identificar el due?o del carrito, osea el usuario logueado
    URL_POST_ADD_ITEM : `https://localhost:7075/api/Cart/add-item?userId=${USER.id}`, // <- hay que pasarle unos parametros
    URL_DELETE_ALL_CART : `https://localhost:7075/api/Cart/${USER.id}`,
};

let allRestaurants = [];
let selectedRestaurantId = null;
let selectedRestaurantName = "";

//   localStorage.removeItem("token");
//   localStorage.removeItem("account");

document.addEventListener("DOMContentLoaded", async () => {
    await showCart(URLS.URL_GET_CART);// para obtener el length del cart
    await reenderRestaurants(URLS.URL_RESTAURANT, res_container);

    //moverse hacia el carrito
    cart_nav.addEventListener("click", () => {
        window.location.href = "../carrito/Carrito.html";
    })

    //cerrar sesion
    btn_logout.addEventListener("click", async () => {
        await cerrarSesion(URLS.URL_DELETE_ALL_CART);
    })

    //#region nav sections
    document.querySelectorAll(".nav-link").forEach(link => {
        link.addEventListener("click", (e) => {
            e.preventDefault();
            const target = link.getAttribute("data-section");

            //ocultar todas las secciones

            document.querySelectorAll(".section").forEach(sec => {
                sec.classList.add("hidden");
            })

            //mostrar seccion seleccionada
            document.getElementById(target).classList.remove("hidden");

            //resaltar la seccion activa
            document.querySelectorAll(".nav-link").forEach(l => l.classList.remove("active"));
            link.classList.add("active");

            //cargar los datos de la cuenta
            if(target === "account-section"){
                infoCuenta();
            }
        })
    })
})


async function cerrarSesion(url){
    swal({
        title: "¿Estás seguro?",
        text: "Tu sesión se cerrará",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then(async res => {
        if (res) {
            localStorage.removeItem("token");
            localStorage.removeItem("account");
            await apiCall(url, "DELETE")
            window.location.href = "../../Index.html";
        }
    });
}

async function renderRestaurants(data, container){
    container.innerHTML = "";

    if(data.length === 0){
        container.innerHTML = "<p>No se encontraron restaurantes</p>";
        return;
    }

    data.forEach(restaurant => {
        const card = document.createElement("div");
        card.className = "restaurant-card";
        card.innerHTML = `
            <img src="${restaurant.imgUrl}" alt="${restaurant.name}"/>
            <div>
                <h4>${restaurant.name}</h4>
                <p>${restaurant.description || "Sin descripcion disponible"}</p>
                <p class="${restaurant.active ? "Open" : "Closed"}">
                ${restaurant.active ? "Abierto" : "Cerrado"}
                </p>
                <p style="color: green;">${restaurant.openingTime.substring(0,5)} - ${restaurant.closingTime.substring(0,5)}</p>
            </div>
        `;

        card.addEventListener("click", () => {
            selectedRestaurantId = restaurant.id;
            selectedRestaurantName = restaurant.name;
            renderFoodsByRestaurants(URLS.URL_FOOD_BY_RESTAURANT,selectedRestaurantId, selectedRestaurantName);
        })

        container.appendChild(card);
    })
}

function infoCuenta(){
    const name = document.getElementById("user-name");
    const email = document.getElementById("user-email");

    email.innerHTML = USER.email;
    name.innerHTML = USER.name;
}

async function renderFoodsByRestaurants(url, id, name){
    try {
        const fullUrl = `${url}${id}`;
        const data = await apiCall(fullUrl, "GET");
        if(!data) return;

        renderFoods(data, name, res_container);
        
    } catch(err) {
        swal({
            title: "Ha ocurrido un error",
            text: err.message,
            icon: "warning"
        })
        .then(() => {
            window.location.href = "./Home.html";
        })
    }
}

async function reenderRestaurants(url, container){
    try {
        const data = await apiCall(url, "GET");
        if(!data) return;

        allRestaurants = data;
        renderRestaurants(data, container);
    } catch(err){
        console.log("Error al renderizar restaurantes: ", err);
    }
}

function renderFoods(data, name, container){
    container.innerHTML = "";

    const header = document.createElement("div");
    header.className = "food-header";
    header.innerHTML = `
        <h3>comidas de: ${name}</h3>
        <button id="close-menu-btn" class="btn-close"> Volver al restaurante</button>
    `;
    container.appendChild(header);

    if(data.length === 0){
        container.innerHTML += "<p>No hay comidas disponibles</p>"
        return;
    }

    const grid = document.createElement("div");
    grid.className = "grid";

    data.forEach(food => {
        const card =  document.createElement("div");
        card.className = "food-card";
        card.innerHTML = `
            <img src="${food.imgUrl || img404}" alt="${food.name}"/>
            <div class="info">
                <h4>${food.name}</h4>
                <p>${food.description}</p>
                <p class="price" style="color: green;">$${food.price.toFixed(2)}</p>
                <button class="add-to-cart-btn">Agregar al carrito</button>
            </div>
        `;
        //addItemToCart('${USER.id}', '${URLS.URL_POST_ADD_ITEM}', '${URLS.URL_GET_CART}', '${URLS.URL_POST_CREATE_CART}', '${food.id}')
        grid.appendChild(card);
        const addBtn = card.querySelector(".add-to-cart-btn");
        addBtn.addEventListener("click", async () => {
            console.log(food.id);
            await addItemToCart(URLS.URL_GET_CART,URLS.URL_POST_ADD_ITEM, food.id);
        })


    })

    container.appendChild(grid);

    document.getElementById("close-menu-btn").addEventListener("click", () => {
        reenderRestaurants(URLS.URL_RESTAURANT, res_container);
    })
}

//esto trae la cantidad de objetos del carrito para mostrarla en pantalla
async function showCart(url){
    try {
        const data = await apiCall(url, "GET");
        const span = document.getElementById("cart-count");

        if(!data.ok) span.innerHTML = "0";

        span.innerHTML = `${data.items.length}`;
    } catch(err){
        console.log("Error al traer el carrito: ", err);
    }
}

async function addItemToCart(urlGet,urlAdd, foodId,) {

    const body = {

        foodId: parseInt(foodId),
        quantity: 1,
    };

    const addedItem = await apiCall(urlAdd, "POST", body);

    console.log("Producto agregado al carrito:", addedItem);

    swal({
        title: "Muy bien!",
        text: "Producto agregado al carrito con éxito!",
        icon: "success",
    });

    // Actualiza el contador del carrito después de agregar
    await showCart(urlGet);

    return addedItem;
}


async function apiCall(url, method, body = null) {
    const options = {
        method,
        headers: {
            "Authorization": `Bearer ${TOKEN}`,
            "Content-Type": "application/json"
        }
    };

    if (body) options.body = JSON.stringify(body);

    try {
        const res = await fetch(url, options);

        if (res.status === 401) {
            // 🔒 Token expirado
            localStorage.removeItem("token");
            localStorage.removeItem("account");
            swal("Sesión expirada", "Por favor inicia sesión de nuevo", "warning")
                .then(() => {
                    window.location.href = "../../Index.html";
                });
            return;
        }

        const data = await res.json();

        if (!res.ok) {
            const error = new Error(data.message || "Unknown Error");
            error.status = res.status;
            throw error;
        }

        return data;

    } catch (err) {
        console.error("Error en la peticion:", err.message);
        throw err;
    }
}