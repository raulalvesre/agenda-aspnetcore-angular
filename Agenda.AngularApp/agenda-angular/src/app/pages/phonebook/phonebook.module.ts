import { PhonebookContactDetailsDialogComponent } from './phonebook-contact-details-dialog/phonebook-contact-details-dialog.component';
import { PhonebookDeletionDialogComponent } from './phonebook-deletion-dialog/phonebook-deletion-dialog.component';
import { PhonebookFormDialogComponent } from './phonebook-form-dialog/phonebook-form-dialog.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SharedModule } from './../../shared/shared.module';
import { MaterialModule } from './../../shared/material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PhonebookComponent } from './phonebook.component';
import { PhonebookSearchComponent } from './phonebook-search/phonebook-search.component';

@NgModule({
  declarations: [
    PhonebookComponent,
    PhonebookSearchComponent,
    PhonebookFormDialogComponent,
    PhonebookDeletionDialogComponent,
    PhonebookContactDetailsDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    SharedModule,
    FlexLayoutModule
  ]
})
export class PhonebookModule { }
