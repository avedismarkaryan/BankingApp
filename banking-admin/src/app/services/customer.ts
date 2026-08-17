import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Account { //API'den gelen veri şeklini TypeScript'e tarif ediyoruz (C#'taki DTO/Model gibi düşün).
  id: number;
  owner: string;
  balance: number;
}

@Injectable({
  providedIn: 'root',
})
export class Customer {
  private apiUrl = 'http://localhost:5036/api/accounts';

  constructor(private http: HttpClient) {} //Angular otomatik olarak HttpClient örneğini buraya enjekte ediyor

  getAll(): Observable<Account[]> {
    return this.http.get<Account[]>(this.apiUrl);
  }
}