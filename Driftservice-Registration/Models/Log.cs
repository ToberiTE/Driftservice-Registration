namespace Driftservice_Registration.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log
    {
        public int LogID { get; set; }

        public string HeadLine { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string SelectedServiceType { get; set; }

        public bool? Webb { get; set; }
    }
}
