import { Component, Input, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { setCount, setWordMode } from 'src/app/store/actions/wordAction';
import { countSelector } from 'src/app/store/selectors/wordSelectors';
import { IAppState } from 'src/app/store/types/AppState';
import { IWordStatistic } from 'src/app/types/words/IWordStatistic';
import { WordMode } from 'src/app/types/words/wordMode';

@Component({
  selector: 'app-words-container',
  templateUrl: './words-container.component.html',
  styleUrls: ['./words-container.component.css']
})
export class WordsContainerComponent implements OnInit {
  public WordMode = WordMode
  count$: Observable<number>
  constructor(private store: Store<IAppState>) {
    this.count$ = this.store.pipe(select(countSelector))
  }

  ngOnInit() {
  }

  buttonClick(event: Event, wordMode: WordMode) {
    this.store.dispatch(setWordMode({ wordMode }))
  }
  countChange(count: number) {
    this.store.dispatch(setCount({ count }))
  }
}
