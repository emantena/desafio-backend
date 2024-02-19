import { Component, OnInit } from '@angular/core';
import { PlanModel } from 'src/app/models/planModel';
import { BaseResponseModel } from 'src/app/models/response/baseResponseModel';
import { PlanService } from 'src/app/services/plan.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  plan: PlanModel[] = [];

  constructor(private planService: PlanService) {}

  ngOnInit() {
    this.getPlan();
  }

  getPlan() {
    this.planService.getPlansActives().subscribe((res: BaseResponseModel) => {
      this.plan = res.data as PlanModel[];
    });
  }
}
