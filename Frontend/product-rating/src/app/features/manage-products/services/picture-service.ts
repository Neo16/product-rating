import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PictureData } from 'src/app/models/PictureData';


@Injectable()
export class PictureService {
    private BASE_URL = 'https://localhost:44394';

    constructor(private http: HttpClient) { }

    public uploadImage(image: File): Observable<PictureData> {
        
        const url = `${this.BASE_URL}/files/upload-picture`;       

        const formData = new FormData();
        formData.append('file', image);    
        return this.http.post<PictureData>(url, formData);
      }
}