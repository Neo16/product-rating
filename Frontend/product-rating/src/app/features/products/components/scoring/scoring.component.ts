import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-scoring',
  templateUrl: './scoring.component.html',
  styleUrls: ['./scoring.component.scss']
})
export class ScoringComponent implements OnInit {

  @Output() addScore: EventEmitter<number> = new EventEmitter();
  @Input() scored: number | null;

  newScore: number = 0;
  closeResult: string;

  constructor(private modalService: NgbModal) { }

  open(content) {
    this.modalService.open(content, { centered: true }).result.then((result) => {
      this.addScore.emit(this.newScore);
    }, (reason) => {

    });
  }

  ngOnInit() {
    if (this.scored) {
      this.newScore = this.scored;
    }
  }

}
