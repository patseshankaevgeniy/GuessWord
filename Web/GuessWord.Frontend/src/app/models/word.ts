export class Word {
    id?: number;
    value: string;
    translations: string[];

    constructor(id: number | undefined, value: string, translations: string[])
    {
        this.id = id;
        this.value = value;
        this.translations = translations;
    }
}