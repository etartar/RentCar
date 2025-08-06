import { inject, Injectable, signal } from '@angular/core';
import { DecodeModel, initialDecode } from '../models/decode.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class Common {
  readonly decode = signal<DecodeModel>({...initialDecode});
  
  readonly #router = inject(Router);

  checkPermission(permission: string){
    if (this.decode().role === "SysAdmin") return true;

    if (this.decode().permissions.some(i => i === permission)) return true;

    return false;
  }

  checkPermissionForRoute(permission: string){
    const res = this.checkPermission(permission);

    if (!res){
      this.#router.navigateByUrl('/unauthorize');
    }

    return res;
  }
}
