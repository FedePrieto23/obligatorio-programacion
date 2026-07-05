const API = {
    cliente: "/api/cliente",
    obra: "/api/obra",
    material: "/api/material",
    materialObra: "/api/materialobra",
    gasto: "/api/gasto",
    usuario: "/api/usuario",
    tipoGasto: "/api/tipogasto",
    oficio: "/api/oficio",
    asignacion: "/api/asignacion"
};

const ADMIN_DEMO = {
    nombre: "Administrador",
    email: "admin@pages.com",
    password: "admin123",
    rol: "Administrador"
};

// Sistema simple de mensajes para login
function mostrarMensajeLogin(mensaje, tipo = "error") {
    const messageDiv = document.getElementById("loginMessage");
    if (!messageDiv) return;

    messageDiv.textContent = mensaje;
    messageDiv.style.display = "block";

    if (tipo === "error") {
        messageDiv.style.backgroundColor = "#fee";
        messageDiv.style.color = "#c33";
        messageDiv.style.border = "1px solid #fcc";
    } else if (tipo === "exito") {
        messageDiv.style.backgroundColor = "#efe";
        messageDiv.style.color = "#3a3";
        messageDiv.style.border = "1px solid #cfc";
    }

    // Auto-ocultar después de 5 segundos si es error
    if (tipo === "error") {
        setTimeout(() => {
            messageDiv.style.display = "none";
        }, 5000);
    }
}

// ================= MODAL PERSONALIZADO =================
function mostrarModalAlerta(titulo, mensaje) {
    return new Promise((resolve) => {
        const overlay = document.getElementById("modalOverlay");
        const titleEl = document.getElementById("modalTitle");
        const bodyEl = document.getElementById("modalBody");
        const confirmBtn = document.getElementById("modalConfirmBtn");
        const cancelBtn = document.getElementById("modalCancelBtn");

        titleEl.textContent = titulo;
        bodyEl.innerHTML = `<p>${mensaje}</p>`;
        confirmBtn.style.display = "flex";
        cancelBtn.style.display = "none";
        confirmBtn.textContent = "Aceptar";

        confirmBtn.onclick = () => {
            cerrarModal();
            resolve(true);
        };

        overlay.style.display = "flex";
    });
}

function mostrarModalConfirmacion(titulo, mensaje) {
    return new Promise((resolve) => {
        const overlay = document.getElementById("modalOverlay");
        const titleEl = document.getElementById("modalTitle");
        const bodyEl = document.getElementById("modalBody");
        const confirmBtn = document.getElementById("modalConfirmBtn");
        const cancelBtn = document.getElementById("modalCancelBtn");

        titleEl.textContent = titulo;
        bodyEl.innerHTML = `<p>${mensaje}</p>`;
        confirmBtn.style.display = "flex";
        cancelBtn.style.display = "flex";
        confirmBtn.textContent = "Confirmar";

        confirmBtn.onclick = () => {
            cerrarModal();
            resolve(true);
        };

        cancelBtn.onclick = () => {
            cerrarModal();
            resolve(false);
        };

        overlay.style.display = "flex";
    });
}

function mostrarModalExito(titulo, mensaje) {
    return mostrarModalAlerta(titulo, mensaje);
}

function mostrarModalError(titulo, mensaje) {
    return mostrarModalAlerta(titulo, mensaje);
}

function cerrarModal() {
    const overlay = document.getElementById("modalOverlay");
    const confirmBtn = document.getElementById("modalConfirmBtn");
    const cancelBtn = document.getElementById("modalCancelBtn");

    overlay.style.display = "none";
    confirmBtn.onclick = null;
    cancelBtn.onclick = null;
}

// Cerrar modal al presionar Escape o hacer clic fuera
document.addEventListener("keydown", (e) => {
    if (e.key === "Escape") {
        const overlay = document.getElementById("modalOverlay");
        if (overlay && overlay.style.display === "flex") {
            cerrarModal();
        }
    }
});

// Cerrar modal al hacer clic en el overlay (fuera del contenedor)
document.addEventListener("click", (e) => {
    const overlay = document.getElementById("modalOverlay");
    if (overlay && overlay.style.display === "flex" && e.target === overlay) {
        cerrarModal();
    }
});

// ================= SISTEMA DE TOASTS/NOTIFICACIONES =================
function mostrarToast(mensaje, tipo = "info", duracion = 4000) {
    const container = document.getElementById("toastContainer");
    if (!container) return;

    const toast = document.createElement("div");
    toast.className = `toast ${tipo}`;

    const iconos = {
        success: "✓",
        error: "✕",
        info: "ⓘ",
        warning: "⚠"
    };

    toast.innerHTML = `
        <div class="toast-icon">${iconos[tipo] || "•"}</div>
        <div class="toast-message">${mensaje}</div>
        <button class="toast-close" onclick="this.parentElement.remove()" aria-label="Cerrar notificación">×</button>
        <div class="toast-progress"></div>
    `;

    container.appendChild(toast);

    // Auto-remover después de la duración
    if (duracion > 0) {
        setTimeout(() => {
            if (toast.parentElement) {
                toast.classList.add("removing");
                setTimeout(() => toast.remove(), 300);
            }
        }, duracion);
    }

    return toast;
}

function mostrarToastExito(mensaje, duracion = 4000) {
    return mostrarToast(mensaje, "success", duracion);
}

function mostrarToastError(mensaje, duracion = 4000) {
    return mostrarToast(mensaje, "error", duracion);
}

function mostrarToastInfo(mensaje, duracion = 4000) {
    return mostrarToast(mensaje, "info", duracion);
}

function mostrarToastAdvertencia(mensaje, duracion = 4000) {
    return mostrarToast(mensaje, "warning", duracion);
}

let usuarioSesion = null;

let cache = {
    clientes: [],
    obras: [],

    materiales: [],
    materialesObra: [],
    gastos: [],
    usuarios: [],
    tiposGasto: [],
    oficios: [],
    asignaciones: []
};

const OFICIOS_BASE = [
    { nombreOficio: "General", descripcionOficio: "Tareas generales" },
    { nombreOficio: "Albañil", descripcionOficio: "Trabajos de albañilería" },
    { nombreOficio: "Electricista", descripcionOficio: "Instalaciones eléctricas" },
    { nombreOficio: "Sanitario", descripcionOficio: "Instalaciones sanitarias" },
    { nombreOficio: "Pintor", descripcionOficio: "Pintura y terminaciones" }
];

const TIPOS_GASTO_BASE = ["Materiales", "Mano de obra", "Transporte", "Otros"];

document.addEventListener("DOMContentLoaded", async function () {
    configurarAuth();
    configurarMenu();
    configurarBuscadores();
    configurarFormularios();
    configurarEventosSelects();
    await cargarOficiosRegistro();

    const sesionGuardada = localStorage.getItem("pagesSesion");
    if (sesionGuardada) {
        usuarioSesion = JSON.parse(sesionGuardada);
        mostrarAplicacion();
    } else {
        mostrarAuth();
    }
});

// ================= AUTENTICACIÓN =================
function configurarAuth() {
    document.getElementById("formLogin").addEventListener("submit", iniciarSesion);
    document.getElementById("formRegistro").addEventListener("submit", registrarCuenta);
    document.getElementById("mostrarRegistro").addEventListener("click", () => alternarAuth("registro"));
    document.getElementById("mostrarLogin").addEventListener("click", () => alternarAuth("login"));
    document.getElementById("btnCerrarSesion").addEventListener("click", cerrarSesion);
}

function alternarAuth(modo) {
    document.getElementById("loginCard").classList.toggle("oculto", modo !== "login");
    document.getElementById("registroCard").classList.toggle("oculto", modo !== "registro");
    if (modo === "registro") cargarOficiosRegistro();
}

async function cargarOficiosRegistro() {
    try {
        let oficios = await cargarJsonSinSesion(API.oficio);

        if (!oficios || oficios.length === 0) {
            for (const oficio of OFICIOS_BASE) {
                await enviarJsonComoAdmin(API.oficio, "POST", oficio);
            }
            oficios = await cargarJsonSinSesion(API.oficio);
        }

        cache.oficios = oficios;
        poblarSelect("registroOficio", cache.oficios, "idOficio", o => o.nombreOficio);
    } catch (error) {
        console.error(error);
        const select = document.getElementById("registroOficio");
        if (select) {
            select.innerHTML = `<option value="">No se pudieron cargar oficios</option>`;
        }
    }
}

async function iniciarSesion(event) {
    event.preventDefault();

    const email = document.getElementById("loginEmail").value.trim().toLowerCase();
    const password = document.getElementById("loginPassword").value;

    if (email === ADMIN_DEMO.email && password === ADMIN_DEMO.password) {
        usuarioSesion = { idUsuario: 0, nombre: ADMIN_DEMO.nombre, email: ADMIN_DEMO.email, rol: ADMIN_DEMO.rol, oficio: "Administración" };
        localStorage.setItem("pagesSesion", JSON.stringify(usuarioSesion));
        await mostrarAplicacion();
        return;
    }

    try {
        const usuarios = await cargarJsonSinSesion(API.usuario);

        const usuario = usuarios.find(u => {
            const emailMatch = (u.emailUsuario || "").toLowerCase() === email;
            const passwordMatch = obtenerContrasenaUsuario(u) === password;
            return emailMatch && passwordMatch;
        });

        if (!usuario) {
            mostrarMensajeLogin("Email o contraseña incorrectos", "error");
            return;
        }

        usuarioSesion = usuarioSesionDesdeApi(usuario);
        localStorage.setItem("pagesSesion", JSON.stringify(usuarioSesion));
        await mostrarAplicacion();
    } catch (error) {
        console.error(error);
        alert("Error al conectar con el servidor");
    }
}

async function registrarCuenta(event) {
    event.preventDefault();

    const nuevoUsuario = {
        nombreUsuario: document.getElementById("registroNombre").value.trim(),
        emailUsuario: document.getElementById("registroEmail").value.trim().toLowerCase(),
        contraseña: document.getElementById("registroPassword").value,
        tipoEmpleado: document.getElementById("registroRol").value,
        idOficio: Number(document.getElementById("registroOficio").value)
    };

    if (!nuevoUsuario.nombreUsuario || !nuevoUsuario.emailUsuario || !nuevoUsuario.contraseña || !nuevoUsuario.idOficio) {
        return;
    }

    if (nuevoUsuario.emailUsuario === ADMIN_DEMO.email) {
        return;
    }

    try {
        const usuarios = await cargarJsonSinSesion(API.usuario);
        if (usuarios.some(u => (u.emailUsuario || "").toLowerCase() === nuevoUsuario.emailUsuario)) {
            return;
        }

        await enviarJsonRegistro(API.usuario, "POST", nuevoUsuario);
        document.getElementById("formRegistro").reset();
        alternarAuth("login");
    } catch (error) {
        console.error(error);
    }
}

function usuarioSesionDesdeApi(usuario) {
    const oficio = usuario.oficio ? usuario.oficio.nombreOficio : obtenerNombreOficio(usuario.idOficio);
    return {
        idUsuario: usuario.idUsuario,
        nombre: usuario.nombreUsuario,
        email: usuario.emailUsuario,
        rol: usuario.tipoEmpleado || "Empleado",
        idOficio: usuario.idOficio,
        oficio
    };
}

function obtenerContrasenaUsuario(usuario) {
    return usuario.contraseña || usuario.contrasena || usuario.password || "";
}

function cerrarSesion() {
    localStorage.removeItem("pagesSesion");
    usuarioSesion = null;
    mostrarAuth();
}

function mostrarAuth() {
    document.getElementById("authView").classList.remove("oculto");
    document.getElementById("appView").classList.add("oculto");
    alternarAuth("login");
    document.getElementById("formLogin").reset();
}

async function mostrarAplicacion() {
    document.getElementById("authView").classList.add("oculto");
    document.getElementById("appView").classList.remove("oculto");

    document.getElementById("usuarioActualNombre").textContent = usuarioSesion.nombre;
    document.getElementById("usuarioActualRol").textContent = textoRol(usuarioSesion.rol);
    document.getElementById("userAvatar").textContent = usuarioSesion.nombre.substring(0, 1).toUpperCase();

    aplicarPermisosVisuales();
    await cargarTodo();
}

function rolActual() {
    return usuarioSesion?.rol || "Empleado";
}

function esAdmin() {
    return rolActual() === "Administrador";
}

function headersJson() {
    return {
        "Content-Type": "application/json",
        "X-Rol-Usuario": rolActual()
    };
}

// ================= CONFIGURACIÓN GENERAL =================
function configurarMenu() {
    document.querySelectorAll(".menu a, .quick-card").forEach(link => {
        link.addEventListener("click", function (event) {
            event.preventDefault();
            const section = this.dataset.section || this.dataset.go;
            irASeccion(section);
        });
    });
}

function irASeccion(section) {
    document.querySelectorAll(".menu a").forEach(a => a.classList.remove("activo"));
    const menuActivo = document.querySelector(`.menu a[data-section="${section}"]`);
    if (menuActivo) menuActivo.classList.add("activo");

    document.querySelectorAll(".page-section").forEach(sec => sec.classList.remove("activa"));
    document.getElementById(`seccion-${section}`).classList.add("activa");

    const titulos = {
        inicio: ["Panel principal", "Gestión de obras, materiales, costos y personal."],
        clientes: ["Clientes", "Consulta de clientes y obras asociadas."],
        obras: ["Obras", "Seguimiento de obras, clientes, costos y personal."],
        asignaciones: ["Personal de obra", "Asignación de empleados registrados a cada obra."],
        materiales: ["Materiales por obra", "Registro de materiales destinados a cada obra."],
        gastos: ["Gastos", "Carga de gastos asociados automáticamente a una obra."]
    };

    document.getElementById("tituloPagina").textContent = titulos[section]?.[0] || "Panel";
    document.getElementById("subtituloPagina").textContent = titulos[section]?.[1] || "";
}

function aplicarPermisosVisuales() {
    const admin = esAdmin();
    document.querySelectorAll(".admin-only").forEach(elemento => {
        elemento.classList.toggle("oculto", !admin);
    });

    document.getElementById("mensajeRol").classList.toggle("oculto", admin);
    renderizarTodo();
}

function configurarBuscadores() {
    document.getElementById("buscarCliente").addEventListener("input", renderizarClientes);
    document.getElementById("buscarObra").addEventListener("input", renderizarObras);
    document.getElementById("filtroEstadoObra").addEventListener("change", renderizarObras);
    document.getElementById("buscarEmpleadoAsignacion").addEventListener("input", renderizarAsignaciones);
    document.getElementById("filtroOficioAsignacion").addEventListener("change", renderizarAsignaciones);
    document.getElementById("filtroRolAsignacion").addEventListener("change", renderizarAsignaciones);
    document.getElementById("clienteDetalleObras").addEventListener("change", renderizarObrasCliente);
    document.getElementById("filtroPanelCliente").addEventListener("change", renderizarPanelFiltros);
    document.getElementById("filtroPanelObra").addEventListener("change", renderizarPanelFiltros);
}

function configurarFormularios() {
    document.getElementById("formCliente").addEventListener("submit", guardarCliente);
    document.getElementById("formObra").addEventListener("submit", guardarObra);
    document.getElementById("formMaterial").addEventListener("submit", guardarMaterial);
    document.getElementById("formGasto").addEventListener("submit", guardarGasto);
}

function configurarEventosSelects() {
    document.getElementById("idObraGasto").addEventListener("change", actualizarResumenGastoSeleccionado);
    document.getElementById("idTipoGasto").addEventListener("change", actualizarResumenGastoSeleccionado);
    document.getElementById("idObraAsignacion").addEventListener("change", renderizarAsignaciones);
}

async function cargarTodo() {
    try {
        const [clientes, obras, materiales, materialesObra, gastos, usuarios, tiposGasto, oficios, asignaciones] = await Promise.all([
            cargarJson(API.cliente),
            cargarJson(API.obra),
            cargarJson(API.material),
            cargarJson(API.materialObra),
            cargarJson(API.gasto),
            cargarJson(API.usuario),
            cargarJson(API.tipoGasto),
            cargarJson(API.oficio),
            cargarJson(API.asignacion)
        ]);

        cache.clientes = clientes;
        cache.obras = obras;
        cache.materiales = materiales;
        cache.materialesObra = materialesObra;
        cache.gastos = gastos;
        cache.usuarios = usuarios;
        cache.tiposGasto = tiposGasto;
        cache.oficios = oficios;
        cache.asignaciones = asignaciones;

        await asegurarDatosBase();
        poblarSelects();
        renderizarTodo();
        actualizarResumenGastoSeleccionado();
    } catch (error) {
        console.error(error);
    }
}

async function asegurarDatosBase() {
    if (!esAdmin()) return;

    let recargarTipos = false;
    let recargarOficios = false;

    if (cache.tiposGasto.length === 0) {
        // Cargar primero para verificar si ya existen
        const tiposExistentes = await cargarJson(API.tipoGasto);
        if (tiposExistentes.length === 0) {
            for (const nombreTipoGasto of TIPOS_GASTO_BASE) {
                await enviarJson(API.tipoGasto, "POST", { nombreTipoGasto });
            }
            recargarTipos = true;
        } else {
            cache.tiposGasto = tiposExistentes;
        }
    }

    if (cache.oficios.length === 0) {
        // Cargar primero para verificar si ya existen
        const oficiosExistentes = await cargarJson(API.oficio);
        if (oficiosExistentes.length === 0) {
            for (const oficio of OFICIOS_BASE) {
                await enviarJson(API.oficio, "POST", oficio);
            }
            recargarOficios = true;
        } else {
            cache.oficios = oficiosExistentes;
        }
    }

    if (recargarTipos) cache.tiposGasto = await cargarJson(API.tipoGasto);
    if (recargarOficios) cache.oficios = await cargarJson(API.oficio);
}

async function cargarJson(url) {
    const respuesta = await fetch(url, { headers: { "X-Rol-Usuario": rolActual() } });

    if (!respuesta.ok) {
        throw new Error(`No se pudo cargar ${url}`);
    }

    return await respuesta.json();
}

async function cargarJsonSinSesion(url) {
    const respuesta = await fetch(url);

    if (!respuesta.ok) {
        throw new Error(`No se pudo cargar ${url}`);
    }

    return await respuesta.json();
}

async function enviarJson(url, metodo, datos) {
    const respuesta = await fetch(url, {
        method: metodo,
        headers: headersJson(),
        body: JSON.stringify(datos)
    });

    return await manejarRespuesta(respuesta);
}

async function enviarJsonRegistro(url, metodo, datos) {
    const respuesta = await fetch(url, {
        method: metodo,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(datos)
    });

    return await manejarRespuesta(respuesta);
}

async function enviarJsonComoAdmin(url, metodo, datos) {
    const respuesta = await fetch(url, {
        method: metodo,
        headers: {
            "Content-Type": "application/json",
            "X-Rol-Usuario": "Administrador"
        },
        body: JSON.stringify(datos)
    });

    return await manejarRespuesta(respuesta);
}

async function manejarRespuesta(respuesta) {
    if (!respuesta.ok) {
        const texto = await respuesta.text();
        throw new Error(texto || "No se pudo completar la acción");
    }

    const contentType = respuesta.headers.get("content-type") || "";
    if (contentType.includes("application/json")) {
        return await respuesta.json();
    }

    return await respuesta.text();
}

async function eliminarPorApi(url) {
    const respuesta = await fetch(url, {
        method: "DELETE",
        headers: { "X-Rol-Usuario": rolActual() }
    });

    if (!respuesta.ok) {
        const texto = await respuesta.text();
        throw new Error(texto || "No se pudo eliminar");
    }
}

function formatoFechaParaInput(fecha) {
    if (!fecha) return "";
    return fecha.toString().substring(0, 10);
}

function formatoMoneda(valor) {
    const numero = Number(valor || 0);
    return `$${numero.toLocaleString("es-UY")}`;
}

function textoRol(rol) {
    if (rol === "TrabajadorTemporal") return "Trabajador temporal";
    return rol || "Empleado";
}

function normalizar(texto) {
    return (texto || "")
        .toString()
        .normalize("NFD")
        .replace(/[\u0300-\u036f]/g, "")
        .toLowerCase();
}

function acciones(entidad, id) {
    if (!esAdmin()) {
        return `<span class="estado estado-pendiente">Solo lectura</span>`;
    }

    return `
        <button class="btn btn-secondary" onclick="editar${entidad}(${id})">Editar</button>
        <button class="btn btn-danger" onclick="eliminar${entidad}(${id})">Eliminar</button>
    `;
}

function poblarSelects() {
    poblarSelect("idClienteObra", cache.clientes, "idCliente", c => `${c.nombreCliente} ${c.apellidoCliente}`);
    poblarSelect("idObraMaterial", cache.obras, "idObra", textoObraSelect);
    poblarSelect("idObraGasto", cache.obras, "idObra", textoObraSelect);
    poblarSelect("idObraAsignacion", cache.obras, "idObra", textoObraSelect);
    poblarSelect("idTipoGasto", cache.tiposGasto, "idTipoGasto", t => t.nombreTipoGasto);
    poblarSelect("registroOficio", cache.oficios, "idOficio", o => o.nombreOficio);
    poblarSelect("clienteDetalleObras", cache.clientes, "idCliente", c => `${c.nombreCliente} ${c.apellidoCliente}`);
    poblarSelect("filtroPanelCliente", cache.clientes, "idCliente", c => `${c.nombreCliente} ${c.apellidoCliente}`, "Todos los clientes");
    poblarSelect("filtroPanelObra", cache.obras, "idObra", textoObraSelect, "Todas las obras");
    poblarSelect("filtroOficioAsignacion", cache.oficios, "idOficio", o => o.nombreOficio, "Todos los oficios");
}

function poblarSelect(idSelect, datos, campoValor, textoFn, textoPlaceholder = "Seleccionar...") {
    const select = document.getElementById(idSelect);
    if (!select) return;

    const valorActual = select.value;
    select.innerHTML = "";

    const placeholder = document.createElement("option");
    placeholder.value = "";
    placeholder.textContent = textoPlaceholder;
    select.appendChild(placeholder);

    if (!datos || datos.length === 0) {
        return;
    }

    datos.forEach(item => {
        const option = document.createElement("option");
        option.value = item[campoValor];
        option.textContent = textoFn(item);
        select.appendChild(option);
    });

    if (valorActual && [...select.options].some(o => o.value === valorActual)) {
        select.value = valorActual;
    }
}

function textoObraSelect(obra) {
    const cliente = obra.cliente ? `${obra.cliente.nombreCliente} ${obra.cliente.apellidoCliente}` : obtenerNombreCliente(obra.idCliente);
    return `${obra.nombreObra} - ${cliente}`;
}

function obtenerNombreCliente(idCliente) {
    const cliente = cache.clientes.find(c => Number(c.idCliente) === Number(idCliente));
    return cliente ? `${cliente.nombreCliente} ${cliente.apellidoCliente}` : "Cliente no encontrado";
}

function obtenerNombreObra(idObra) {
    const obra = cache.obras.find(o => Number(o.idObra) === Number(idObra));
    return obra ? obra.nombreObra : "Obra no encontrada";
}

function obtenerTipoGasto(idTipoGasto) {
    const tipo = cache.tiposGasto.find(t => Number(t.idTipoGasto) === Number(idTipoGasto));
    return tipo ? tipo.nombreTipoGasto : "Sin tipo";
}

function obtenerNombreOficio(idOficio) {
    const oficio = cache.oficios.find(o => Number(o.idOficio) === Number(idOficio));
    return oficio ? oficio.nombreOficio : "Sin oficio";
}

function obtenerMaterial(idMaterial) {
    return cache.materiales.find(m => Number(m.idMaterial) === Number(idMaterial));
}

function esTipoMateriales(idTipoGasto) {
    return normalizar(obtenerTipoGasto(idTipoGasto)) === "materiales";
}

function gastosObra(idObra) {
    return cache.gastos
        .filter(g => Number(g.idObra) === Number(idObra) && !esTipoMateriales(g.idTipoGasto))
        .reduce((total, gasto) => total + Number(gasto.montoGasto || 0), 0);
}

function costoMaterialesObra(idObra) {
    return cache.materialesObra
        .filter(mo => Number(mo.idObra) === Number(idObra) && normalizar(mo.estadoMO) !== "faltante")
        .reduce((total, mo) => {
            const material = mo.material || obtenerMaterial(mo.idMaterial);
            return total + Number(mo.cantidadMO || 0) * Number(material?.precioUnitario || 0);
        }, 0);
}

function totalObra(idObra) {
    return gastosObra(idObra) + costoMaterialesObra(idObra);
}

function asignacionesObra(idObra) {
    return cache.asignaciones.filter(a => Number(a.idObra) === Number(idObra));
}

function empleadosRegistrados() {
    return cache.usuarios.filter(u => normalizar(u.tipoEmpleado) !== "administrador");
}

function actualizarStats() {
    document.getElementById("statClientes").textContent = cache.clientes.length;
    document.getElementById("statObras").textContent = cache.obras.filter(o => normalizar(o.estadoObra) !== "finalizada").length;
    document.getElementById("statMateriales").textContent = cache.materialesObra.length;
    document.getElementById("statGastos").textContent = cache.gastos.length;
}

function renderizarTodo() {
    renderizarClientes();
    renderizarObras();
    renderizarMateriales();
    renderizarGastos();
    renderizarAsignaciones();
    renderizarObrasCliente();
    renderizarPanelFiltros();
    actualizarStats();
}

// ================= PANEL =================
function renderizarPanelFiltros() {
    const contenedor = document.getElementById("resultadoPanelFiltros");
    if (!contenedor) return;

    const idCliente = Number(document.getElementById("filtroPanelCliente")?.value || 0);
    const idObra = Number(document.getElementById("filtroPanelObra")?.value || 0);

    let obras = cache.obras;
    if (idCliente) obras = obras.filter(o => Number(o.idCliente) === idCliente);
    if (idObra) obras = obras.filter(o => Number(o.idObra) === idObra);

    const totalGastos = obras.reduce((sum, obra) => sum + gastosObra(obra.idObra), 0);
    const totalMateriales = obras.reduce((sum, obra) => sum + costoMaterialesObra(obra.idObra), 0);
    const totalPersonal = obras.reduce((sum, obra) => sum + asignacionesObra(obra.idObra).length, 0);

    contenedor.innerHTML = `
        <div class="insight-card"><span>Obras encontradas</span><strong>${obras.length}</strong></div>
        <div class="insight-card"><span>Personal asignado</span><strong>${totalPersonal}</strong></div>
        <div class="insight-card"><span>Gastos generales</span><strong>${formatoMoneda(totalGastos)}</strong></div>
        <div class="insight-card"><span>Materiales</span><strong>${formatoMoneda(totalMateriales)}</strong></div>
    `;
}

// ================= CLIENTES =================
function obrasDeCliente(idCliente) {
    return cache.obras.filter(o => Number(o.idCliente) === Number(idCliente));
}

function renderizarClientes() {
    const filtro = normalizar(document.getElementById("buscarCliente")?.value || "");
    const tabla = document.getElementById("tablaClientes");
    tabla.innerHTML = "";

    cache.clientes
        .filter(c => normalizar(`${c.nombreCliente} ${c.apellidoCliente} ${c.documento} ${c.emailCliente}`).includes(filtro))
        .forEach(cliente => {
            const cantidadObras = obrasDeCliente(cliente.idCliente).length;
            tabla.innerHTML += `
                <tr>
                    <td>${cliente.nombreCliente}</td>
                    <td>${cliente.apellidoCliente}</td>
                    <td>${cliente.documento}</td>
                    <td>${cliente.emailCliente}</td>
                    <td><button class="badge-link" onclick="verObrasCliente(${cliente.idCliente})">${cantidadObras} obra${cantidadObras === 1 ? "" : "s"}</button></td>
                    <td class="acciones">${acciones("Cliente", cliente.idCliente)}</td>
                </tr>
            `;
        });
}

function verObrasCliente(idCliente) {
    document.getElementById("clienteDetalleObras").value = idCliente;
    renderizarObrasCliente();
    document.getElementById("detalleObrasCliente").scrollIntoView({ behavior: "smooth" });
}

function renderizarObrasCliente() {
    const contenedor = document.getElementById("detalleObrasCliente");
    if (!contenedor) return;

    const idCliente = Number(document.getElementById("clienteDetalleObras")?.value || 0);
    if (!idCliente) {
        contenedor.innerHTML = `<div class="empty-small">Seleccioná un cliente para ver sus obras.</div>`;
        return;
    }

    const obras = obrasDeCliente(idCliente);
    if (obras.length === 0) {
        contenedor.innerHTML = `<div class="empty-small">Este cliente todavía no tiene obras registradas.</div>`;
        return;
    }

    contenedor.innerHTML = obras.map(obra => `
        <div class="mini-item">
            <div><strong>${obra.nombreObra}</strong><span>${obra.estadoObra} · ${obra.direccionObra}</span></div>
            <div class="mini-total">${formatoMoneda(totalObra(obra.idObra))}</div>
        </div>
    `).join("");
}

async function guardarCliente(event) {
    event.preventDefault();

    const id = document.getElementById("idCliente").value;
    const cliente = {
        nombreCliente: document.getElementById("nombreCliente").value,
        apellidoCliente: document.getElementById("apellidoCliente").value,
        documento: document.getElementById("documento").value,
        direccionCliente: document.getElementById("direccionCliente").value,
        emailCliente: document.getElementById("emailCliente").value
    };

    try {
        if (id) {
            await enviarJson(`${API.cliente}/${id}`, "PUT", cliente);
        } else {
            await enviarJson(API.cliente, "POST", cliente);
        }

        limpiarFormularioCliente();
        await cargarTodo();
    } catch (error) {
        console.error("Error al guardar cliente:", error);
        mostrarModalError("Error", "Error al guardar cliente: " + error.message);
    }
}

function editarCliente(id) {
    const cliente = cache.clientes.find(c => Number(c.idCliente) === Number(id));
    if (!cliente) return;

    document.getElementById("idCliente").value = cliente.idCliente;
    document.getElementById("nombreCliente").value = cliente.nombreCliente;
    document.getElementById("apellidoCliente").value = cliente.apellidoCliente;
    document.getElementById("documento").value = cliente.documento;
    document.getElementById("direccionCliente").value = cliente.direccionCliente;
    document.getElementById("emailCliente").value = cliente.emailCliente;
    document.getElementById("tituloFormCliente").textContent = "Editar cliente";
    document.getElementById("seccion-clientes").scrollIntoView({ behavior: "smooth" });
}

async function eliminarCliente(id) {
    const confirmar = await mostrarModalConfirmacion("Eliminar cliente", "¿Estás seguro de que deseas eliminar este cliente?");
    if (!confirmar) return;

    try {
        await eliminarPorApi(`${API.cliente}/${id}`);
        await cargarTodo();
    } catch (error) {
    }
}

function limpiarFormularioCliente() {
    document.getElementById("formCliente").reset();
    document.getElementById("idCliente").value = "";
    document.getElementById("tituloFormCliente").textContent = "Registrar cliente";
}

// ================= OBRAS =================
function renderizarObras() {
    const tabla = document.getElementById("tablaObras");
    const filtro = normalizar(document.getElementById("buscarObra")?.value || "");
    const estadoFiltro = normalizar(document.getElementById("filtroEstadoObra")?.value || "");
    tabla.innerHTML = "";

    cache.obras
        .filter(obra => {
            const cliente = obra.cliente ? `${obra.cliente.nombreCliente} ${obra.cliente.apellidoCliente}` : obtenerNombreCliente(obra.idCliente);
            const coincideTexto = normalizar(`${obra.nombreObra} ${cliente} ${obra.estadoObra}`).includes(filtro);
            const coincideEstado = !estadoFiltro || normalizar(obra.estadoObra) === estadoFiltro;
            return coincideTexto && coincideEstado;
        })
        .forEach(obra => {
            const cliente = obra.cliente ? `${obra.cliente.nombreCliente} ${obra.cliente.apellidoCliente}` : obtenerNombreCliente(obra.idCliente);
            const gastos = gastosObra(obra.idObra);
            const materiales = costoMaterialesObra(obra.idObra);
            const personal = asignacionesObra(obra.idObra).length;
            tabla.innerHTML += `
                <tr>
                    <td>${obra.nombreObra}</td>
                    <td>${cliente}</td>
                    <td><span class="estado estado-proceso">${obra.estadoObra}</span></td>
                    <td>${formatoMoneda(obra.presupuesto)}</td>
                    <td>${formatoMoneda(gastos)}</td>
                    <td>${formatoMoneda(materiales)}</td>
                    <td><strong>${formatoMoneda(gastos + materiales)}</strong></td>
                    <td>${personal}</td>
                    <td class="acciones">${acciones("Obra", obra.idObra)}</td>
                </tr>
            `;
        });
}

async function guardarObra(event) {
    event.preventDefault();

    const id = document.getElementById("idObra").value;
    const obra = {
        nombreObra: document.getElementById("nombreObra").value,
        presupuesto: Number(document.getElementById("presupuesto").value),
        direccionObra: document.getElementById("direccionObra").value,
        fechaInicio: document.getElementById("fechaInicioObra").value,
        fechaFin: document.getElementById("fechaFinObra").value,
        estadoObra: document.getElementById("estadoObra").value,
        idCliente: Number(document.getElementById("idClienteObra").value)
    };

    try {
        if (!obra.idCliente) {
            return;
        }

        if (id) {
            await enviarJson(`${API.obra}/${id}`, "PUT", obra);
        } else {
            await enviarJson(API.obra, "POST", obra);
        }

        limpiarFormularioObra();
        await cargarTodo();
    } catch (error) {
    }
}

function editarObra(id) {
    const obra = cache.obras.find(o => Number(o.idObra) === Number(id));
    if (!obra) return;

    document.getElementById("idObra").value = obra.idObra;
    document.getElementById("nombreObra").value = obra.nombreObra;
    document.getElementById("presupuesto").value = obra.presupuesto;
    document.getElementById("direccionObra").value = obra.direccionObra;
    document.getElementById("fechaInicioObra").value = formatoFechaParaInput(obra.fechaInicio);
    document.getElementById("fechaFinObra").value = formatoFechaParaInput(obra.fechaFin);
    document.getElementById("estadoObra").value = obra.estadoObra;
    document.getElementById("idClienteObra").value = obra.idCliente;
    document.getElementById("tituloFormObra").textContent = "Editar obra";
    document.getElementById("seccion-obras").scrollIntoView({ behavior: "smooth" });
}

async function eliminarObra(id) {
    const confirmar = await mostrarModalConfirmacion("Eliminar obra", "¿Estás seguro de que deseas eliminar esta obra?");
    if (!confirmar) return;

    try {
        await eliminarPorApi(`${API.obra}/${id}`);
        await cargarTodo();
    } catch (error) {
    }
}

function limpiarFormularioObra() {
    document.getElementById("formObra").reset();
    document.getElementById("idObra").value = "";
    document.getElementById("tituloFormObra").textContent = "Registrar obra";
}

// ================= ASIGNACIONES =================
function renderizarAsignaciones() {
    const listaDisponibles = document.getElementById("listaEmpleadosAsignacion");
    const listaAsignados = document.getElementById("listaAsignadosObra");
    if (!listaDisponibles || !listaAsignados) return;

    const idObra = Number(document.getElementById("idObraAsignacion")?.value || 0);
    const filtroTexto = normalizar(document.getElementById("buscarEmpleadoAsignacion")?.value || "");
    const filtroOficio = Number(document.getElementById("filtroOficioAsignacion")?.value || 0);
    const filtroRol = document.getElementById("filtroRolAsignacion")?.value || "";

    if (!idObra) {
        listaDisponibles.innerHTML = `<div class="empty-small">Seleccioná una obra para asignar empleados.</div>`;
        listaAsignados.innerHTML = `<div class="empty-small">Todavía no hay una obra seleccionada.</div>`;
        return;
    }

    const asignados = asignacionesObra(idObra);
    const idsAsignados = new Set(asignados.map(a => Number(a.idUsuario)));

    const empleados = empleadosRegistrados().filter(usuario => {
        const oficio = usuario.oficio ? usuario.oficio.nombreOficio : obtenerNombreOficio(usuario.idOficio);
        const coincideTexto = normalizar(`${usuario.nombreUsuario} ${usuario.emailUsuario} ${oficio} ${usuario.tipoEmpleado}`).includes(filtroTexto);
        const coincideOficio = !filtroOficio || Number(usuario.idOficio) === filtroOficio;
        const coincideRol = !filtroRol || usuario.tipoEmpleado === filtroRol;
        return coincideTexto && coincideOficio && coincideRol;
    });

    listaDisponibles.innerHTML = empleados.length === 0
        ? `<div class="empty-small">No hay empleados registrados que coincidan con el filtro.</div>`
        : empleados.map(usuario => {
            const oficio = usuario.oficio ? usuario.oficio.nombreOficio : obtenerNombreOficio(usuario.idOficio);
            const yaAsignado = idsAsignados.has(Number(usuario.idUsuario));
            return `
                <div class="employee-item">
                    <div><strong>${usuario.nombreUsuario}</strong><span>${textoRol(usuario.tipoEmpleado)} · ${oficio} · ${usuario.emailUsuario}</span></div>
                    ${esAdmin() ? `<button class="btn ${yaAsignado ? "btn-light" : "btn-primary"}" ${yaAsignado ? "disabled" : ""} onclick="asignarEmpleado(${usuario.idUsuario})">${yaAsignado ? "Asignado" : "Asignar"}</button>` : ""}
                </div>
            `;
        }).join("");

    listaAsignados.innerHTML = asignados.length === 0
        ? `<div class="empty-small">Esta obra todavía no tiene empleados asignados.</div>`
        : asignados.map(asignacion => {
            const usuario = asignacion.usuario || cache.usuarios.find(u => Number(u.idUsuario) === Number(asignacion.idUsuario));
            const oficio = usuario?.oficio ? usuario.oficio.nombreOficio : obtenerNombreOficio(usuario?.idOficio);
            return `
                <div class="employee-item assigned">
                    <div><strong>${usuario?.nombreUsuario || "Usuario no encontrado"}</strong><span>${textoRol(usuario?.tipoEmpleado)} · ${oficio}</span></div>
                    ${esAdmin() ? `<button class="btn btn-danger" onclick="quitarAsignacion(${asignacion.idAsignacion})">Quitar</button>` : ""}
                </div>
            `;
        }).join("");
}

async function asignarEmpleado(idUsuario) {
    const idObra = Number(document.getElementById("idObraAsignacion").value);
    if (!idObra) {
        return;
    }

    try {
        await enviarJson(API.asignacion, "POST", { idUsuario, idObra });
        await cargarTodo();
    } catch (error) {
    }
}

async function quitarAsignacion(idAsignacion) {
    const confirmar = await mostrarModalConfirmacion("Quitar empleado", "¿Estás seguro de que deseas quitar este empleado de la obra?");
    if (!confirmar) return;

    try {
        await eliminarPorApi(`${API.asignacion}/${idAsignacion}`);
        await cargarTodo();
    } catch (error) {
    }
}

// ================= MATERIALES =================
function materialObraFilas() {
    const filas = cache.materialesObra.map(mo => ({
        materialObra: mo,
        material: mo.material || obtenerMaterial(mo.idMaterial),
        obra: mo.obra || cache.obras.find(o => Number(o.idObra) === Number(mo.idObra))
    }));

    const materialesRelacionados = new Set(cache.materialesObra.map(mo => Number(mo.idMaterial)));
    cache.materiales
        .filter(material => !materialesRelacionados.has(Number(material.idMaterial)))
        .forEach(material => filas.push({ materialObra: null, material, obra: null }));

    return filas;
}

function renderizarMateriales() {
    const tabla = document.getElementById("tablaMateriales");
    tabla.innerHTML = "";

    materialObraFilas().forEach(fila => {
        const material = fila.material;
        const materialObra = fila.materialObra;
        if (!material) return;

        const cantidad = materialObra?.cantidadMO || 0;
        const costo = normalizar(materialObra?.estadoMO || material.estadoMaterial) === "faltante" ? 0 : cantidad * Number(material.precioUnitario || 0);
        const obra = fila.obra ? fila.obra.nombreObra : "Sin obra asignada";
        const estado = materialObra?.estadoMO || material.estadoMaterial;
        const accionesMaterial = esAdmin()
            ? `<button class="btn btn-secondary" onclick="editarMaterialAsignado(${materialObra?.idMaterialObra || 0}, ${material.idMaterial})">Editar</button>
               <button class="btn btn-danger" onclick="eliminarMaterialAsignado(${materialObra?.idMaterialObra || 0}, ${material.idMaterial})">Eliminar</button>`
            : `<span class="estado estado-pendiente">Solo lectura</span>`;

        tabla.innerHTML += `
            <tr>
                <td>${material.nombreMaterial}</td>
                <td>${obra}</td>
                <td>${material.unidadMedida}</td>
                <td>${cantidad || "-"}</td>
                <td>${formatoMoneda(material.precioUnitario)}</td>
                <td><strong>${formatoMoneda(costo)}</strong></td>
                <td><span class="estado estado-activa">${estado}</span></td>
                <td class="acciones">${accionesMaterial}</td>
            </tr>
        `;
    });
}

async function guardarMaterial(event) {
    event.preventDefault();

    const idMaterial = document.getElementById("idMaterial").value;
    const idMaterialObra = document.getElementById("idMaterialObra").value;
    const idObra = Number(document.getElementById("idObraMaterial").value);
    const cantidadMO = Number(document.getElementById("cantidadMaterialObra").value);
    const estadoMO = document.getElementById("estadoMaterial").value;

    const material = {
        nombreMaterial: document.getElementById("nombreMaterial").value,
        unidadMedida: document.getElementById("unidadMedida").value,
        precioUnitario: Number(document.getElementById("precioUnitarioMaterial").value),
        estadoMaterial: estadoMO
    };

    try {
        if (!idObra) {
            return;
        }

        if (!cantidadMO || cantidadMO <= 0) {
            return;
        }

        let materialGuardado;

        if (idMaterial) {
            materialGuardado = await enviarJson(`${API.material}/${idMaterial}`, "PUT", material);
        } else {
            materialGuardado = await enviarJson(API.material, "POST", material);
        }

        const relacion = {
            idObra,
            idMaterial: Number(materialGuardado.idMaterial || idMaterial),
            cantidadMO,
            estadoMO
        };

        if (idMaterialObra) {
            await enviarJson(`${API.materialObra}/${idMaterialObra}`, "PUT", relacion);
        } else {
            await enviarJson(API.materialObra, "POST", relacion);
        }

        limpiarFormularioMaterial();
        await cargarTodo();
    } catch (error) {
    }
}

function editarMaterialAsignado(idMaterialObra, idMaterial) {
    const material = obtenerMaterial(idMaterial);
    const relacion = cache.materialesObra.find(mo => Number(mo.idMaterialObra) === Number(idMaterialObra));
    if (!material) return;

    document.getElementById("idMaterial").value = material.idMaterial;
    document.getElementById("idMaterialObra").value = relacion?.idMaterialObra || "";
    document.getElementById("idObraMaterial").value = relacion?.idObra || "";
    document.getElementById("nombreMaterial").value = material.nombreMaterial;
    document.getElementById("unidadMedida").value = material.unidadMedida;
    document.getElementById("precioUnitarioMaterial").value = material.precioUnitario;
    document.getElementById("cantidadMaterialObra").value = relacion?.cantidadMO || 1;
    document.getElementById("estadoMaterial").value = relacion?.estadoMO || material.estadoMaterial;
    document.getElementById("tituloFormMaterial").textContent = "Editar material";
    document.getElementById("seccion-materiales").scrollIntoView({ behavior: "smooth" });
}

async function eliminarMaterialAsignado(idMaterialObra, idMaterial) {
    const confirmar = await mostrarModalConfirmacion("Eliminar material", "¿Estás seguro de que deseas eliminar este material?");
    if (!confirmar) return;

    try {
        if (idMaterialObra) {
            await eliminarPorApi(`${API.materialObra}/${idMaterialObra}`);
        }

        await eliminarPorApi(`${API.material}/${idMaterial}`);
        await cargarTodo();
    } catch (error) {
    }
}

function limpiarFormularioMaterial() {
    document.getElementById("formMaterial").reset();
    document.getElementById("idMaterial").value = "";
    document.getElementById("idMaterialObra").value = "";
    document.getElementById("tituloFormMaterial").textContent = "Registrar material";
}

function editarMaterial(id) { editarMaterialAsignado(0, id); }
function eliminarMaterial(id) { eliminarMaterialAsignado(0, id); }

// ================= GASTOS =================
function renderizarGastos() {
    const tabla = document.getElementById("tablaGastos");
    tabla.innerHTML = "";

    cache.gastos.forEach(gasto => {
        const obra = gasto.obra ? gasto.obra.nombreObra : obtenerNombreObra(gasto.idObra);
        const tipo = gasto.tipoGasto ? gasto.tipoGasto.nombreTipoGasto : obtenerTipoGasto(gasto.idTipoGasto);
        tabla.innerHTML += `
            <tr>
                <td>${gasto.descGasto}</td>
                <td>${formatoMoneda(gasto.montoGasto)}</td>
                <td>${formatoFechaParaInput(gasto.fechaGasto)}</td>
                <td>${obra}</td>
                <td>${tipo}</td>
                <td class="acciones">${acciones("Gasto", gasto.idGasto)}</td>
            </tr>
        `;
    });
}

async function guardarGasto(event) {
    event.preventDefault();

    const id = document.getElementById("idGasto").value;
    const idObra = Number(document.getElementById("idObraGasto").value);
    const idTipoGasto = Number(document.getElementById("idTipoGasto").value);
    const monto = esTipoMateriales(idTipoGasto) ? costoMaterialesObra(idObra) : Number(document.getElementById("montoGasto").value);

    const gasto = {
        descGasto: document.getElementById("descGasto").value,
        montoGasto: monto,
        comprobanteGasto: document.getElementById("comprobanteGasto").value,
        fechaGasto: document.getElementById("fechaGasto").value,
        idObra,
        idTipoGasto
    };

    try {
        if (!gasto.idObra) {
            return;
        }

        if (!gasto.idTipoGasto) {
            return;
        }

        if (esTipoMateriales(gasto.idTipoGasto)) {
            gasto.montoGasto = costoMaterialesObra(gasto.idObra);
            document.getElementById("montoGasto").value = gasto.montoGasto;
            if (!gasto.descGasto) gasto.descGasto = "Materiales de obra";
        }

        if (id) {
            await enviarJson(`${API.gasto}/${id}`, "PUT", gasto);
        } else {
            await enviarJson(API.gasto, "POST", gasto);
        }

        limpiarFormularioGasto();
        await cargarTodo();
    } catch (error) {
    }
}

function editarGasto(id) {
    const gasto = cache.gastos.find(g => Number(g.idGasto) === Number(id));
    if (!gasto) return;

    document.getElementById("idGasto").value = gasto.idGasto;
    document.getElementById("descGasto").value = gasto.descGasto;
    document.getElementById("montoGasto").value = gasto.montoGasto;
    document.getElementById("comprobanteGasto").value = gasto.comprobanteGasto;
    document.getElementById("fechaGasto").value = formatoFechaParaInput(gasto.fechaGasto);
    document.getElementById("idObraGasto").value = gasto.idObra;
    document.getElementById("idTipoGasto").value = gasto.idTipoGasto;
    document.getElementById("tituloFormGasto").textContent = "Editar gasto";
    actualizarResumenGastoSeleccionado();
    document.getElementById("seccion-gastos").scrollIntoView({ behavior: "smooth" });
}

async function eliminarGasto(id) {
    const confirmar = await mostrarModalConfirmacion("Eliminar gasto", "¿Estás seguro de que deseas eliminar este gasto?");
    if (!confirmar) return;

    try {
        await eliminarPorApi(`${API.gasto}/${id}`);
        await cargarTodo();
    } catch (error) {
    }
}

function limpiarFormularioGasto() {
    document.getElementById("formGasto").reset();
    document.getElementById("idGasto").value = "";
    document.getElementById("montoGasto").readOnly = false;
    document.getElementById("tituloFormGasto").textContent = "Registrar gasto";
    actualizarResumenGastoSeleccionado();
}

function actualizarResumenGastoSeleccionado() {
    const idObra = Number(document.getElementById("idObraGasto")?.value || 0);
    const idTipoGasto = Number(document.getElementById("idTipoGasto")?.value || 0);
    const resumen = document.getElementById("resumenObraGasto");
    const montoInput = document.getElementById("montoGasto");

    if (!resumen || !montoInput) return;

    const materiales = idObra ? costoMaterialesObra(idObra) : 0;

    if (esTipoMateriales(idTipoGasto)) {
        montoInput.value = materiales;
        montoInput.readOnly = true;
        resumen.textContent = idObra
            ? `Tipo Materiales: se cargará automáticamente ${formatoMoneda(materiales)} según materiales comprados/usados en esta obra.`
            : "Seleccioná una obra para calcular automáticamente el monto de materiales.";
        return;
    }

    montoInput.readOnly = false;

    if (!idObra) {
        resumen.textContent = "Seleccioná una obra para ver su costo actual.";
        return;
    }

    resumen.textContent = `Costo actual de la obra: ${formatoMoneda(totalObra(idObra))} (${formatoMoneda(gastosObra(idObra))} en gastos generales + ${formatoMoneda(materiales)} en materiales).`;
}

// ================= ALERTAS =================
async function cargarAlertas() {
    try {
        const alertas = await cargarJson(`${API.base || ''}/api/alerta/activas`);
        cache.alertas = alertas || [];
        mostrarAlertas(cache.alertas);
        actualizarCountAlertas();
    } catch (error) {
        console.error("Error cargando alertas:", error);
    }
}

async function verificarAlertas() {
    try {
        await enviarJsonComoAdmin(`${API.base || ''}/api/alerta/verificar`, "POST", {});
        await cargarAlertas();
    } catch (error) {
    }
}

function mostrarAlertas(alertas) {
    const tabla = document.getElementById("tablaAlertas");
    if (!tabla) return;

    tabla.innerHTML = "";
    alertas.forEach(alerta => {
        const fila = document.createElement("tr");
        const tipoClass = alerta.tipoAlerta === "Agotado" ? "alert alert-danger" : "alert alert-warning";
        fila.innerHTML = `
            <td>${cache.obras.find(o => o.idObra === alerta.idObra)?.nombreObra || "Sin obra"}</td>
            <td>${alerta.materialObra?.material?.nombreMaterial || "Sin material"}</td>
            <td><span class="${tipoClass}">${alerta.tipoAlerta}</span></td>
            <td>${alerta.cantidadActual}</td>
            <td>${alerta.cantidadMinima}</td>
            <td>${new Date(alerta.fechaAlerta).toLocaleDateString()}</td>
            <td>${alerta.resuelta ? "Resuelta" : "Activa"}</td>
            <td>
                ${!alerta.resuelta ? `<button class="btn btn-sm btn-success" onclick="resolverAlerta(${alerta.idAlerta})">Resolver</button>` : ""}
            </td>
        `;
        tabla.appendChild(fila);
    });
}

async function resolverAlerta(idAlerta) {
    try {
        await enviarJsonComoAdmin(`${API.base || ''}/api/alerta/resolver/${idAlerta}`, "PUT", {});
        await cargarAlertas();
    } catch (error) {
    }
}

async function actualizarCountAlertas() {
    try {
        const resp = await cargarJson(`${API.base || ''}/api/alerta/contar-activas`);
        const badge = document.getElementById("countAlertasActivas");
        if (badge) {
            badge.textContent = resp.activas || 0;
        }
    } catch (error) {
        console.error("Error contando alertas:", error);
    }
}

// ================= AUDITORÍA =================
async function cargarAuditoria() {
    try {
        const tabla = document.getElementById("filtroTablaAuditoria")?.value || "";
        const accion = document.getElementById("filtroAccionAuditoria")?.value || "";

        let url = `${API.base || ''}/api/auditoria`;
        if (tabla) url += `/tabla/${tabla}`;

        const auditorias = await cargarJson(url);
        cache.auditorias = auditorias || [];

        if (accion) {
            cache.auditorias = cache.auditorias.filter(a => a.accion === accion);
        }

        mostrarAuditoria(cache.auditorias);
    } catch (error) {
        console.error("Error cargando auditoría:", error);
        // Sin mostrar cartel - solo dejamos el error en consola
    }
}

function mostrarAuditoria(auditorias) {
    const tabla = document.getElementById("tablaAuditoria");
    if (!tabla) return;

    tabla.innerHTML = "";
    auditorias.forEach(audit => {
        const fila = document.createElement("tr");
        const accionClass = audit.accion === "Create" ? "badge-success" : audit.accion === "Update" ? "badge-warning" : "badge-danger";
        fila.innerHTML = `
            <td>${audit.tabla}</td>
            <td><span class="badge ${accionClass}">${audit.accion}</span></td>
            <td>${audit.descripcionCambio}</td>
            <td>ID Usuario: ${audit.idUsuario}</td>
            <td>${new Date(audit.fechaHora).toLocaleString()}</td>
            <td><button class="btn btn-sm btn-info" onclick="mostrarDetallesAuditoria('${audit.tabla}', ${audit.idRegistro})">Ver cambios</button></td>
        `;
        tabla.appendChild(fila);
    });
}

async function mostrarDetallesAuditoria(tabla, idRegistro) {
    try {
        const cambios = await cargarJson(`${API.base || ''}/api/auditoria/registro/${tabla}/${idRegistro}`);
        const detalles = cambios.map(c => 
            `${c.accion} en ${c.tabla} (${new Date(c.fechaHora).toLocaleString()}):\n` +
            `Antes: ${c.datosAnteriores}\n` +
            `Después: ${c.datosNuevos}`
        ).join("\n\n");
    } catch (error) {
    }
}

// Agregar listeners para filtros
document.addEventListener("DOMContentLoaded", () => {
    document.getElementById("filtroTablaAuditoria")?.addEventListener("change", cargarAuditoria);
    document.getElementById("filtroAccionAuditoria")?.addEventListener("change", cargarAuditoria);
    document.getElementById("btnRefrescarAuditoria")?.addEventListener("click", cargarAuditoria);
});

