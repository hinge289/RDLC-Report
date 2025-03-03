using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class ScannedDocument
    {
        [Key]
        public int SCANDOCUMENT_ID { get; set; }
        public string DOCUMENT_ID { get; set; }
        public int COURSE_ID { get; set; }
        public int SUBJECT_ID { get; set; }
        public int QC_ISASSIGN_ID { get; set; }
        public int IS_REVIEW { get; set; }
        public string SEAT_NUMBER { get; set; }
        public string QUESTION_ID { get; set; }
        public string REJECTIONRESION { get; set; } // Kept exact name from DB
    }

}
