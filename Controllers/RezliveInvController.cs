using ConsolidationSystem.BAL;
using ConsolidationSystem.MethodClass;
using ConsolidationSystem.Models;
using ConsolidationSystem.Models.ResponseModels;
using ExcelDataReader;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WebApp.DAL.Models.ResponseModels;

namespace ConsolidationSystem.Controllers
{
    [Authorize]
    public class RezliveInvController : Controller
    {
        private static readonly ImportDocBAL _InternalDocBAL = new ImportDocBAL();
        private static readonly ProcessResultBAL _ProcessResultBAL = new ProcessResultBAL();
        private static readonly HistoryBAL _HistoryBAL = new HistoryBAL();
        public ActionResult RezliveInv()
        {
           
           //var mdata= _cs_RezliveInvBAL.GetAll();
           // List<cs_RezliveInvView> CustomerVMlist = new List<cs_RezliveInvView>();
           // CustomerVMlist = _cs_RezliveInvBAL.GetAll().ToList();            
            return View();
        }

       
        [HttpPost]
        public ActionResult RezliveInv(HttpPostedFileBase Compfile, HttpPostedFileBase CRMfiles)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = Methods.ConsolidationProcess(Compfile, CRMfiles, "RezliveInv", User.Identity.Name);
                #region
                //if (Compfile != null && Compfile.ContentLength > 0 && CRMfiles != null && CRMfiles.ContentLength > 0)
                //{
                //    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                //    // to get started. This is how we avoid dependencies on ACE or Interop:
                //    Stream streamComp = Compfile.InputStream;
                //    Stream streamCRM = CRMfiles.InputStream;

                //    IExcelDataReader readerComp = null;
                //    IExcelDataReader readerCRM = null;

                //    if (Compfile.FileName.EndsWith(".xls") && CRMfiles.FileName.EndsWith(".xls"))
                //    {
                //        readerComp = ExcelReaderFactory.CreateBinaryReader(streamComp);
                //        readerCRM = ExcelReaderFactory.CreateBinaryReader(streamCRM);
                //    }
                //    else if (Compfile.FileName.EndsWith(".xlsx") && CRMfiles.FileName.EndsWith(".xlsx"))
                //    {
                //        readerComp = ExcelReaderFactory.CreateOpenXmlReader(streamComp);
                //        readerCRM = ExcelReaderFactory.CreateOpenXmlReader(streamCRM);
                //    }
                //    else if (Compfile.FileName.EndsWith(".xlsm") && CRMfiles.FileName.EndsWith(".xlsm"))
                //    {
                //        readerComp = ExcelReaderFactory.CreateReader(streamComp);
                //        readerCRM = ExcelReaderFactory.CreateReader(streamCRM);
                //    }
                //    else
                //    {
                //        ViewBag.Message = "This file format is not supported:  " + Compfile.FileName + "   " + CRMfiles.FileName;
                //        return View("RezliveInv");
                //    }
                //    HistoryViewModel _HistoryViewModel = new HistoryViewModel();
                //    _HistoryViewModel.BatchNo = DateTime.Now.ToString("yyyyMMddHHmm");
                //    _HistoryViewModel.CreatedDateTime = DateTime.Now.ToString("dd/MM/yyyy");
                //    _HistoryViewModel.CreatedUserName = User.Identity.Name;
                //    _HistoryViewModel.CRMFileName = CRMfiles.FileName;
                //    _HistoryViewModel.InvFileName = Compfile.FileName;
                //    _HistoryViewModel.Type = "RezliveInv";
                //    _HistoryBAL.Create(_HistoryViewModel);
                //    DataTable dtColoumnCRM = new DataTable();

                //    DataTable dtCRM = new DataTable();
                //    dtCRM = readerCRM.AsDataSet().Tables[0];
                //    DataRow row;

                //    for (int i = 0; i < dtCRM.Columns.Count; i++)
                //    {
                //        dtColoumnCRM.Columns.Add(dtCRM.Rows[0][i].ToString());
                //    }

                //    int rowscounter = 0;
                //    for (int row_ = 1; row_ < dtCRM.Rows.Count; row_++)
                //    {
                //        row = dtColoumnCRM.NewRow();

                //        for (int col = 0; col < dtCRM.Columns.Count; col++)
                //        {
                //            row[col] = dtCRM.Rows[row_][col].ToString();
                //            rowscounter++;
                //        }
                //        dtColoumnCRM.Rows.Add(row);
                //    }

                //    int fieldcount = readerComp.FieldCount;
                //    int rowcount = readerComp.RowCount;
                //    DataTable dtColoumn = new DataTable();

                //    ImportDoc _InternalDoc = new ImportDoc();

                //    DataTable dtRow_ = new DataTable();
                //    try
                //    {
                //        dtRow_ = readerComp.AsDataSet().Tables[0];
                //        for (int i = 0; i < dtRow_.Columns.Count; i++)
                //        {
                //            dtColoumn.Columns.Add(dtRow_.Rows[0][i].ToString());
                //        }
                //        int rowsInvcounter = 0;
                //        for (int k = 1; k < dtRow_.Rows.Count; k++)
                //        {
                //            row = dtColoumn.NewRow();

                //            for (int col = 0; col < dtRow_.Columns.Count; col++)
                //            {
                //                row[col] = dtRow_.Rows[k][col].ToString();
                //                rowsInvcounter++;
                //            }
                //            dtColoumn.Rows.Add(row);
                //        }


                //        for (int i = 0; i < dtColoumn.Rows.Count; i++)
                //        {
                //            string strInternalReference = dtColoumn.Rows[i]["Internal Reference"].ToString();

                //            DateTime dtDocDate = Convert.ToDateTime(dtColoumn.Rows[i]["Doc Date"]);

                //            string strDescription = dtColoumn.Rows[i]["Description"].ToString();

                //            string strDocNo = dtColoumn.Rows[i]["Doc No."].ToString();

                //            string strAmount = dtColoumn.Rows[i]["Amount"].ToString();
                //            decimal decAmount = decimal.Parse("0.00");
                //            if (!string.IsNullOrEmpty(strAmount))
                //            {
                //                decAmount = decimal.Parse(dtColoumn.Rows[i]["Amount"].ToString());
                //            }
                //            else
                //            {
                //                decAmount = decimal.Parse("0.00");
                //            }
                //            ProcessResultInvView _ProcessResultInvView = new ProcessResultInvView();
                //            //checking data in CRM file by 
                //            var res = from rowz in dtColoumnCRM.AsEnumerable()
                //                      where rowz.Field<string>("Partner's Ref No") == strDocNo
                //                      select rowz;
                //            DataTable dst = new DataTable();

                //            try
                //            {
                //                dst = res.CopyToDataTable();
                //            }
                //            catch (Exception)
                //            {

                //            }
                //            if (dst.Rows.Count > 0)
                //            {
                //                for (int j = 0; j < dst.Rows.Count; j++)
                //                {
                //                    string strPartnerRef = Convert.ToString(dst.Rows[j]["Partner's Ref No"]);


                //                    _ProcessResultInvView.InternalRef = strInternalReference;
                //                    _ProcessResultInvView.DocDate = dtDocDate;
                //                    _ProcessResultInvView.Description = strDescription;
                //                    _ProcessResultInvView.DocNo = strDocNo;
                //                    _ProcessResultInvView.ItemNo = "";
                //                    _ProcessResultInvView.Currency = Convert.ToString(dst.Rows[j]["Currency"]);
                //                    _ProcessResultInvView.Amount = decAmount;
                //                    _ProcessResultInvView.CRM_Amount = decimal.Parse(dst.Rows[j]["Total Cost"].ToString());

                //                    if (_ProcessResultInvView.Amount > _ProcessResultInvView.CRM_Amount)
                //                    {
                //                        decimal deciAmountDiff = _ProcessResultInvView.Amount - _ProcessResultInvView.CRM_Amount;
                //                        _ProcessResultInvView.AmountDiff = deciAmountDiff;
                //                    }
                //                    else
                //                    {
                //                        decimal deciAmountDiff = _ProcessResultInvView.CRM_Amount - _ProcessResultInvView.Amount;
                //                        _ProcessResultInvView.AmountDiff = deciAmountDiff;
                //                    }
                //                    _ProcessResultInvView.Type = "RezliveInv";
                //                    _ProcessResultInvView.BatchNo = DateTime.Now.ToString("yyyyMMddHHmm"); ;
                //                    _ProcessResultInvView.CreatedDateTime = DateTime.Now.ToString("dd/MM/yyyy");
                //                    _ProcessResultInvView.CreatedUserName = User.Identity.Name;

                //                    _ProcessResultInvView.CRM_BookingStatus = Convert.ToString(dst.Rows[j]["Booking Status"]);
                //                    _ProcessResultInvView.CRM_PartnerRefNo = strPartnerRef;
                //                    _ProcessResultInvView.CRM_PropertyName = Convert.ToString(dst.Rows[j]["Property Name"]);
                //                    _ProcessResultInvView.CRM_Property_Country = Convert.ToString(dst.Rows[j]["Country"]);
                //                    _ProcessResultInvView.CRM_RefCode = Convert.ToString(dst.Rows[j]["QVI Reference Code"]);

                //                }
                //                _ProcessResultBAL.Create(_ProcessResultInvView);
                //            }


                //        }


                //        ViewBag.Message = "File Uploaded Sucessfully   " + Compfile.FileName;
                //        return View("RezliveInv");
                //    }
                //    catch (Exception ex)
                //    {
                //        readerComp.Close();
                //        readerComp.Dispose();
                //        ModelState.AddModelError("File", "Unable to Upload file!");
                //        return View("RezliveInv");
                //    }
                //    readerComp.Close();
                //    readerComp.Dispose();

                //}
                //else
                //{
                //    ViewBag.Message = "File  Please Upload Your file";
                //    return View("RezliveInv");
                //}
                #endregion
            }
            return View("RezliveInv");
        }

        public ActionResult ExportRezliveInv(string strType,string strDate)
        {
            string fileName = "RezliveInv-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
            var forms = _ProcessResultBAL.GetAll("RezliveInv");
            DataTable dt = Methods.GetDataTableForForms(forms);
            var excelFileStream = Methods.CreateExcel(dt, "Closed Forms");
            var bytes = excelFileStream.ToArray();
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

    }
}