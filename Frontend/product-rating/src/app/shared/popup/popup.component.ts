import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-popup',
  templateUrl: './popup.component.html',
  styleUrls: ['./popup.component.scss']
})
export class PopupComponent implements OnInit {

  @Input() title: string;
  @Input() body: string;
  @Input() negativeButtonVisible: boolean = false;
  @Input() positiveButtonText: string = "Ok";
  @Input() negativeButtonText: string = "Cancel";

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

  ok(){
    this.activeModal.close();
  }

  cancel(){
    this.activeModal.dismiss();
  }

}
