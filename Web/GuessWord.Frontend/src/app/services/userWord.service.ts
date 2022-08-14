import { UserWord } from "../models/userWord";
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { UserWordDto, UserWordPatchDto } from "../clients/GuessWordApiClient";
import { Word } from "../models/word";

@Injectable()
export class UserWordService {
  
    private httpClient: HttpClient;

    private headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiRmFudGEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiYWRtaW4iLCJ1c2VyIl0sImV4cCI6MTY1OTc4OTA4NywiaXNzIjoiVGVzdC5jb20iLCJhdWQiOiJUZXN0LmNvbSJ9.dNLdOCWxjOJNC2hBdAWgmXTgmX5CsMTUMFAFMjJmsh8`
      })
    private url = "https://localhost:59378/api/user-words";
    private data: any;

    constructor(httpclient: HttpClient){
        this.httpClient = httpclient
    }

    public getUserWords(): Observable<UserWord[]>{
        return this.httpClient.get<UserWordDto[]>(this.url, {headers: this.headers})
        .pipe(map(dtos => dtos.map(dto => new UserWord(dto.id, dto.word!, dto.translations!, dto.status!))));
       
    }

    public getUserWord(id: number){
        return this.httpClient.get(this.url + '/' + id, {headers: this.headers});
    }

    public createUserWord(newWord: Word): Observable<any> {
       var userWordDto = new UserWordDto({
            word: newWord.value,
            translations: newWord.translations
       })
        return this.httpClient.post(this.url, userWordDto, {headers: this.headers});
    }

    public updateUserWord(id: number, status: UserWordPatchDto): Observable<any>{
        return this.httpClient.patch(this.url + '/' + id, status);
    }
    
    public deleteUserWord(id: number){
        return this.httpClient.delete(this.url + '/' + id, {headers: this.headers});
    }
}