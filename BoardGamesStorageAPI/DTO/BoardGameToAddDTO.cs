namespace BoardGamesStorageAPI.DTO
{
    public class BoardGameToAddDTO
    {
        public string BoardGameName { get; set; }
        public int MinimumPlayers { get; set; }
        public int MaximumPlayers { get; set; }
        public int PlayTimeInMinutes { get; set; }
        public int AgeLimit { get; set; }
        public int YearOfProduction { get; set; }
        public string Publisher { get; set; }
        public string Author { get; set; }

        public BoardGameToAddDTO()
        {
            if (BoardGameName == null)
            {
                BoardGameName = string.Empty;
            }

            if (Publisher == null)
            {
                Publisher = string.Empty;
            }

            if (Author == null)
            {
                Author = string.Empty;
            }
        }
    }
}
