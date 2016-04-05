namespace KS.SportsPool.Data.POCO
{
    public class Athlete : PocoDataObject
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public string GroupName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }

        public int Points
        {
            get
            {
                return Goals + Assists;
            }
        }
    }
}
