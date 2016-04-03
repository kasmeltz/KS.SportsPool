namespace KS.SportsPool.Data.POCO
{
    public class Athlete : PocoDataObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
    }
}
