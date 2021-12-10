import { ContactManagementService } from './../contact-management.service';
import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-contact-management-deletion-dialog',
  templateUrl: './contact-management-deletion-dialog.component.html',
  styleUrls: ['./contact-management-deletion-dialog.component.scss']
})
export class ContactManagementDeletionDialogComponent {

  constructor(
    private contactManagementService: ContactManagementService,
    public dialogRef: MatDialogRef<ContactManagementDeletionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  deleteContact() {
    this.contactManagementService.delete(this.data.contactId)
      .subscribe(resp => this.dialogRef.close(true));
  }
}
