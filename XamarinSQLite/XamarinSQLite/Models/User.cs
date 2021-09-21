using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using XamarinSQLite.Core;

namespace XamarinSQLite.Models
{
    public class User : BaseNotify
    {
        [Key]
        public int Id { get; set; }
        public string AvatarBase64 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public int Number { get; set; }
    }
}
