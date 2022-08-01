import { createReducer, on } from "@ngrx/store";
import { IWordState } from "../types/IWordState";
import * as WordAction from "../actions/wordAction";
import { WordMode } from "src/app/types/words/wordMode";
export const initialWordState: IWordState = {
    loading: false,
    wordStatistics: null,   
    count: 5,
    error: null,
    wordMode: WordMode.ONE,
    text: '',
}

export const wordReducer = createReducer(initialWordState,
    on(WordAction.getWords, (state) => ({ ...state, loading: true })),
    on(WordAction.getWordsSuccess, (state, actions) => ({
        ...state,
        loading: false,
        wordStatistics: actions.wordStatistic
    })),
    on(WordAction.getWordsFailure, (state, actions) => ({
        ...state,
        loading: false,
        error: actions.error
    })),
    on(WordAction.setCount, (state, actions) => ({
        ...state,
        count: actions.count
    })),
    on(WordAction.setWordMode, (state, actions) => ({
        ...state,
        wordMode: actions.wordMode
    })),
    on(WordAction.setText, (state, actions) => ({
        ...state,
        text: actions.text
    })),
)