const API_URL = "http://localhost:5122/api/Productos"; // ajusta el puerto si es diferente

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
            <td>${p.descripcion || "-"}</td>
            <td>$${p.precio.toFixed(2)}</td>
            <td>${p.stock}</td>
            <td>${p.estado ? "Disponible" : "Agotado"}</td>
            <td>${new Date(p.fechaCreacion).toLocaleDateString()}</td>
          <td>
    <button style="background-color: #4CAF50; color: white;" onclick="editProduct(${p.id}, '${escapeQuotes(p.nombre)}', '${escapeQuotes(p.descripcion)}', ${p.precio}, ${p.stock}, ${p.estado})">✏️</button>
    <button style="background-color: #f44336; color: white;" onclick="deleteProduct(${p.id})">🗑️</button>
</td>

        `;
        productList.appendChild(row);
    });
}

// Escapar comillas simples para evitar errores al editar
function escapeQuotes(text) {
    return text ? text.replace(/'/g, "\\'") : "";
}

// Guardar o editar producto
productForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const producto = {
        nombre: document.getElementById("nombre").value,
        descripcion: document.getElementById("descripcion").value,
        precio: parseFloat(document.getElementById("precio").value),
        stock: parseInt(document.getElementById("stock").value),
        estado: document.getElementById("estado").value === "true"
    };

    try {
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
    } catch (error) {
        console.error("Error al guardar el producto:", error);
        alert("Ocurrió un error al guardar el producto.");
    }
});

function editProduct(id, nombre, descripcion, precio, stock, estado) {
    document.getElementById("nombre").value = nombre;
    document.getElementById("descripcion").value = descripcion;
    document.getElementById("precio").value = precio;
    document.getElementById("stock").value = stock;
    document.getElementById("estado").value = estado ? "true" : "false";
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
