import { Body, Controller, Post } from '@nestjs/common';
import { SignInModel } from 'src/models/users/sign-in.model';
import { UserModel } from 'src/models/users/user.model';
import { UserService } from 'src/services/user.service';
import { BaseController } from './base.controller';

@Controller('users')
export class UsersController extends BaseController {
  private readonly _userService: UserService;

  constructor(userService: UserService) {
    super();

    this._userService = userService;
  }

  @Post('sign-in')
  async signIn(@Body() model: SignInModel): Promise<UserModel> {
    const user = await this._userService.signIn(model);

    if (!user) {
      return this.badRequest({
        title: 'Username is already taken',
        message: 'Please, try again with a different username',
      });
    }

    return this.ok(user);
  }
}
