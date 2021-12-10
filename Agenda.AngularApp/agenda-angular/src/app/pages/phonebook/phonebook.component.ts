import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, startWith, takeUntil, tap } from 'rxjs/operators';
import { ContactApiResponse } from 'src/app/shared/interfaces/contact/contact-api-response';
import { ContactSearchParams } from './../../shared/interfaces/search-params/contact-search-params';
import { PhonebookContactDetailsDialogComponent } from './phonebook-contact-details-dialog/phonebook-contact-details-dialog.component';
import { PhonebookDeletionDialogComponent } from './phonebook-deletion-dialog/phonebook-deletion-dialog.component';
import { PhonebookFormDialogComponent } from './phonebook-form-dialog/phonebook-form-dialog.component';
import { PhonebookService } from './phonebook.service';

@Component({
  selector: 'app-phonebook',
  templateUrl: './phonebook.component.html',
  styleUrls: ['./phonebook.component.scss']
})
export class PhonebookComponent implements OnInit, OnDestroy {
  endSubscriptionsNotifier = new Subject();
  displayedColumns: string[] = ["id", "name", "first-telephone", "actions"];
  contacts: MatTableDataSource<ContactApiResponse>;
  totalContacts: number;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  searchParamsForm: FormGroup;
  telephoneTypes: any[];

  constructor(
    private phonebookService: PhonebookService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.contacts = new MatTableDataSource<ContactApiResponse>();
    
    this.phonebookService.getTelephoneTypes()
      .subscribe((resp: any) => this.telephoneTypes = resp);

    this.searchParamsForm = this.formBuilder.group({
      idContato: null,
      nomeContato: null,
      ddd: null,
      numeroTelefone: null
    });

    this.searchParamsForm.valueChanges
      .pipe(
        startWith(this.searchParamsForm),
        debounceTime(300),
        distinctUntilChanged((prev, curr) => this.isSearchParamsUnchanged(prev, curr)),
        tap(() => this.paginator.firstPage()),
        takeUntil(this.endSubscriptionsNotifier)
      )
      .subscribe(params => this.getContactPage());
  }

  private isSearchParamsUnchanged(prev: any, curr: any) {
    return (
      prev.idContato === curr.idContato &&
      prev.nomeContato?.trim() === curr.nomeContato?.trim() &&
      prev.ddd === curr.ddd &&
      prev.numeroTelefone?.trim() === curr.numeroTelefone?.trim()
      );
  }

  getContactPage() {
    let contactSearchParams: ContactSearchParams = this.searchParamsForm.value;
    contactSearchParams.take = 9;
    contactSearchParams.skip = (this.paginator.pageIndex) * contactSearchParams.take;

    this.phonebookService.getPage(contactSearchParams)
      .subscribe(
        (resp) => {
          this.contacts.data = resp.data;
          this.totalContacts = resp.total;
        },
        (error) => console.log('deu ruim na hora de buscar os contatos')
      );
  }

  openContactDetails(contact: ContactApiResponse) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.height = '80vh';
    dialogConfig.width = '60vw';
    dialogConfig.data = {
      contact: contact,
    };

    this.dialog.open(PhonebookContactDetailsDialogComponent, dialogConfig);
  }

  openContactFormDialog(contact: ContactApiResponse = null) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.height = '80vh';
    dialogConfig.width = '50vw';
    dialogConfig.data = {
      contact: contact,
      telephoneTypes: this.telephoneTypes
    };

    this.dialog.open(PhonebookFormDialogComponent, dialogConfig)
      .afterClosed().subscribe(dialogResult => {
        if (!!dialogResult) {
          let operation = dialogResult.operation == 'add' ? 'adicionado' : 'atualizado'
          this.getContactPage();
          this.snackBar.open(`Contato ${operation} com sucesso`, 'OK', { duration: 3000 });
        }
      });
  }

  openContactDeletionDialog(contactId: number) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.height = '26vh';
    dialogConfig.width = '33vw';
    dialogConfig.disableClose = true;
    dialogConfig.data = { contactId };

    this.dialog.open(PhonebookDeletionDialogComponent, dialogConfig)
      .afterClosed().subscribe(contactWasDeleted => {
        if (contactWasDeleted) {
          this.getContactPage()
        }
      });
  }

  ngOnDestroy(): void {
    this.endSubscriptionsNotifier.next();
    this.endSubscriptionsNotifier.complete();
  }

}
