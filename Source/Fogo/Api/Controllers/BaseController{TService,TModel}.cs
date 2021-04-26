using Fogo.Models;
using Fogo.Services;
using Fogo.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace Fogo.Api.Controllers {

    public class BaseController<TService, TModel> :
        ReadOnlyController<TService, TModel>
        where TService : IService<TModel>
        where TModel : class, IComparableModel<TModel>, new() {

        public BaseController(
            ILoggerFactory loggerFactory,
            TService service) :
            base(loggerFactory, service) {
        }

        [HttpPost("save")]
        public IActionResult Save([FromBody] TModel model, CancellationToken cancellationToken = default) {
            try {
                return Ok(_service.SaveAsync(model, cancellationToken));
            } catch (Exception exception) {
                _logger.LogError(exception, exception.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] TModel model, CancellationToken cancellationToken = default) {
            try {
                return Ok(_service.CreateAsync(model, cancellationToken));
            } catch (Exception exception) {
                _logger.LogError(exception, exception.Message);
                if (exception is ConflictException) {
                    return Conflict();
                }
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] TModel model, CancellationToken cancellationToken = default) {
            try {
                return Ok(_service.UpdateAsync(model, cancellationToken));
            } catch (Exception exception) {
                _logger.LogError(exception, exception.Message);
                if (exception is NotFoundException) {
                    return NotFound();
                }
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute, Bind("Id")] TModel model, CancellationToken cancellationToken = default) {
            try {
                return Ok(_service.UpdateAsync(model, cancellationToken));
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