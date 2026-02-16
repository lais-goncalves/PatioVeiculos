import { Component } from '@angular/core';
import {Titulo} from '../../shared/titulo/titulo';
import {RouterLink, RouterLinkActive, RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-relatorios',
  imports: [
    Titulo,
    RouterOutlet,
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './relatorios.html',
  styleUrl: './relatorios.scss',
})
export class Relatorios {

}
