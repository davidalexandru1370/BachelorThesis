import { Providers } from "src/core/constants/providers";
import { User } from "src/core/domain/user.entity";
import { DataSource } from "typeorm";

export const userProviders = [
  {
    provide: Providers.USER_REPOSITORY,
    useFactory: (dataSource: DataSource) => dataSource.getRepository(User),
    inject: [Providers.DATA_SOURCE],
  },
];
