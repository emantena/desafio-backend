import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  AbstractControl,
  FormControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';

import { LoginModel } from '../../models/loginModel';
import { LoginErrorModel } from '../../models/loginErrorModel';
import { ResponseModel } from '../../models/responseModel';
import { AuthService } from '../../services/auth.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  public errorMessage = '';

  loginForm: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });
  submitted = false;

  constructor(
    private _authService: AuthService,
    private _router: Router,
    private _userService: UserService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  get f(): { [key: string]: AbstractControl } {
    return this.loginForm.controls;
  }

  public autenticar(): void {
    this.errorMessage = '';

    const loginModel = new LoginModel();

    loginModel.email = this.loginForm!.get('username')!.value;
    loginModel.password = this.loginForm.get('password')!.value;

    if (loginModel.email === '' || loginModel.password === '') {
      this.errorMessage = 'informe login e senha';
      return;
    }

    this._authService.login(loginModel).subscribe(
      () => {
        this._router.navigate(['']);
      },
      (err: ResponseModel) => {
        console.log(err);
        if (err.error) {
          const errorMessage = JSON.stringify(err.error);
          const loginErrorModel = JSON.parse(errorMessage) as LoginErrorModel;
          this.errorMessage = loginErrorModel.message;
        }
      }
    );
  }
}
