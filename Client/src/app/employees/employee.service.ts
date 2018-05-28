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
        this._serviceUrl = baseUrl + 'api/Employee/';
    }

    getEmployees(): Observable<IEmployee[]> {
        return this._http.get(this._serviceUrl)
            .pipe(map(resp => <IEmployee[]> resp.json()))
    }
    //添加
    addEmployee(employee){
        return this._http.post(this._serviceUrl,employee)
        .pipe(map(resp => resp.json()))
    }
    //删除
    deleteEmployee(id){
        return this._http.delete(this._serviceUrl+id)
        .pipe(map(resp => resp.json()))
    }
    updateEmployee(employee){
        return this._http.put(this._serviceUrl+employee.id,employee)
        .pipe(map(resp => resp.json()))
    }
    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}