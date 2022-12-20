using ConsolidationSystem.DAL;
using ConsolidationSystem.Models;
using ConsolidationSystem.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DAL.Models.ResponseModels;

namespace ConsolidationSystem.BAL
{
    public class ProcessResultBAL
    {
        private static readonly ProcessResultDAL _ProcessResultDAL = new ProcessResultDAL();
        public List<ProcessResultInvView> GetAll(string strType)
        {
            try
            {
                var VarReturn = _ProcessResultDAL.List().Where(w => w.Type.Equals(strType)).Select(s => new ProcessResultInvView
                {
                    InternalRef = s.InternalRef,
                    BatchNo = s.BatchNo,
                    DocDate = s.DocDate,
                    Description = s.Description,
                    DocNo = s.DocNo,
                    ItemNo = s.ItemNo,
                    Currency = s.Currency,
                    Amount = s.Amount,
                    CRM_RefCode = s.CRM_RefCode,
                    CRM_PartnerRefNo = s.CRM_PartnerRefNo,
                    CRM_BookingStatus = s.CRM_BookingStatus,
                    CRM_PropertyName = s.CRM_PropertyName,
                    CRM_Property_Country = s.CRM_Property_Country,
                    CRM_Amount = s.CRM_Amount,
                    AmountDiff = s.AmountDiff,
                    Type = s.Type,
                    CreatedDateTime = s.CreatedDateTime,
                    CreatedUserName = s.CreatedUserName,
                    IsEnabled = s.IsEnabled
                }).ToList();
                return VarReturn;
            }
            catch (Exception)
            {
                return null;
            }
           
        }

        public List<ProcessResultInvView> GetAllProcessResultByDate(string strType,string strDate)
        {
            try
            {
                var VarReturn = _ProcessResultDAL.List().Where(w => w.Type.Equals(strType)& w.CreatedDateTime.Equals(strDate)).Select(s => new ProcessResultInvView
                {
                    InternalRef = s.InternalRef,
                    BatchNo = s.BatchNo,
                    DocDate = s.DocDate,
                    Description = s.Description,
                    DocNo = s.DocNo,
                    ItemNo = s.ItemNo,
                    Currency = s.Currency,
                    Amount = s.Amount,
                    CRM_RefCode = s.CRM_RefCode,
                    CRM_PartnerRefNo = s.CRM_PartnerRefNo,
                    CRM_BookingStatus = s.CRM_BookingStatus,
                    CRM_PropertyName = s.CRM_PropertyName,
                    CRM_Property_Country = s.CRM_Property_Country,
                    CRM_Amount = s.CRM_Amount,
                    AmountDiff = s.AmountDiff,
                    Type = s.Type,
                    CreatedDateTime = s.CreatedDateTime,
                    CreatedUserName = s.CreatedUserName,
                    IsEnabled = s.IsEnabled
                }).ToList();
                return VarReturn;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public List<ProcessResultInvView> GetBatchNo(string strType)
        {
            try
            {
                var VarReturn = _ProcessResultDAL.List().Where(w => w.Type.Equals(strType)).Select(s => new ProcessResultInvView
                {
                    BatchNo = s.BatchNo
                }).Distinct().ToList();
                return VarReturn;
            }
            catch (Exception)
            {

                return null;
            }
           
        }

        public string BatchDeleteProcessResult(string strBatchNo)
        {
            try
            {
                var VarReturn = _ProcessResultDAL.Delete(strBatchNo);

                return VarReturn;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ResponseObject<CreateProcessResultRespone> Create(ProcessResultInvView ProcessResult)
        {
            ResponseObject<CreateProcessResultRespone> response = null;
            var Id = Guid.Empty;
            try
            {
                using (var _ProcessResultDAL = new ProcessResultDAL())
                {
                    Id = _ProcessResultDAL.Save(ProcessResult);
                }
                if (Id.Equals(Guid.Empty))
                    throw new Exception();
                else
                    response = new ResponseObject<CreateProcessResultRespone>
                    {
                        ResponseType = "success",
                        Message = "Successfully created ."
                    };
            }
            catch (Exception ex)
            {
                response = new ResponseObject<CreateProcessResultRespone>
                {
                    ResponseType = "error",
                    Message = "Something went wrong while creating ."
                };
            }
            return response;
        }

    }
}