import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { Observable } from 'rxjs';
import { map , catchError} from 'rxjs/operators';

import { IEmployee } from './../models/employee';

@Injectable()
export class EmployeeService {
    constructor(private _http: Http) { }

    getEmployees(): Observable<IEmployee[]> {
        return this._http.get('api/Employee/GetAll')
            .pipe(map(resp => <IEmployee[]> resp.json()))
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}