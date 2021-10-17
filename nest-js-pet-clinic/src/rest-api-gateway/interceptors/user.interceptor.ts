import {
  CallHandler,
  ClassSerializerInterceptor,
  ExecutionContext,
  PlainLiteralObject,
} from '@nestjs/common';
import { ClassTransformOptions } from '@nestjs/common/interfaces/external/class-transform-options.interface';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
// Interceptor To Transform Class to DTO without polluting the Entity class with Class-transformer Decorators
export class UserInterceptor extends ClassSerializerInterceptor {
  // Overriding the serialize Method of ClassSerializeInterceptor to modify the dto object as it leaves
  serialize(
    response: PlainLiteralObject | Array<PlainLiteralObject>,
    options: ClassTransformOptions,
  ): PlainLiteralObject | PlainLiteralObject[] {
    // Take the value that should be returned
    // Can be a pet, an owner, a vet or an array of them
    const ret = super.serialize(response, options);

    // check if array
    if (Array.isArray(ret)) {
      ret.forEach((entity) => {
        this.deleteSensitive(entity);
      });
    } else {
      this.deleteSensitive(ret);
    }
    return ret;
  }

  // The method that deletes sensitive information
  deleteSensitive(entity: PlainLiteralObject) {
    // Default Case; Querying the UserEntity
    let user = entity;

    // Querying the entity through a Pet
    if (entity.owner !== undefined) user = entity.owner;

    delete user.password;
    delete user.createdAt;
    delete user.updatedAt;
    delete user.deletedAt;
  }
}
