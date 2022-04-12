
export function DragableWidget() { 
        var containers = document.querySelectorAll('.draggable-zone');

        if (containers.length === 0) {
            return false;
        }

        var swappable = new Sortable.default(containers, {
            draggable: '.draggable',
            handle: '.draggable .draggable-handle',
            mirror: {
                //appendTo: selector,
                appendTo: 'body',
                constrainDimensions: true
            }
        });
  
}

export function AccordianCollapse(id) {
    $(`#${id}`).collapse('hide');
}

export function ShowCollapse(id) {
    $(`#${id}`).collapse('show');
}
