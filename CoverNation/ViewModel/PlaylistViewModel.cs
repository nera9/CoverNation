using CoverNation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoverNation.ViewModel
{
    public class PlaylistViewModel
    {
        public class PlaylistViewModelIndex
        {
            public int PlaylistId { get; set; }
            public string PlaylistName { get; set; }
            public string PlaylistThumbnail { get; set; }
            public int NumberOfCovers { get; set; }
        }

        public class PlaylistViewModelIndexList
        {
            public bool IsVisitor { get; set; }
            public bool IsDeleteList { get; set; }
            public List<PlaylistViewModelIndex> Playlists { get; set; }
        }

        public class PlayIndex
        {
            public int CoverId { get; set; }
            public string Thumbnail { get; set; }
        }
        public class ModalPlay
        {
           public List<PlayIndex> PlaylistPlayContent { get; set; }
        }
        public class AddNewCover
        {
            public int LastMade { get; set; }
            public List<SelectListItem> PlaylistNames { get; set; }
        }
        public class DeleteList
        {
            public int PlaylistId { get; set; }
            public List<CoverManagmentViewModel.CoverShow> CoverDeleteList { get; set; }
        }
    }
}