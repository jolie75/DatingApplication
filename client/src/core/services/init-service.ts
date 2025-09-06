import { inject, Injectable } from '@angular/core';
import { AccountService } from './account-service';
import{ Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InitService {
  private accountService = inject(AccountService);

  init(){
    const UserString = localStorage.getItem('user');
    if(!UserString) return of(null);
    const user = JSON.parse(UserString);
    this.accountService.currentUser.set(user);

    return of(null)
  }
  
}
