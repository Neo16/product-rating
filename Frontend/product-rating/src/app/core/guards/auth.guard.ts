
import { Injectable, Injector } from '@angular/core';
import { CanActivate, CanActivateChild, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from '../services/account.service';

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild {

    private authService: AccountService;

    constructor(private router: Router, private injector: Injector) {
        this.authService = this.injector.get(AccountService);     
    }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<boolean> | Promise<boolean> | boolean {

        const expectedRoles = next.data.expectedRoles;
        var userRoles = this.authService.getUserRoles();

        var hasExpectedRole = false;
        if (userRoles) {
            expectedRoles.forEach((role: string) => {
                if (userRoles.some(e => e == role)) {
                   hasExpectedRole = true;
                }
            });
        }
        if (!hasExpectedRole) {
            this.router.navigate(['account', 'login']);
        }                   
        return hasExpectedRole;    
    }
    
    canActivateChild(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<boolean> | Promise<boolean> | boolean {
        return this.canActivate(next, state);
    }
}
