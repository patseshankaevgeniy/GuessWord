import { IAdvice } from "../models/advice";


export class AdvicesService {
    private advices: IAdvice[] = [
        {'id': 1, 'imageUrl': 'https://i.pinimg.com/736x/95/30/41/953041070f000d45c05c912005f63724.jpg', 'text': 'first advice'},
        {'id': 2, 'imageUrl': 'https://img3.akspic.ru/previews/8/3/3/8/6/168338/168338-nyujork-ulice_nyu_jorka-ulica-manhetten-zdanie-500x.jpg', 'text': 'second advice'},
        {'id': 3, 'imageUrl': 'https://img3.akspic.ru/previews/6/5/4/8/6/168456/168456-list-vektornaya_grafika-vetv-derevo-siluet-500x.jpg', 'text': 'third advice'}
    ]

    public getAdvices(): IAdvice[] {
        return this.advices
    }
}