import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders,  } from "@angular/common/http";
import { Observable } from "rxjs";


@Injectable()
export class HomeService {
  apiUrl: string = "http://localhost:5000/api/";
  formData: FormData = new FormData();

  constructor(private http: HttpClient) {
  }

  getAllProducts() {
    return this.http.get(this.apiUrl + "productcatalog");
  }

  insertProduct(data: any, filesList: File) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    this.formData = new FormData();
    for (const key in data) {
      if (data.hasOwnProperty(key)) {
        this.formData.append(key,(data as any)[key]);
      }
    }

    if (filesList != null) {
      this.formData.append(filesList.name,filesList);
    }

    let body = JSON.stringify(data);

    return this.http.post(this.apiUrl + "productcatalog/insert/", this.formData);

  }
}
