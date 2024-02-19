import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RentVehicleComponent } from './rent-vehicle/rent-vehicle.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'rent-vehicle',
      },
      {
        path: 'rent-vehicle',
        component: RentVehicleComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PlanRoutingModule {}
