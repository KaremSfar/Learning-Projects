import {
  ExecutionContext,
  Injectable,
  UnauthorizedException,
} from '@nestjs/common';
import { Reflector } from '@nestjs/core';
import { AuthGuard } from '@nestjs/passport';
import { Role } from 'src/entities/util/role.enum';
import { ROLES_KEY } from '../decorators/roles.decorator';

@Injectable()
export class RolesGuard extends AuthGuard('jwt') {
  // Injection of NestJs Core's Reflector : To get Classes Metadata
  constructor(private reflector: Reflector) {
    super();
  }

  canActivate(context: ExecutionContext) {
    // User is not retrieved from JWT Token yet
    // The canActivate Method of AuthGuard will, after injected with a PassportStrategy,
    // use that strategy to verify the user and add it to the request
    return super.canActivate(context);
  }

  // after retrieving the user will apply the handle request logic to grant of deny access
  // Returns the user to be injected in request
  handleRequest(err, user, info, context) {
    // Uses Reflector to get Class Metadata and looks for the IS_PUBLIC_KEY key;
    // If Found, the value associated will be set to true; check decorator: Public
    const requiredRoles = this.reflector.getAllAndOverride<Role[]>(ROLES_KEY, [
      context.getHandler(),
      context.getClass(),
    ]);
    if (!requiredRoles) {
      return true;
    }

    if (user) return user;
    // If Public no need for Authentication

    if (!user || !requiredRoles.some((role) => user.role === role)) {
      throw new UnauthorizedException();
    }

    if (err) {
      throw err;
    }
    return user;
  }
}
