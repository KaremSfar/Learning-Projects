import {
  Body,
  Controller,
  Delete,
  Get,
  Param,
  Patch,
  UnauthorizedException,
  UseGuards,
  UseInterceptors,
} from '@nestjs/common';
import { ApiTags } from '@nestjs/swagger';
import { VetEntity } from 'src/entities/vet.entity';
import { UserInterceptor } from '../interceptors/user.interceptor';
import { VetService } from '../services/vet.service';
import { UpdateVetDto } from '../dto/vet.dto';
import { UserEntity } from 'src/entities/generic-entities/user.entity';
import { User } from 'src/auth/guards-module/decorators/get.user.decorator';
import { AuthGuard } from '@nestjs/passport';
import { RolesGuard } from 'src/auth/guards-module/guards/roles.guard';
import { Roles } from 'src/auth/guards-module/decorators/roles.decorator';
import { Role } from 'src/entities/util/role.enum';
import { UpdateResult } from 'typeorm';
@ApiTags('Vet')
@UseInterceptors(UserInterceptor)
@Controller('/vets')
export class VetController {
  constructor(private vetService: VetService) {}

  // GET ALL VETS
  @Get()
  getVets(): Promise<VetEntity[]> {
    return this.vetService.getVets();
  }

  // GET A SPECIFIC VET
  @Get(':id')
  @UseInterceptors(UserInterceptor)
  getVet(@Param('id') id: string): Promise<VetEntity> {
    return this.vetService.getVet(id);
  }

  // UPDATE A VET; Only if admin or user
  @Patch(':id')
  @UseGuards(AuthGuard('jwt'))
  update(
    @Param('id') id: string,
    @Body() updateVetDto: UpdateVetDto,
    @User() user: UserEntity,
  ): Promise<VetEntity> {
    // TODO: get out of controller
    if (user.id !== id && user.role != 'admin')
      throw new UnauthorizedException();

    return this.vetService.update(id, updateVetDto);
  }

  // Soft Delete the Thing
  @Roles(Role.Admin)
  @UseGuards(RolesGuard)
  @Delete(':id')
  remove(@Param('id') id: string): Promise<UpdateResult> {
    return this.vetService.delete(id);
  }

  // Restore The deleted entry
  @Roles(Role.Admin)
  @UseGuards(RolesGuard)
  @Patch('restore/:id')
  restore(@Param('id') id: string): Promise<UpdateResult> {
    return this.vetService.restore(id);
  }
}
