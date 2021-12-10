import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { BaseFormComponent } from 'src/app/shared/components/base-form/base-form.component';
import { UserApiRequest } from '../../../shared/interfaces/user/user-api-request';
import { UserApiResponse } from '../../../shared/interfaces/user/user-api-response';
import { UserManagementService } from './../user-management.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.scss'],
})
export class UserFormComponent extends BaseFormComponent implements OnInit {
  user: UserApiResponse;
  roles: any[];

  constructor(
    private formBuilder: FormBuilder,
    private userManagementService: UserManagementService,
    public dialogRef: MatDialogRef<UserFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    super();
  }

  ngOnInit() {
    this.user = this.data.user;
    this.roles = this.data.roles;

    this.formulario = this.formBuilder.group({
      username: [
        this.user?.username,
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(16),
        ],
        this.usernameAlreadyRegistered.bind(this),
      ],
      name: [this.user?.name, [Validators.required, Validators.maxLength(200)]],
      email: [
        this.user?.email,
        [Validators.required, Validators.email],
        this.emailAlreadyRegistered.bind(this),
      ],
      password: [
        null,
        [
          Validators.required,
          Validators.minLength(6),
          this.passwordWithAtLeastOneNumber,
          Validators.maxLength(30),
        ],
      ],
      role: [null],
    });

    if (!!this.user) {
      this.formulario.get('role').setValue(this.getExistingUserRoleId());
    } else {
      this.formulario.get('role').setValue(this.roles[1].id);
    }
  }

  private getExistingUserRoleId(): number {
    let roleName = this.user.role;
    return this.roles.find((r) => r.name.localeCompare(roleName) == 0).id;
  }

  submit() {
    if (this.user == null) {
      this.addUser();
    } else {
      this.updateUser();
    }
  }

  private addUser() {
    let userApiRequest = this.createUserApiRequestFromFormValues();

    this.userManagementService.create(userApiRequest)
      .subscribe(r => {
        this.dialogRef.close({
          operation: 'add',
        });
      });
  }

  private createUserApiRequestFromFormValues(): UserApiRequest {
    let userApiRequest: UserApiRequest = {
      roleId: this.formulario.get('role').value,
      name: this.formulario.get('name').value,
      email: this.formulario.get('email').value,
      username: this.formulario.get('username').value,
      password: this.formulario.get('password').value,
    };
    
    return userApiRequest;
  }

  private updateUser() {
    let userId = this.user.id;
    let userApiRequest = this.createUserApiRequestFromFormValues();

    this.userManagementService.update(userId, userApiRequest)
      .subscribe(r => {
        this.dialogRef.close({
          operation: 'update',
        });
      });
  }

  passwordWithAtLeastOneNumber(
    formControl: AbstractControl
  ): { [key: string]: boolean } | null {
    if (!formControl.value) return null;

    if (!/\d/.test(formControl.value))
      return { passwordWithAtLeastOneNumber: true };

    return null;
  }

  usernameAlreadyRegistered(formControl: AbstractControl) {
    if (this.user?.username.localeCompare(formControl.value) == 0)
      return of(null);

    return this.userManagementService
      .isUsernameAlreadyRegistered(formControl.value)
      .pipe(
        map((usernameExists) =>
          usernameExists ? { alreadyRegistered: true } : null
        )
      );
  }

  emailAlreadyRegistered(formControl: AbstractControl) {
    if (this.user?.email.localeCompare(formControl.value) == 0) 
      return of(null);

    return this.userManagementService
      .isEmailAlreadyRegistered(formControl.value)
      .pipe(
        map((emailExists) =>
          emailExists ? { alreadyRegistered: true } : null
        )
      );
  }

}
