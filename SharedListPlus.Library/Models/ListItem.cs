using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedListPlus.Library.Models
{
	public class ListItem
	{
		public long ListItemId { get; set; }
		public string Item { get; set; }
		public string StrikethroughItem { get; set; }
		public string Category { get; set; }
		public DateTime TimeAdded { get; set; }
		public long? GroupId { get; set; }
		public Group AssignedGroup { get; set; }
		public bool IsSelected { get; set; }
		public ListItem(string Item)
		{
			//this.Item = Item;
			this.Item = Item;
			TimeAdded = DateTime.Now;
			StrikethroughItem = ConvertToStrikethrough(Item);
		}
		private string ConvertToStrikethrough(string stringToChange)
		{
			var newString = "";
			foreach (var character in stringToChange)
			{
				newString += $"{character}\u0336";
			}
			return newString;
		}
	}
}
