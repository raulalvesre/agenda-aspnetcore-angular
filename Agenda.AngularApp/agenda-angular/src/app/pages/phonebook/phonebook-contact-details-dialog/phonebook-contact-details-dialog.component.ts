import { MatTableDataSource } from '@angular/material/table';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ContactApiResponse } from './../../../shared/interfaces/contact/contact-api-response';
import { Component, Inject, OnInit } from '@angular/core';
import { ContactTelephoneApiResponse } from 'src/app/shared/interfaces/contact/contact-telephone-api-response';

@Component({
  selector: 'app-phonebook-contact-details-dialog',
  templateUrl: './phonebook-contact-details-dialog.component.html',
  styleUrls: ['./phonebook-contact-details-dialog.component.scss']
})
export class PhonebookContactDetailsDialogComponent implements OnInit {
  contact: ContactApiResponse;
  dataSource: MatTableDataSource<ContactTelephoneApiResponse>;
  columnsToDisplay: string[] = ["id", "type", "phoneNumber", "description", "creationDate", "lastUpdateDate"];

  constructor(
    public matDialogRef: MatDialogRef<PhonebookContactDetailsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit() {
    this.contact = this.data.contact;
    this.dataSource = new MatTableDataSource(this.contact.telephones);
  }

}
