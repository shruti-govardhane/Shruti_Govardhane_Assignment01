﻿using LibraryManagementSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.ComponentModel;
using Container = Microsoft.Azure.Cosmos.Container;
using LibraryManagementSystem.Entity;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : ControllerBase
    {

        //DB Connection
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryManagementSystem";
        public string ContainerName = "Member";
        public Container container;

        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }
        public MemberController()
        {
            container = GetContainer();
        }

        [HttpPost]//Add Member
        public async Task<MemberDto> AddMember(MemberDto memberDto)
        {
            
            MemberEntity member = new MemberEntity();
            member.Name = memberDto.Name;
            member.DateOfBirth = memberDto.DateOfBirth;
            member.Email = memberDto.Email;
            
            member.Id = Guid.NewGuid().ToString();
            member.UId = member.Id;
            member.DocumentType = "Members";
            member.CreatedBy = "Shruti";
            member.CreatedOn = DateTime.Now;
            member.UpdatedBy = "";
            member.UpdatedOn = DateTime.Now;
            member.Version = 1;
            member.Active = true;
            member.Archived = false;

            MemberEntity response = await container.CreateItemAsync(member);


            MemberDto responseDto = new MemberDto();
            responseDto.Name = responseDto.Name;
            responseDto.DateOfBirth = responseDto.DateOfBirth;
            responseDto.Email = responseDto.Email;
            return responseDto;
        }

        //Get Member By UID
        [HttpGet]

        public async Task<MemberDto> GetMemberByUID(string UId)
        {
            var member = container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.UId == UId).FirstOrDefault();

            MemberDto MemberDto = new MemberDto();

            MemberDto.UId = member.UId;
            MemberDto.Name = member.Name;
            MemberDto.DateOfBirth = member.DateOfBirth;
            MemberDto.Email = member.Email;

            return MemberDto;
        }
        [HttpGet]
        //Get All Member
        public async Task<List<MemberDto>> GetAllMember()
        {
            var members = container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.DocumentType == "Members").ToList();

            List<MemberDto> MemberDtos = new List<MemberDto>();
            foreach (var member in members)
            {
                MemberDto memberdto = new MemberDto();
                memberdto.UId = member.UId;
                memberdto.Name = member.Name;
                memberdto.DateOfBirth = member.DateOfBirth;
                memberdto.Email=member.Email;

                MemberDtos.Add(memberdto);
            }
            return MemberDtos;
        }

        //update

        [HttpPost]


        public async Task<MemberDto> UpdateMember(MemberDto member)
        {
            
            var existingMember = container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.UId == member.UId && q.Active == true && q.Archived == false).FirstOrDefault();

            
            await container.ReplaceItemAsync(existingMember, existingMember.Id);

            

            existingMember.Id = Guid.NewGuid().ToString();
            existingMember.UpdatedBy = "Shruti";
            existingMember.UpdatedOn = DateTime.Now;
            existingMember.Version = existingMember.Version + 1;
            existingMember.Active = true;
            existingMember.Archived = false;

         
            existingMember.Name = member.Name;
            existingMember.DateOfBirth = member.DateOfBirth;
            existingMember.Email = member.Email;
  


            existingMember = await container.CreateItemAsync(existingMember);


            MemberDto reponse = new MemberDto();
            reponse.UId = existingMember.UId;
            reponse.Name = existingMember.Name;
            reponse.DateOfBirth = existingMember.DateOfBirth;
            reponse.Email = existingMember.Email;
            
            return reponse;

        }

    }
}

        
    
