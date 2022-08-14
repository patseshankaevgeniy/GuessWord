export class UserWord {
    id?: number;
    word: string;
    translations: string[];
    status: number;

    constructor(id: number | undefined, word: string, translations: string[], status: number){

        this.id = id,
        this.status = status,
        this.translations = translations,
        this.word = word
    }
}