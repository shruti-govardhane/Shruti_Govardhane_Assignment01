﻿using LibraryManagementSystem.DTO;
using LibraryManagementSystem.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly string URI = "https://localhost:8081";
        private readonly string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private readonly string DatabaseName = "LibraryManagementSystem";
        private readonly string ContainerName = "Issue";//Issue Container
        private readonly Container container;
        private readonly Container bookContainer;

        public IssueController()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            container = database.GetContainer(ContainerName);
            bookContainer = database.GetContainer("Book"); //  Book container 
        }

        [HttpPost]
        public async Task<IActionResult> IssueBook(String bookiId, String memberId)
        {
            //Console.WriteLine(issue);
            IssueEntity issue = new IssueEntity();
            String commonId = Guid.NewGuid().ToString();
            issue.UId = commonId;
            issue.Id = commonId;
            issue.DocumentType = "issue";
            issue.UpdatedBy = "Shruti";
            issue.IssueDate = DateTime.Now;
            issue.IsReturned = false;
            issue.CreatedBy = "Shruti";
            issue.CreatedOn = DateTime.Now;
            issue.UpdatedOn = DateTime.Now;
            issue.Version = 1;
            issue.Active = true;
            issue.Archived = false;

            var bookQuery = bookContainer.GetItemLinqQueryable<BookEntity>(true).Where(b => b.UId == bookiId).FirstOrDefault();

            if (bookQuery == null)
            {
                return NotFound("Book not found.");
            }
            else
            {
                Console.WriteLine("BOOK FOUND ");
                issue.BookId = bookiId;
                issue.MemberId = memberId;
            }

            bookQuery.IsIssued = true;
            await bookContainer.ReplaceItemAsync(bookQuery, bookQuery.UId);

            await container.CreateItemAsync(issue);

            return CreatedAtAction(nameof(GetIssueByUid), new { uid = issue.UId }, issue);
        }

        //Get by UID
        [HttpGet("{uid}")]
        public async Task<IActionResult> GetIssueByUid(string uid)
        {
            var getIssue = container.GetItemLinqQueryable<IssueEntity>(true).Where(q => q.UId == uid).FirstOrDefault();
            if (getIssue == null)
            {
                return NotFound("Issue not found.");
            }
            return Ok(getIssue);
        }

        //Update issue entity
        [HttpPost("{uid}")]


        public async Task<IssueEntity> UpdateIssue(IssueEntity issue)
        {

            var existingIssue = container.GetItemLinqQueryable<IssueEntity>(true).Where(q => q.UId == issue.UId && q.Active == true && q.Archived == false).FirstOrDefault();
            if (existingIssue == null)
            {
             
               // Console.WriteLine("No existing issue found ");
                return null; 
            }


            await container.ReplaceItemAsync(existingIssue, existingIssue.Id);



            existingIssue.Id = Guid.NewGuid().ToString();
            existingIssue.UpdatedBy = "Shruti";
            existingIssue.UpdatedOn = DateTime.Now;
            existingIssue.Version = existingIssue.Version + 1;
            existingIssue.Active = true;
            existingIssue.Archived = false;


            existingIssue.BookId = issue.BookId;
            existingIssue.MemberId = issue.MemberId;
            existingIssue.IssueDate = issue.IssueDate;
            existingIssue.IsReturned = issue.IsReturned;



            existingIssue = await container.CreateItemAsync(existingIssue);

            //Response
            IssueEntity reponse = new IssueEntity();
            
            reponse.BookId = existingIssue.BookId;
            reponse.IssueDate = existingIssue.IssueDate;
            reponse.IssueDate = existingIssue.IssueDate;
            reponse.IsReturned = existingIssue.IsReturned;

            return reponse;

        }
    }
}

