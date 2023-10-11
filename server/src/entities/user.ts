export class User {
  constructor(username: string) {
    this.username = username;
    this.createdAt = new Date();
  }

  public username: string;
  public createdAt: Date;
}
