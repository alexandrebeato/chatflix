import { Injectable } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { User } from 'src/entities/user';
import { SignInModel } from 'src/models/users/sign-in.model';
import { UserModel } from 'src/models/users/user.model';
import { UserRepository } from 'src/repositories/user.repository';

@Injectable()
export class UserService {
  private readonly _userRepository: UserRepository;
  private readonly _jwtService: JwtService;

  constructor(userRepository: UserRepository, jwtService: JwtService) {
    this._userRepository = userRepository;
    this._jwtService = jwtService;
  }

  async signIn(model: SignInModel): Promise<UserModel> {
    let user = await this._userRepository.getByUsername(model.username);

    if (user) return null;

    user = new User(model.username);

    await this._userRepository.save(user);

    const token_payload = { sub: user.username };

    const token = this._jwtService.sign(token_payload, {
      secret: process.env.JWT_TOKEN_SECRET,
    });

    return {
      username: user.username,
      createdAt: user.createdAt,
      token: token,
    };
  }

  async getUserFromToken(token: string): Promise<User> {
    const token_payload = this._jwtService.verify(token, {
      secret: process.env.JWT_TOKEN_SECRET,
    });

    return await this._userRepository.getByUsername(token_payload.sub);
  }
}
