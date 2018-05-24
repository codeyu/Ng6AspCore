import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class AppService {
    private _serviceUrl = 'api/hello';

    constructor(private _http: Http) { }

    sayHello(): Observable <string>  {
        return this._http.get(this._serviceUrl)
            .pipe(map(resp => resp.text()));
    }
}