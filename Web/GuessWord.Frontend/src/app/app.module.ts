import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule , FormsModule} from '@angular/forms'
import { Ng2SearchPipeModule } from 'ng2-search-filter';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { WordComponent } from './components/words/word.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { AdvicesComponent } from './components/advices/advices.component';
import { WordsComponent } from './components/content/words/words.component';
import { SettingsComponent } from './components/content/settings/settings.component';
import { GameComponent } from './components/content/game/game.component';
import { InfoComponent } from './components/content/info/info.component';
import { SearchWordsComponent } from './components/content/words/search-words/search-words.component';
import { UserWordsComponent } from './components/content/words/user-words/user-words.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { WordService } from './services/words.service';
import { UserWordService } from './services/userWord.service';





@NgModule({
  declarations: [
    AppComponent,
    WordComponent,
    HeaderComponent,
    FooterComponent,
    NavigationComponent,
    AdvicesComponent,
    WordsComponent,
    SettingsComponent,
    GameComponent,
    InfoComponent,
    SearchWordsComponent,
    UserWordsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    Ng2SearchPipeModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [HttpClient, WordService, UserWordService],
  bootstrap: [AppComponent]
})
export class AppModule { }
