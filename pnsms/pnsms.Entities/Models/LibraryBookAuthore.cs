using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class LibraryBookAuthore: Entity
    {
        public LibraryBookAuthore()
        {
            this.LibraryBookAuthorOfBooks = new List<LibraryBookAuthorOfBook>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<LibraryBookAuthorOfBook> LibraryBookAuthorOfBooks { get; set; }
    }
}
