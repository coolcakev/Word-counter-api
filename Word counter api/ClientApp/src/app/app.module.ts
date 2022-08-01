import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from 'src/environments/environment';

import { AppComponent } from './app.component';
import { wordReducer } from './store/reducers/wordReducer';
import { FormComponent } from './components/form/form.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { WordsContainerComponent } from './components/words-container/words-container.component';
import { EffectsModule } from '@ngrx/effects';
import { WordEffects } from './store/effects/wordEffects';
import { HttpClientModule } from '@angular/common/http';
import { WordTableComponent } from './components/word-table/word-table.component';

@NgModule({
  declarations: [
    AppComponent,
    FormComponent,
    WordsContainerComponent,
    WordTableComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    StoreModule.forRoot({}),
    StoreModule.forFeature("word",wordReducer),
    EffectsModule.forRoot(),
    EffectsModule.forFeature([WordEffects]),
    StoreDevtoolsModule.instrument({
      maxAge: 25, // Retains last 25 states
      logOnly: environment.production, // Restrict extension to log-only mode
      autoPause: true, // Pauses recording actions and state changes when the extension window is not open
    }),
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
