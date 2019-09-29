import { Directive, Output, EventEmitter, HostListener } from '@angular/core';

@Directive({
    selector: '[long-press]'
})
export class LongPress {
    timeout: any;

    @Output()
    onLongPress = new EventEmitter();

    @HostListener('touchstart', ['$event'])
    @HostListener('mousedown', ['$event'])
    onMouseDown(event) {       
        this.timeout = setTimeout(() => {
            this.onLongPress.emit(event);
        }, 500);
    }

    @HostListener('touchend')
    @HostListener('mouseup')
    @HostListener('mouseleave')
    endPress() {
        clearTimeout(this.timeout);
    }
}
