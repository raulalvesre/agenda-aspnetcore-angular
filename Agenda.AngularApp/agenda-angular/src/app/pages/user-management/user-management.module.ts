import { FlexLayoutModule } from '@angular/flex-layout';
import { MaterialModule } from './../../shared/material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './../../shared/shared.module';
import { UserSearchComponent } from './user-search/user-search.component';
import { UserDetailsDialogComponent } from './user-details-dialog/user-details-dialog.component';
import { UserFormComponent } from './user-form-dialog/user-form.component';
import { UserManagementComponent } from './user-management.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserDeletionDialogComponent } from './user-deletion-dialog/user-deletion-dialog.component';

@NgModule({
    declarations: [
        UserManagementComponent,
        UserFormComponent,
        UserDeletionDialogComponent,
        UserDetailsDialogComponent,
        UserSearchComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        MaterialModule,
        FormsModule,
        ReactiveFormsModule,
        FlexLayoutModule,
    ]
})
export class UserManagementModule { }