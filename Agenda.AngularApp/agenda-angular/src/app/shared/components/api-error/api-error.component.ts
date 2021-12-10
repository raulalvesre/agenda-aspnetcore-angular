import { Component, Input } from '@angular/core';

import { ApiBadResponse } from './../../interfaces/api-bad-response';


@Component({
  selector: 'app-api-error',
  templateUrl: './api-error.component.html',
  styleUrls: ['./api-error.component.scss']
})
export class ApiErrorComponent {

  @Input() apiError: ApiBadResponse = { hasError: false };

  constructor() { }

}
