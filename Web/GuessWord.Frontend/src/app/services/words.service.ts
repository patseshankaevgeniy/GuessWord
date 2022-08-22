import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { GuessWordApiClient, IWordDto, WordDto } from "../clients/GuessWordApiClient";
import { Word } from "../models/word";

@Injectable()
export class WordService {
    private httpClient: HttpClient;
    private url: string = "https://localhost:59378/api/words";
    private headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiSWNlIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoidXNlciIsImV4cCI6MTY1OTM3NjU2OSwiaXNzIjoiVGVzdC5jb20iLCJhdWQiOiJUZXN0LmNvbSJ9.sagYj6SFmAYECMPPO6wr9T7k1CY3Fhm7byTY5jWXvg4`
      })
    constructor( httpClient: HttpClient){
       this.httpClient = httpClient;
      
    }

    public getWords(): Observable<Word[]> {
        return this.httpClient
            .get<IWordDto[]>("https://localhost:59378/api/words", {headers: this.headers})
            .pipe(map(dtos => dtos.map(dto => new Word(dto.id, dto.value!, dto.translations!))));
    }

    public getWord(): void {
        return 
    }

    public getByLetter(letter: string): Observable<Word[]> {    
            return  this.httpClient
            .get<IWordDto[]>(this.url + "/search?" + letter, {headers: this.headers, params: new HttpParams().set('letter', letter)})
            .pipe(map(dtos => dtos.map(dto => new Word(dto.id, dto.value!, dto.translations!))));       
    }

    public createWord(): boolean {
        return true;
    }

    public deleteWord(): boolean {
        return true;
    }

    public updateWord(): void {
        
    }
}