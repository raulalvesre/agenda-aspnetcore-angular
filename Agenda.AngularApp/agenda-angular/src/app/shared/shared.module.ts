import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { RouterModule } from '@angular/router';
import { MaterialModule } from './material/material.module';
import { LayoutComponent } from './components/layout/layout.component';
import { FormDebugComponent } from './components/form-debug/form-debug.component';
import { InputFieldComponent } from './components/input-field/input-field.component';
import { Input, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorMsgComponent } from './components/error-msg/error-msg.component';
import { ApiErrorComponent } from './components/api-error/api-error.component';

@NgModule({
  declarations: [
    InputFieldComponent,
    ApiErrorComponent,
    ErrorMsgComponent,
    FormDebugComponent,
    LayoutComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  exports: [
    InputFieldComponent,
    ApiErrorComponent,
    ErrorMsgComponent,
    FormDebugComponent,
    LayoutComponent,
  ],
})
export class SharedModule {}
