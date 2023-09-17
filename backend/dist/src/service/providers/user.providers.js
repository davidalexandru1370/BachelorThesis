"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.userProviders = void 0;
const providers_1 = require("../../core/constants/providers");
const user_entity_1 = require("../../core/domain/user.entity");
exports.userProviders = [
    {
        provide: providers_1.Providers.USER_REPOSITORY,
        useFactory: (dataSource) => dataSource.getRepository(user_entity_1.User),
        inject: [providers_1.Providers.DATA_SOURCE],
    },
];
//# sourceMappingURL=user.providers.js.map