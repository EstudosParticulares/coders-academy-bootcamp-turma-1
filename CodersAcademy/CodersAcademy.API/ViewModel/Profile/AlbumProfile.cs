using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodersAcademy.API.Model;
using CodersAcademy.API.ViewModel.Request;

namespace CodersAcademy.API.ViewModel.Profile
{
    public class AlbumProfile : AutoMapper.Profile
    {
        public AlbumProfile()
        {
            CreateMap<AlbumRequest, Album>().ReverseMap();
            CreateMap<MusicRequest, Music>().ReverseMap();
        }
    }
}
