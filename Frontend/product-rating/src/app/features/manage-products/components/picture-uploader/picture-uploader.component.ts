import { Component, OnInit, Inject, Input } from '@angular/core';
import { PictureData } from 'src/app/models/PictureData';
import { PictureService } from '../../services/picture-service';

@Component({
  selector: 'app-picture-uploader',
  templateUrl: './picture-uploader.component.html',
  styleUrls: ['./picture-uploader.component.scss']
})
export class PictureUploaderComponent implements OnInit {

  @Input()
  picture: PictureData;

  constructor(private pictureservice: PictureService) { }

  processFile(imageInput: any) {
    const file: File = imageInput.files[0];
    const reader = new FileReader();

    reader.addEventListener('load', (event: any) => {    
      this.pictureservice.uploadImage(file).subscribe(
        (res) => {                
          this.picture.data = res.data;
          this.picture.id = res.id;      
        },
        (err) => {
          alert("Not OK");
          console.log(err);
        })
    });

    reader.readAsDataURL(file);
  }

  ngOnInit() {
  }

}
