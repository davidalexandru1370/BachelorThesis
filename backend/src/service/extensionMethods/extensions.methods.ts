import { JwtService } from "@nestjs/jwt";
import { User } from "src/core/domain/user.entity";

declare module "@nestjs/jwt" {
  interface JwtService {
    generateJwtToken(user: User): Promise<string>;
  }
}

async function generateJwtToken(user: User): Promise<string> {
  const payload = {
    sub: user.id,
    email: user.email,
  };
  const token = await this.jwtService.signAsync(payload);

  return token;
}

JwtService.prototype.generateJwtToken = generateJwtToken;
