import { Component, Inject, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { debounceTime, map, startWith, switchMap, takeUntil, tap, distinctUntilChanged } from 'rxjs/operators';
import { ContactFormBaseComponent } from 'src/app/shared/components/contact-form-base/contact-form-base.component';
import { ContactApiAdminResponse } from 'src/app/shared/interfaces/contact/contact-api-admin-response';
import { ContactApiAdminRequest } from './../../../shared/interfaces/contact/contact-api-admin-request';
import { SearchParamsBase } from './../../../shared/interfaces/search-params/search-params-base';
import { TokenInformation } from './../../../shared/interfaces/token-information';
import { JwtTokenService } from './../../../shared/services/jwt-token.service';
import { UserManagementService } from './../../user-management/user-management.service';
import { ContactManagementService } from './../contact-management.service';

@Component({
  selector: 'app-contact-management-form-dialog',
  templateUrl: './contact-management-form-dialog.component.html',
  styleUrls: ['./contact-management-form-dialog.component.scss'],
})
export class ContactManagementFormDialogComponent
  extends ContactFormBaseComponent<ContactApiAdminResponse>
  implements OnInit
{

  filteredUsers: Observable<any>;
  currentUser: TokenInformation;

  get telephones() {
    return this.formulario.get('telephones') as FormArray;
  }

  constructor(
    formBuilder: FormBuilder,
    contactManagementService: ContactManagementService,
    jwtTokenService: JwtTokenService,
    dialogRef: MatDialogRef<ContactManagementFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data: any,
    private userService: UserManagementService
  ) {
    super(formBuilder, contactManagementService, dialogRef, data);
    this.currentUser = jwtTokenService.getUserObjectFromToken();
  }

  ngOnInit() {
    this.contact = this.data.contact;

    this.formulario = this.formBuilder.group({
      name: [
        this.contact?.name,
        [Validators.required, Validators.maxLength(200)],
      ],
      telephones: this.formBuilder.array([]),
      owner: [null, [Validators.required, this.validUser]],
    });

    this.filteredUsers = this.formulario.get('owner').valueChanges
      .pipe(
        tap(x => this.triggerAllTelephonesValidation()),
        startWith(''),
        debounceTime(400),
        distinctUntilChanged(),
        switchMap((value: string) => {
          let params = { username: value } as SearchParamsBase;
          return this.userService.getPage(params);
        }),
        map((userPage) => userPage.data),
        takeUntil(this.endSubscriptionsNotifier)
      );

    if (this.contact != null) {
      this.setExistingContactOwner(); 
      this.populateTelephoneArray();
    } else {
      this.setOwnerToCurrentUser();
    }
  }

  private setExistingContactOwner() {
    this.formulario.get('owner').setValue({
      id: this.contact.owner.id,
      username: this.contact.owner.username,
    });
  }

  private setOwnerToCurrentUser() {
    this.formulario.get('owner').setValue({
      id: this.currentUser.id,
      username: this.currentUser.username,
    });
  }

  private triggerAllTelephonesValidation() {
    for (let telGroup of this.telephones.controls) {
      let realTelGroup = telGroup as FormGroup;
      realTelGroup.controls.telephoneNumber.updateValueAndValidity();
    }
  }

  isTelephoneNumberAlreadyRegistered(formControl: AbstractControl) {
    let telephone = formControl?.value;
    if (this.contact?.telephones.some(t => t.telephoneFormatted.localeCompare(telephone) == 0))
      return of(null);

    let ownerId = this.formulario.get('owner').value.id;

    return this.service.isTelephoneNumberAlreadyRegistered(telephone, ownerId)
      .pipe(
        map(isAlreadyRegistered => isAlreadyRegistered ? { alreadyRegistered: true } : null)
      );
  }

  private validUser(formControl: AbstractControl) {
    if (formControl.value == null) return null;

    const userSelected = formControl.value;

    if (!userSelected.id || !userSelected.username) {
      return { invalidUser: true };
    }

    return null;
  }

  displayFn(user: any) {
    return user ? user.username : undefined;
  }

  createContactApiRequest() {
    let contactApiRequest: ContactApiAdminRequest = {
      ownerId: this.formulario.get('owner').value.id,
      name: this.formulario.get('name').value,
      telephones: this.formulario.get('telephones').value,
    };

    return contactApiRequest;
  }
}
