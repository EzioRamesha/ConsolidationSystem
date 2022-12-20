using ConsolidationSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsolidationSystem.DAL
{
    internal class ProcessResultDAL : DALBase
    {
        public IQueryable<ProcessResult> List()
        {
            return _dbContext.processResult;
        }

        internal Guid Save(Models.ProcessResultInvView ProcessResultInvView)
        {
            Guid returnValue = Guid.Empty;
            try
            {
                var _ProcessResult = new ProcessResult
                {
                    InternalRef = ProcessResultInvView.InternalRef,
                    BatchNo= ProcessResultInvView.BatchNo,
                    DocDate = ProcessResultInvView.DocDate,
                    Description = ProcessResultInvView.Description,
                    DocNo = ProcessResultInvView.DocNo,
                    ItemNo = ProcessResultInvView.ItemNo,
                    Currency = ProcessResultInvView.Currency,
                    Amount = ProcessResultInvView.Amount,
                 
                    CRM_RefCode = ProcessResultInvView.CRM_RefCode,
                    CRM_PartnerRefNo = ProcessResultInvView.CRM_PartnerRefNo,
                    CRM_BookingStatus = ProcessResultInvView.CRM_BookingStatus,
                    CRM_PropertyName = ProcessResultInvView.CRM_PropertyName,
                    CRM_Property_Country= ProcessResultInvView.CRM_Property_Country,
                    CRM_Amount = ProcessResultInvView.CRM_Amount,
                    AmountDiff = ProcessResultInvView.AmountDiff,
                    Type = ProcessResultInvView.Type,
                    CreatedDateTime = ProcessResultInvView.CreatedDateTime,
                    CreatedUserName = ProcessResultInvView.CreatedUserName,
                    IsEnabled = ProcessResultInvView.IsEnabled
                };
                _dbContext.processResult.Add(_ProcessResult);
                _dbContext.SaveChanges();
                returnValue = _ProcessResult.Id;
            }
            catch (Exception ex)
            {
                // Save log
            }
            return returnValue;
        }
        internal string Delete(string strBatchNo)
        {
            var form = List();

            var deleteing = form.Where(w => w.BatchNo == strBatchNo).ToList();
            deleteing.ForEach(f =>
            {              
                _dbContext.Entry(f).State = System.Data.Entity.EntityState.Deleted;
            });

           _dbContext.SaveChanges();
            return "success";
     
            
        }
    }
}