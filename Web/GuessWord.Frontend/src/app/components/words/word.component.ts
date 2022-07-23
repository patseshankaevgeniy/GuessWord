import { Component, Input } from '@angular/core'
import { IWord } from 'src/app/models/word'

@Component({
    selector: 'app-word',
    templateUrl: './word.component.html'
})

export class WordComponent {
    @Input() word: IWord
}