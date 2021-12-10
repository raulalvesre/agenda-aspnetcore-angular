import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ContactApiAdminResponse } from 'src/app/shared/interfaces/contact/contact-api-admin-response';
import { ContactTelephoneApiResponse } from 'src/app/shared/interfaces/contact/contact-telephone-api-response';

@Component({
  selector: 'app-contact-management-details',
  templateUrl: './contact-management-details.component.html',
  styleUrls: ['./contact-management-details.component.scss']
})
export class ContactManagementDetailsComponent implements OnInit {

  contact: ContactApiAdminResponse;
  dataSource: MatTableDataSource<ContactTelephoneApiResponse>;
  columnsToDisplay: string[] = ["id", "type", "phoneNumber", "description", "creationDate", "lastUpdateDate"];

  constructor(
    public matDialogRef: MatDialogRef<ContactManagementDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit() {
    this.contact = this.data.contact;
    this.dataSource = new MatTableDataSource(this.contact.telephones);
  }

}
