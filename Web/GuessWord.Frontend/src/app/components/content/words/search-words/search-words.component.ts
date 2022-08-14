import { Component, OnInit } from '@angular/core';
import { Word } from 'src/app/models/word';
import { WordService } from 'src/app/services/words.service';
import {FormBuilder, FormsModule} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { UserWordService } from 'src/app/services/userWord.service';

@Component({
  selector: 'app-search-words',
  templateUrl: './search-words.component.html',
  styleUrls: ['./search-words.component.css'],
  styles: [],
  providers: [WordService, HttpClient]
})

export class SearchWordsComponent implements OnInit {

   

  private readonly wordService: WordService;
  private readonly userWordService: UserWordService;
  private searchForm: any;
  public term: string;

  public words: Word[] = [];

  constructor(
    wordsService: WordService, 
    private formBuilder: FormBuilder, 
    userWordService: UserWordService) {
      this.wordService = wordsService;
      this.searchForm = this.formBuilder.group({
        search:'',
      })
   }

  ngOnInit() {
    this.wordService.getWords().subscribe(words => {
      this.words = words;
    });   
  }

  getByLetter() {
    if(this.term != null){
      this.wordService.getByLetter(this.term).subscribe(words => {
        this.words = words;
      })
    }
    else{
      this.wordService.getWords().subscribe(words =>{
        this.words = words;
      })
    }
  }

  addUserWord(word: Word){
    
  }

  

}
