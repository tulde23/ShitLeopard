export class PredicateBuilder {
  private predicates: string[];
  constructor() {
    this.predicates = [];
  }

  public Add(field: string, value: any, primitive: boolean): PredicateBuilder {
    if (value) {
      if (primitive) {
        this.predicates.push(`${field}:${value}`);
      } else {
        this.predicates.push(`${field}:\"${value}\"`);
      }
    }
    return this;
  }

  public format(): string{
    if( this.predicates.length > 0){
        return `(${this.predicates.join(', ')})`;
    }
    return '';
  }
  

}
