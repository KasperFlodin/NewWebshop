import { CommonModule } from '@angular/common';
import { Component, OnInit} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';

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

  constructor(
    private formbuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
  ) { 
    if (this.accountService.currentUserValue) 
    {this.router.navigate(['/'])}
  }

  ngOnInit(): void {
    this.form = this.formbuilder.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      phone: ['', Validators.required],
      address: ['', Validators.required],
      city: ['', Validators.required],
      zip: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
    })
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  onSubmit() {
    this.submitting = true;

    // reset alert on submit
    this.error = '';

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.accountService.register(this.form.value)
      .subscribe({
        next: () => {
          this.router.navigate(['/login']);
        },
        error: error => {
          this.error = error;
          this.loading = false;
        }
      });
  }

  cancel() {
    this.error = '';
    this.router.navigate(['/']);
  }

  registerForm: FormGroup = this.resetForm();

  resetForm(): FormGroup {
    return new FormGroup({
      firstname: new FormControl(null, Validators.required),
      lastname: new FormControl(null, Validators.required),
      phone: new FormControl(null, Validators.required),
      address: new FormControl(null, Validators.required),
      city: new FormControl(null, Validators.required),
      zip: new FormControl(null, [Validators.required, Validators.min(4)]),
      email: new FormControl(null, Validators.required),
      password: new FormControl(null, [Validators.required, Validators.min(4)]),
    });
  }

}
