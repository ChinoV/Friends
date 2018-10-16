using System.Collections.Generic;

namespace FriendsWebApp.Data.Entities
{
    public class JPerson
    {
        public JPerson()
        {
            Neighbors = new List<int>();
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public string Left { get; set; }

        public string Top { get; set; }

        public List<int> Neighbors { get; set; }
    }
}
