import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PhonebookService } from './../phonebook.service';
import { Component, Inject, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-phonebook-deletion-dialog',
  templateUrl: './phonebook-deletion-dialog.component.html',
  styleUrls: ['./phonebook-deletion-dialog.component.scss']
})
export class PhonebookDeletionDialogComponent {
  endSubscriptionsNotifier = new Subject();

  constructor(
    private phonebookService: PhonebookService,
    public dialogRef: MatDialogRef<PhonebookDeletionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  deleteContact() {
    this.phonebookService.delete(this.data.contactId)
      .subscribe(resp => this.dialogRef.close(true));
  }

}
