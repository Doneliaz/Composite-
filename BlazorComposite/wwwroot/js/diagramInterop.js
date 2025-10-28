// wwwroot/js/diagramInterop.js

window.diagramInterop = {

    /**
     * Habilita el arrastre para un elemento SVG.
     * @param {string} elementId - El ID del elemento SVG a arrastrar.
     * @param {DotNetObjectReference} dotnetHelper - Referencia al componente Blazor para invocar métodos .NET.
     */
    enableDragging: function (elementId, dotnetHelper) {
        const element = document.getElementById(elementId);
        if (!element) return;

        let isDragging = false;
        let startX, startY;
        let initialElementX, initialElementY; // Para rastrear la posición inicial del elemento

        const onMouseDown = (e) => {
            // Solo arrastrar con el botón izquierdo
            if (e.button !== 0) return; 
            
            isDragging = true;
            // Guardar la posición inicial del ratón
            startX = e.clientX;
            startY = e.clientY;

            // Prevenir la selección de texto u otros comportamientos por defecto
            e.preventDefault(); 
            element.style.cursor = 'grabbing'; // Cambiar cursor
        };

        const onMouseMove = (e) => {
            if (!isDragging) return;

            // Calcular el desplazamiento (delta)
            const dx = e.clientX - startX;
            const dy = e.clientY - startY;

            // Llamar al método .NET para mover el elemento
            // Pasamos el ID del elemento y el desplazamiento
            dotnetHelper.invokeMethodAsync('MoveShape', elementId, dx, dy);

            // Actualizar la posición de inicio para el siguiente movimiento
            startX = e.clientX;
            startY = e.clientY;
        };

        const onMouseUp = (e) => {
            if (e.button !== 0) return;

            isDragging = false;
            element.style.cursor = 'grab'; // Volver al cursor normal
        };
        
        const onMouseLeave = (e) => {
            // Opcional: si quieres que se detenga si el ratón sale del SVG
            // isDragging = false;
            // element.style.cursor = 'grab';
        };

        // Asignar eventos
        element.addEventListener('mousedown', onMouseDown);
        
        // ¡Importante! Escuchar en 'document' o 'window' para mousemove y mouseup
        // Esto asegura que el arrastre continúe incluso si el ratón
        // se mueve fuera del elemento o del SVG.
        document.addEventListener('mousemove', onMouseMove);
        document.addEventListener('mouseup', onMouseUp);
        
        // Opcional: cambiar el cursor al pasar por encima
        element.style.cursor = 'grab';

        // NOTA: Esta implementación no incluye una función de "limpieza" (dispose)
        // que elimine los event listeners. Para una app de producción,
        // sería bueno añadirla.
    },

    /**
     * Obtiene el rectángulo delimitador (bounding box) de un elemento.
     * @param {Element} element - La referencia al elemento DOM (pasada desde Blazor).
     * @returns {DOMRect} - Un objeto con { x, y, top, left, width, height, etc. }
     */
    getBoundingClientRect: function (element) {
        if (element) {
            return element.getBoundingClientRect();
        }
        return null; // Devuelve null si el elemento no existe
    }
};