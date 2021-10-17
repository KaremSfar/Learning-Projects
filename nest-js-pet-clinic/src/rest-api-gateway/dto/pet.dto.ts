import { IsNotEmpty, IsOptional } from 'class-validator';

// Create a pet DTO 
export class CreatePetDto {
  @IsNotEmpty()
  name: string;
}

// Update a pet DTO
export class UpdatePetDto {
  @IsOptional()
  @IsNotEmpty()
  name: string;
}
