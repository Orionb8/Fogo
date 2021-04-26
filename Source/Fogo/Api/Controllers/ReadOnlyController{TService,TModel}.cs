using Fogo.Models;
using Fogo.Requests;
using Fogo.Services;
using Fogo.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fogo.Api.Controllers {

    public class ReadOnlyController<TService, TModel> :
        ControllerBase
        where TService : IReadOnlyService<TModel>
        where TModel : class, IComparableModel<TModel>, new() {
        protected readonly ILogger _logger;
        protected readonly TService _service;

        public ReadOnlyController(
            ILoggerFactory loggerFactory,
            TService service) {
            _logger = loggerFactory.CreateLogger(GetType());
            _service = service;
        }

        [HttpGet("page")]
        public virtual async Task<IActionResult> Get([FromQuery] PageRequest request, CancellationToken cancellationToken = default) {
            try {
                return Ok(await _service.ReadAsync(request, cancellationToken));
            } catch (Exception exception) {
                _logger.LogError(exception, exception.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] Request request, CancellationToken cancellationToken = default) {
            try {
                return Ok(await _service.ReadAsync(request, cancellationToken));
            } catch (Exception exception) {
                _logger.LogError(exception, exception.Message);
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get([FromRoute, Bind("Id")] TModel model, CancellationToken cancellationToken = default) {
            try {
                return Ok(await _service.ReadAsync(model, cancellationToken));
            } catch (Exception exception) {
                _logger.LogError(exception, exception.Message);
                if (exception is NotFoundException) {
                    return NotFound();
                }
                return BadRequest();
            }
        }
    }
}