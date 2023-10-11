import {
  WebSocketGateway,
  WebSocketServer,
  SubscribeMessage,
  OnGatewayInit,
  OnGatewayConnection,
  OnGatewayDisconnect,
} from '@nestjs/websockets';
import { Server } from 'socket.io';
import { Message } from 'src/entities/message';
import { CreateMessageModel } from 'src/models/messages/create-message.model';
import { UserService } from 'src/services/user.service';

@WebSocketGateway()
export class ChatGateway
  implements OnGatewayInit, OnGatewayConnection, OnGatewayDisconnect
{
  private readonly _userService: UserService;

  constructor(userService: UserService) {
    this._userService = userService;
  }

  @WebSocketServer() server: Server;

  async handleConnection(socket: any) {
    // Get connected user
    const token = socket.handshake.query.token;

    const user = await this._userService.getUserFromToken(token);

    if (!user) {
      socket.disconnect(true);
      return;
    }

    console.log(`connected: ${user.username}`);

    // Subscribe to events
    socket.join(user.username);
  }

  async handleDisconnect() {
    console.log('disconnected');
  }

  async afterInit() {
    console.log('initialized');
  }

  @SubscribeMessage('chat')
  async onChat(client: any, messageModel: CreateMessageModel) {
    const fromUser = await this._userService.getUserFromToken(
      messageModel.from.token,
    );

    if (!fromUser) {
      client.disconnect(true);
      return;
    }

    const message: Message = new Message(
      fromUser.username,
      messageModel.to,
      messageModel.message,
    );

    this.server.emit(message.to, message);

    console.log(
      `${message.id} - Message from ${message.from} to ${message.to}: ${message.message}`,
    );
  }
}
