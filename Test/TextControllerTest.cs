using Businnes_logic.Interfaces;
using Domain.DTOs;
using Domain.Enums.TextEnums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word_counter_api.Controllers;
using Xunit;

namespace Test
{
    public class TextControllerTest
    {
        private readonly TextController _textController;
        private readonly Mock<ITextService> _textservice;
        private readonly Mock<IMemoryCache> _memoryCashe;
        public TextControllerTest()
        {
            _textservice = new Mock<ITextService>();
            _memoryCashe = new Mock<IMemoryCache>();
            _textController = new TextController(_textservice.Object, _memoryCashe.Object);
        }

        [Fact]
        public async Task GetWord_TextMode_Is_Null_Return_BadRequest()
        {
            //Arrange
            string textMode = null;
            TextDTO textDTO = new TextDTO();
           
            //Act            
            var actual = await _textController.GetWord(textMode, textDTO);

            //Assert
            Assert.IsType<BadRequestResult>(actual);
        }

        [Fact]
        public async Task GetWord_TextDTO_Is_Null_Return_BadRequest()
        {
            //Arrange
            string textMode = "One";
            TextDTO textDTO = null;

            //Act            
            var actual = await _textController.GetWord(textMode, textDTO);

            //Assert
            Assert.IsType<BadRequestResult>(actual);
        }      
    }
}
