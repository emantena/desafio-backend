import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  public setLocalStorageItem(key: string, value: string) {
    window.localStorage.setItem(key, value);
  }

  public getLocalStorageItem(key: string): string {
    return window.localStorage.getItem(key) ?? '';
  }

  public removeLocalStorageItem(key: string) {
    window.localStorage.removeItem(key);
  }
}
