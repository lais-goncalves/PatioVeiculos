import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PatioAgora } from './components/patio-agora/patio-agora'
import {Navbar} from './shared/navbar/navbar';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
}
