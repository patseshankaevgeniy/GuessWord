  import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IStatus } from 'src/app/models/status';
import { UserWord } from 'src/app/models/userWord';
import { UserWordService } from 'src/app/services/userWord.service';

@Component({
  selector: 'app-user-words',
  templateUrl: './user-words.component.html',
  styleUrls: ['./user-words.component.css'],
  providers: [UserWordService, HttpClient]
})
export class UserWordsComponent implements OnInit {
  private readonly userWordService: UserWordService;


  public filterStatus: string;
  public statuses: Array<IStatus> = [
    {id: 1, value: 'done'}, {id: 2, value: 'new'}, {id: 3, value: 'in progress'}]

public userWords: UserWord[];

  constructor(userWordService: UserWordService) { 
    this.userWordService = userWordService;
  }

  ngOnInit(): void {
    this.userWordService.getUserWords().subscribe(words => {
      this.userWords = words;
    });
  }
  
  deleteUserWord(word: UserWord): void{
    // let wordIndex = this.userWords.findIndex(d => d.id === word.id);
    // this.userWords.splice(wordIndex, 1);
    this.userWordService.deleteUserWord(word.id!).subscribe();
  }

}
