using Xunit;
using BookClub2;
using Moq;
using BookClub2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookClub2.Models;
using BookClub2.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BookClubTests
{
    public class AuthorsControllerTest
    {

        private List<Author> GetTestAuthors()
        {
            var authors = new List<Author>();
            authors.Add(new Author()
            {
                Id = 1,
                Forename = "Jan",
                Surname = "Kowalski",
                Yob = 1980,
                CountryOfBirth = "polska"
            });
            authors.Add(new Author()
            {
                Id = 2,
                Forename = "Adam",
                Surname = "Nowak",
                Yob = 1999,
                CountryOfBirth = "niemcy"
            });
            return authors;
        }


        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfAuthors()
        {
            // Arrange
            var mockContext = new Mock<BookClub2Context>();
            mockContext.Setup(x => x.Author.AddRange(GetTestAuthors()));

            var controller = new AuthorsController(mockContext.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<BookClub2.Models.Author>>(
                viewResult.ViewData.Model);
            Assert.Equal(2,model.Count());
        }
    }
}