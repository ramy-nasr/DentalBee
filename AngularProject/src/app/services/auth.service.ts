import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7011/api/auth';

  constructor(private http: HttpClient, private router: Router) { }

  register(userData: { username: string, email: string, password: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, userData);
  }

  login(credentials: { username: string, password: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, credentials);
  }

    saveToken(token: string): void {
      localStorage.setItem('authToken', token);
    }
  
    getToken(): string | null {
      return localStorage.getItem('authToken');
    }
  
    logout(): void {
      localStorage.removeItem('authToken');
      this.router.navigate(['/login']);
    }
  
    isAuthenticated(): boolean {
      return !!this.getToken();
    } 
}
