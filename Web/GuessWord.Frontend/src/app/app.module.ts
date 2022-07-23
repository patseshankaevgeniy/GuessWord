import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

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
    InfoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
