const API_URL = "http://localhost:5122/api/Productos"; // ajusta el puerto

const productForm = document.getElementById("product-form");
const productList = document.getElementById("product-list");
const cancelEdit = document.getElementById("cancel-edit");

let editMode = false;
let currentId = null;

// Cargar productos al iniciar
window.addEventListener("DOMContentLoaded", loadProducts);

async function loadProducts() {
    const res = await fetch(API_URL);
    const data = await res.json();

    productList.innerHTML = "";
    data.forEach(p => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${p.id}</td>
            <td>${p.nombre}</td>
            <td>${p.descripcion}</td>
            <td>$${p.precio}</td>
            <td>
                <button onclick="editProduct(${p.id}, '${p.nombre}', '${p.descripcion}', ${p.precio})">✏️ Editar</button>
                <button onclick="deleteProduct(${p.id})">🗑️ Eliminar</button>
            </td>
        `;
        productList.appendChild(row);
    });
}

// Guardar o editar producto
productForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const producto = {
        nombre: document.getElementById("nombre").value,
        descripcion: document.getElementById("descripcion").value,
        precio: parseFloat(document.getElementById("precio").value)
    };

    if (editMode) {
        await fetch(`${API_URL}/${currentId}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ id: currentId, ...producto })
        });
        editMode = false;
        currentId = null;
        cancelEdit.style.display = "none";
    } else {
        await fetch(API_URL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(producto)
        });
    }

    productForm.reset();
    loadProducts();
});

function editProduct(id, nombre, descripcion, precio) {
    document.getElementById("nombre").value = nombre;
    document.getElementById("descripcion").value = descripcion;
    document.getElementById("precio").value = precio;
    currentId = id;
    editMode = true;
    cancelEdit.style.display = "inline";
}

cancelEdit.addEventListener("click", () => {
    editMode = false;
    currentId = null;
    productForm.reset();
    cancelEdit.style.display = "none";
});

async function deleteProduct(id) {
    if (confirm("¿Seguro que quieres eliminar este producto?")) {
        await fetch(`${API_URL}/${id}`, { method: "DELETE" });
        loadProducts();
    }
}
