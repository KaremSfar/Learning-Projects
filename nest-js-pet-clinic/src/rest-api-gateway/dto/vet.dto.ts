import { IsEmail, IsNotEmpty, IsOptional } from 'class-validator';

export class UpdateVetDto {
  @IsNotEmpty()
  @IsOptional()
  username: string;

  @IsEmail()
  @IsNotEmpty()
  @IsOptional()
  email: string;

  @IsOptional()
  address: string;

  @IsOptional()
  telephone: string;
}
