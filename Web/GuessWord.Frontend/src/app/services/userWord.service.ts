import { UserWord } from "../models/userWord";
import { HttpHeaders} from '@angular/common/http';
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { Word } from "../models/word";
import { ApiClient, UserWordDto, UserWordPatchDto } from "../clients/api.client";

@Injectable()
export class UserWordService {
    private apiClient: ApiClient;
    private headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiRmFudGEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiYWRtaW4iLCJ1c2VyIl0sImV4cCI6MTY1OTc4OTA4NywiaXNzIjoiVGVzdC5jb20iLCJhdWQiOiJUZXN0LmNvbSJ9.dNLdOCWxjOJNC2hBdAWgmXTgmX5CsMTUMFAFMjJmsh8`
      })

    constructor(apiClient: ApiClient){
        this.apiClient = apiClient;
    }

    public getUserWords(): Observable<UserWord[]>{
        return this.apiClient.getUserWords()
        .pipe(map(dtos => dtos.map(dto => new UserWord(dto.id, dto.word!, dto.translations!, dto.status!))));
       
    }

    public getUserWord(id: number): Observable<UserWord>{
        return this.apiClient.getUserWord(id)
        .pipe(map(dto=> new UserWord(dto.id!, dto.word!, dto.translations!, dto.status!)));
    }

    public createUserWord(newWord: Word): Observable<any> {
       var userWordDto = new UserWordDto({
            word: newWord.value,
            translations: newWord.translations
       })
        return this.apiClient.createUserWord(userWordDto);
    }

    public updateUserWord(id: number, status: UserWordPatchDto): Observable<any>{
        return this.apiClient.updateUserWord(id, status);
    }
    
    public deleteUserWord(id: number): Observable<void> {
        return this.apiClient.deleteUserWord(id);
    }
}