import { Component, OnInit } from '@angular/core';
import { IAdvice } from 'src/app/models/advice';
import { AdvicesService } from 'src/app/services/advices.service';

@Component({
  selector: 'app-advices',
  templateUrl: './advices.component.html',
  styleUrls: ['./advices.component.css'],
  providers: [AdvicesService]
})
export class AdvicesComponent implements OnInit {

  private readonly advicesService: AdvicesService;

  public advices: IAdvice[];

  constructor(advicesService: AdvicesService) {
    this.advicesService = advicesService;
  }

  ngOnInit(): void {
    this.advices = this.advicesService.getAdvices();
  }

}
