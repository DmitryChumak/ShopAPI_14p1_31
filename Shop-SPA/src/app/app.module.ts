import { BrowserModule, HammerGestureConfig, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import {
  BsDropdownModule,
  TabsModule,
  PaginationModule,
  ButtonsModule,
  CollapseModule,
  ModalModule
} from 'ngx-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { ProductsListComponent } from './product/products-list/products-list.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { AuthGuard } from './_guards/auth.guard';
import { AlertifyService } from './_services/alertify.service';
import { ProductService } from './_services/product.service';
import { ProductListResolver } from './_resolvers/product-list.resolver';
import { appRoutes } from './routes';
import { HasRoleDirective } from './_directives/hasRole.directive';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ProductCardComponent } from './product/products-list/product-card/product-card.component';

export function tokenGet() {
  return localStorage.getItem('token');
}

export class CustomHammerConfig extends HammerGestureConfig {
  overrides = {
    pinch: { enable: false },
    rotate: { enable: false }
  };
}

@NgModule({
   declarations: [
      AppComponent,
      ProductsListComponent,
      ProductCardComponent,
      NavComponent,
      HasRoleDirective,
      HomeComponent,
      RegisterComponent
   ],
   imports: [
      BrowserModule,
      FormsModule,
      HttpClientModule,
      ReactiveFormsModule,
      BsDropdownModule.forRoot(),
      PaginationModule.forRoot(),
      TabsModule.forRoot(),
      ButtonsModule.forRoot(),
      CollapseModule.forRoot(),
      ModalModule.forRoot(),
      RouterModule.forRoot(appRoutes, {
        onSameUrlNavigation: 'reload'
      }),
      JwtModule.forRoot({
      config: {
        tokenGetter: tokenGet,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth/authenticate']
      }
    }),
    BrowserAnimationsModule
  ],
  providers: [
    AuthService,
    AuthGuard,
    AlertifyService,
    ProductService,
    ProductListResolver,
    { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
