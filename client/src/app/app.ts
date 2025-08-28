import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { async, lastValueFrom } from 'rxjs';
import { Nav } from '../layout/nav/nav';
import { AccountService } from '../core/services/account-service';
import { Home } from "../features/home/home";
import { User } from '../types/users';

@Component({
  selector: 'app-root',
  imports: [Nav, Home],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit{
private accountService = inject(AccountService);
private http = inject(HttpClient);
protected readonly title =('Dating application');
protected members = signal<User[]>([])

  async ngOnInit() {
    this.members.set(await this.getMembers());
    this.setCurrentUser();
  }


  //peristing the login with signals

  setCurrentUser(){
    const UserString = localStorage.getItem('user');
    if(!UserString) return;
    const user = JSON.parse(UserString);
    this.accountService.currentUser.set(user);
  }

  async getMembers() {
    try {
      // Use lastValueFrom from rxjs and specify the response type as User[]
      return lastValueFrom(this.http.get<User[]>('https://localhost:5001/api/members'));
    } catch (error) {
      console.log(error);
      throw error; // returning a promise so don't need a subscribtion
    }
  }
}