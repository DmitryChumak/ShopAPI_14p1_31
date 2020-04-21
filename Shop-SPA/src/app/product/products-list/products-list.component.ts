import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/_models/product';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ProductService } from 'src/app/_services/product.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})
export class ProductsListComponent implements OnInit {
  products: Product[];
  pagination: Pagination;
  productParams: any = {};
  isCollapsed = true;
  filterForm: FormGroup;
  order: string;
  categoryName: string;
  CountList = [
    { value: 12, display: '12' },
    { value: 24, display: '24' },
    { value: 48, display: '48' }
  ];
  OrderList = [
    { value: 'low', display: 'descending' },
    { value: 'high', display: 'ascending' }
  ];
  CategoryList = [
    { value: 'Smartphone', display: 'smartphones' },
    { value: 'Tablet', display: 'tablets' },
    { value: 'Printer', display: 'printers' },
    { value: 'EBook Reader', display: 'ebooks' },
    { value: 'Headphones', display: 'headphones' },
    { value: 'Notebook', display: 'notebooks' },
    { value: 'TV', display: 'tvs' },
    { value: 'Camera', display: 'cameras' }
  ];

  constructor(
    private productService: ProductService,
    private alertify: AlertifyService,
    private fb: FormBuilder,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.order = this.categoryName = '';
    this.route.data.subscribe(data => {
        // tslint:disable-next-line: no-string-literal
        this.products = data['products'].result;
        // tslint:disable-next-line: no-string-literal
        this.pagination = data['products'].pagination;
      });
    this.pagination.itemsPerPage = 12;
    this.createEditingForm();
    this.loadProducts();
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadProducts();
  }

  resetFilters() {
    this.order = this.categoryName = '';
    this.loadProducts();
  }

  createEditingForm() {
    this.filterForm = this.fb.group({});
  }

  loadProducts() {
    if (this.filterForm.valid) {
      this.productParams = Object.assign({}, this.filterForm.value);
      this.productParams.orderBy = this.order;
      this.productParams.categoryName = this.categoryName;
      this.productService
        .getProducts(
          this.pagination.currentPage,
          this.pagination.itemsPerPage,
          this.productParams
        )
        .subscribe(
          (res: PaginatedResult<Product[]>) => {
            this.isCollapsed = true;
            // tslint:disable-next-line: no-string-literal
            this.products = res.result;
            this.pagination = res.pagination;
          },
          error => {
            this.alertify.error(error);
          }
        );
    }
  }
}
