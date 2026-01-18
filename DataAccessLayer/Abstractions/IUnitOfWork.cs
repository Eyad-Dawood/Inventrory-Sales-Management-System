using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstractions
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
