using ConsolidationSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsolidationSystem.DAL
{
    internal class HistoryDAL : DALBase
    {
        public IQueryable<History> List()
        {
            return _dbContext.history;
        }

        internal Guid Save(Models.HistoryViewModel HistoryView)
        {
            Guid returnValue = Guid.Empty;
            try
            {
                var history = new History
                {
                    BatchNo = HistoryView.BatchNo,
                    InvFileName = HistoryView.InvFileName,
                    CRMFileName = HistoryView.CRMFileName,
                    CreatedDateTime = HistoryView.CreatedDateTime,
                    CreatedUserName = HistoryView.CreatedUserName,                 
                    Type = HistoryView.Type
                    
                };
                _dbContext.history.Add(history);
                _dbContext.SaveChanges();
                returnValue = history.Id;
            }
            catch (Exception ex)
            {
                // Save log
            }
            return returnValue;
        }


    }
}