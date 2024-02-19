import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RentVehicleComponent } from './rent-vehicle/rent-vehicle.component';
import { PlanRoutingModule } from './plan.routing.module';

@NgModule({
  declarations: [RentVehicleComponent],

  imports: [CommonModule, ReactiveFormsModule, PlanRoutingModule, FormsModule],
})
export class PlanModule {}
