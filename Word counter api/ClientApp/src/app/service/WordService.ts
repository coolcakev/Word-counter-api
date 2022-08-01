import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Api } from '.';
import { IWordStatistic } from '../types/words/IWordStatistic';
import { WordMode } from '../types/words/wordMode';
import { WordDTO } from '../types/words/WordDTO';

@Injectable({ providedIn: 'root' })
export class WordService {
    constructor(private httpClient: HttpClient) {

    }

    getWords(textMode:WordMode,wordDTO: WordDTO):Observable<IWordStatistic[]> {
        return this.httpClient.post<IWordStatistic[]>(`${Api.WORDAPI}/${textMode}`,wordDTO)
    }
}