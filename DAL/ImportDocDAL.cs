using ConsolidationSystem.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ConsolidationSystem.DAL
{
    internal class ImportDocDAL:DALBase
    {
       
        public IQueryable<ImportDoc> List()
        {
            return _dbContext.ImportDocs;
        }

        internal Guid Save(Models.ImportDoc ImportDoc)
        {
            Guid returnValue = Guid.Empty;
            try
            {
                var importDoc = new ImportDoc
                {
                    
                    InternalReference = ImportDoc.InternalReference,
                    DocDate = ImportDoc.DocDate,
                    Description = ImportDoc.Description,
                    DocNo = ImportDoc.DocNo,
                    ItemNo = ImportDoc.ItemNo,
                    Currency = ImportDoc.Currency,
                    Type = ImportDoc.Type,
                    Amount = ImportDoc.Amount,
                    CreatedDateTime = ImportDoc.CreatedDateTime,
                    CreatedUserName = ImportDoc.CreatedUserName,
                    IsEnabled = ImportDoc.IsEnabled
                };
                _dbContext.ImportDocs.Add(importDoc);
                _dbContext.SaveChanges();
                returnValue = importDoc.Id;
            }
            catch (Exception ex)
            {
                // Save log
            }
            return returnValue;
        }

    }
}