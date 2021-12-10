import { UserManagementService } from './../user-management.service';
import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-user-deletion-dialog',
  templateUrl: './user-deletion-dialog.component.html',
  styleUrls: ['./user-deletion-dialog.component.scss']
})
export class UserDeletionDialogComponent {
  userId: number;

  constructor(private userManagementService: UserManagementService,
    public dialogRef: MatDialogRef<UserDeletionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  deleteUser() {
    this.userManagementService.delete(this.data.userId)
      .subscribe(resp => this.dialogRef.close(true));
  }

}
