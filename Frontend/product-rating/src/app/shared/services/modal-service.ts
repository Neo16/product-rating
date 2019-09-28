import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PopupComponent } from '../popup/popup.component';

@Injectable()
export class ModalService {

    constructor(private ngModalService: NgbModal) { }

    openInformationModal(title: string, message: string): Promise<any> {
        var modalRef = this.ngModalService.open(PopupComponent);
        (modalRef.componentInstance as PopupComponent).title = title;
        (modalRef.componentInstance as PopupComponent).body = message; 
        
        return modalRef.result;
    }

    openConfirmationModal(title: string, message: string): Promise<any>  {
        var modalRef = this.ngModalService.open(PopupComponent);

        (modalRef.componentInstance as PopupComponent).title = title;
        (modalRef.componentInstance as PopupComponent).body = message;    
        (modalRef.componentInstance as PopupComponent).negativeButtonVisible = true;  

        return modalRef.result;
    }
}
