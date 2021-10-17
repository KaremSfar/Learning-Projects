import {
  Body,
  Controller,
  Delete,
  Get,
  Param,
  Patch,
  Post,
  UseGuards,
  UseInterceptors,
} from '@nestjs/common';
import { OwnerEntity } from 'src/entities/owner.entity';
import { Role } from 'src/entities/util/role.enum';
import { User } from 'src/auth/guards-module/decorators/get.user.decorator';
import { Roles } from 'src/auth/guards-module/decorators/roles.decorator';
import { RolesGuard } from 'src/auth/guards-module/guards/roles.guard';
import { CreatePetDto, UpdatePetDto } from '../dto/pet.dto';
import { UserInterceptor } from '../interceptors/user.interceptor';
import { PetService } from '../services/pet.service';

// The Owner's Route to pets management
@UseGuards(RolesGuard)
@UseInterceptors(UserInterceptor)
@Controller('pets')
export class PetController {
  constructor(private petService: PetService) {}

  // Get Owned Pets
  @Roles(Role.Owner)
  @Get()
  getPets(@User() owner: OwnerEntity) {
    return this.petService.getByOwner(owner);
  }

  /// Add a New Pet
  @Roles(Role.Owner)
  @Post()
  createPet(@User() ownerEntity: OwnerEntity, @Body() petDto: CreatePetDto) {
    return this.petService.createPet(ownerEntity, petDto);
  }

  // Edit a Pet
  @Roles(Role.Owner)
  @Patch('/:id')
  updatePet(
    @User() ownerEntity: OwnerEntity,
    @Body() petDto: UpdatePetDto,
    @Param('id') id: string,
  ) {
    return this.petService.updatePet(id, petDto, ownerEntity);
  }

  // Delete an owned Pet
  @Roles(Role.Owner)
  @Delete('/:id')
  deletePet(@User() ownerEntity: OwnerEntity, @Param('id') id: string) {
    return this.petService.delete(id, ownerEntity);
  }

  // Recover a Pet if has the id
  @Roles(Role.Owner)
  @Patch('restore/:id')
  recoverPet(@User() ownerEntity: OwnerEntity, @Param('id') id: string) {
    return this.petService.recover(id, ownerEntity);
  }
}
