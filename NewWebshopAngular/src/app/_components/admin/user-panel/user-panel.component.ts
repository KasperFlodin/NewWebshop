import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { User, resetUser } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';

@Component({
  standalone: true,
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  imports: [CommonModule, FormsModule],
  styles: [
  ]
})
export class UserPanelComponent implements OnInit {
  message: string ='';
  users: User[] = [];
  user: User = resetUser();

  constructor(
    private userService: UserService,
  ) { }

  ngOnInit(): void {
    this.userService.getAll().subscribe(u => this.users = u);
  }

  edit(user: User): void {
    this.user = {
      id: user.id,
      firstname: user.firstname,
      lastname: user.lastname,
      city: user.city,
      address: user.address,
      zip: user.zip,
      phone: user.phone,
      email: user.email,
    }
  }

  delete(user: User): void {
    if (confirm('Er du sikker på du vil slette?')) {
      this.userService.delete(user.id).subscribe(() => {
        this.users = this.users.filter(x => x.id != user.id)
      });
    }
  }

  cancel(): void {
    this.message = '';
    this.user = resetUser();
  }

  save(): void {
    this.message = '';

    if (this.user.id==0) {
      this.userService.create(this.user).subscribe({
        next: (x) => {
          this.users.push(x);
          this.user = resetUser();
        },
        error: (err) => {
          this.message = Object.values(err.error.errors).join(', ');
        }
      });
    }
    else {
      this.userService.update(this.user).subscribe({
        error: (err) => {
          this.message = Object.values(err.error.errors).join(', ');
        },
        complete: () => {



          this.userService.getAll().subscribe(x => this.users = x);
          this.user = resetUser();
        }
      });
    }
  }



}
