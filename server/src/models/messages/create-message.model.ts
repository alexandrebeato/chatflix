export interface CreateMessageModel {
  from: {
    token: string;
  };
  to: string;
  message: string;
}
