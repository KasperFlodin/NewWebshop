import { CommonModule } from '@angular/common';
import { Component, OnInit} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User, resetUser } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  selector: 'app-register',
  templateUrl: './register.component.html',
  styles: [
  ]
})

export class RegisterComponent implements OnInit {
  form!: FormGroup;
  loading = false;
  submitting = false;
  error?: string;
  user: User = this.resetUser();
  users: User[] = [];

  constructor(
    private formbuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private userService: UserService,
  ) { 
    if (this.accountService.currentUserValue) 
    {this.router.navigate(['/'])}
  }

  ngOnInit(): void {
    // this.form = this.formbuilder.group({
    //   firstName: ['', Validators.required],
    //   lastName: ['', Validators.required],
    //   phone: ['', Validators.required],
    //   address: ['', Validators.required],
    //   city: ['', Validators.required],
    //   zip: ['', Validators.required],
    //   email: ['', Validators.required],
    //   password: ['', [Validators.required, Validators.minLength(4)]],
    // })

    this.userService.getAll().subscribe(x => this.users = x);

  }

  // convenience getter for easy access to form fields
  // get f() { return this.form.controls; }

  // onSubmit() {
  //   this.submitting = true;

  //   // reset alert on submit
  //   this.error = '';

  //   // stop here if form is invalid
  //   if (this.form.invalid) {
  //     return;
  //   }
  //   this.loading = true;
  //   this.accountService.register(this.form.value)
  //     .subscribe({
  //       next: () => {
  //         this.router.navigate(['/login']);
  //       },
  //       error: error => {
  //         this.error = error;
  //         this.loading = false;
  //       }
  //     });
  // }

  save(): void {
    this.error = '';
    if (this.user.id == 0) {
      this.userService.create(this.registerForm.value).subscribe({
        next: (x) => {
          this.users.push(x);
          this.user = this.resetUser();
        },
        error: (err) => {
          console.warn(Object.values(err.console.error.errors).join(', '));
        }
      });
    }
    // else {
    //   // update
    //   this.userService.update(this.user.id, this.registerForm.value).subscribe({
    //     error: (err) => {
    //       console.warn(Object.values(err.error.errors).join(', '));
    //       // err.error.errors dykker ind i arrayet for at finde errors stringen inden i error
    //     },
    //     complete: () => {
    //       this.userService.getAll().subscribe(x => this.users = x);
    //       this.user = resetUser();
    //     }
    //   });
    // }
  }

  resetUser(): User {
    return {firstName: '', lastName: '', phone: 0, address: '', city: '', zip: 0, email: ''};
  }

  cancel() {
    this.error = '';
    this.router.navigate(['/']);
  }

  registerForm: FormGroup = this.resetForm();

  resetForm(): FormGroup {
    return new FormGroup({
      firstName: new FormControl(null, Validators.required),
      lastName: new FormControl(null, Validators.required),
      phone: new FormControl(null, Validators.required),
      address: new FormControl(null, Validators.required),
      city: new FormControl(null, Validators.required),
      zip: new FormControl(null, [Validators.required, Validators.min(4)]),
      email: new FormControl(null, Validators.required),
      // password: new FormControl(null, [Validators.required, Validators.min(4)]),
    });
  }

}
