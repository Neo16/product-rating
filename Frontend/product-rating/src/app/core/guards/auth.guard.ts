
import { Injectable } from '@angular/core';
import { CanActivate, CanActivateChild, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild {

    constructor(private router: Router) {
    }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<boolean> | Promise<boolean> | boolean {

        const expectedRoles = next.data.expectedRoles;
        var userRoles = JSON.parse(localStorage.getItem('productrating-userroles')) as string[];

        var hasExpectedRole = false;
        if (userRoles) {
            expectedRoles.forEach((role: string) => {
                if (hasExpectedRole = userRoles.some(e => e == role)) {
                   hasExpectedRole = true;
                }
            });
        }

        if (hasExpectedRole) {
            return true;
        }
        else {
            this.router.navigate(['account', 'login']);
            return false;
        }

    }
    canActivateChild(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<boolean> | Promise<boolean> | boolean {
        return this.canActivate(next, state);
    }
}
