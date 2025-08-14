import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { async, lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit{
private http = inject(HttpClient);
protected readonly title =('Dating application');
protected members = signal<any>([])

  async ngOnInit() {
    this.members.set(await this.getMembers());
  }
  async getMembers() {
    try {
      // Use lastValueFrom from rxjs
      return lastValueFrom(this.http.get('https://localhost:5001/api/members'));
    } catch (error) {
      console.log(error);
      throw error; // returning a promise so don't need a subscribtion
    }
  }
}