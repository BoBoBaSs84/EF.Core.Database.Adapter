using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Adapter.Repositories.Data.Interfaces;

public interface IMasterDataRepository : IUnitOfWork<MasterDataContext>
{
}
