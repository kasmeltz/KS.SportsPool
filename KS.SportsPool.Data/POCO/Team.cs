namespace KS.SportsPool.Data.POCO
{
    public class Team : PocoDataObject
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int Round1 { get; set; }
        public int Round2 { get; set; }
        public int Round3 { get; set; }
        public int Round4 { get; set; }
        public int Round5 { get; set; }
    }
}
