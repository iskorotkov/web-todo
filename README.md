# Web todo app

A simple To Do web app.

- [Web todo app](#web-todo-app)
  - [Tech stack](#tech-stack)
  - [Deploy](#deploy)

## Tech stack

- C# 9.0/.NET 5
- ASP.NET Core Web API
- SQL Server
- NGINX
- Docker
- Docker Compose or Kubernetes + Istio

## Deploy

1. [Setup] Install `kubectl` and `make`.

1. [Optional] Create namespace for your app:

    ```sh
    kubectl create namespace web-todo
    ```

1. Create secret with SQL Server password:

    ```sh
    kubectl create secret generic sqlserver -n web-todo --from-literal=SA_PASSWORD="Password-123"
    ```

1. Install Istio in your cluster. One of the possible ways to install it:

   1. [Optional] Install Istio CLI.
   1. [Optional] Run `istioctl install`.

1. Deploy app:

    ```sh
    make deploy
    ```

1. [Optional, Cleanup] To undeploy the app, use the following:

    ```sh
    make undeploy
    ```
