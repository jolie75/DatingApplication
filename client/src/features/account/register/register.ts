import { Component, inject, input, output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterCreds, User } from '../../../types/users';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../../core/services/account-service';

@Component({
  selector: 'app-register',
  imports:[FormsModule, CommonModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  private accountService = inject(AccountService);
  cancelRegister= output<boolean>();
  protected creds = {} as RegisterCreds;

  register(){
    this.accountService.register(this.creds).subscribe({
      next: (response: any)=> {
        console.log(response);
        this.cancel();
      },
      error: (error: any)=> console.log(error)
    })
  }


   cancel(){
    this.cancelRegister.emit(false);
    console.log("Registration cancelled");
  }

}
