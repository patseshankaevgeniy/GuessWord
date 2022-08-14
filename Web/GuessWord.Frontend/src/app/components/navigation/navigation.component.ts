import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  
  }
  public goGame() {
    this.router.navigate(['game'])
  }

  public goInfo() {
    this.router.navigate(['info'])
  }
  
  public goSettings() {
    this.router.navigate(['settings'])
  }

  public goWords() {
    this.router.navigate(['words'])
  }
  
}
