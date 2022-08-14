import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GameComponent } from './components/content/game/game.component';
import { InfoComponent } from './components/content/info/info.component';
import { SettingsComponent } from './components/content/settings/settings.component';
import { WordsComponent } from './components/content/words/words.component';

const appRoutes: Routes = [
  {path: 'game', component: GameComponent},
  {path: 'info', component: InfoComponent},
  {path:'settings', component: SettingsComponent},
  {path: 'words', component: WordsComponent}
]

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
