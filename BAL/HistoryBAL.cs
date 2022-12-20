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
    public class HistoryBAL
    {
        private static readonly HistoryDAL _HistoryDAL = new HistoryDAL();
        public List<HistoryViewModel> GetAll()
        {
            return _HistoryDAL.List().Select(s => new HistoryViewModel
            {
                BatchNo = s.BatchNo,
                InvFileName = s.InvFileName,
                CRMFileName = s.CRMFileName,
                CreatedDateTime = s.CreatedDateTime,
                CreatedUserName = s.CreatedUserName,
                Type = s.Type

            }).ToList();
        }

        public ResponseObject<CreateHistoryResponseModel> Create(HistoryViewModel historyViewModel)
        {
            ResponseObject<CreateHistoryResponseModel> response = null;
            var Id = Guid.Empty;
            try
            {
                using (var _HistoryDAL = new HistoryDAL())
                {
                    Id = _HistoryDAL.Save(historyViewModel);
                }
                if (Id.Equals(Guid.Empty))
                    throw new Exception();
                else
                    response = new ResponseObject<CreateHistoryResponseModel>
                    {
                        ResponseType = "success",
                        Message = "Successfully created ."
                    };
            }
            catch (Exception ex)
            {
                response = new ResponseObject<CreateHistoryResponseModel>
                {
                    ResponseType = "error",
                    Message = "Something went wrong while creating ."
                };
            }
            return response;
        }
    }
}