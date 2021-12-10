import { ContactManagementComponent } from './pages/contact-management/contact-management.component';
import { PhonebookComponent } from './pages/phonebook/phonebook.component';
import { Interceptor } from './shared/interceptors/interceptor';
import { UserManagementComponent } from './pages/user-management/user-management.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './pages/login/login.component';
import { LayoutComponent } from './shared/components/layout/layout.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'agenda',
    component: LayoutComponent,
    pathMatch: 'full',
    children: [{ path: '', component: PhonebookComponent }],
    canActivate: [AuthGuard],
    data: {
      allowedRoles: ['STANDARD USER'],
    },
  },
  {
    path: 'gerenciamento-usuarios',
    component: LayoutComponent,
    pathMatch: 'full',
    children: [{ path: '', component: UserManagementComponent }],
    canActivate: [AuthGuard],
    data: {
      allowedRoles: ['ADMIN'],
    },
  },
  {
    path: 'gerenciamento-contatos',
    component: LayoutComponent,
    pathMatch: 'full',
    children: [{ path: '', component: ContactManagementComponent }],
    canActivate: [AuthGuard],
    data: {
      allowedRoles: ['ADMIN'],
    },
  },
  {
    path: '',
    component: LoginComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: Interceptor, multi: true },
  ],
})
export class AppRoutingModule {}
