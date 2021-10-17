import { Module } from '@nestjs/common';
import { DataModule } from 'src/data/data.module';
import { GuardsModule } from 'src/auth/guards-module/guards.module';
import { PetController } from './controllers/pet.controller';
import { VetController } from './controllers/vet.controller';
import { OwnerService } from './services/owner.service';
import { PetService } from './services/pet.service';
import { VetService } from './services/vet.service';

@Module({
  imports: [GuardsModule, DataModule],
  providers: [VetService, OwnerService, PetService],
  controllers: [VetController, PetController],
})
export class RestApiGatewayModule {}
