using System.ComponentModel.DataAnnotations;

namespace BoardGamesStorageAPI.DTO
{
    public class BoardGameDTO
    {
        [Required(ErrorMessage = "The BoardGame Name field is required.")]
        public string BoardGameName { get; set; }

        [Range(1, 100, ErrorMessage = "Provide correct minimum number of Players.")]
        public int MinimumPlayers { get; set; }

        [Range(1, 100, ErrorMessage = "Provide correct maximum number of Players.")]
        public int MaximumPlayers { get; set; }

        [Range(1, 1000, ErrorMessage = "Provide correct Play Time")]
        public int PlayTimeInMinutes { get; set; }

        [Range(1, 18, ErrorMessage = "Age Limit must be a positive integer.")]
        public int AgeLimit { get; set; }

        [Range(1900, 2100, ErrorMessage = "Year Of Production must be between 1900 and 2100.")]
        public int YearOfProduction { get; set; }

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
