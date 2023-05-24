using AutoMapper;
using BoardGamesStorageAPI.DTO;
using BoardGamesStorageAPI.Models;

namespace BoardGamesStorageAPI.Profiles
{
    public class MappingConfig :Profile
    {
        public MappingConfig()
        {
            CreateMap<BoardGameDTO, BoardGame>();
        }
    }
}
