import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { ApiClient, IWordDto, WordDto } from "../clients/api.client";
import { Word } from "../models/word";


@Injectable()
export class WordService {
    private apiClient: ApiClient;
    private headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiSWNlIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoidXNlciIsImV4cCI6MTY1OTM3NjU2OSwiaXNzIjoiVGVzdC5jb20iLCJhdWQiOiJUZXN0LmNvbSJ9.sagYj6SFmAYECMPPO6wr9T7k1CY3Fhm7byTY5jWXvg4`
      })
    constructor(apiClient: ApiClient){
      this.apiClient = apiClient;
    }

    public getWords(): Observable<Word[]> {
        return this.apiClient
            .getWords()
            .pipe(map(dtos => dtos.map(dto => new Word(dto.id, dto.value!, dto.translations!))));
    }

    public getWord(): void {
        return 
    }

    public getByLetter(letter: string): Observable<Word[]> {    
            return  this.apiClient
            .getWords(letter, undefined)
            .pipe(map(dtos => dtos.map(dto => new Word(dto.id, dto.value!, dto.translations!))));       
    }

    public createWord(newWord: Word): Observable<any> {
        var newWordDto = new WordDto({
            value: newWord.value,
            translations: newWord.translations
        })
        return this.apiClient.createWord(newWordDto);
    }
}