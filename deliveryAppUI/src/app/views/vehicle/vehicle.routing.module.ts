import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListVehicleComponent } from './list-vehicle/list-vehicle.component';
import { CreateVehicleComponent } from './create-vehicle/create-vehicle.component';
import { EditVehicleComponent } from './edit-vehicle/edit-vehicle.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'list',
      },
      {
        path: 'list',
        component: ListVehicleComponent,
      },
      {
        path: 'create',
        component: CreateVehicleComponent,
      },
      {
        path: 'edit/:plate',
        component: EditVehicleComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VehicleRoutingModule {}
