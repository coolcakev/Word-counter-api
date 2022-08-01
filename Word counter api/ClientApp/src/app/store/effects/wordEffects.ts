import { HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { select, Store } from "@ngrx/store";
import { catchError, map, mergeMap, of, take } from "rxjs";
import { WordService } from "src/app/service/WordService";
import { WordDTO } from "src/app/types/words/WordDTO";
import * as WordAction from "../actions/wordAction";
import { IAppState } from "../types/AppState";

@Injectable()
export class WordEffects {
    constructor(private actions$: Actions,
        private wordService: WordService,
        private store: Store<IAppState>) {
    }

    getWords$ = createEffect(() => this.actions$.pipe(
        ofType(WordAction.getWords,WordAction.setCount,WordAction.setWordMode),
        mergeMap(() => {
            return this.store.pipe(
                select(x => x.word),
                take(1),
                mergeMap((wordState) => {              
                    const wordDTO: WordDTO = {
                        count: wordState.count,
                        text: wordState.text,
                    }
                    return this.wordService.getWords(wordState.wordMode, wordDTO).pipe(
                        map((wordStatistic) => WordAction.getWordsSuccess({ wordStatistic })),
                        catchError((error: HttpErrorResponse) => of(WordAction.getWordsFailure({ error: error.message })))
                    )
                })                
            )
        })
    ))
}