using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoverNation.Models;

namespace CoverNation.ViewModel
{
    public class GenreViewModel
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public string GenreImage { get; set; }
        public List<Genre> GenreList { get; set; }
    }


    public class GenreRow
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public string GenreImage { get; set; }

    }
    public class GenreIndexVM
    {
        public List<GenreRow> GenresList { get; set; }
    }
}

