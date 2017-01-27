﻿using BookStore.Api.Models;
using BookStore.Core.Http;
using BookStore.Core.Http.Filters;
using BookStore.Core.Messaging;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookStore.Api.Controllers
{
    [RoutePrefix("books")]
    public class BookController : ExtendedApiController
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
        public async Task<IHttpActionResult> Create(CreateBookModel model)
        {
            var command = model.ToCommand();

            await _commandBus.SendAsync(command).ConfigureAwait(false);

            return Accepted();
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
        [ModelValidation]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Update(Guid id, UpdateBookModel model)
        {
            var command = model.ToCommand(id);

            await _commandBus.SendAsync(command).ConfigureAwait(false);

            return Accepted();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
