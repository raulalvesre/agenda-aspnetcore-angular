import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, startWith, takeUntil, tap } from 'rxjs/operators';
import { ContactApiResponse } from 'src/app/shared/interfaces/contact/contact-api-response';
import { ApiRecordPage } from './../../shared/interfaces/api-record-page';
import { ContactApiAdminResponse } from './../../shared/interfaces/contact/contact-api-admin-response';
import { ContactAdminSearchParams } from './../../shared/interfaces/search-params/contact-admin-search-params';
import { ContactManagementDeletionDialogComponent } from './contact-management-deletion-dialog/contact-management-deletion-dialog.component';
import { ContactManagementDetailsComponent } from './contact-management-details/contact-management-details.component';
import { ContactManagementFormDialogComponent } from './contact-management-form-dialog/contact-management-form-dialog.component';
import { ContactManagementService } from './contact-management.service';

@Component({
  selector: 'app-contact-management',
  templateUrl: './contact-management.component.html',
  styleUrls: ['./contact-management.component.scss'],
})
export class ContactManagementComponent implements OnInit, OnDestroy {
  endSubscriptionsNotifier = new Subject();
  displayedColumns: string[] = ['id', 'name', 'first-telephone', 'actions'];
  contacts: MatTableDataSource<ContactApiAdminResponse>;
  searchParamsForm: FormGroup;
  telephoneTypes: any[];
  totalContacts: number;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private contactManagementService: ContactManagementService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.contacts = new MatTableDataSource<ContactApiAdminResponse>();

    this.contactManagementService
      .getTelephoneTypes()
      .subscribe((resp: any) => (this.telephoneTypes = resp));

    this.searchParamsForm = this.formBuilder.group({
      idUsuario: null,
      idContato: null,
      nomeContato: null,
      ddd: null,
      numeroTelefone: null,
    });

    this.searchParamsForm.valueChanges
      .pipe(
        startWith(this.searchParamsForm),
        debounceTime(300),
        distinctUntilChanged((prev, curr) => this.isSearchParamsUnchanged(prev, curr)),
        tap(() => this.paginator.firstPage()),
        takeUntil(this.endSubscriptionsNotifier)
      )
      .subscribe((params) => this.getContactPage());
  }

  private isSearchParamsUnchanged(prev: any, curr: any) {
    return (
      prev.idUsuario === curr.idUsuario &&
      prev.idContato === curr.idContato &&
      prev.nomeContato?.trim() === curr.nomeContato?.trim() &&
      prev.ddd === curr.ddd &&
      prev.numeroTelefone?.trim() === curr.numeroTelefone?.trim()
    );
  }

  getContactPage() {
    let contactSearchParams: ContactAdminSearchParams = this.searchParamsForm.value;
    contactSearchParams.take = 9;
    contactSearchParams.skip = this.paginator.pageIndex * contactSearchParams.take;

    this.contactManagementService.getPage(contactSearchParams).subscribe(
      (resp: ApiRecordPage<ContactApiAdminResponse>) => {
        this.contacts.data = resp.data;
        this.totalContacts = resp.total;
      },
      (error) => console.log('deu ruim na hora de buscar os contatos')
    );
  }

  openContactDetails(contact: ContactApiResponse) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.height = '90vh';
    dialogConfig.width = '60vw';
    dialogConfig.data = {
      contact: contact,
    };

    this.dialog.open(ContactManagementDetailsComponent, dialogConfig);
  }

  openContactFormDialog(contact: ContactApiResponse = null) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.height = '80vh';
    dialogConfig.width = '50vw';
    dialogConfig.data = {
      contact: contact,
      telephoneTypes: this.telephoneTypes,
    };

    this.dialog
      .open(ContactManagementFormDialogComponent, dialogConfig)
      .afterClosed()
      .subscribe((dialogResult) => {
        if (!!dialogResult) {
          let operation = dialogResult.operation == 'add' ? 'adicionado' : 'atualizado';
          this.getContactPage();
          this.snackBar.open(`Contato ${operation} com sucesso`, 'OK', { duration: 3000 });
        }
      });
  }

  openContactDeletionDialog(contactId: number) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.height = '30vh';
    dialogConfig.width = '33vw';
    dialogConfig.disableClose = true;
    dialogConfig.data = { contactId };

    this.dialog
      .open(ContactManagementDeletionDialogComponent, dialogConfig)
      .afterClosed()
      .subscribe((contactWasDeleted) => {
        if (contactWasDeleted) {
          this.getContactPage();
        }
      });
  }

  ngOnDestroy(): void {
    this.endSubscriptionsNotifier.next();
    this.endSubscriptionsNotifier.complete();
  }

}
