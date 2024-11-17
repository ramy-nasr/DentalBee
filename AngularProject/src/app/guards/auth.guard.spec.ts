import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { AuthService } from '../services/auth.service';
import { of } from 'rxjs';

describe('AuthGuard', () => {
  let authGuard: AuthGuard;
  let authService: jasmine.SpyObj<AuthService>;
  let router: jasmine.SpyObj<Router>;

  beforeEach(() => {
    // Create mock AuthService and Router
    authService = jasmine.createSpyObj('AuthService', ['isAuthenticated']);
    router = jasmine.createSpyObj('Router', ['navigate']);

    // Configure the testing module
    TestBed.configureTestingModule({
      providers: [
        AuthGuard,
        { provide: AuthService, useValue: authService },
        { provide: Router, useValue: router }
      ]
    });

    // Inject AuthGuard service
    authGuard = TestBed.inject(AuthGuard);
  });

  it('should allow access if the user is authenticated', () => {
    // Mock the isAuthenticated method to return true
    authService.isAuthenticated.and.returnValue(true);

    const result = authGuard.canActivate();

    expect(result).toBe(true); // Should allow access
    expect(router.navigate).not.toHaveBeenCalled(); // Router should not navigate
  });

  it('should deny access and redirect to login if the user is not authenticated', () => {
    // Mock the isAuthenticated method to return false
    authService.isAuthenticated.and.returnValue(false);

    const result = authGuard.canActivate();

    expect(result).toBe(false); // Should deny access
    expect(router.navigate).toHaveBeenCalledWith(['/login']); // Should navigate to the login page
  });
});