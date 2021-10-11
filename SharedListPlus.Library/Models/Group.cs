using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedListPlus.Library.Models
{
	public class Group
	{
		public long GroupId { get; set; }
		public List<Person> GroupMembers { get; set; }
		public List<ListItem> ListItems { get; set; }
		public string GroupName { get; set; }
		public Group()
		{
			GroupMembers = new List<Person>();
			ListItems = new List<ListItem>();
		}
	}
}
