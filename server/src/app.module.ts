import { Module } from '@nestjs/common';
import { UsersController } from './controllers/users.controller';
import { UserService } from './services/user.service';
import { JwtService } from '@nestjs/jwt';
import { CacheModule } from '@nestjs/cache-manager';
import { UserRepository } from './repositories/user.repository';
import { ConfigModule } from '@nestjs/config';
import { ChatGateway } from './gateways/chat.gateway';

@Module({
  imports: [CacheModule.register(), ConfigModule.forRoot()],
  controllers: [UsersController],
  providers: [JwtService, UserRepository, UserService, ChatGateway],
})
export class AppModule {}
