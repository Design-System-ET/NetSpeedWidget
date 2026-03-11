# NetSpeedWidget

**NetSpeedWidget** es un widget ligero de escritorio desarrollado en C# (.NET / WinForms) que permite visualizar una prueba de velocidad de internet directamente desde el escritorio utilizando WebView2.

El widget carga una página de speedtest personalizada dentro de una ventana sin bordes, permitiendo integrarlo visualmente como un pequeño monitor de red en el escritorio.

### Tecnologías utilizadas

- C#
- .NET / Windows Forms
- Microsoft WebView2
- JavaScript (inyección para modificar DOM)

### Requisitos

- Para ejecutar la aplicación es necesario tener instalado:
- .NET Runtime compatible con el proyecto
- Microsoft Edge WebView2 Runtime

Descarga WebView2 Runtime:
```
https://developer.microsoft.com/en-us/microsoft-edge/webview2/
```

### Instalación

Clonar el repositorio:
```git clone
https://github.com/Design-System-ET/NetSpeedWidget.git
```
### Abrir el proyecto en:
```
Visual Studio
```
Compilar y ejecutar.

### Uso

Al iniciar la aplicación:

1. El widget se posiciona automáticamente en la parte inferior derecha de la pantalla.
2. Se carga la página de Speedtest dentro del WebView.
3. Se aplican modificaciones visuales mediante scripts:

  - eliminación de logos
  - ocultación de selectores
  - personalización del fondo

El widget puede moverse arrastrando la ventana.

### Personalización

El proyecto permite modificar la página cargada utilizando JavaScript inyectado desde C#, por ejemplo:

- eliminar elementos del DOM
- cambiar estilos
- ocultar componentes

Esto permite adaptar el widget para distintos servicios de speedtest.

### Licencia

Este proyecto está distribuido bajo la licencia MIT.
