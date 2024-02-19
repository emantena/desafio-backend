import { Component, OnInit } from '@angular/core';
import { INavData, navItems } from '../_nav';
import { Observable } from 'rxjs';
import { UserModel } from 'src/app/models/userModel';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';
import { navDeliveryMan } from '../_navDeliveryMan';

@Component({
  selector: 'app-default-layout',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.css'],
})
export class DefaultLayoutComponent implements OnInit {
  public navItems: INavData[] = [];
  public user$: Observable<UserModel> | undefined;
  public initials = '';
  public name = '';
  public role = '';

  constructor(private _userService: UserService, private _router: Router) {
    this.user$ = _userService.getUser();
    this.initials = this._userService.getInitialName();
    this.name = this._userService.getUserName();
    this.role = this._userService.getRole();
  }

  ngOnInit(): void {
    if (this.role.toLocaleLowerCase() === 'deliveryman') {
      this.navItems = navDeliveryMan;
    } else {
      this.navItems = navItems;
    }
  }

  public logout() {
    this._userService.logout();
    this._router.navigate(['/login']);
  }
}
