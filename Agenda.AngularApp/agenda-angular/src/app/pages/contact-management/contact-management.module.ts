import { FlexLayoutModule } from '@angular/flex-layout';
import { ContactManagementDetailsComponent } from './contact-management-details/contact-management-details.component';
import { ContactManagementDeletionDialogComponent } from './contact-management-deletion-dialog/contact-management-deletion-dialog.component';
import { ContactManagementFormDialogComponent } from './contact-management-form-dialog/contact-management-form-dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './../../shared/material/material.module';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactManagementComponent } from './contact-management.component';
import { ContactManagementSearchComponent } from './contact-management-search/contact-management-search.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    MaterialModule,
    FlexLayoutModule,
  ],
  declarations: [
    ContactManagementComponent,
    ContactManagementSearchComponent,
    ContactManagementFormDialogComponent,
    ContactManagementDeletionDialogComponent,
    ContactManagementDetailsComponent,
  ],
})
export class ContactManagementModule {}
