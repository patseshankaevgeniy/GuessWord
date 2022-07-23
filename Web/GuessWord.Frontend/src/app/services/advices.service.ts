import { IAdvice } from "../models/advice";


export class AdvicesService {
    private advices: IAdvice[] = [
        {'id': 1, 'imageUrl': '', 'text': 'first advice'},
        {'id': 2, 'imageUrl': '', 'text': 'second advice'},
        {'id': 3, 'imageUrl': '', 'text': 'third advice'}
    ]

    public getAdvices(): IAdvice[] {
        return this.advices
    }
}