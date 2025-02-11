import { Injectable } from '@angular/core';
import { AES, enc } from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  public isExisting = (key: string) : boolean => !localStorage.getItem(key)

  public save = (key: string, value: string) => localStorage.setItem(key, this.encrypt(key, value))

  public get(key: string) : string {
    let data = ''
    let value = localStorage.getItem(key)
    
    if(value)
      data = this.decrypt(key, value)
    return data ? data : ''
  }

  public saveData = (key: string, data: any) => this.save(key, JSON.stringify(data))

  public getData = (key: string) => {
    let d = this.get(key)
    if(!d)
      return null
    return JSON.parse(d)
  }

  public remove = (key: string) => localStorage.removeItem(key)

  public clearAll = () => localStorage.clear()

  private encrypt = (key: string, strToEncrypt: string) => AES.encrypt(strToEncrypt, key).toString()

  private decrypt = (key: string, strToDecrypt: string) => AES.decrypt(strToDecrypt, key).toString(enc.Utf8)
}
