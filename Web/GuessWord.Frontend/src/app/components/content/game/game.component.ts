import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-game',
  template: `<h3>Game</h3>`,
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
