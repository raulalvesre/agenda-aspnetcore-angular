import { ContactApiResponse } from 'src/app/shared/interfaces/contact/contact-api-response';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ContactFormBaseComponent } from 'src/app/shared/components/contact-form-base/contact-form-base.component';
import { ContactApiRequest } from './../../../shared/interfaces/contact/contact-api-request';
import { PhonebookService } from './../phonebook.service';

@Component({
  selector: 'app-phonebook-form-dialog',
  templateUrl: './phonebook-form-dialog.component.html',
  styleUrls: ['./phonebook-form-dialog.component.scss']
})
export class PhonebookFormDialogComponent 
  extends ContactFormBaseComponent<ContactApiResponse> 
  implements OnInit 
{
  
  constructor(
    formBuilder: FormBuilder,
    phonebookService: PhonebookService,
    dialogRef: MatDialogRef<PhonebookFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data: any
  ) {
    super(formBuilder, phonebookService, dialogRef, data);
  }

  ngOnInit() {
    this.contact = this.data.contact;

    this.formulario = this.formBuilder.group({
      name: [this.contact?.name, [Validators.required, Validators.maxLength(200)]],
      telephones: this.formBuilder.array([])
    });

    if (this.contact != null)
      this.populateTelephoneArray();
  }

  createContactApiRequest() {
    let contactApiRequest: ContactApiRequest = {
      name: this.formulario.get('name').value,
      telephones: this.formulario.get('telephones').value
    };

    return contactApiRequest;
  }

}
