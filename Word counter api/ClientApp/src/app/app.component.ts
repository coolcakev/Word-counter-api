import { Component } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { wordStatisticSelector } from './store/selectors/wordSelectors';
import { IAppState } from './store/types/AppState';
import { IWordStatistic } from './types/words/IWordStatistic';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  wordsStatistics$: Observable<IWordStatistic[] | null>
  constructor(private store: Store<IAppState>) {
    this.wordsStatistics$ = this.store.pipe(select(wordStatisticSelector))
  }
}
