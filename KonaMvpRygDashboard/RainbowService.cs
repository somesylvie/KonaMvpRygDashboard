using KonaMvpRygDashboard.Database;

namespace KonaMvpRygDashboard
{
    public interface IRainbowService
    {
        List<TeamStats> GetDirectTeamStatsByManagerId(string managerId);
        List<TeamStats> GetMultiTeamStatsByManagerId(string managerId);
    }
    public class RainbowService : IRainbowService
    {
        public List<TeamStats> GetDirectTeamStatsByManagerId(string managerId)
        {
            var directTeamEntries = GetUserEntriesByManagerId(managerId);
            return ConvertUserEntriesToStats(directTeamEntries, managerId);
        }
        public List<TeamStats> GetMultiTeamStatsByManagerId(string managerId)
        {
            var multiTeamStats = new List<TeamStats>();

            var totalTeamEntries = new List<UserStatusEntry>();

            var peopleStack = new Stack<string>();
            peopleStack.Push(managerId);

            while (peopleStack.Count > 0)
            {
                var potentialManagerId = peopleStack.Pop();

                var currentTeamEntries = GetUserEntriesByManagerId(potentialManagerId);
                if (currentTeamEntries.Count > 0)
                {
                    // we could potentially get to the same team member from more than one manager. Check if that person is on the list before adding
                    if (!totalTeamEntries.Exists(userEntry => userEntry.SlackTeamId == currentTeamEntries.First().SlackTeamId))
                    {
                        totalTeamEntries.AddRange(currentTeamEntries);
                        var currentTeamMembers = currentTeamEntries.Select(entry => entry.SlackUserId).Distinct().ToList();
                        currentTeamMembers.ForEach(teamMember => peopleStack.Push(teamMember));
                        multiTeamStats.AddRange(ConvertUserEntriesToStats(currentTeamEntries, potentialManagerId));
                    }
                }

            }

            return multiTeamStats;
        }

        private List<TeamStats> ConvertUserEntriesToStats(List<UserStatusEntry> userStatusEntries, string managerId)
        {
            var teamStats = new List<TeamStats>();
            var teamStatusEntries = userStatusEntries.GroupBy(entry => entry.SlackTeamId);
            foreach(IGrouping<String, UserStatusEntry> teamStatusEntry in teamStatusEntries)
            {
                if (teamStatusEntry.Count() > 0)
                {
                    var oneTeamStats = new TeamStats()
                    {
                        TeamId = teamStatusEntry.FirstOrDefault()?.SlackTeamId,
                        RedCount = teamStatusEntry.Count(entry => entry.Selection.Equals("red")),
                        YellowCount = teamStatusEntry.Count(entry => entry.Selection.Equals("yellow")),
                        GreenCount = teamStatusEntry.Count(entry => entry.Selection.Equals("green")),
                        ManagerId = managerId
                    };
                    teamStats.Add(oneTeamStats);
                }
            }
            return teamStats;
        }

        private List<UserStatusEntry> GetUserEntriesByManagerId(string managerId)
        {
            List<UserStatusEntry> userEntriesDbo;

            using (var db = new SqliteDatabaseContext())
            {
                userEntriesDbo = db.UserStatusEntries
                    .Where(UserStatusEntry => UserStatusEntry.SlackTeamId.Contains(managerId)).ToList();
            }

            return userEntriesDbo;
        }
    }
}
