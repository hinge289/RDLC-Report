using System.Data;
using System.Data.Common;
using System.Reflection;
using WebApplication4.Models;

namespace WebApplication4.Repo
{
    public class BLClass
    {
        private DbConnection1 _context;

        public BLClass(DbConnection1 context)
        {
            _context = context;
        }
        public DataTable getdata()
        {
            DataTable dt = new DataTable("ScannedDocuments");

            // ✅ Define columns
            dt.Columns.Add("ScannedDocumentId", typeof(int));
            dt.Columns.Add("DocumentId", typeof(string));
            dt.Columns.Add("CourseId", typeof(int));
            dt.Columns.Add("SubjectId", typeof(int));
            dt.Columns.Add("QcIsAssignId", typeof(int));
            dt.Columns.Add("IsReview", typeof(int));
            dt.Columns.Add("SeatNumber", typeof(string));
            dt.Columns.Add("QuestionId", typeof(string));
            dt.Columns.Add("RejectionReason", typeof(string));

            // ✅ Insert sample rows
            dt.Rows.Add(1, "DOC123", 101, 201, 1, 0, "SEAT001", "Q001", "No Issues");
            dt.Rows.Add(2, "DOC456", 102, 202, 2, 1, "SEAT002", "Q002", "Blurred Image");
            dt.Rows.Add(3, "DOC789", 103, 203, 3, 0, "SEAT003", "Q003", "Invalid Answer");

            return dt;
        }
        //public DataTable GetScannedDocumentsReport()
        //{
        //    DataTable dt = new DataTable();

        //    // Define columns matching DataSet (ds_Document.xsd)
        //    dt.Columns.Add("SCANDOCUMENT_ID", typeof(int));
        //    dt.Columns.Add("DOCUMENT_ID", typeof(string));
        //    dt.Columns.Add("COURSE_ID", typeof(int));
        //    dt.Columns.Add("SUBJECT_ID", typeof(int));
        //    dt.Columns.Add("QC_ISASSIGN_ID", typeof(int));
        //    dt.Columns.Add("IS_REVIEW", typeof(int));
        //    dt.Columns.Add("SEAT_NUMBER", typeof(string));
        //    dt.Columns.Add("QUESTION_ID", typeof(string));


        //    // Fetch data using Entity Framework
        //    var data = _context.scan_document.ToList();

        //    foreach (var item in data)
        //    {
        //        dt.Rows.Add(
        //            item.SCANDOCUMENT_ID,
        //            item.DOCUMENT_ID,
        //            item.COURSE_ID,
        //            item.SUBJECT_ID,
        //            item.QC_ISASSIGN_ID,
        //            item.IS_REVIEW,
        //            item.SEAT_NUMBER,
        //            item.QUESTION_ID

        //        );
        //    }

        //    return dt;
        //}

        //public List<ScannedDocument> GetScanDoucment()
        //{
        //    return _context.scan_document.ToList();
        //}
        //public DataTable ListToDataTable<T>(List<T> items)
        //{
        //    DataTable dataTable = new DataTable(typeof(T).Name);

        //    // Get all the properties using reflection
        //    PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    // Creating columns in the DataTable
        //    foreach (PropertyInfo prop in props)
        //    {
        //        dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        //    }

        //    // Adding rows to the DataTable
        //    foreach (T item in items)
        //    {
        //        var values = new object[props.Length];

        //        for (int i = 0; i < props.Length; i++)
        //        {
        //            values[i] = props[i].GetValue(item, null);
        //        }

        //        dataTable.Rows.Add(values);
        //    }

        //    return dataTable;
        //}
    }
}
