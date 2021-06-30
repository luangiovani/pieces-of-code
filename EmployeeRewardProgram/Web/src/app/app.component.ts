import { Component, ChangeDetectorRef, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AuthenticationService } from './_services';
declare function initApp(): any;

@Component({
    selector: 'app-root',
    templateUrl: 'app.component.html'
})
export class AppComponent implements OnInit {
    currentUser: any;
    title: string;

    constructor(
        private changeDetectorRef: ChangeDetectorRef,
        private router: Router,
        private authenticationService: AuthenticationService
    ) {
        if (this.authenticationService.currentUser !== null &&
            this.authenticationService.currentUser !== undefined) {
                this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
                this.title = 'Employee Reward Program - Web Application';

                router.events.subscribe(event => {
                    if (event instanceof NavigationEnd) {
                    this.ngOnInit();
                    }
                    // Instance of should be:
                    // NavigationEnd
                    // NavigationCancel
                    // NavigationError
                    // RoutesRecognized
                });
        } else {
            this.router.navigate(['/login']);
        }
    }

    ngOnInit() {
        this.changeDetectorRef.detectChanges();
        initApp();
    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }
}
