namespace KonaMvpRygDashboard
{
    // Keeping user-level info out of the data to return to the front end, so that skip levels can't see individual level info for people they don't directly manage
    public class TeamStats
    {
        public string ManagerId { get; set; }
        public string TeamId { get; set; }
        public int GreenCount { get; set; }
        public int YellowCount { get; set; }
        public int RedCount { get; set; }
    }
}
