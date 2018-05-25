import { Inject, Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { API_URL } from './shared/tokens/api.url.token';
@Injectable()
export class AppService {
    private _serviceUrl: string;

    constructor(
        @Inject(API_URL)
        private baseUrl: string,
        private _http: Http
    ) { 
        this._serviceUrl = baseUrl + 'api/hello';
    }

    sayHello(): Observable <string>  {
        return this._http.get(this._serviceUrl)
            .pipe(map(resp => resp.text()));
    }
}