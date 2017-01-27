using BookStore.Api.Models;
using BookStore.Core.Http.Filters;
using BookStore.Core.Messaging;
using System;
using System.Web.Http;

namespace BookStore.Api.Controllers
{
    [RoutePrefix("books")]
    public class BookController : ApiController
    {
        private readonly ICommandBus _commandBus;

        public BookController(ICommandBus commandBus)
        {
            if (commandBus == null) throw new ArgumentNullException(nameof(commandBus));
            _commandBus = commandBus;
        }

        [HttpPost]
        [ModelValidation]
        [Route("")]
        public IHttpActionResult Create(CreateBookModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Find()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IHttpActionResult Get(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IHttpActionResult Update(Guid id, UpdateBookModel model)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
