using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public class BlackListRepository : GenericRepository<BlackList>, IBlackListRepository
    {
        public BlackListRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
