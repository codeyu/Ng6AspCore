import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { AppService } from './app.service';

import { EmployeeListComponent } from './employees/employee-list.component';
import { EmployeeService } from './employees/employee.service';

import { environment } from '../environments/environment';
import { API_URL } from './shared/tokens/api.url.token';

@NgModule({
  declarations: [
    AppComponent,
    EmployeeListComponent
  ],
  imports: [
    BrowserModule,
    HttpModule
  ],
  providers: [AppService, EmployeeService, { provide: API_URL, useValue: environment.apiPath }],
  bootstrap: [AppComponent]
})
export class AppModule { }
