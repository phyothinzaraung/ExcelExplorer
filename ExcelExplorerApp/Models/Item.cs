using System.ComponentModel.DataAnnotations;

namespace ExcelExplorerApp.Models
{
    public class Item{
        public int Id {get; set;}
        public required string Title {get; set;}
        public required string Dept {get; set;}
        public required string Division {get; set;}
        public int BadgeTypeID { get; set; }
        public required string BadgeType { get; set; }
    }

}