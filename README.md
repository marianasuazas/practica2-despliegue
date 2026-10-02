# Práctica 2 · Despliegue de Software

API REST de productos contenerizada con Docker y desplegada en Kubernetes local.

## Integrantes

- Daniel Zapata Ramírez
- María Paulina Vargas Lenis
- Mariana Suaza Serna
- Leonel Antonio Martínez Silgado
- Sebastián Ciro Medellín 

## Tecnología utilizada

- .NET 10 con ASP.NET Core Web API (controladores)
- Swagger con el paquete Swashbuckle.AspNetCore
- Docker Desktop con build multietapa
- Datos en memoria, sin base de datos

## Estructura del repositorio

- Practica2Api/: código fuente de la API
- Dockerfile: instrucciones para construir la imagen
- .dockerignore: archivos excluidos de la imagen

## Endpoints

- GET /api/productos: lista todos los productos
- GET /api/productos/{id}: consulta un producto
- POST /api/productos: crea un producto
- PUT /api/productos/{id}: actualiza un producto
- DELETE /api/productos/{id}: elimina un producto

## Parte 1 · Ejecución con Docker

Desde la raíz del repositorio:

    docker build -t practica2-api:v1 .
    docker images
    docker run -d -p 8080:8080 --name practica2-api practica2-api:v1
    docker ps

Puertos: 8080 en el host y 8080 dentro del contenedor.

Para probar la API abrir http://localhost:8080/swagger en el navegador.

Ejemplo de JSON para POST api/Productos

    {
      "id": 5,
      "nombre": "Pantalla",
      "precio": 500000,
      "stock": 10
    }


## Parte 2 · Kubernetes

Pendiente.

## Video

Pendiente: enlace al video de YouTube.
