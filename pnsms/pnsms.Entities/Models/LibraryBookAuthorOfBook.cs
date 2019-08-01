using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class LibraryBookAuthorOfBook: Entity
    {
        public int Id { get; set; }
        public int LibraryBookId { get; set; }
        public int LibraryBookAuthorId { get; set; }
        public virtual LibraryBookAuthore LibraryBookAuthore { get; set; }
        public virtual LibraryBook LibraryBook { get; set; }
    }
}
