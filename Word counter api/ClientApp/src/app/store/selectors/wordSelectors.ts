import { createSelector } from "@ngrx/store";
import { IAppState } from "../types/AppState";

export const selectFeature=(state:IAppState)=>state.word

export const isLoadingSelector=createSelector(selectFeature,(state)=>state.loading)
export const wordStatisticSelector=createSelector(selectFeature,(state)=>state.wordStatistics)
export const errorSelector=createSelector(selectFeature,(state)=>state.error)
export const countSelector=createSelector(selectFeature,(state)=>state.count)
export const wordModeSelector=createSelector(selectFeature,(state)=>state.wordMode)
export const textSelector=createSelector(selectFeature,(state)=>state.text)