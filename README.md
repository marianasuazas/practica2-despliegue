# Práctica 2 · Despliegue de Software

O sea, básicamente en esta práctica construimos una **API REST de productos** en .NET 10, la metimos en un contenedor con **Docker** y luego, tipo, la desplegamos todita en un cluster local de **Kubernetes (K8s)**.

---

## Integrantes

- Daniel Zapata Ramírez
- María Paulina Vargas Lenis
- Mariana Suaza Serna
- Leonel Antonio Martínez Silgado
- Sebastián Ciro Medellín 

---

## Tecnología utilizada

Bueno, digamos que las tecnologías que usamos para armar todo esto fueron:

- **.NET 10 (ASP.NET Core Web API):** O sea, la API base estructurada con controladores.
- **Swagger (Swashbuckle):** Para probar los endpoints interactiva y fácilmente desde el navegador.
- **Docker Desktop:** Para armar la imagen del proyecto usando *build multietapa* (tipo para que no quede pesada).
- **Kubernetes (K8s):** Para orquestar y desplegar el contenedor en un entorno local usando manifiestos YAML.
- **Datos en memoria:** Nada de bases de datos complejas por ahora, todo vive en memoria local.

---

## Estructura del repositorio

Tipo, la estructura del proyecto nos quedó ordenada así:

- `Practica2Api/`: Código fuente de la API en C#.
- `Dockerfile`: Las instrucciones para construir la imagen de Docker.
- `.dockerignore`: Para no meter archivos innecesarios en la imagen.
- `k8s/`: Carpeta con los manifiestos YAML para Kubernetes (`namespace.yaml`, `deployment.yaml`, `service.yaml`).
- `evidencias/`: Capturas de pantalla comprobando que todo sí funcionó.

---

## Endpoints de la API

O sea, los endpoints que tiene la API para gestionar los productos son:

- `GET /api/productos` -> Lista todos los productos.
- `GET /api/productos/{id}` -> Busca un producto específico por ID.
- `POST /api/productos` -> Crea un nuevo producto.
- `PUT /api/productos/{id}` -> Actualiza un producto existente.
- `DELETE /api/productos/{id}` -> Elimina un producto.

---

## Parte 1 · Ejecución con Docker

Bueno, para correr la API de forma individual en Docker, abres la terminal en la raíz del repo y tiras estos comandos, tipo así:

```bash
# 1. Construir la imagen de Docker
docker build -t practica2-api:v1 .

# 2. Ver que la imagen sí haya quedado guardada
docker images

# 3. Poner a correr el contenedor mapeando el puerto 8080
docker run -d -p 8080:8080 --name practica2-api practica2-api:v1

# 4. Confirmar que el contenedor esté vivito y corriendo
docker ps
```

Puertos: O sea, el puerto `8080` de tu PC se conecta directo al puerto `8080` del contenedor.

Para verificar, abres el navegador en `http://localhost:8080/swagger` y listo.

Ejemplo de JSON para enviar en el `POST /api/Productos`:

```json
{
  "id": 5,
  "nombre": "Pantalla",
  "precio": 500000,
  "stock": 10
}
```

### Evidencia de Docker Desktop

Aquí se ve tipo el contenedor de la API corriendo bien en Docker Desktop:

![Contenedores en Docker Desktop](./evidencias/4.png)

---

## Parte 2 · Despliegue en Kubernetes (K8s)

Bueno, aquí viene la parte interesante. O sea, para el paso 2 ya no corremos el contenedor suelto en Docker, sino que se lo entregamos a **Kubernetes** para que él se encargue de administrarlo.

### 1. Estructura de los Manifiestos (YAML)

Eh, dentro de la carpeta `k8s/` creamos tres archivos YAML esenciales:

- `namespace.yaml`: Tipo para crear un espacio aislado llamado `practica2` y no revolver nada con el resto del cluster.
- `deployment.yaml`: Define el Pod con nuestra imagen `practica2-api:v1`, configurándole recursos de CPU y memoria (1 réplica).
- `service.yaml`: Crea un servicio tipo `NodePort` para exponer la API hacia afuera en el puerto local `30080`.

Así se ven los archivos listos en el editor:

![Archivos de configuración K8s](./evidencias/6.png)

---

### 2. Comandos para aplicar la configuración en K8s

O sea, para desplegar todo en Kubernetes, ejecutas estos comandos en la terminal:

```bash
# Crear el namespace
kubectl apply -f k8s/namespace.yaml

# Crear el Deployment (el Pod con la API)
kubectl apply -f k8s/deployment.yaml

# Crear el Servicio para acceder desde afuera
kubectl apply -f k8s/service.yaml
```

*O si quieres tirar todo de una:* `kubectl apply -f k8s/`

---

### 3. Verificación del despliegue en K8s

Nada, para revisar que todo esté funcionando al 100%, tiramos estos comandos de comprobación:

#### a) Verificar los Pods
Ejecutas `kubectl get pods -n practica2` y te debe mostrar el pod en estado `Running`:

![Pod en ejecución](./evidencias/1.png)

#### b) Verificar el Servicio
Ejecutas `kubectl get svc -n practica2` para comprobar que el puerto `8080` de la API esté mapeado al `30080` de tu máquina:

![Servicio NodePort en K8s](./evidencias/2.png)

#### c) Vista desde Docker Desktop (Sección Kubernetes)
O sea, si entras a la pestaña de Kubernetes en Docker Desktop, seleccionas el namespace `practica2` y puedes ver cómo el Deployment y el Pod están activos:

![Kubernetes en Docker Desktop](./evidencias/5.png)

---

### 4. Prueba final en el navegador (Swagger en K8s)

Bueno, ya con el servicio `NodePort` levantado en el puerto `30080`, solo es abrir el navegador e ir a:

`http://localhost:30080/swagger/index.html`

Y listo, como ven en la imagen, ya la API está respondiendo perfectamente dentro del cluster de Kubernetes:

![Swagger UI corriendo en Kubernetes](./evidencias/3.png)

---

## Video Demostrativo

Nada, en este enlace pueden ver el video donde explicamos todo el paso a paso del despliegue:

- [Pendiente: Enlace al video de YouTube]
