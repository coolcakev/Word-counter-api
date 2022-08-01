import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { wordStatisticSelector } from 'src/app/store/selectors/wordSelectors';
import { IAppState } from 'src/app/store/types/AppState';
import { IWordStatistic } from 'src/app/types/words/IWordStatistic';

@Component({
  selector: 'app-word-table',
  templateUrl: './word-table.component.html',
  styleUrls: ['./word-table.component.css']
})
export class WordTableComponent implements OnInit {

  wordsStatistics$: Observable<IWordStatistic[] | null>
  constructor(private store: Store<IAppState>) {
    this.wordsStatistics$ = this.store.pipe(select(wordStatisticSelector))
  }
  ngOnInit() {
  }

}
