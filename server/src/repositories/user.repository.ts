import { CACHE_MANAGER } from '@nestjs/cache-manager';
import { Cache } from 'cache-manager';
import { Inject, Injectable } from '@nestjs/common';
import { User } from 'src/entities/user';

@Injectable()
export class UserRepository {
  constructor(@Inject(CACHE_MANAGER) private cacheManager: Cache) {}

  async getByUsername(username: string): Promise<User | undefined> {
    return this.cacheManager.get(username);
  }

  async save(user: User): Promise<void> {
    this.cacheManager.set(
      user.username,
      user,
      parseInt(process.env.CACHING_USERNAME_TTL_IN_HOURS) * 60 * 60 * 1000,
    );
  }

  async delete(username: string): Promise<void> {
    this.cacheManager.del(username);
  }
}
