export class Message {
    constructor(
      public body?: string,
      public isError?: boolean,
      public payload?: any
    ) {}
  }