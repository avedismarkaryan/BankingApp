import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Feature {
  id: number;
  name: string;
  isEnabled: boolean;
}

@Injectable({
  providedIn: 'root',
})
export class FeatureService {
  private apiUrl = 'http://localhost:5036/api/features';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Feature[]> {
    return this.http.get<Feature[]>(this.apiUrl);
  }

  update(id: number, isEnabled: boolean): Observable<Feature> {
    return this.http.patch<Feature>(`${this.apiUrl}/${id}`, isEnabled);
  }
}