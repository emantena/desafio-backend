import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { VehicleRoutingModule } from './vehicle.routing.module';
import { CreateVehicleComponent } from './create-vehicle/create-vehicle.component';
import { ListVehicleComponent } from './list-vehicle/list-vehicle.component';
import { EditVehicleComponent } from './edit-vehicle/edit-vehicle.component';

@NgModule({
  declarations: [
    CreateVehicleComponent,
    ListVehicleComponent,
    EditVehicleComponent,
  ],

  imports: [
    CommonModule,
    ReactiveFormsModule,
    VehicleRoutingModule,
    FormsModule,
  ],
})
export class VehicleModule {}
