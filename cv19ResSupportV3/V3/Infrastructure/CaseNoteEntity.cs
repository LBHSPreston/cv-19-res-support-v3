using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cv19ResSupportV3.V3.Infrastructure
{
    [Table("resident_case_notes")]
    public class CaseNoteEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("resident_id")]
        [ForeignKey("ResidentEntity")]
        public int ResidentId { get; set; }

        [Column("help_request_id")]
        [ForeignKey("HelpRequestEntity")]
        public int HelpRequestId { get; set; }
        [Column("case_notes")]
        public string CaseNote { get; set; }


        public ResidentEntity ResidentEntity { get; set; }
        public HelpRequestEntity HelpRequestEntity { get; set; }

    }
}
