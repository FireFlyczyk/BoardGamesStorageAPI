namespace BoardGamesStorageAPI.DTO
{
    public class BoardGameDTO
    {
        public string BoardGameName { get; set; }
        public int MinimumPlayers { get; set; }
        public int MaximumPlayers { get; set; }
        public int PlayTimeInMinutes { get; set; }
        public int AgeLimit { get; set; }
       // public int YearOfProduction { get; set; }
        public string Publisher { get; set; }
        public string Author { get; set; }

        public BoardGameDTO()
        {
          BoardGameName = string.Empty;

          Publisher = string.Empty;

          Author = string.Empty;
            
        }
    }
}
