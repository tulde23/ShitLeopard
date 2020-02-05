import { Subject } from 'rxjs';

import { Message } from '@/models/Message';

export class MessageService {
  private subject = new Subject<Message>();
  constructor() {}
  public sendMessage(message: Message) {
    this.subject.next(message);
  }
  public clear() {
    this.subject.next();
  }
  public getMessage() {
    return this.subject.asObservable();
  }
}
