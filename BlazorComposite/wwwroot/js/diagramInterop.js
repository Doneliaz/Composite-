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

        const onMouseDown = (e) => {
            // Solo arrastrar con el botón izquierdo
            if (e.button !== 0) return; 
            
            isDragging = true;
            startX = e.clientX;
            startY = e.clientY;

            // Prevenir la selección de texto
            e.preventDefault(); 
            
            // --- ¡CAMBIO IMPORTANTE! ---
            // Detiene la propagación del evento.
            // Evita que el 'OnCanvasMouseDown' de Blazor se dispare.
            e.stopPropagation(); 
            // --------------------------

            element.style.cursor = 'grabbing';
        };

        const onMouseMove = (e) => {
            if (!isDragging) return;

            const dx = e.clientX - startX;
            const dy = e.clientY - startY;

            dotnetHelper.invokeMethodAsync('MoveShape', elementId, dx, dy);

            startX = e.clientX;
            startY = e.clientY;
        };

        const onMouseUp = (e) => {
            if (e.button !== 0) return;

            isDragging = false;
            element.style.cursor = 'grab';
        };
        
        // Asignar eventos
        element.addEventListener('mousedown', onMouseDown);
        document.addEventListener('mousemove', onMouseMove);
        document.addEventListener('mouseup', onMouseUp);
        
        element.style.cursor = 'grab';
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
        return null;
    }
};