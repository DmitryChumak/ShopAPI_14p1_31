import { Component, OnInit, Input } from '@angular/core';
import { Product } from 'src/app/_models/product';
import { AuthService } from 'src/app/_services/auth.service';
import { ProductService } from 'src/app/_services/product.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  @Input() product: Product;
  constructor(
    private productService: ProductService,
    private alertify: AlertifyService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  deleteProduct() {
    this.alertify.confirm('Are you sure you want to delete the product?', () => {
      this.productService
        .deleteProduct(this.product.id)
        .subscribe(
          next => {
            this.alertify.success('The product was deleted!');
            this.router.navigateByUrl('/home', { skipLocationChange: true }).then(() => {
              this.router.navigate(['/products']);
            });
          },
          error => {
            this.alertify.error('Problems with delete operation!');
          }
        );
    });
  }

}
