import { Component, OnInit } from '@angular/core';
import { HomeService } from "./home.service";
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [HomeService]
})
export class HomeComponent implements OnInit {

  products: Array<any>;
  fileToUpload: any;
  productName: string;
  productPrice: number;

  constructor(private service: HomeService) {

  }

  ngOnInit() {
    this.service.getAllProducts().subscribe(res => {
      this.products = res as Array<any>;
      console.log(this.products);
    });
  }

  fileChange(data: any) {
    if (data.target.files.length > 0) {
      this.fileToUpload = data.target.files[0];
    }
  }

  submitData(data: NgForm) {

    var temp = new Products();

    temp.productName = data.value.productName;
    temp.productPrice = data.value.productPrice;
    //temp.image = this.fileToUpload;

    

    var name = this.productName;
    var price = this.productPrice;

    console.log(data);

    this.service.insertProduct(temp,this.fileToUpload).subscribe(res => {

    });
  }
  addProduct() {
    var name = this.productName;
    var price = this.productPrice;
  }

}


export class Products {
  productId: number;
  productName: string;
  productPrice: number;
  image: any;
}
