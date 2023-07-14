import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule],
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [
  ]
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  error = '';
  role?: string = '';

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private accountService: AccountService
  ) { }

  ngOnInit(): void {
    // redirect to home if already logged in
    if (this.accountService.currentUserValue != null && this.accountService.currentUserValue.id != null) {
      this.role = this.accountService.currentUserValue.role;
      this.router.navigate(['/']);
    }
  }

  login(): void {
    // reset alert on submit
    this.error = '';

    console.log(this.email, this.password);
    this.accountService.login(this.email, this.password)
      .subscribe({
        next: () => {
          // get return url from activatedRoute parameters, or default to home
          let returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/';
          this.router.navigate([returnUrl]);
        },
        error: err => {
          if (err.error?.status == 400 || err.error?.status == 401 || err.error?.status == 500) {
            this.error = 'Forkert brugernavn eller password';
          }
          else {
            this.error = err.error.title;
          }
        }
      });
  }

  register() {
    this.router.navigate(['register']);
  }

}
