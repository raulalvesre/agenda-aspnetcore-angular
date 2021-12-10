import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-contact-management-search',
  templateUrl: './contact-management-search.component.html',
  styleUrls: ['./contact-management-search.component.scss']
})
export class ContactManagementSearchComponent {

  @Input() searchParamsForm: FormGroup;

  constructor() { }

}
