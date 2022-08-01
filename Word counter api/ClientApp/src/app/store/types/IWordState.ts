import { IWordStatistic } from "src/app/types/words/IWordStatistic";
import { WordMode } from "src/app/types/words/wordMode";

export interface IWordState {
    loading: boolean;
    error: string | null;
    wordStatistics: IWordStatistic[] | null
    count: number;
    wordMode: WordMode,
    text: string;
}