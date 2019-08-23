using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class LibraryBook: Entity
    {
        public LibraryBook()
        {
            this.LibraryBookAuthorOfBooks = new List<LibraryBookAuthorOfBook>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public string ISBN { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<LibraryBookAuthorOfBook> LibraryBookAuthorOfBooks { get; set; }
    }
}
