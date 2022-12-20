using ConsolidationSystem.BAL;
using ConsolidationSystem.Models;
using ExcelDataReader;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace ConsolidationSystem.MethodClass
{
    public class Methods
    {
        private static readonly ImportDocBAL _ImportDocBAL = new ImportDocBAL();
        private static readonly ProcessResultBAL _ProcessResultBAL = new ProcessResultBAL();
        private static readonly HistoryBAL _HistoryBAL = new HistoryBAL();
        public static string ConsolidationProcess(HttpPostedFileBase Compfile, HttpPostedFileBase CRMfiles,string stype,string strUserName)
        {           
                if (Compfile != null && Compfile.ContentLength > 0 && CRMfiles != null && CRMfiles.ContentLength > 0)
                {
                // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                // to get started. This is how we avoid dependencies on ACE or Interop:
                    System.IO.Stream streamComp = Compfile.InputStream;
                    Stream streamCRM = CRMfiles.InputStream;
                    IExcelDataReader readerComp = null;
                    IExcelDataReader readerCRM = null;

                    if (Compfile.FileName.EndsWith(".xls") && CRMfiles.FileName.EndsWith(".xls"))
                    {
                        readerComp = ExcelReaderFactory.CreateBinaryReader(streamComp);
                        readerCRM = ExcelReaderFactory.CreateBinaryReader(streamCRM);
                    }
                    else if (Compfile.FileName.EndsWith(".xlsx") && CRMfiles.FileName.EndsWith(".xlsx"))
                    {
                        readerComp = ExcelReaderFactory.CreateOpenXmlReader(streamComp);
                        readerCRM = ExcelReaderFactory.CreateOpenXmlReader(streamCRM);
                    }
                    else if (Compfile.FileName.EndsWith(".xlsm") && CRMfiles.FileName.EndsWith(".xlsm"))
                    {
                        readerComp = ExcelReaderFactory.CreateReader(streamComp);
                        readerCRM = ExcelReaderFactory.CreateReader(streamCRM);
                    }
                    else
                    {                      
                        return("This file format is not supported:  " + Compfile.FileName + "   " + CRMfiles.FileName);
                    }

                    HistoryViewModel _HistoryViewModel = new HistoryViewModel();
                    _HistoryViewModel.BatchNo = DateTime.Now.ToString("yyyyMMddHHmm");
                    _HistoryViewModel.CreatedDateTime = DateTime.Now.ToString("dd/MM/yyyy");
                    _HistoryViewModel.CreatedUserName = strUserName;
                    _HistoryViewModel.CRMFileName = CRMfiles.FileName;
                    _HistoryViewModel.InvFileName = Compfile.FileName;
                    _HistoryViewModel.Type = stype;
                    _HistoryBAL.Create(_HistoryViewModel);

                    DataTable dtColoumnCRM = new DataTable();
                    DataTable dtCRM = new DataTable();
                    dtCRM = readerCRM.AsDataSet().Tables[0];
                    DataRow row;

                    for (int i = 0; i < dtCRM.Columns.Count; i++)
                    {
                        dtColoumnCRM.Columns.Add(dtCRM.Rows[0][i].ToString());
                    }

                    int rowscounter = 0;
                    for (int row_ = 1; row_ < dtCRM.Rows.Count; row_++)
                    {
                        row = dtColoumnCRM.NewRow();

                        for (int col = 0; col < dtCRM.Columns.Count; col++)
                        {
                            row[col] = dtCRM.Rows[row_][col].ToString();
                            rowscounter++;
                        }
                        dtColoumnCRM.Rows.Add(row);
                    }
                    int fieldcount = readerComp.FieldCount;
                    int rowcount = readerComp.RowCount;
                    DataTable dtColoumn = new DataTable();
                    DataTable dtRow_ = new DataTable();
                    try
                    {
                        dtRow_ = readerComp.AsDataSet().Tables[0];
                        for (int i = 0; i < dtRow_.Columns.Count; i++)
                        {
                            dtColoumn.Columns.Add(dtRow_.Rows[0][i].ToString());
                        }
                        int rowsInvcounter = 0;
                        for (int k = 1; k < dtRow_.Rows.Count; k++)
                        {
                            row = dtColoumn.NewRow();

                            for (int col = 0; col < dtRow_.Columns.Count; col++)
                            {
                                row[col] = dtRow_.Rows[k][col].ToString();
                                rowsInvcounter++;
                            }
                            dtColoumn.Rows.Add(row);
                        }
                        for (int i = 0; i < dtColoumn.Rows.Count; i++)//internal excel file 
                        {
                            string strInternalReference = dtColoumn.Rows[i]["Internal Reference"].ToString();
                            DateTime dtDocDate = Convert.ToDateTime(dtColoumn.Rows[i]["Doc Date"]);
                            string strDescription = dtColoumn.Rows[i]["Description"].ToString();
                            string strDocNo = dtColoumn.Rows[i]["Doc No."].ToString();
                            string strAmount = dtColoumn.Rows[i]["Amount"].ToString();
                            decimal decAmount = decimal.Parse("0.00");
                            if (!string.IsNullOrEmpty(strAmount))
                            { decAmount = decimal.Parse(dtColoumn.Rows[i]["Amount"].ToString());}
                            else
                            { decAmount = decimal.Parse("0.00");}
                            ProcessResultInvView _ProcessResultInvView = new ProcessResultInvView();
                            //checking data in CRM file by 
                            var res = from rowz in dtColoumnCRM.AsEnumerable()
                                      where rowz.Field<string>("Partner's Ref No") == strDocNo
                                      select rowz;
                            DataTable dst = new DataTable();
                            try
                            {
                                dst = res.CopyToDataTable();
                            }
                            catch (Exception)
                            {
                            }
                            if (dst.Rows.Count > 0)
                            {
                                for (int j = 0; j < dst.Rows.Count; j++) //CRM Excel file 
                                {
                                    string strPartnerRef = Convert.ToString(dst.Rows[j]["Partner's Ref No"]);
                                    _ProcessResultInvView.InternalRef = strInternalReference;
                                    _ProcessResultInvView.DocDate = dtDocDate;
                                    _ProcessResultInvView.Description = strDescription;
                                    _ProcessResultInvView.DocNo = strDocNo;
                                    _ProcessResultInvView.ItemNo = "";
                                    _ProcessResultInvView.Currency = Convert.ToString(dst.Rows[j]["Currency"]);
                                    _ProcessResultInvView.Amount = decAmount;
                                    _ProcessResultInvView.CRM_Amount = decimal.Parse(dst.Rows[j]["Total Cost"].ToString());

                                    if (_ProcessResultInvView.Amount > _ProcessResultInvView.CRM_Amount)
                                    {
                                        decimal deciAmountDiff = _ProcessResultInvView.Amount - _ProcessResultInvView.CRM_Amount;
                                        _ProcessResultInvView.AmountDiff = deciAmountDiff;
                                    }
                                    else
                                    {
                                        decimal deciAmountDiff = _ProcessResultInvView.CRM_Amount - _ProcessResultInvView.Amount;
                                        _ProcessResultInvView.AmountDiff = deciAmountDiff;
                                    }
                                    _ProcessResultInvView.Type = stype;
                                    _ProcessResultInvView.BatchNo = DateTime.Now.ToString("yyyyMMddHHmm"); ;
                                    _ProcessResultInvView.CreatedDateTime = DateTime.Now.ToString("dd/MM/yyyy");
                                    _ProcessResultInvView.CreatedUserName = strUserName;

                                    _ProcessResultInvView.CRM_BookingStatus = Convert.ToString(dst.Rows[j]["Booking Status"]);
                                    _ProcessResultInvView.CRM_PartnerRefNo = strPartnerRef;
                                    _ProcessResultInvView.CRM_PropertyName = Convert.ToString(dst.Rows[j]["Property Name"]);
                                    _ProcessResultInvView.CRM_Property_Country = Convert.ToString(dst.Rows[j]["Country"]);
                                    _ProcessResultInvView.CRM_RefCode = Convert.ToString(dst.Rows[j]["QVI Reference Code"]);

                                }
                                _ProcessResultBAL.Create(_ProcessResultInvView);
                            }

                        }
                       
                        return ("File Uploaded Sucessfully   " + Compfile.FileName);
                    }
                    catch (Exception ex)
                    {
                      
                    }
                    readerComp.Close();
                    readerComp.Dispose();

                }
                   else
                {
                    return "File  Please Upload Your file";
                }
            return "ok";
        }

        public static MemoryStream CreateExcel(DataTable dt, string sheetName)
        {
            IWorkbook workbook = new XSSFWorkbook();
            IFont boldFont = workbook.CreateFont();
            boldFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            ISheet sheet1 = workbook.CreateSheet(sheetName);

            IRow row1 = sheet1.CreateRow(0);

            for (int j = 0; j < dt.Columns.Count; j++)
            {

                ICell cell = row1.CreateCell(j);
                string columnName = dt.Columns[j].ToString();
                cell.SetCellValue(columnName);
                cell.CellStyle = workbook.CreateCellStyle();
                cell.CellStyle.SetFont(boldFont);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    string columnName = dt.Columns[j].ToString();
                    cell.SetCellValue(dt.Rows[i][columnName].ToString());
                }
            }
            var exportData = new MemoryStream();
            workbook.Write(exportData);
            return exportData;
        }

        public static DataTable GetDataTableForForms(List<ProcessResultInvView> ProcessResultInvView)
        {
            var dt = new DataTable();
            dt.Columns.Add("#");
            dt.Columns.Add("BatchNo");
            dt.Columns.Add("InternalRef");
            dt.Columns.Add("DocDate");
            dt.Columns.Add("Description");
            dt.Columns.Add("DocNo");
            dt.Columns.Add("ItemNo");
            dt.Columns.Add("Currency");
            dt.Columns.Add("Amount");

            dt.Columns.Add("DifferentAmount");
            dt.Columns.Add("CRM_RefCode");
            dt.Columns.Add("CRM_PartnerRefNo");
            dt.Columns.Add("CRM_BookingStatus");
            dt.Columns.Add("CRM_PropertyName");
            dt.Columns.Add("CRM_Amount");
            dt.Columns.Add("AmountDiff");




            int i = 0;
            foreach (var item in ProcessResultInvView)
            {
                i++;
                var dr = dt.NewRow();
                dr["#"] = i;
                dr["BatchNo"] = item.BatchNo;
                dr["InternalRef"] = item.InternalRef;
                dr["DocDate"] = item.DocDate.ToString("dd/MMM/yyyy");
                dr["Description"] = item.Description;
                dr["DocNo"] = item.DocNo;
                dr["ItemNo"] = item.ItemNo;
                dr["Currency"] = item.Currency;
                dr["Amount"] = item.Amount;
                dr["CRM_RefCode"] = item.CRM_RefCode;
                dr["CRM_PartnerRefNo"] = item.CRM_PartnerRefNo;
                dr["CRM_BookingStatus"] = item.CRM_BookingStatus;
                dr["CRM_PropertyName"] = item.CRM_PropertyName;
                dr["CRM_Amount"] = item.CRM_Amount;
                dr["AmountDiff"] = item.AmountDiff;


                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}