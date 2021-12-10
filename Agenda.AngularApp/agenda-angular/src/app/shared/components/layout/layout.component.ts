import { JwtTokenService } from './../../services/jwt-token.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  userRole: string;

  constructor(
    private jwtTokenService: JwtTokenService,
    private router: Router) { }

  ngOnInit() {
    this.userRole = this.jwtTokenService.getUserObjectFromToken()?.role;
  }

  logOut() {
    this.jwtTokenService.destroyToken();
    this.router.navigateByUrl('/login');
  }

}
