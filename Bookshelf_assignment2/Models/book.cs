namespace Assignment1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("book")]
    public partial class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int book_id { get; set; }

        [StringLength(100)]
        public string title { get; set; }

        public int? author_id { get; set; }

        public int? year_published { get; set; }

        [StringLength(1000)]
        public string summary { get; set; }

        public virtual author author { get; set; }
    }
}
