import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ContactManagementModule } from './pages/contact-management/contact-management.module';
import { LoginModule } from './pages/login/login.module';
import { PhonebookModule } from './pages/phonebook/phonebook.module';
import { UserManagementModule } from './pages/user-management/user-management.module';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    SharedModule,
    LoginModule,
    UserManagementModule,
    PhonebookModule,
    ContactManagementModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
