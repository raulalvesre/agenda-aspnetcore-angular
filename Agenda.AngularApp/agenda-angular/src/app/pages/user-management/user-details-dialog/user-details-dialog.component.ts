import { UserApiResponse } from '../../../shared/interfaces/user/user-api-response';
import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-user-details-dialog',
  templateUrl: './user-details-dialog.component.html',
  styleUrls: ['./user-details-dialog.component.scss']
})
export class UserDetailsDialogComponent implements OnInit {
  
  user: UserApiResponse;

  constructor(
    public dialogRef: MatDialogRef<UserDetailsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.user = this.data.user;
  }
  
}
