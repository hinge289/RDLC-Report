using ExcelDataReader;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WebApplication4.Repo
{
    public class GTH
    {
       
        private SqlConnection Sql;
        public GTH()
        {
            
        }
        public DataTable ReadCsvFile(IFormFile file)
        {


            DataTable dataTable = new DataTable();

            using (var stream = file.OpenReadStream())
            using (var csvReader = new TextFieldParser(stream))
            {
                csvReader.SetDelimiters(new[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;

                var colFields = csvReader.ReadFields();
                foreach (var column in colFields)
                {
                    dataTable.Columns.Add(column);
                }


                while (!csvReader.EndOfData)
                {
                    var fields = csvReader.ReadFields();
                    dataTable.Rows.Add(fields);
                }
            }

            return dataTable;

        }
        public DataTable ReadExcelFile(IFormFile file)
        {
            DataTable dataTable = new DataTable();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = file.OpenReadStream())
            {
                IExcelDataReader reader = null;

                if (Path.GetExtension(file.FileName).ToLower() == ".xls")
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (Path.GetExtension(file.FileName).ToLower() == ".xlsx")
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                });

                dataTable = result.Tables[0];
                reader.Close();
            }

            return dataTable;
        }





        public bool UplodeScanDocument(DataTable dataTable)
        {


            try
            {
                foreach (DataRow row in dataTable.Rows)
                {

                    if (row["IsAccept"] != DBNull.Value)
                    {
                        string isAcceptValue = row["IsAccept"].ToString();


                        if (isAcceptValue == "0")
                        {
                            row["IsAccept"] = 0;
                        }
                        else if (isAcceptValue == "1")
                        {
                            row["IsAccept"] = 1;
                        }
                        else
                        {

                            row["IsAccept"] = DBNull.Value;
                        }
                    }
                }

                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(Sql))
                {

                    sqlBulkCopy.DestinationTableName = "dbo.ScanDocument";


                    sqlBulkCopy.ColumnMappings.Add("ScanDocumentId", "ScanDocumentId");
                    sqlBulkCopy.ColumnMappings.Add("DocumentId", "DocumentId");
                    sqlBulkCopy.ColumnMappings.Add("CourseId", "CourseId");
                    sqlBulkCopy.ColumnMappings.Add("SubjectId", "SubjectId");
                    sqlBulkCopy.ColumnMappings.Add("QCId", "QCId");
                    sqlBulkCopy.ColumnMappings.Add("SeatNo", "SeatNo");
                    sqlBulkCopy.ColumnMappings.Add("IsAccept", "IsAccept");

                    Sql.Open();
                    sqlBulkCopy.WriteToServer(dataTable);
                    Sql.Close();
                }
                return true;


            }
            catch (Exception)
            {
                return false;
                throw;
            }



        }
    }
}
