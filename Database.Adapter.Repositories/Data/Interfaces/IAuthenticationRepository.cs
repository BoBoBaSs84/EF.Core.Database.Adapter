﻿using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Infrastructure.Interfaces;

namespace Database.Adapter.Repositories.Data.Interfaces;

public interface IAuthenticationRepository : IUnitOfWork<AuthenticationContext>
{
}
