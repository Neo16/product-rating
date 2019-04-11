import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnumToArrayPipe } from './pipes/enumToArray';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [EnumToArrayPipe],
  imports: [
    FormsModule,
    CommonModule
  ],
  exports: [
    FormsModule,
    EnumToArrayPipe
  ]
})
export class SharedModule { }
