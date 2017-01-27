using BookStore.Api.Models;
using BookStore.Api.ReadModels;
using BookStore.Core.Http;
using BookStore.Core.Http.Filters;
using BookStore.Core.Messaging;
using BookStore.Core.ReadModels;
using BookStore.Domain.Commands;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookStore.Api.Controllers
{
    [RoutePrefix("books")]
    public class BookController : ExtendedApiController
    {
        private readonly ICommandBus _commandBus;
        private readonly IDao<BookReadModel> _dao;

        public BookController(ICommandBus commandBus, IDao<BookReadModel> dao)
        {
            if (commandBus == null) throw new ArgumentNullException(nameof(commandBus));
            if (dao == null) throw new ArgumentNullException(nameof(dao));
            _commandBus = commandBus;
            _dao = dao;
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
        public async Task<IHttpActionResult> Find()
        {
            var books = await _dao.FindAsync().ConfigureAwait(false);

            return Ok(books);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var book = await _dao.GetAsync(id).ConfigureAwait(false);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
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
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var command = new DeleteBook
            {
                BookId = id
            };

            await _commandBus.SendAsync(command).ConfigureAwait(false);

            return Accepted();
        }
    }
}
