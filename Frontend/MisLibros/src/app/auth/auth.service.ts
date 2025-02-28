import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface RegisterDto {
  nombreUsuario: string;
  email: string;
  nombreCompleto: string;
  rol: string;
  password: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // Asegúrate de actualizar esta URL con la de tu API
  private baseUrl = 'https://localhost:7149/api/Users';

  constructor(private http: HttpClient) { }

  register(registerData: RegisterDto): Observable<any> {
    return this.http.post(`${this.baseUrl}/register`, registerData);
  }
}
