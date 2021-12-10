import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, startWith, takeUntil, tap } from 'rxjs/operators';
import { UserApiResponse } from '../../shared/interfaces/user/user-api-response';
import { UserSearchParams } from './../../shared/interfaces/search-params/user-search-params';
import { UserDeletionDialogComponent } from './user-deletion-dialog/user-deletion-dialog.component';
import { UserDetailsDialogComponent } from './user-details-dialog/user-details-dialog.component';
import { UserFormComponent } from './user-form-dialog/user-form.component';
import { UserManagementService } from './user-management.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss'],
})
export class UserManagementComponent implements OnInit, OnDestroy {
  endSubscriptionsNotifier = new Subject();
  displayedColumns: string[] = [
    'id',
    'role',
    'username',
    'actions'
  ];
  users: MatTableDataSource<UserApiResponse>;
  totalUsers: number;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  searchParamsForm: FormGroup;
  userRoles: any[];

  constructor(
    private userManagementService: UserManagementService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit() {
    this.userManagementService.getUserRoles().subscribe((resp: any) => {
      this.userRoles = resp;
    });

    this.searchParamsForm = this.formBuilder.group({
      id: null,
      roleId: null,
      name: null,
      email: null,
      username: null,
    });

    this.searchParamsForm.valueChanges
      .pipe(
        startWith(this.searchParamsForm),
        debounceTime(500),
        distinctUntilChanged((prev, curr) => this.isSearchParamsUnchanged(prev, curr)),
        tap(() => this.paginator.firstPage()),
        takeUntil(this.endSubscriptionsNotifier)
      )
      .subscribe((params) => this.getUserPage());
  }

  private isSearchParamsUnchanged(prev: any, curr: any) {
    console.log(prev, curr);
    return (
      prev.id === curr.id &&
      prev.roleId === curr.roleId &&
      prev.name?.trim() === curr.name?.trim() &&
      prev.email?.trim() === curr.email?.trim() &&
      prev.username?.trim() === curr.username?.trim()
    );
  }

  getUserPage() {
    let userSearchParams: UserSearchParams = this.searchParamsForm.value;
    userSearchParams.take = 9;
    userSearchParams.skip = this.paginator.pageIndex * userSearchParams.take;

    this.userManagementService.getPage(userSearchParams).subscribe(
      (resp) => {
        this.users = new MatTableDataSource(resp.data);
        this.totalUsers = resp.total;
      },
      (error) => console.log('erro na hora de buscar contatos na API')
    );
  }

  openUserDetailsDialog(user: UserApiResponse) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose;
    dialogConfig.height = '51vh';
    dialogConfig.width = '35vw';
    dialogConfig.data = { user };

    this.dialog.open(UserDetailsDialogComponent, dialogConfig);
  }

  openUserFormDialog(user: UserApiResponse = null) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.height = '78vh';
    dialogConfig.width = '40vw';
    dialogConfig.data = {
      user: user,
      roles: this.userRoles,
    };

    this.dialog
      .open(UserFormComponent, dialogConfig)
      .afterClosed()
      .subscribe((dialogResult) => {
        if (!!dialogResult) {
          let operation = dialogResult.operation == 'add' ? 'adicionado' : 'atualizado';
          this.getUserPage();
          this.snackBar.open(`Usuário ${operation} com sucesso`, 'OK', {
            duration: 3000,
          });
        }
      });
  }

  openUserDeletionDialog(userId: number = 0) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.height = '26vh';
    dialogConfig.width = '33vw';
    dialogConfig.disableClose = true;
    dialogConfig.data = { userId };

    this.dialog
      .open(UserDeletionDialogComponent, dialogConfig)
      .afterClosed()
      .subscribe((userWasRemoved) => {
        if (userWasRemoved) {
          this.getUserPage();
        }
      });
  }

  ngOnDestroy(): void {
    this.endSubscriptionsNotifier.next();
    this.endSubscriptionsNotifier.complete();
  }

}
