import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, CommonModule],
  templateUrl:'./nav.html',
  styleUrls: ['./nav.css']
})
export class Nav {
  protected accountService = inject(AccountService)
  protected creds: any = {}
user: any;

  login() {
    this.accountService.login(this.creds).subscribe(
      {
      next: result=> {
      console.log(result);
      this.creds = {};
      },
        error: error=>alert(error.message)
      })
  }

logout() {
  this.accountService.logout();
}
}
