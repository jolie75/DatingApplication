import { CommonModule } from '@angular/common';
import { Component, signal, Input } from '@angular/core';
import { Register } from "../account/register/register";
import { User } from '../../types/users';

@Component({
  selector: 'app-home',
  imports: [CommonModule, Register],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {
  protected registerMode = signal(false);

  showRegister(value: boolean) {
    this.registerMode.set(value);
  }
}
