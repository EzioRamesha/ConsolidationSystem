using ConsolidationSystem.Data;
using ConsolidationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsolidationSystem.DAL
{
    internal class DALBase : IDisposable
    {
        public ApplicationDbContext _dbContext;

        public DALBase()
        {
            _dbContext = new ApplicationDbContext();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
        //public static readonly DatabaseContext _dbContext = new DatabaseContext();
    }
}