import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl, Validators
} from '@angular/forms';
import { Router } from '@angular/router';
import { BaseFormComponent } from 'src/app/shared/components/base-form/base-form.component';
import { UserLoginRequest } from '../../shared/interfaces/user-login-req';
import { JwtTokenService } from './../../shared/services/jwt-token.service';
import { LoginService } from './login.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends BaseFormComponent implements OnInit {
  apiError: string;

  constructor(
    private loginService: LoginService,
    private formBuilder: FormBuilder,
    private jwtTokenService: JwtTokenService,
    private router: Router
  ) {
    super();
  }

  ngOnInit(): void {
    this.formulario = this.formBuilder.group({
      username: [
        null,
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(16),
        ],
      ],
      password: [
        null,
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(30),
        ],
      ],
    });
  }

  submit() {
    this.fazerLogin();
  }

  private fazerLogin() {
    let userLoginRequest: UserLoginRequest = {
      username: this.formulario.value['username'],
      password: this.formulario.value['password'],
    };

    this.loginService.logIn(userLoginRequest).subscribe(
      (response: any) => {
        this.jwtTokenService.setToken(response.token);
        this.redirectToCorrectPage();
      },
      (errorResponse) => {
        this.apiError = errorResponse.error.message;
      }
    );
  }

  getFormControl(formControlName: string): FormControl {
    return this.formulario.get(formControlName) as FormControl;
  }

  private redirectToCorrectPage() {
    let userRole = this.jwtTokenService.getUserObjectFromToken().role;

    if (userRole.localeCompare('ADMIN') == 0) {
      this.router.navigate(['gerenciamento-usuarios']);
    } else {
      this.router.navigate(['agenda']);
    }
  }
}
