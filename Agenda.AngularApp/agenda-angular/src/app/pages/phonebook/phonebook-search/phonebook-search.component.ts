import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-phonebook-search',
  templateUrl: './phonebook-search.component.html',
  styleUrls: ['./phonebook-search.component.scss']
})
export class PhonebookSearchComponent {
  @Input() searchParamsForm: FormGroup;

  constructor() { }
  
}
