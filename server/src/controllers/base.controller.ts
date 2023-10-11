import { BadRequestException } from '@nestjs/common';

export abstract class BaseController {
  protected ok(object?: any): any {
    return object;
  }

  protected badRequest(object?: any): any {
    throw new BadRequestException(object);
  }
}
