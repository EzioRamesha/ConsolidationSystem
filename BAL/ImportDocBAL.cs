using ConsolidationSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConsolidationSystem.Helpers;
using ConsolidationSystem.Models;
using ConsolidationSystem.Models.ResponseModels;
using WebApp.DAL.Models.ResponseModels;

namespace ConsolidationSystem.BAL
{
    public class ImportDocBAL
    {
        private static readonly ImportDocDAL _ImportDocDAL = new ImportDocDAL();
        public List<ImportDoc> GetAll(string strType)
        {
            return _ImportDocDAL.List().Where(w => w.Type.Equals(strType)).Select(s => new ImportDoc
            {                
                InternalReference = s.InternalReference,
                DocDate = s.DocDate,
                Description = s.Description,
                DocNo = s.DocNo,
                ItemNo = s.ItemNo,
                Currency=s.Currency,
                Amount=s.Amount,
                CreatedDateTime=s.CreatedDateTime,
                CreatedUserName=s.CreatedUserName,
                Type = s.Type,
                IsEnabled =s.IsEnabled

            }).ToList();
        }
        public ResponseObject<CreateImportDocResponse> Create(ImportDoc internalDoc)
        {
            ResponseObject<CreateImportDocResponse> response = null;
            var Id = Guid.Empty;
            try
            {
                using (var _ImportDocDAL = new ImportDocDAL())
                {
                    Id = _ImportDocDAL.Save(internalDoc);
                }
                if (Id.Equals(Guid.Empty))
                    throw new Exception();
                else
                    response = new ResponseObject<CreateImportDocResponse>
                    {
                        ResponseType = "success",
                        Message = "Successfully created ."
                    };
            }
            catch (Exception ex)
            {
                response = new ResponseObject<CreateImportDocResponse>
                {
                    ResponseType = "error",
                    Message = "Something went wrong while creating ."
                };
            }
            return response;
        }

    }
}