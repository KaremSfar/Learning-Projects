import { SetMetadata } from '@nestjs/common';
import { Role } from 'src/entities/util/role.enum';

//Decorator used to specify which roles must be met in order to give access to route
//Exemple : @Roles(Role.Admin)
export const ROLES_KEY = 'roles';
export const Roles = (...roles: Role[]) => SetMetadata(ROLES_KEY, roles);
