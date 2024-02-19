import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './views/login/login.component';
import { AuthGuard } from './services/guard/auth.guard';
import { HomeComponent } from './views/home/home.component';
import { DefaultLayoutComponent } from './views/containers/default-layout/default-layout.component';
import { DashboardComponent } from './views/dashboard/dashboard.component';
import { DelyverymanCreateComponent } from './views/deliveryman-create/delyveryman-create.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'deliveryman',
    component: DelyverymanCreateComponent,
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', component: DashboardComponent },
      {
        path: 'vehicle',
        canActivate: [AuthGuard],
        loadChildren: () =>
          import('./views/vehicle/vehicle.module').then((m) => m.VehicleModule),
      },
      {
        path: 'order',
        canActivate: [AuthGuard],
        loadChildren: () =>
          import('./views/order/order.module').then((m) => m.OrderModule),
      },
      {
        path: 'account',
        loadChildren: () =>
          import('./views/account/accoutn.module').then((m) => m.AccoutnModule),
      },
      {
        path: 'plan',
        loadChildren: () =>
          import('./views/plan/plan.module').then((m) => m.PlanModule),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
