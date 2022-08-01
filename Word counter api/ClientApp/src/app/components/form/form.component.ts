import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { getWords, setText } from 'src/app/store/actions/wordAction';
import { textSelector } from 'src/app/store/selectors/wordSelectors';
import { IAppState } from 'src/app/store/types/AppState';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent { 
  text$: Observable<string>;
  constructor(private store: Store<IAppState>) {
    this.text$ = this.store.pipe(select(textSelector))
  }

  textChange(text:string) {   
    this.store.dispatch(setText({text}))
  }
  analyze(event: Event){
    this.store.dispatch(getWords())
  }
  

}
