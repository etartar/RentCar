import { HttpClient, HttpContext, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ErrorService } from './error';
import { Result } from '../models/result.model';
import { SKIP_ERROR_HANDLER } from '../app.config';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  readonly #http = inject(HttpClient);
  readonly #error = inject(ErrorService);

  getResource<T>(endpoint: string){
    return this.#http.get<Result<T>>(endpoint, {
      context: new HttpContext().set(SKIP_ERROR_HANDLER, false)
    });
  }
  
  get<T>(endpoint: string, callBack: (res: T) => void, errorCallBack?: (err: HttpErrorResponse) => void){
    this.#http.get<Result<T>>(endpoint, {
      context: new HttpContext().set(SKIP_ERROR_HANDLER, true)
    }).subscribe({
      next: (res) => {
        callBack(res.data!);
      },
      error: (err: HttpErrorResponse) => {
        this.#error.handle(err);
        if (errorCallBack){
          errorCallBack(err);
        }
      }
    });
  }
  
  post<T>(endpoint: string, body: any, callBack: (res: T) => void, errorCallBack?: (err: HttpErrorResponse) => void){
    this.#http.post<Result<T>>(endpoint, body, {
      context: new HttpContext().set(SKIP_ERROR_HANDLER, true)
    }).subscribe({
      next: (res) => {
        callBack(res.data!);
      },
      error: (err: HttpErrorResponse) => {
        this.#error.handle(err);
        if (errorCallBack){
          errorCallBack(err);
        }
      }
    });
  }

  put<T>(endpoint: string, body: any, callBack: (res: T) => void, errorCallBack?: (err: HttpErrorResponse) => void){
    this.#http.put<Result<T>>(endpoint, body, {
      context: new HttpContext().set(SKIP_ERROR_HANDLER, true)
    }).subscribe({
      next: (res) => {
        callBack(res.data!);
      },
      error: (err: HttpErrorResponse) => {
        this.#error.handle(err);
        if (errorCallBack){
          errorCallBack(err);
        }
      }
    });
  }

  delete<T>(endpoint: string, callBack: (res: T) => void, errorCallBack?: (err: HttpErrorResponse) => void){
    this.#http.delete<Result<T>>(endpoint, {
      context: new HttpContext().set(SKIP_ERROR_HANDLER, true)
    }).subscribe({
      next: (res) => {
        callBack(res.data!);
      },
      error: (err: HttpErrorResponse) => {
        this.#error.handle(err);
        if (errorCallBack){
          errorCallBack(err);
        }
      }
    });
  }
}
