import { PredicateBuilder } from '@/queries';
import { FieldBuilder } from './FieldBuilder';

export class AbstractQuery {
  protected builder = new PredicateBuilder();
  protected name: string;
  protected fields = new FieldBuilder();
  constructor() {}
  public get OperationName(): string {
    return this.name;
  }
  public Operation(name: string) {
    this.name = name;
    return this;
  }
  public Parameter(name: string, value: any, primitive: boolean) {
    this.builder.Add(name, value, primitive);
    return this;
  }
  public Format(): any {
    const operation = this.name;
    const parameters = this.builder.format();
    const fields = this.fields.format();

    const q = {
      query: `{${operation}${parameters}{${fields}}}`
    };
    console.log('query', JSON.stringify(q));
    return q;
  }
  protected BuildFromTerm(term: string) {
    if (term) {
      const tokens = term.split('&');
      console.log('tokens', tokens);
      tokens.forEach(t => {
        const predicate = t.trim().split('=');
        if (predicate.length === 2) {
          this[predicate[0]] = predicate[1];
        }
      });
      if (tokens.length === 1 && term.indexOf('=') < 0) {
        this.Parameter('term', term, false);
      }
    }
  }
}
