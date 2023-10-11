import { v4 as uuidv4 } from 'uuid';

export class Message {
  constructor(from: string, to: string, message: string) {
    this.id = uuidv4();
    this.from = from;
    this.to = to;
    this.message = message;
    this.createdAt = new Date();
  }

  public id: string;
  public from: string;
  public to: string;
  public message: string;
  public createdAt: Date;
}
