using AutoMapper;
using ContactManagementService.Entities;
using ContactManagementService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagementApi.MapperConfiguration
{
  
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ContactModel, Contact>();
            CreateMap<EntrepriseModel, Entreprise>();
            CreateMap<EntrepriseAddressModel, EntrepriseAddress>();
            CreateMap<EntrepriseContactModel, EntrepriseContact>();
            CreateMap<EntrepriseContact, EntrepriseContactModel>();
        }
    }
}
