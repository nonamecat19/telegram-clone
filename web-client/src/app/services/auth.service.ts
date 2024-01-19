import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";

const BASE_URL = "https://localhost:3001/api/v1"

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient)
  constructor() { }

  login(name: string, password: string) {
    return this.http.post(`${BASE_URL}/auth/login`, {
      Name: name,
      Password: password
    })
  }
}
