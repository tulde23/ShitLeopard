export class FieldBuilder{
    private fieldNames = [];
    public Add(name: string){
       this.fieldNames.push(name);
       return this;
    }
    public AddComplex(name: string, ...childFields){
        const f = childFields.join(',');
        this.fieldNames.push(`${name}{${f}}`);
     }
     public AddRaw(name: string){
        this.fieldNames.push(name);
        return this;
     }
     public format(): string{
        return this.fieldNames.join(',');
     }
}