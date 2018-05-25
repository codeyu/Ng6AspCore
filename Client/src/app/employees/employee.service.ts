import { Inject, Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { Observable } from 'rxjs';
import { map , catchError} from 'rxjs/operators';

import { IEmployee } from './../models/employee';

import { API_URL } from '../shared/tokens/api.url.token';
@Injectable()
export class EmployeeService {
    private _serviceUrl: string;

    constructor(
        @Inject(API_URL)
        private baseUrl: string,
        private _http: Http
    ) { 
        this._serviceUrl = baseUrl + 'api/Employee/GetAll';
    }

    getEmployees(): Observable<IEmployee[]> {
        return this._http.get(this._serviceUrl)
            .pipe(map(resp => <IEmployee[]> resp.json()))
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}