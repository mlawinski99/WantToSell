import { Component } from '@angular/core';
import {MatMenu, MatMenuItem, MatMenuTrigger} from "@angular/material/menu";
import {MatToolbar} from "@angular/material/toolbar";
import {MatButton} from "@angular/material/button";
import {RouterLink} from "@angular/router";
import {faCaretDown} from "@fortawesome/free-solid-svg-icons/faCaretDown";
import {FaIconComponent, FaIconLibrary} from "@fortawesome/angular-fontawesome";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    MatMenu,
    MatToolbar,
    MatMenuTrigger,
    MatButton,
    RouterLink,
    MatMenuItem,
    FaIconComponent
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  username: string = 'User';

  constructor(library: FaIconLibrary) {
    library.addIcons(faCaretDown);
  }

  logout() {
    console.log('User logged out');
  }
}
