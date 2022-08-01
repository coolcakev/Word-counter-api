import { createAction, props } from "@ngrx/store";
import { IWordStatistic } from "src/app/types/words/IWordStatistic";
import { WordMode } from "src/app/types/words/wordMode";

export const getWords=createAction("[Words] Get Words")
export const getWordsSuccess=createAction("[Words] Get Words Success",props<{wordStatistic:IWordStatistic[]}>())
export const getWordsFailure=createAction("[Words] Get Words Failure",props<{error:string}>())

export const setCount=createAction("[Words] Set Count",props<{count:number}>())
export const setWordMode=createAction("[Words] Set Word Mode",props<{wordMode:WordMode}>()) 
export const setText=createAction("[Words] Set Text",props<{text:string}>()) 

