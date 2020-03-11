import { Question } from './question';

export class QuestionAnswer {
  constructor(
    public question?: Question,
    public answer?: any,
    public match?: boolean,
    public comment?: string,
    public isArray?: boolean
  ) {}
}
