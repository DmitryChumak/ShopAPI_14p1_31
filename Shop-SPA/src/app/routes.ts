import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { ProductsListComponent } from './product/products-list/products-list.component';
import { ProductListResolver } from './_resolvers/product-list.resolver';

export const appRoutes: Routes = [
    { path: 'home', component: HomeComponent },
    {
      path: '',
      runGuardsAndResolvers: 'always',
      canActivate: [AuthGuard],
      children: [
        {
          path: 'products',
          component: ProductsListComponent,
          resolve: { products: ProductListResolver }
        }
      ]
    },
    { path: '**', redirectTo: 'home', pathMatch: 'full' }
  ];
