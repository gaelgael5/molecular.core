
/**
* NOTE: This class is auto generated by the template angular2-typescript-service-interface.cshtml.
* Do not edit the class manually.
* * OpenAPI spec version: 0.
* Do not edit the class manually.
*/

import { Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Configuration } from '../configuration.model';

export interface IGenerator {
   defaultHeaders: Headers;
   configuration: Configuration;
   [others: string]: any;

      /**
      * SchemaApi.Controllers.GeneratorController.Index (SchemaApi)
      */

      Index(viewName : String) : Observable<{}>;
}

